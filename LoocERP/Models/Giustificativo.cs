using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace LoocERP.Models
{
    public class Giustificativo
    {
        public Giustificativo()
        {
            Id = Guid.NewGuid();
            Active = true;
        }  

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Codice da utilizzare nella procedura di riferimento")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome Giustificativo")]
        public string Name { get; set; }
        
    
        [Display(Name = "Descrizione Giustificativo")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? Description { get; set; }
            
        [Display(Name = "Fonte Normativa")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? FonteNormativa { get; set; }

        [Display(Name = "Personale che può fruire del giustificativo ")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? Destinatari { get; set; }

        [Display(Name = "Modalità di Imputazione")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? ModalitaDiImputazione { get; set; }

        [Display(Name = "Adempimento a Carico dell'Interessato")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? AdempimentiCaricoInteressato { get; set; }
        
        [Display(Name = "Adempimento responsabile personale")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? AdempimentiResponsabileUOG { get; set; }

        [Display(Name = "Adempimento responsabile personale")]
        [MaxLength(4000, ErrorMessage = "Lunghezza massima 4000 caratteri")]
        public string? AdempimentiResponsabilePersonale { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        [Display(Name = "Attivo")]
        public bool? Active { get; set; }

        [Display(Name = "Tipo")]
        public int type { get; set; }
    }
}
