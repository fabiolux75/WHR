using System.ComponentModel.Design;
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
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Web.Helpers;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.Entity.Core.Mapping;
using Microsoft.Data.SqlClient;

namespace LoocERP.Controllers.API {




    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DdtRicevutiController : ControllerBase
    {


        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public DdtRicevutiController(ApplicationDBContext context,UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            _context = context;
        }





        [HttpGet("status")]
        public IActionResult status()
        {
            return Ok("Ok");
        }


    

        
        /// <summary>
        ///  Caricamento dati per popolazione tabella DDT in HR
        /// </summary>
        /// <returns></returns>
        [HttpPost("in")]
        public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
        {


            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria           = "Id";
            var orderAscendingDirection = true;




            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }



            var result = _context.Set<DdtRicevuti>()
                            .Include(p=>p.Mittente)
                            .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                            .AsQueryable();

            var totalResultsCount = await result.CountAsync();
            // Filtro intervallo di tempo
            result = result.Where(c => (dtParameters.AdditionalValues.ElementAt(0) != null) ? c.DataDDT >= DateTime.ParseExact(dtParameters.AdditionalValues.ElementAt(0), "MM-dd-yyyy", CultureInfo.InvariantCulture) : true)
                           .Where(c => (dtParameters.AdditionalValues.ElementAt(1) != null) ? c.DataDDT <= DateTime.ParseExact(dtParameters.AdditionalValues.ElementAt(1), "MM-dd-yyyy", CultureInfo.InvariantCulture) : true);

            result = result.Where(c => dtParameters.AdditionalValues.ElementAt(2).Equals("1") ? c.DataExpDatev == null : true);

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Mittente != null && r.Mittente.RagioneSociale.ToUpper().Contains(searchBy.ToUpper()) ||
                                            r.NumeroProgressivoDocumento != null && r.NumeroProgressivoDocumento.Equals(searchBy)
                                        );
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = await result.CountAsync();

            var data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync();



            var jsonData = new { draw = dtParameters.Draw, recordsFiltered = filteredResultsCount, recordsTotal = totalResultsCount, data = data };

            return Ok(jsonData);


        }


        /// <summary>
        ///  Azione per download file XML da interfaccia fatture HR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("export")]
         
        public async Task<ActionResult> Export([FromBody] JsonElement ids)

        {
            
            var listaIds    = ids.GetProperty("ids");
            var fromDate    = ids.GetProperty("fromDate").ToString();
            var toDate      = ids.GetProperty("toDate").ToString();
            var exported    = ids.GetProperty("exported").ToString();
            

            var countIds        = listaIds.GetArrayLength();
            string nomeFile     = "Export_Datev_" + DateTime.UtcNow.ToString("dd_MM_yyyy")+".csv";
            List<DdtRicevuti> ddtlist   = new List<DdtRicevuti>();
            AppUser user        = await userManager.GetUserAsync(HttpContext.User);


            if (countIds > 0) {  // DDT selezionati
                    List<Guid> listIds = new List<Guid>();
                    foreach (var item in listaIds.EnumerateArray())
                    {
                        listIds.Add(new Guid(item.ToString()));
                    }

                ddtlist = _context.Set<DdtRicevuti>()
                                .Include(p => p.Mittente)
                                .Include(p => p.Dettagli)
                                .ThenInclude(p=> p.Aliquota)
                                .Include(p => p.ModalitaPagamento)
                                .Include(p => p.TipoPagamento)
                                .Where(c => listIds.Contains(c.Id)).ToList();

            } else if (!String.IsNullOrEmpty(fromDate) && !String.IsNullOrEmpty(toDate)) {  // DDT intervallo di tempo
                    ddtlist = _context.Set<DdtRicevuti>()
                                .Include(p => p.Mittente)
                                .Include(p => p.Dettagli)
                                .ThenInclude(p=> p.Aliquota)
                                .Include(p => p.ModalitaPagamento)
                                .Include(p => p.TipoPagamento)
                                .Where(c => c.DataDDT >= DateTime.ParseExact(fromDate,"MM-dd-yyyy",CultureInfo.InvariantCulture) 
                                        && c.DataDDT <= DateTime.ParseExact(toDate,"MM-dd-yyyy",CultureInfo.InvariantCulture)
                                )
                                .Where(c => (exported.Equals("1") ) ? c.DataExpDatev == null :  true )
                                .ToList();

            } else { // Tutti i DDT 
                ddtlist = _context.Set<DdtRicevuti>()
                            .Include(p => p.Mittente)
                            .Include(p => p.Dettagli)
                            .ThenInclude(p=> p.Aliquota)
                            .Include(p => p.ModalitaPagamento)
                            .Include(p => p.TipoPagamento)
                            .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                            .Where(c => (exported.Equals("1") ) ? c.DataExpDatev == null :  true )
                            .ToList();
            }


             string csv = "Rinumera documento;Numero documento;Suffisso;Tipo riga;Numero riga;Data documento;E' fattura accompagnatoria;Data competenza IVA;C.F. Nominativo;Codice nominativo;Descrizione documento;Tipo di pagamento;Modalita pagamento"+
             ";Stato documento;Numero ordine cliente;Descrizione banca appoggio;Codice IBAN;Cod. BIC;Note;Scadenze;Applica IVA esigibilita differita;Causale contabile;Tipo operazione dati fattura;Codice identificativo;Percorso file copia conforme;Tipo di contributo previdenziale;Percentuale contributo previdenziale;Codice IVA previdenza;Aliquota IVA previdenza;Percentuale ritenuta;Percentuale ritenuta su imponibile;Codice articolo;Codice articolo fornitore;Codice a barre variante;Descrizione riga;Tipo articolo;Unita di misura;Codice IVA;Aliquota IVA;Quantita;Prezzo unitario;Sconto unitario;Inizio erogazione;Fine erogazione;Codice magazzino;Descrizione magazzino;Codice magazzino destinazione trasferimento;Centro di costo;Descrizione centro di costo;Classe spesa;Descrizione spesa;Importo spesa;Codice IVA spesa;Codice IVA spesa;Codice divisa;Data cambio;Valore cambio;Concorre alla previdenza;Concorre alla ritenuta;lotto\r\n";

                                         
              List<Guid> listIdsDDTUpdate = new List<Guid>();   
             foreach (var ddtItem in ddtlist)
             {
                listIdsDDTUpdate.Add(new Guid(ddtItem.Id.ToString()));


                string identificativo = ddtItem.Mittente.FiscalCode ?? ddtItem.Mittente.PIva.ToString();



                 foreach (var dett in ddtItem.Dettagli)
                 {

                    string infoIntestazione = "false;" + ddtItem.NumeroProgressivoDocumento.ToString() + ";"
                        + ddtItem.Suffisso + ";N;" + dett.NumeroRiga + ";"
                        + ddtItem.DataDDT.ToShortDateString() + ";;;"
                        + identificativo + ";;Bolla di consegna;";

                    var codPag          = (ddtItem.TipoPagamento == null ) ?  "" : ddtItem.TipoPagamento.codiceTipoPagamento ;
                    var codModPag       = (ddtItem.ModalitaPagamento == null ) ?  "" : ddtItem.ModalitaPagamento.codiceModalitaPagamento ;
                    var codIvaDatev     = (dett.Aliquota == null) ? "" : dett.Aliquota.codDatev;
                    var aliquota_iva    = (dett.Aliquota == null) ? "" : dett.Aliquota.aliquota.ToString();
                    var TipoArticolo    = (dett.FlagAttrezzatura == 1 ) ?  "S" : "B" ;


                    csv += infoIntestazione 
                             
                             + codPag +";"
                             + codModPag  +";;;;;;;;;;;;;;;;;;;"
                             + dett.Codice + ";;;"
                             + dett.Descrizione + ";"
                             + TipoArticolo + ";"   // Tipo Articolo
                             + dett.UM + ";"
                             + codIvaDatev  + ";"
                             + aliquota_iva + ";"
                             + dett.Quantita + ";"
                             + dett.PrezzoListino1 + ";;;;;;;"
                             + ddtItem.ProjectCodice +";"
                             + ddtItem.ProjectName +";;;;;;;;;;;\r\n";                            

                 }

             }

            try
            {
                // listIdsDDTUpdate.Add(new Guid("6720e728-d165-4b07-a799-8ca592e8197b"));
                // listIdsDDTUpdate.Add(new Guid("cbc4caf1-8f18-4675-9232-73ff6c9c66b6"));

                var idsToJoin = string.Join(",", listIdsDDTUpdate);
                string query = "UPDATE C_DDT_Ricevuti SET DataExpDatev = getdate() WHERE Id IN  (@list)";

                var result = _context.Database.ExecuteSqlRaw(query, new SqlParameter("@list", idsToJoin));    
            }
            catch (Exception ex)
            {
                // return Json(new { esito =0, msg = ex.Message});
            }


             return Ok(File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", nomeFile));


        }
    }
}  
