using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class LeasingViewModel
    {
        public VettoreLeasing leasing { get; set; }
        public List<VettoreAssegnazione> VettoriAssociati { get; set; }
    }
}
