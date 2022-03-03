using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models.Sdi
{

    public class ChartFatture
    {

        public int Mese { get; set; }

        public double FattureEmesse { get; set; }

        public double  FattureRicevute { get; set; }


        public ChartFatture(int mese){
            this.Mese = mese;

            this.FattureEmesse = default(double);
            this.FattureRicevute = default(double);
        }

    }
}
