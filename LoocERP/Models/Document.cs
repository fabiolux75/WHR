using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public enum DocumentGroup
    {
        DocumentArea, // 0- gestione da pagina documenti
        ProfiloUtente, // 1 - documento relativi al profilo utente
        TitoloStudio, // 2 - documento relativi al profilo utente
    }

    public class Document
    {
        public Document()
        {
            Id = Guid.NewGuid();
        }        

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Document Group")]
        public DocumentGroup? DocumentGroup { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Numero*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Numero { get; set; }

        
        [Display(Name = "Descrizione")]
        [MaxLength(2000, ErrorMessage = "Lunghezza massima 2000 caratteri")]
        public string? Description { get; set; }

        [Display(Name = "Dettagli")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 2000 caratteri")]
        public string? Details { get; set; }
        
        [Display(Name = "Tipologia*")]
        [ForeignKey("Domain")]
        public Guid? DocumentTypeId { get; set; }
        public virtual Domain DocumentType { get; set; }
                
        [Display(Name = "Data Inizio Validità")]        
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]        
        public DateTime? ValidFrom { get; set; }
        
        [Display(Name = "Scadenza/Data Fine Validità")]  
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? ValidTo { get; set; }


        [ForeignKey("AppUser")]
        public String? UserId { get; set; }
        public virtual AppUser User { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        //Nome file per DB
        [Display(Name = "Nome file carcato")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string? FileName { get; set; }
    }
}
