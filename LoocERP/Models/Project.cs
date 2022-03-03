using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class Project
    {

        public Project()
        {
            this.Active = 1; //default progetto attivo
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome Commessa*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Descrizione Commessa*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Codice Commessa*")]
        [MaxLength(6, ErrorMessage = "Lunghezza massima 6 caratteri")]
        [RegularExpression(@"^[a-zA-Z0-9]{0,6}$", ErrorMessage = "asdasdas.")]
        public string Codice { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Data Apertura")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Data Chiusura")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Attivo")]
        public int? Active { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
