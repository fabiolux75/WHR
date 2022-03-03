using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class EditCompanyViewModel
    {
        public EditCompanyViewModel()
        {
            Company = new ANA_Company();
            Companies = new List<Models.ANA_Company>();
            Users = new List<Models.AppUser>();
        }


        public bool isExternal { get; set; }
        public ANA_Company Company { get; set; }
        public List<Models.ANA_Company>? Companies { get; set; }
        public List<Models.AppUser>? Users { get; set; }
    }
}
