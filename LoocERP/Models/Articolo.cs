using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models
{
    public class Articolo
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid ID { get; set; }
        public string CodiceMag { get; set; }
        public string Descrizione { get; set; }


        [Display(Name = "Unità di misura")]
        [ForeignKey("um")]
        public int idUm { get; set; }
        public virtual Um um { get; set; }
        public double ScortaMinima { get; set; }
        public double Qty { get; set; }


        [Display(Name = "Marca")]
        [ForeignKey("marca")]
        public int? IdMarca { get; set; }
        public virtual Marche marca { get; set; }
        
        public int? IdCategoria { get; set; }
        public Guid MultiTenantId { get; set; }
        public double PrezzoVendita { get; set; }


        public int IdFamiglia { get; set; }
        public int IdSottoFamiglia { get; set; }
        public int IdGruppo { get; set; }
        public int IdSottoGruppo { get; set; }

    }
}
