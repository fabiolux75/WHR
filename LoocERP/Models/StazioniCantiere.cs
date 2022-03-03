using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class StazioniCantiere
    {

        public StazioniCantiere()
        {
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        
        public virtual Stazione Stazione { get; set; }

        [ForeignKey("Stazione")]
        [Display(Name = "Stazione")]
        public Guid? Stazione_id { get; set; }


        public DateTime StartValidity { get; set; }
        public DateTime EndValidity { get; set; }

        [Display(Name = "Latitudine")]
        public double Latitude { get; set; }
        [Display(Name = "Longitudine")]
        public double Longitude { get; set; }
        [Display(Name = "Km Copertura")]
        public double KmCopertura { get; set; }


        [Display(Name = "Cantiere")]
        [ForeignKey("Cantiere")]
        public Guid? CantiereId { get; set; }
        public virtual Cantiere Cantiere { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
