using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VettoreCantiere
    {
        public VettoreCantiere()
        {
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public Guid CantiereId { get; set; }
        public Cantiere cantiere { get; set; }



        [Display(Name = "Codice")]
        public int VettoreId { get; set; }
        public virtual Vettore Vettore { get; set; }
        public DateTime? WorkDate { get; set; }        
    }
}
