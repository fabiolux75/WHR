using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace LoocERP.Components
{

  public static class ModPagamento {


        private static Dictionary<string, string> dictModPagamento;


        static ModPagamento() {

            dictModPagamento = new Dictionary<string, string>
            {
                {"MP01" , "Contanti"},
                {"MP02" , "Assegno"},
                {"MP03" , "Assegno circolare"},
                {"MP04" , "Contanti presso Tesoreria"},
                {"MP05" , "Bonifico"},
                {"MP06" , "Vaglia cambiario"},
                {"MP07" , "Bollettino bancario"},
                {"MP08" , "Carta di pagamento"},
                {"MP09" , "RID"},
                {"MP10" , "RID Utenze"},
                {"MP11" , "RID veloce"},
                {"MP12" , "RIBA"},
                {"MP13" , "MAV"},
                {"MP14" , "Quietanza erario"},
                {"MP15" , "Giroconto su conti di contabilità speciale"},
                {"MP16" , "Domiciliazione bancaria"},
                {"MP17" , "Dmiciliazione postale"},
                {"MP18" , "Bollettino di c/c postale"},
                {"MP19" , "SEPA Direct Debit"},
                {"MP20" , "SEPA Direct Debit CORE"},
                {"MP21" , "SEPA Direct Debit B2B"},
                {"MP22" , "Trattenuta su somme già riscosse"},
                {"MP23" , "PagoPA"},

            };
        }

        public static string GetDescription(string codiceModPagamento)
        {
            string descr = string.Empty;

            descr = (string.IsNullOrWhiteSpace(codiceModPagamento)) ? "codice errato" : dictModPagamento[codiceModPagamento.Trim()];

            return descr;
        }
    }
    public class ModPagamentoViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string codiceModPagamento)
        {
            string descrizione = ModPagamento.GetDescription(codiceModPagamento);
            return View("Default",descrizione);
        }

    }
}