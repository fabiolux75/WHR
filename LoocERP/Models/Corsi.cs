using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class Corsi
    {
        public Corsi()
        {
            ID = Guid.NewGuid();
            Stato = 1;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid ID { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Data Fine Corso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Data Inizio Validità Corso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ValidFrom { get; set; }

        [Display(Name = "Data Fine Validità Corso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ValidTo { get; set; }

        [MaxLength(200)]
        [Display(Name = "Rilasciato da")]
        public string ReleasedFrom { get; set; }

        [Display(Name = "Rilasciato il")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ReleasedAt { get; set; }

        [MaxLength(200)]
        [Display(Name = "Docente")]
        public string Docente { get; set; }

        [Display(Name = "Stato del corso")]
        public int Stato { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Specializzazione")]
        public Guid? SpecializzazioneId { get; set; }
        public virtual Specializzazione Specializzazione { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
