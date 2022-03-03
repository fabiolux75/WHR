using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VettoreParcheggio
    {
        public VettoreParcheggio() { }

        public Guid Id { get; set; }
        public Guid ParcheggioId { get; set; }
        virtual public Parcheggio Parcheggio { get; set; }
        public int VettoreId { get; set; }
        virtual public Vettore Vettore { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
