using System;
using LoocERP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using CsvHelper;
using LoocERP.Models;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace LoocERP.Controllers
{
    public class DdtController : Controller
    {

        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;
        public DdtController(ApplicationDBContext context,UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            _context = context;
        }


        [Authorize("DDT.Manage")]
        public IActionResult Ddt(string inout)
        {

            return View();
        }

        [Authorize("DDT.Manage")]        
        public IActionResult DdtRicevuti(string inout)
        {

            return View();
        }


        [Authorize("DDT.Manage")]
        public IActionResult DdtExtreme(string inout)
        {

            return View();
        }

       public ArchiveFile WriteCsvToMemory()
        {

            var listArticoliExp = _context.Set<Looc_GOF_Magazzino>()
                                                                    .Include(c => c.um)
                                                                    .Include(c => c.cat)
                                                                    .Include(c => c.aliquota)
                                                                    .Where(c => !c.isExported)
                                                                    .Where(c => c.CodCliente.Equals("GV"))
                                                                    .ToList();


                        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };          
                        using (var memoryStream = new MemoryStream())
                        using (var streamWriter = new StreamWriter(memoryStream))
                        using (var csvWriter = new CsvWriter(streamWriter, config)) {
                            csvWriter.WriteField("Tipo Articolo");
                            csvWriter.WriteField("Codice Categoria");
                            csvWriter.WriteField("Descrione categoria");
                            csvWriter.WriteField("Stato articolo");
                            csvWriter.WriteField("Codice articolo");
                            csvWriter.WriteField("Descrizione articolo");
                            csvWriter.WriteField("Altra descrizione");
                            csvWriter.WriteField("Unita misura");
                            csvWriter.WriteField("data ultimo acquisto");
                            csvWriter.WriteField("ultimo prezzo acquisto");
                            csvWriter.WriteField("maggiorazione prezzo");
                            csvWriter.WriteField("prezzo netto");
                            csvWriter.WriteField("prezzo ivato");
                            csvWriter.WriteField("codice iva");
                            csvWriter.WriteField("Aliquota IVA");
                            csvWriter.WriteField("Quantita per confezione");
                            csvWriter.WriteField("Quantita minima vendibile");
                            csvWriter.WriteField("Quantita multipla vendibile");
                            csvWriter.WriteField("Tara");
                            csvWriter.WriteField("Peso lordo");
                            csvWriter.WriteField("Costo imballaggio");
                            csvWriter.WriteField("Ubicazione");
                            csvWriter.WriteField("Codice a barre articolo");
                            csvWriter.WriteField("Codifica codice a barre");
                            csvWriter.WriteField("Serie");
                            csvWriter.WriteField("Variante1");
                            csvWriter.WriteField("Variante 2");
                            csvWriter.WriteField("Codice a barre variante");
                            csvWriter.WriteField("Concorre al calcolo del contributo previdenziale");
                            csvWriter.WriteField("Concorre al calcolo della ritenuta d'acconto");
                            csvWriter.WriteField("Giacenza minima");
                            csvWriter.WriteField("Quantità riordino");
                            csvWriter.WriteField("Area");
                            csvWriter.WriteField("Codice articolo fornitore");
                            csvWriter.WriteField("Codice fornitore");
                            csvWriter.WriteField("Contropartita contabile");
                            csvWriter.WriteField("Descrizione contropartita contabile");
                            csvWriter.WriteField("Note");
                            csvWriter.WriteField("Codice magazzino");
                            csvWriter.WriteField("Descrizione magazzino");
                            csvWriter.WriteField("Abilita gestione lotti");
                            csvWriter.WriteField("Abilita gestione numeri di serie");
                            csvWriter.WriteField("Codifica proposta");
                            csvWriter.WriteField("Valore Codifica proposta");
                            csvWriter.WriteField("Marca");
                            csvWriter.WriteField("Modello");
                            csvWriter.NextRecord();

                        // if(listArticoliExp.Count > 0) {
                            
                            foreach (var articolo in listArticoliExp)
                            {
                                var codDatev = "";
                                var aliquota = "";
                                if (articolo.aliquota != null) {
                                    codDatev = articolo.aliquota.codDatev;
                                    aliquota = articolo.aliquota.aliquota.ToString();
                                } 

                                
                                csvWriter.WriteField((articolo.flagBeneServizio == 0) ? 'B' : 'S');
                                csvWriter.WriteField(articolo.cat.Codice);
                                csvWriter.WriteField(articolo.cat.Nome);
                                csvWriter.WriteField("");
                                csvWriter.WriteField(articolo.Codice);
                                csvWriter.WriteField(articolo.Descr);
                                csvWriter.WriteField("");
                                csvWriter.WriteField(articolo.um.descr);
                                csvWriter.WriteField("");
                                csvWriter.WriteField(articolo.PrezzoListino1);
                                csvWriter.WriteField("");
                                csvWriter.WriteField(articolo.PrezzoListino1);
                                csvWriter.WriteField("");
                                csvWriter.WriteField(codDatev);
                                csvWriter.WriteField(aliquota);
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField(articolo.codContropartita); //Contropartita contabile
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.NextRecord();
                                articolo.isExported = true;
                            }
                               // _context.SaveChanges();  // Decommentare in versione stabile
                                streamWriter.Flush();
                                ArchiveFile file = new ArchiveFile();
                                file.FileBytes = memoryStream.ToArray();
                                file.Name = "ExportMagDatev_"+DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                                file.Extension = "csv";
                                return file;                            
                        }

            // }  


        }        

        public async Task<IActionResult> ExportMagazzinoDatev()
        {

            var result          = WriteCsvToMemory();
            var memoryStream    = new MemoryStream(result.FileBytes);
            return new  FileStreamResult(memoryStream, "text/csv") { FileDownloadName = result.Name + "." + result.Extension };

        }        

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {

           AppUser user = await userManager.GetUserAsync(HttpContext.User);


            var c_ddt = _context.C_DDT.Include(p=>p.Mittente)
                                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(c_ddt, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

    }
}