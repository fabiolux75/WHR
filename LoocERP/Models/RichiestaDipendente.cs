using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class RichiestaDipendente
    {
        public enum TipologiaRichiestaEnum
        {
            [Display(Name = "Richiesta in GIORNI")]
            Giorni = 1, // 1
            [Display(Name = "Richiesta in ORE")]
            Ore = 2 // 2
        }

        public enum StatoEnum
        {
            Aperto = 1, // 1
            Accettato = 2, // 2
            Rifiutato = 3 // 3
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Dipendente")]
        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public virtual AppUser User { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Tipologia Richiesta")]
        public TipologiaRichiestaEnum TipologiaRichiesta { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome Richiesta")]
        [ForeignKey("Giustificativo")]
        public Guid? RichiestaId { get; set; }
        public virtual Giustificativo Giustificativo { get; set; }

        [Display(Name = "Data della richiesta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Data { get; set; }

        [Display(Name = "Richiesta Dal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RichiestaDal { get; set; }

        [Display(Name = "Richiesta Al")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RichiestaAl { get; set; }

        [Display(Name = "Richiesta per Il")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RichiestaIl { get; set; }

        [Display(Name = "Numero Giorni")]
        public int NumeroGiorni { get; set; }

        [Display(Name = "Numero Ore")]
        public int NumeroOre { get; set; }

        //[Required(ErrorMessage = "Campo richiesto")]
        //[Display(Name = "Destinatario richiesta")]
        //[ForeignKey("AppUser")]
        //public string? RichiestaAId { get; set; }
        //public virtual AppUser RichiestaA { get; set; }

        [Display(Name = "Stato della richiesta")]
        public StatoEnum Stato { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
