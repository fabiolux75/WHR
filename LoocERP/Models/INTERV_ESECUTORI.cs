using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace LoocERP.Models
{
    public class INTERV_ESECUTORI
    {

        public String CodCliente { get; set; }
        public String codSettore { get; set; }
        public String codSottosettore { get; set; }
        public bool FlgInterno { get; set; }
        [Key]
        public int Codice { get; set; }
        public String Descr { get; set; }
        [Display(Name = "Data Creazione*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCreazione { get; set; }
        [Display(Name = "Data Modifica*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataModifica { get; set; }
        public String OperatoreCreazione { get; set; }
        public String OperatoreModifica { get; set; }
        public String? Note { get; set; }
        public String Uid { get; set; }
    }
}
