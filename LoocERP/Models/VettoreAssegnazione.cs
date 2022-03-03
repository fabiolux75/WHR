using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VettoreAssegnazione
    {
        public VettoreAssegnazione()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public int CodiceVettore { get; set; }

        [ForeignKey("NoleggioId")]
        public Noleggio Noleggio { get; set; }

        [ForeignKey("CodiceVettore")]
        public Vettore Vettore { get; set; }

        [ForeignKey("LeasingId")]
        public VettoreLeasing Leasing { get; set; }
        public Guid? NoleggioId { get; set; }
        public Guid? LeasingId { get; set; }

    }
}
