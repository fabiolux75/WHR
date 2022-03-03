using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class Turno
    {

        public Turno()
        {
            this.Active = 1;
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome Turno")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Ora Inizio Turno")]
        public TimeSpan OraInizio { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Ora Fine Turno")]
        public TimeSpan OraFine { get; set; }

        [Display(Name = "Cantiere")]
        [ForeignKey("Cantiere")]
        public Guid? CantiereId { get; set; }        
        public virtual Cantiere Cantiere { get; set; }

        public DateTime dataInizio { get; set; }
        public DateTime dataFine { get; set; }

        [Display(Name = "Attivo")]
        public int? Active { get; set; } //0 Turno attivo - 1 Turno disattivo

        [Display(Name = "Stato")]
        public int? Stato { get; set; } //1 Chiuso - 0 Aperto


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        //public virtual ICollection<Rel_> Rel_MansioneUser {get; set; }


        [Display(Name = "Minuti Pausa")]
        public int Pausa { get; set; }


        [Display(Name = "Auto chiusura")]
        public int oraAutoChiusura { get; set; }

        
        public virtual List<Rel_TurnoUser> TurniUser { get; set; }
    }
}
