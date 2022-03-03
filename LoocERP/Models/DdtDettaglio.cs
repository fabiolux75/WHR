using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models
{
    public class DdtDettaglio
    {

        [Key]
        public Guid Id { get; set; }
        public string Codice { get; set; }

        public string Descrizione { get; set; }
        public string UM { get; set; }
        public double Quantita { get; set; }
        public string PrezzoListino1 { get; set; }


        [ForeignKey("Ddt")]
        public Guid IdDDT { get; set; }
        public virtual Ddt Ddt { get; set; }
        public int FlagAttrezzatura { get; set; }
        public int NumeroRiga { get; set; }
                
        [ForeignKey("Aliquota")]
        public string AliquotaIVA { get; set; }
        public virtual Looc_GOF_Aliquota Aliquota { get; set; }

    }
}
