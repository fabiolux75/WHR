using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VettoreLeasing
    {
        public VettoreLeasing()
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
        public Guid? Company_id { get; set; }
        public Guid? MultiTenantId { get; set; }
        
    }
}
