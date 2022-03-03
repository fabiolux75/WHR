using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class MansioneMacchina
    {

        public MansioneMacchina()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [Display(Name = "Codice")]
        [MaxLength(200)]
        public string? Codice { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "Descrizione")]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Mansione")]
        [ForeignKey("Mansione")]
        public Guid? MansioneId { get; set; }        
        public virtual Mansione Mansione { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
