using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class Domain
    {
        public Domain()
        {
            Id = Guid.NewGuid();           
        }

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Nome Dominio*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "Tipologia Dominio*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Tipo { get; set; }

        [Display(Name = "Codice")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string Code { get; set; }
        

        [Display(Name = "Dominio")]
        [ForeignKey("Domain")]
        public Guid? ParentId { get; set; }
        public virtual MultiTenant Parent { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

    }
}
