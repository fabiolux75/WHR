using LoocERP.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class Mansione
    {
        public Mansione()
        {
            ID = Guid.NewGuid();
            isAssignedAsDefault = 0;
            isEnabledManageHour = 0;
        }

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [MaxLength(200)]
        public string Codice { get; set; }


        [JsonProperty(PropertyName = "Name")]
        [Display(Name = "Nome")]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Descrizione { get; set; }

        [MaxLength(200)]
        public int? isAssignedAsDefault { get; set; } //0 non è assegnata di default -> altrimenti viene assegnata di default al cantiere  
        public int? isEnabledManageHour { get; set; } //0 non è abilitata gestione Ore - 1 è abilitata alla gestione ore
        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        
        public virtual ICollection<Rel_MansioneUser> MansioneUser { get; set; }


    }
}
