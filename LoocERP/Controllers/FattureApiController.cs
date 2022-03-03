using System;
using System.Xml;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using LoocERP.Models;
using LoocERP.Data;
using LoocERP.Models.Sdi;
using System.Linq;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using LoocERP.Models.Helpers;
using LoocERP.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LoocERP.Controllers {




    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FattureApiController : ControllerBase
    {


        private readonly Data.ApplicationDBContext _context;

        public FattureApiController(ApplicationDBContext context)
        {
            _context = context;
        }





        [HttpGet("status")]
        public IActionResult status()
        {
            return Ok("Ok");
        }



        /*
          Entry-point fatture in entrata inviate da Aruba
        */

        [HttpPost("in")]
        public IActionResult GetFattura(FatturaIn invoice)
        {

            var data = Convert.FromBase64String(invoice.invoiceBase64);
            string decodedString = Encoding.UTF8.GetString(data);

            XmlDocument fattura = new XmlDocument();
            fattura.LoadXml(decodedString);
            string json = JsonConvert.SerializeXmlNode(fattura);
            string fatturaJson = json.Replace(@"\", string.Empty);

            JObject readFattura = JObject.Parse(fatturaJson);
            string pivaMittente = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaHeader"]["CedentePrestatore"]["DatiAnagrafici"]["IdFiscaleIVA"]["IdCodice"];
            string DataDoc = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaBody"]["DatiGenerali"]["DatiGeneraliDocumento"]["Data"];

            SdiDoc doc = new SdiDoc();

            // CHECK dati DDT
            var InfoDDT = readFattura["p:FatturaElettronica"]["FatturaElettronicaBody"]["DatiGenerali"]["DatiDDT"];
            int NumDDT = 0;
            Guid? idDdt = null;

            if (InfoDDT != null)
            {
                NumDDT = (int)readFattura["p:FatturaElettronica"]["FatturaElettronicaBody"]["DatiGenerali"]["DatiDDT"]["NumeroDDT"];
                var ddt = _context.C_DDT.FirstOrDefault(z => z.NumeroProgressivoDocumento == NumDDT);
                

                if (ddt != null)
                {
                    idDdt = ddt.Id;
                }
                else
                { // DDT non trovato
                  //doc.NumDDT = NumDDT;   // Salvare il numero di DDT presente nella fattura elettronica
                }
            }

            
            //doc.DdtId = idDdt;
            doc.XmlBase64       = invoice.invoiceBase64;
            doc.filename        = invoice.filename;
            doc.DataCreazione   = DateTime.Now;
            doc.DataDoc         = DateTime.Parse(DataDoc);
            doc.InOut           = "1";

            Guid idCompany = _context.Set<ANA_Company>().Where(c => c.FiscalCode.Equals(pivaMittente) || c.PIva.Equals(pivaMittente)).Select(c => c.ID).FirstOrDefault();
            if (idCompany != Guid.Empty)
            {
                doc.IDCompany = idCompany;
            }



            doc.CompanyName = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaHeader"]["CedentePrestatore"]["DatiAnagrafici"]["Anagrafica"]["Denominazione"];
            doc.Importo = (decimal)readFattura["p:FatturaElettronica"]["FatturaElettronicaBody"]["DatiGenerali"]["DatiGeneraliDocumento"]["ImportoTotaleDocumento"];
            doc.TipoDoc = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaBody"]["DatiGenerali"]["DatiGeneraliDocumento"]["TipoDocumento"];

            _context.Set<SdiDoc>().Add(doc);

            string msg = "Documento importato";
            try
            {
                _context.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine("Errore");
                msg = "Errore nel salvataggio";
            }

            return Ok(msg);
        }

        [HttpPost("fatturein")]
        public IActionResult FattureIn()
        {
            try
            {

                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var fatture = (from tempcustomer in _context.Set<SdiDoc>() select tempcustomer);

                /*  if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    {
                        fatture = fatture.OrderBy(sortColumn + " " + sortColumnDirection);
                    }
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        fatture = fatture.Where(m => m.FirstName.Contains(searchValue) 
                                                    || m.LastName.Contains(searchValue) 
                                                    || m.Contact.Contains(searchValue) 
                                                    || m.Email.Contains(searchValue) );
                    }*/
                recordsTotal = fatture.Count();
                var data = fatture.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };

                return Ok(jsonData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



     

        
        /// <summary>
        ///  Caricamento dati per popolazione tabella fatture in HR
        /// </summary>
        /// <returns></returns>
        [HttpPost("fatturedue")]
        public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = _context.Set<SdiDoc>().AsQueryable();


            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.CompanyName != null && r.CompanyName.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = await result.CountAsync();
            var totalResultsCount = await _context.C_SdiDoc.CountAsync();
            var data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync();
            var jsonData = new { draw = dtParameters.Draw, recordsFiltered = filteredResultsCount, recordsTotal = totalResultsCount, data = data };

            return Ok(jsonData);

            /*return Json(new DtResult<SdiDoc>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync()
            });*/

        }


        /// <summary>
        ///  Azione per download file XML da interfaccia fatture HR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("download")]
        public async Task<ActionResult> DownloadFile(string id)
        {
            
            SdiDoc doc  = _context.Set<SdiDoc>().FirstOrDefault(a => a.Id.ToString().Equals(id));

            string nomeFile        = "";
            string decodedString   = "";

            if (doc != null) {
                var data            = Convert.FromBase64String(doc.XmlBase64);
                decodedString       = Encoding.UTF8.GetString(data);
                XmlDocument fattura = new XmlDocument();

                fattura.LoadXml(decodedString);
                string json         = JsonConvert.SerializeXmlNode(fattura);
                string fatturaJson  = json.Replace(@"\", string.Empty);

                JObject readFattura = JObject.Parse(fatturaJson);       
                var IdPaese         = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaHeader"]["DatiTrasmissione"]["IdTrasmittente"]["IdPaese"];
                var IdCodice        = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaHeader"]["DatiTrasmissione"]["IdTrasmittente"]["IdCodice"];
                var fmt             = (string)readFattura["p:FatturaElettronica"]["FatturaElettronicaHeader"]["DatiTrasmissione"]["FormatoTrasmissione"];
                nomeFile            = IdPaese + IdCodice + "_" + fmt + ".xml";

                return File(Encoding.UTF8.GetBytes(decodedString), "application/force-download", nomeFile);

            }

            return Ok();


        }

    }


}  
