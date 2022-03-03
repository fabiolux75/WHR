using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class MED_INTERV_ESECUTORI_INFO
    {
        [Key]
        public Int64 Codice { get; set; }
        public int CodEsecutore { get; set; }
        public string Descr { get; set; }
        public int Cancellato { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataModifica { get; set; }
        public string OperatoreCreazione { get; set; }
        public string OperatoreModifica { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public float CostoOrario { get; set; }
        public bool AccessoAccettazione { get; set; }
    }
}
