using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class Rel_MansioneUser
    {

        public Rel_MansioneUser()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        
        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Mansione")]
        [ForeignKey("Mansione")]
        public Guid? MansioneId { get; set; }        
        public virtual Mansione Mansione { get; set; }

        //[Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Macchina")]
        [ForeignKey("MansioneMacchina")]
        public Guid? MansioneMacchinaId { get; set; }
        public virtual MansioneMacchina MansioneMacchina { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Utente")]
        [ForeignKey("AppUser")]
        public String UserId { get; set; }        
        public virtual AppUser User { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }


        
        [Display(Name = "Data Assegnazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataAssegnazione { get; set; }

        [Display(Name = "Data Inizio Attività")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataInizioAttivita { get; set; }

        [Display(Name = "Data Fine Attività")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataFineAttivita { get; set; }

        [Display(Name = "Sempre Valido")]
        public bool SempreValido { get; set; }

        [Display(Name = "Livello Competenza")]
        public int? LivelloCompetenza { get; set; }
        
    }
}
