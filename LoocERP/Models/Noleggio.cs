using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class Noleggio
    {
        public Noleggio()
        {

        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Numero protocollo")]
        public int ProtocolNumber { get; set; }
        [Display(Name = "Data Inizio")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Data Fine")]
        public DateTime EndDate { get; set; }
        
        public int CodiceVettore { get; set; }
        public Guid? MultiTenantId { get; set; }


        [ForeignKey("Company_id")]
        public virtual ANA_Company company { get; set; }


        [ForeignKey("CodiceVettore")]
        public virtual Vettore Vettore { get; set; }

        [Display(Name = "Azienda")]
        public Guid? Company_id { get; set; }

    }
}
