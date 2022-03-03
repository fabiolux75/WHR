using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace LoocERP.Components
{

  public static class Esigibilita {


        private static Dictionary<string, string> dictEsigibilita;


        static Esigibilita() {

            dictEsigibilita = new Dictionary<string, string>
            {
                {"I" , "IVA ad esigibilità immediata"},
                {"D" , "IVA ad esigibilità differita"},
                {"S" , "Scissione dei pagamenti"},
            };
        }

        public static string GetDescription(string codiceEsigibilita)
        {
            string descr = string.Empty;

            descr = (string.IsNullOrWhiteSpace(codiceEsigibilita)) ? "codice errato" : dictEsigibilita[codiceEsigibilita.Trim()];

            return descr;
        }
    }
    public class EsigibilitaViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string codiceEsigibilita)
        {
            string descrizione = Esigibilita.GetDescription(codiceEsigibilita);
            return View("Default",descrizione);
        }

    }
}