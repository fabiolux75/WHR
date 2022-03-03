using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public enum TipologiaCantiere
    {
        Normale,
        Generico
    }
    public class Cantiere
    {

        public Cantiere()
        {
            this.Active = true; //default progetto attivo
            this.CapocantiereId = null;
            this.isGeneric = TipologiaCantiere.Normale;
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome Cantiere*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Name { get; set; }

        [Display(Name = "Descrizione Cantiere*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Codice Cantiere*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Codice { get; set; }        

        [Display(Name = "Cordinate: Latitude")]        
        public string? Latitude { get; set; }

        [Display(Name = "Cordinate: Longitude")]
        public string? Longitude { get; set; }

        [Display(Name = "Km di copertura")]
        public string? Round { get; set; }

        [Display(Name = "Nazione")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Nazione { get; set; }

        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        [Display(Name = "Regione")]
        public string? Regione { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Provincia { get; set; }

        [Display(Name = "Città")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Citta { get; set; }

        [Display(Name = "Indirizzo")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Indirizzo { get; set; }

        [Display(Name = "Progetto")]
        [ForeignKey("Project")]
        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        [Display(Name = "Data Apertura*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Data Fine Cantiere*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [ForeignKey("AppUser")]        
        public Guid? CapocantiereId { get; set; }                

        [Display(Name = "Attivo")]
        public bool Active { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }
        public virtual IEnumerable<VettoreCantiere> VettoreCantieres { get; set; }
        public TipologiaCantiere isGeneric { get; set; }

        public virtual IEnumerable<Turno> Turni { get; set; }

    }
}