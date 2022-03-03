using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models
{
    public partial class MedAnaVettoriInfo
    {

        public MedAnaVettoriInfo()
        {

        }

        [Key]
        public int Codice { get; set; }
        public int CodAnaVettore { get; set; }
        public string Descr { get; set; }
        public byte? Cancellato { get; set; }
        public DateTime? DataCreazione { get; set; }
        public DateTime? DataModifica { get; set; }
        public string OperatoreCreazione { get; set; }
        public string OperatoreModifica { get; set; }
        public double? Consumo { get; set; }
        public double? Tolleranza { get; set; }
        public double? Costo { get; set; }
        public double? TolleranzaCosto { get; set; }
        public string RifDefault { get; set; }
        public double? LimiteVelocita { get; set; }
        public double? LimiteTemperatura { get; set; }
        public double? LimitePermettivita { get; set; }
        public double? LimiteLivello { get; set; }
        public string TipoDistribuzione { get; set; }
        public double? CostoCustom { get; set; }

        [ForeignKey("CodAnaVettore")]
        public Vettore vettore { get; set; }
    }
}
