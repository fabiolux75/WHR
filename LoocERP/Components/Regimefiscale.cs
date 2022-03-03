using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace LoocERP.Components
{

  public static class RegimeFiscale {


        private static Dictionary<string, string> dictRegime;


        static RegimeFiscale() {

            dictRegime = new Dictionary<string, string>
            {
                {"RF01" , "Ordinario"},
                {"RF02" , "Contribuenti minimi (art.1, c.96-117, L. 244/07)"},
                {"RF04" , "Agricoltura e attività connesse e pesca (artt.34 e 34-bis, DPR 633/72)"},
                {"RF05" , "Vendita sali e tabacchi (art.74, c.1, DPR. 633/72)"},
                {"RF06" , "Commercio fiammiferi (art.74, c.1, DPR 633/72)"},
                {"RF07" , "Editoria (art.74, c.1, DPR 633/72)"},
                {"RF08" , "Gestione servizi telefonia pubblica (art.74, c.1, DPR 633/72)"},
                {"RF09" , "Rivendita documenti di trasporto pubblico e di sosta (art.74, c.1, DPR 633/72)"},
                {"RF10" , "Intrattenimenti, giochi e altre attività di cui alla tariffa allegata al DPR 640/72 (art.74, c.6, DPR 633/72)"},
                {"RF11" , "Agenzie viaggi e turismo (art.74-ter, DPR 633/72)"},
                {"RF12" , "Agriturismo (art.5, c.2, L. 413/91)"},
                {"RF13" , "Vendite a domicilio (art.25-bis, c.6, DPR 600/73)"},
                {"RF14" , "Rivendita beni usati, oggetti d’arte, d’antiquariato o da collezione (art.36, DL 41/95)"},
                {"RF15" , "Agenzie di vendite all’asta di oggetti d’arte, antiquariato o da collezione (art.40-bis, DL 41/95)"},
                {"RF16" , "IVA per cassa P.A. (art.6, c.5, DPR 633/72)"},
                {"RF17" , "IVA per cassa (art. 32-bis, DL 83/2012)"},
                {"RF18" , "Altro"},
                {"RF19" , "Regime forfettario (art.1, c.54-89, L. 190/2014)"},

            };
        }

        public static string GetDescription(string codiceRegime)
        {
            string descr = string.Empty;

            descr = (string.IsNullOrWhiteSpace(codiceRegime)) ? "codice errato" : dictRegime[codiceRegime.Trim()];

            return descr;
        }
    }
    public class RegimeFiscaleViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string codiceRegime)
        {
            string descrizione = RegimeFiscale.GetDescription(codiceRegime);
            return View("Default",descrizione);
        }

    }
}