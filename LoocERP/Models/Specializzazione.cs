using LoocERP.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LoocERP.Models
{
    public class Specializzazione
    {
        public Specializzazione()
        {
            ID = Guid.NewGuid();
        }

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "Codice")]
        [MaxLength(200)]
        public string Codice { get; set; }

        [MaxLength(2000)]
        public string Descrizione { get; set; }


        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }

}
