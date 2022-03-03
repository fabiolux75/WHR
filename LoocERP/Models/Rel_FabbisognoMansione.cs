using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class Rel_FabbisognoMansione
    {

        public Rel_FabbisognoMansione()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }
        

        [Required(ErrorMessage = "Campo richiesto")]
        
        [Display(Name = "Cantiere")]
        [ForeignKey("Cantiere")]
        public Guid CantiereId { get; set; }        
        public virtual Cantiere Cantiere { get; set; }
        
        [Display(Name = "Mansione")]
        [ForeignKey("Mansione")]
        public Guid? MansioneId { get; set; }            
        public virtual Mansione Mansione { get; set; }
        
        public string? MansioneDesc { get; set; }

        [Display(Name = "Quantita")]        
        public int? Quantita { get; set; }


        [Display(Name = "Turno")]
        [ForeignKey("Turno")]
        public Guid? TurnoId { get; set; }        
        public virtual Turno Turno { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }


    }
}
