using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class NoleggioViewModel
    {
        public Noleggio noleggio { get; set; }
        public List<VettoreAssegnazione> VettoriAssociati { get; set; }
        public List<NoleggiOption> noleggiOptions { get; set; }
    }
}
