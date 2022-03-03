using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
            Companies = new List<ANA_Company>();
            User = new AppUser();
            int? type = 0;
        }
        
        public AppUser User { get; set; }
        public List<ANA_Company> Companies { get; set; }
        public int? type { get; set; }
    }
}
