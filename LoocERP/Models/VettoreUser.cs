using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VettoreUser
    {
        public VettoreUser()
        {
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [ForeignKey("AppUser")]        
        public String UserId { get; set; }        
        public virtual AppUser User { get; set; }        
        
        [Display(Name = "Codice")]
        public int VettoreId { get; set; }        
        public virtual Vettore Vettore { get; set; }

        [Display(Name = "Data Inizio Validità")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Data Fine Validità")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
       
    }
}
