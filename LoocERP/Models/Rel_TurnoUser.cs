using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class Rel_TurnoUser
    {

        public Rel_TurnoUser()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Giorno Lavoro")]
        public DateTime WorkDate { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Turno")]
        public Guid? TurnoId { get; set; }

        [ForeignKey("TurnoId")]
        public virtual Turno Turno { get; set; }


        [ForeignKey("AppUser")]
        public String UserId { get; set; }
        public virtual AppUser User { get; set; }


        [Display(Name = "Mansione")]
        [ForeignKey("Mansione")]
        public Guid? MansioneId { get; set; }        
        public virtual Mansione Mansione { get; set; }


        [Display(Name = "Specializzazione")]
        [ForeignKey("Specializzazione")]
        public Guid? SpecializzazioneId { get; set; }        
        public virtual Specializzazione Specializzazione { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
