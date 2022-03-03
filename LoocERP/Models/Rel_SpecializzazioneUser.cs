using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class Rel_SpecializzazioneUser
    {

        public Rel_SpecializzazioneUser()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [MaxLength(200)]
        [Display(Name = "Codice")]   
        public string Code { get; set; }

        [MaxLength(200)]
        [Display(Name = "Nome")] 
        public string Name { get; set; }


        [MaxLength(4000)]
        [Display(Name = "Descrizione")] 
        public string Description { get; set; }

        [Display(Name = "Data Inizio Corso")]        
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]        
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "Data Fine Corso")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? EndDate { get; set; }

        [Display(Name = "Data Inizio Validità Corso")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? ValidFrom { get; set; }
        

        [Display(Name = "Data Fine Validità Corso")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? ValidTo { get; set; }
        
        [MaxLength(200)]
        [Display(Name = "Rilasciato da")] 
        public string ReleasedFrom { get; set; }

        [Display(Name = "Rilasciato il")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? ReleasedAt { get; set; }


        [MaxLength(50)]
        [Display(Name = "Votazione")] 
        public string Vote { get; set; }

        [Display(Name = "Promosso")] 
        public bool isPromosso { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Specializzazione")]
        public Guid? SpecializzazioneId { get; set; }
        public virtual Specializzazione Specializzazione { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Dipendente")]
        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public virtual AppUser User { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        //Nome file per DB
        [Display(Name = "Nome file carcato")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string? FileName { get; set; }

        [Display(Name = "Corso")]
        [ForeignKey("Corsi")]
        public Guid? CorsoId { get; set; }
        public virtual Corsi Corsi { get; set; }

    }
}
