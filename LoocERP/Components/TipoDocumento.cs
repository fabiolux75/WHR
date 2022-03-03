using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace LoocERP.Components
{

  public static class TipoDocumento {


        private static Dictionary<string, string> dictTipoDoc;


        static TipoDocumento() {

            dictTipoDoc = new Dictionary<string, string>
            {
                {"TD01" , "Fattura"},
                {"TD02" , "Acconto/anticipo su fattura"},
                {"TD03" , "Acconto/anticipo su parcella"},
                {"TD04" , "Nota di credito"},
                {"TD05" , "Nota di debito"},
                {"TD06" , "Parcella"},
                {"TD16" , "Integrazione fattura reverse charge interno"},
                {"TD17" , "Integrazione/autofattura per acquisto servizi dall'estero"},
                {"TD18" , "Integrazione per acquisto di beni intracomunitari"},
                {"TD19" , "Integrazione/autofattura per acquisto di beni ex art.17 c.2 DPR 633/72"},
                {"TD20" , "Autofattura per regolarizzazione e integrazione delle fatture (ex art.6 c.8 e 9-bis d.lgs. 471/97 o art.46 c.5 D.L. 331/93)"},
                {"TD21" , "Autofattura per splafonamento"},
                {"TD22" , "Estrazione beni da Deposito IVA"},
                {"TD23" , "Estrazione beni da Deposito IVA con versamento dell'IVA"},
                {"TD24" , "Fattura differita di cui all'art. 21, comma 4, lett. a)"},
                {"TD25" , "Fattura differita di cui all'art. 21, comma 4, terzo periodo lett. b)"},
                {"TD26" , "Cessione di beni ammortizzabili e per passaggi interni (ex art.36 DPR 633/72)"},
                {"TD27" , "Fattura per autoconsumo o per cessioni gratuite senza rivalsa"},

            };
        }

        public static string GetDescription(string codiceTipoDoc)
        {
            string descr = string.Empty;

            descr = (string.IsNullOrWhiteSpace(codiceTipoDoc)) ? "codice errato" : dictTipoDoc[codiceTipoDoc.Trim()];

            return descr;
        }
    }
    public class TipoDocumentoViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string codiceTipoDoc)
        {
            string descrizione = TipoDocumento.GetDescription(codiceTipoDoc);
            return View("Default",descrizione);
        }

    }
}