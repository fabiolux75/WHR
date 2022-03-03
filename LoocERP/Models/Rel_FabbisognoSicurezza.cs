using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace LoocERP.Models
{
    public class Rel_FabbisognoSicurezza
    {

        public Rel_FabbisognoSicurezza()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }
        

        [Required(ErrorMessage = "Campo richiesto")]
        
        [Display(Name = "Cantiere")]
        [ForeignKey("Cantiere")]
        public Guid CantiereId { get; set; }

        [NotMapped]        
        public virtual Cantiere Cantiere { get; set; }
        
        [Display(Name = "Specializzazione")]
        [ForeignKey("C_Specializzazioni")]
        public Guid? SpecializzazioneId { get; set; }
        public string? SpecializzazioneDesc { get; set; }             
        public virtual Specializzazione Specializzazione { get; set; }        

        [Display(Name = "Quantita")]        
        public int? Quantita { get; set; }



        [Display(Name = "Turno")]
        [ForeignKey("Turno")]
        public Guid? TurnoId { get; set; }
        [NotMapped]
        public virtual Turno Turno { get; set; }
        

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
