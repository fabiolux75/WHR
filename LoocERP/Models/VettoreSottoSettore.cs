using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VettoreSottoSettore
    {
        public VettoreSottoSettore()
        {
        }

        [Key]
        [Display(Name = "Codice")]
        public String Codice { get; set; }
                
        [MaxLength(6)]
        [Display(Name = "Codice Cliente")]
        public string CodCliente { get; set; }

        [MaxLength(6)]
        [Display(Name = "Codice Settore")]
        public string CodSett { get; set; }

        [MaxLength(100)]
        [Display(Name = "Descrizione")]
        public string Descr { get; set; }
    }
}
