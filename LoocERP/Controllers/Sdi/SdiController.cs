using System;
using LoocERP.Data;
using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using LoocERP.Models;
using System.Threading.Tasks;
using System.Text.Json;
using LoocERP.Models.Sdi;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using LoocERP.Controllers.Sdi;
using System.IO;
using FatturaElettronica.Ordinaria;
using FatturaElettronica.Defaults;
using FatturaElettronica.Extensions;
using LoocERP.ViewModels.Sdi;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using FatturaElettronica.Validators;
using LoocERP.Helpers;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using LoocERP.ViewModels;

namespace LoocERP.Controllers
{
    public class SdiController : Controller
    {

        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly FatturaElettronicaHelper _feHelper;
        public SdiController(ApplicationDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
            _feHelper = new FatturaElettronicaHelper(context);
        }



        public IActionResult Fatture(string inout)
        {
            return View();
        }
     

        public async Task<IActionResult> FattureExtreme(string inout)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            ViewData["inout"] = inout;
            return View(new SelectBoxViewModel{ Items = _feHelper.getAnniFattura(user.Id, inout)});
        }        

        public IActionResult FattureDdt(Guid idFattura)
        {
            
            var fattura    = _context.Set<SdiDoc>().Include("SdiDocDdts.ddt").Where(c => c.Id.Equals(idFattura)).FirstOrDefault();
            var data        = Convert.FromBase64String(fattura.XmlBase64);
            var contents    = new MemoryStream(data);
            var fatt        = new FatturaOrdinaria();

            fatt = FatturaOrdinaria.CreateInstance(Instance.Privati);
            fatt.ReadXml(contents);
            ViewBag.idFattura = idFattura;

            var FatturaVM = new FatturaViewModel
            {
                fattura = fattura,
                eFattura = fatt
            };
            
            return View(FatturaVM);
        }        


        public IActionResult ViewFattura(Guid id)
        {
            var eFattura    = _context.Set<SdiDoc>().Find(id);

            var fattura     = eFattura.getDecodeFatturaXml();

            ViewBag.idFattura = id;
            return View(fattura);
        }        



        [HttpGet]
        public async Task<IActionResult> Get(string anno, string tipoFattura, DataSourceLoadOptions loadOptions) {

           AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var list_fatture = _context.C_SdiDoc
                                .Include(p => p.ANA_Company)
                                .Include(p => p.Company)
                                .Where( p => p.InOut.Equals(tipoFattura))
                                .Where(p => p.UserId.ToLower().Equals(user.Id.ToLower()))
                                .Where(p => (anno == null) ? true : p.annoDoc == anno)
                                ;



            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(list_fatture, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }



        public  IActionResult getPdf (Guid idPdf){

            var eFattura    = _context.Set<SdiDoc>().Find(idPdf);
            try
            {
                var ws_client   = new WsSdi();
                var pdfInfo     = ws_client.getPdf(eFattura.filename);
                var pdfInfoObj  = JObject.Parse(pdfInfo);

                if ((string)pdfInfoObj["result"] == "KO") return NotFound("Errore nella risposta dal WS");
                
                byte[] bytes    = Convert.FromBase64String((string)pdfInfoObj["base64"]);

                return File(bytes, "application/octet-stream", $"{eFattura.filename}.pdf");
                 
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> getDdt(Guid idFattura, DataSourceLoadOptions loadOptions) {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var listaDdtNew = (from ddt in _context.C_DDT
                               join pivot in _context.C_SdiDocDdt
                               on ddt.Id equals pivot.DdtId
                               where pivot.SdiDocId == idFattura
                               select new
                               {
                                   Id = ddt.Id,
                                   DataDDT = ddt.DataDDT,
                                   Numero = ddt.NumeroProgressivoDocumento
                               });

            return Json(await DataSourceLoader.LoadAsync(listaDdtNew, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }


        [HttpGet]
        public async Task<IActionResult> DdtDetails(Guid DdtId, DataSourceLoadOptions loadOptions) {

            var dettagli = _context.Set<DdtDettaglio>().Where(c => c.IdDDT.Equals(DdtId));

            return Json(await DataSourceLoader.LoadAsync(dettagli, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        [HttpGet]
        public async Task<IActionResult> getDdtByFornitore(Guid idFornitore, DataSourceLoadOptions loadOptions) {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var listaDdtFornitore = _context.Set<Ddt>().Where(x => x.IdMittente == idFornitore);

            return Json(await DataSourceLoader.LoadAsync(listaDdtFornitore, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }                

        [HttpPost]
        public JsonResult associaDdt(string param, Guid fattID) {

        var listaIdDDT = JsonConvert.DeserializeObject<dynamic>(param);
        var transaction = _context.Database.BeginTransaction();
        var esito = true;

        try
        {
           
            foreach (var idDDt in listaIdDDT)
            {
                var temp = new SdiDocDdt { DdtId = idDDt, SdiDocId = fattID };
                _context.Add(temp);
                _context.SaveChanges();
            }
            transaction.Rollback();          
             
        }
        catch (Exception ex )
        {
            transaction.Rollback();
            esito = false;

        }  

        return Json(new {res = esito});

        }                


        public IActionResult CreaFattura() {

            return View();
        }

        public IActionResult SendFatturaXml() {

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> GetTipiDocumento(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.FE_TipiDocumento
                         orderby i.Descrizione
                         select new {
                             ID = i.Codice,
                             Descrizione = "("+ i.Codice +") - "+ i.Descrizione
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        
        [HttpPost]
        public async Task<ActionResult> UploadInvoiceXML (IFormFile invoiceFile) {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            try
            {
                
                string fileName = invoiceFile.FileName;
                string ext      = Path.GetExtension(fileName);

                if (!ext.Equals(".xml")) {
                    return BadRequest("Estensione del file non consentita. Sono permessi solo file .XML");
                }

                if (invoiceFile == null || invoiceFile.Length == 0) {

                    return BadRequest("il file selezionato è vuoto");
                }   

                var fattura = new FatturaOrdinaria();
                fattura     = FatturaOrdinaria.CreateInstance(Instance.Privati);

                var ms = new MemoryStream();
                invoiceFile.CopyTo(ms);
                var fileBytes       = ms.ToArray();
                string base64File   = Convert.ToBase64String(fileBytes);
                fattura.ReadXml(ms);
                

                // Validazione della fattura
                var fatturaValida = fattura.Validate();

                
                if (!fatturaValida.IsValid) {
                    List<string> listError = new List<string>();
                    foreach (var error in fatturaValida.Errors)
                    {
                        listError.Add(error.ErrorMessage);
                    }
                    return BadRequest(string.Join(" - ",listError));
                }

                // Invio fattura
                //string nomeFileFatturaSDI = _feHelper.sendInvoice(base64File);
                string nomeFileFatturaSDI = "nome_file_fattura";

                if (!string.IsNullOrEmpty(nomeFileFatturaSDI)){
                    List<Guid> listaId = _feHelper.storeInvoice(fattura, base64File, user);

                    if (listaId.Any()){
                        var idsToJoin = string.Join(",", listaId);
                        var paramList = new[] {
                            new SqlParameter("@list",idsToJoin),
                            new SqlParameter("@filename",nomeFileFatturaSDI)
                    };

                        string query    = "UPDATE C_SdiDoc SET filename = @filename WHERE Id IN  (@list)";
                        var result      = _context.Database.ExecuteSqlRaw(query, paramList);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("si è verificato un errore nella procedura di caricamento!");
            }


            return Ok();
        }

        public async Task<IActionResult> DashBoard()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            return View(new SelectBoxViewModel{ Items = _feHelper.getAnniDashBoard(user.Id )});

        }   

        [HttpGet]
        public async Task<object> getChart(string anno, DataSourceLoadOptions loadOptions){

            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            IEnumerable<Object> fatture = _feHelper.getChartData(user.Id, anno);

            List<ChartFatture> list = new List<ChartFatture>();

            for (int i = 1; i < 13; i++) {
                list.Add(new ChartFatture(i));
            }

            foreach (var fattura in fatture)
            {
                dynamic temp        = fattura;
                var mese            = temp.mese;
                double importo      =  Math.Round((double)temp.importo,2);
                double imponibile   =  Math.Round((double)temp.imponibile,2);


                list[mese-1].FattureEmesse = (temp.inout.Equals("1")) ? Math.Round(list[mese-1].FattureEmesse + importo,2) : Math.Round(list[mese-1].FattureEmesse,2);
                list[mese-1].FattureRicevute = (temp.inout.Equals("0")) ? Math.Round(list[mese-1].FattureRicevute + importo,2) : Math.Round(list[mese-1].FattureRicevute,2);
            }

            return DataSourceLoader.Load(list, loadOptions);
        }

    }
}

