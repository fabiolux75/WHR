using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class TimeSheet
    {

        public TimeSheet()
        {         
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public string? NomeOperatore { get; set; }
        public string? CodiceOperatore { get; set; }
        public DateTime? DataLogin { get; set; }
        public TimeSpan? OraLogin { get; set; }
        public DateTime? DataLogout { get; set; }
        public TimeSpan? OraLogout { get; set; }

        public int? IdDevice { get; set; }
        public string? CodiceVettore { get; set; }
        public string? TargaVettore { get; set; }
        public string? CodiceAzienda { get; set; }
        public string? Evento { get; set; }
        public string? Stato { get; set; }
        public string? CodiceCantiere { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        
        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
