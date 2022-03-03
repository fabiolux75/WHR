using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using FatturaElettronica.Ordinaria;
using LoocERP.Constants;
using LoocERP.Data;
using LoocERP.Models;
using LoocERP.Models.Sdi;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace LoocERP.Helpers
{
    public class FatturaElettronicaHelper
    {
        private const string API_URL = "http://ea9a-151-50-0-148.ngrok.io/api";

        private const string API_EMAIL = "fabio@fabio05.it";
        private const string API_PWD      = "Fab10_Lux";
        private const string API_AUTH     = "/user/token";
        private const string API_SEND     = "/Invoice/send";

        private readonly Data.ApplicationDBContext _context;

        public FatturaElettronicaHelper (ApplicationDBContext context) {
            _context = context;
        }

        public string getToken(){

            var client  = new RestClient(API_URL);
            var request = new RestRequest(API_AUTH, Method.POST);
            var token   = string.Empty;

            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new { Email = API_EMAIL, Password = API_PWD });

            IRestResponse response = client.Execute(request);
            dynamic res = JObject.Parse(response.Content);

            if (res.result != null) {
                token = res.result.token;
            }

            return token;
        }

        public string sendInvoice(string invoiceBase64){

            var client      = new RestClient(API_URL);
            var request     = new RestRequest(API_SEND, Method.POST);
            var token       = this.getToken();
            var filename    = string.Empty;

            if (string.IsNullOrEmpty(token))
            {
                return filename;
            }


            request.AddHeader("Accept", "application/json");
            request.AddHeader("authorization", "Bearer " + token );
            request.AddJsonBody(new { invoice = invoiceBase64 });

            IRestResponse response = client.Execute(request);
            dynamic res = JObject.Parse(response.Content);

            if (res.result != null){
                filename = res.result.uploadFileName;
            }
            
            return filename;

        }


        public List<Guid> storeInvoice(FatturaOrdinaria fattura, string fattBase64, AppUser user) {

            Guid idFattura = Guid.Empty;

            List<Guid> listId = new List<Guid>();

            foreach (var fat in fattura.FatturaElettronicaBody)
            {
                decimal totImponibile = default(decimal);
                var destinatario = string.Empty;

                foreach (var dato in fat.DatiBeniServizi.DatiRiepilogo)
                {
                    totImponibile = totImponibile + dato.ImponibileImporto;
                    
                }

                SdiDoc doc = new SdiDoc();

                doc.XmlBase64       = fattBase64;
                doc.numDoc          = fattura.FatturaElettronicaHeader.DatiTrasmissione.ProgressivoInvio;
                doc.DataCreazione   = DateTime.Now;
                doc.DataDoc         = fat.DatiGenerali.DatiGeneraliDocumento.Data;
                doc.annoDoc         = fat.DatiGenerali.DatiGeneraliDocumento.Data.Year.ToString();
                doc.InOut           = FEConstants.FatturaInUscita;
                doc.Importo         = fat.DatiGenerali.DatiGeneraliDocumento.ImportoTotaleDocumento ?? 0;
                doc.TipoDoc         = fat.DatiGenerali.DatiGeneraliDocumento.TipoDocumento;
                doc.IDCompany       = user.IDCompany;
                doc.Imponibile      = totImponibile;
                doc.CompanyName     = fattura.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.Anagrafica.Denominazione ?? 
                                        fattura.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.Anagrafica.CognomeNome ;
                doc.Imponibile      = totImponibile;
                doc.UserId          = user.Id;

                _context.Set<SdiDoc>().Add(doc);
                _context.SaveChanges();
                listId.Add(doc.Id);
            }

            return listId;
        }


        public IEnumerable<string> getAnniFattura(string idUser, string tipoFattura = null) {

            var anniDisponibili = _context.Set<SdiDoc>()
                                    .Where(t => t.UserId.ToLower().Equals(idUser))
                                    .Where(t => (t.InOut != null) ?  t.InOut.Equals(tipoFattura) : true)
                                    .Where( t => t.TipoDoc.Equals("TD01"))
                                    .Select(t => t.annoDoc.ToString())
                                    .Distinct()
                                    .ToList();
            return anniDisponibili;
        }


        public IEnumerable<Object> getChartData(string idUser, string anno) {

            var result = _context.Set<SdiDoc>()
                        .Where(t => t.UserId.ToLower().Equals(idUser))
                        .Where(t => t.annoDoc.Equals(anno))
                        .Where(t => t.TipoDoc.Equals("TD01"))
                        .Select(t => new
                        {
                            mese = t.DataDoc.Month,
                            importo = t.Importo,
                            imponibile = t.Imponibile,
                            inout = t.InOut.ToString()
        })
                        .ToList();

            return result;

        }



        public IEnumerable<string> getAnniDashBoard(string idUser) {

            var anniDisponibili = _context.Set<SdiDoc>()
                                    .Where(t => t.UserId.ToLower().Equals(idUser))
                                    .Where( t => t.TipoDoc.Equals("TD01"))
                                    .Select(t => t.annoDoc.ToString())
                                    .Distinct()
                                    .ToList();


            return anniDisponibili.OrderByDescending(x => x).ToList();
        }        
    }
}