using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class CreateCompanyViewModel
    {
        public CreateCompanyViewModel()
        {
            Company = new ANA_Company();
            Companies = new List<ANA_Company>();
        }
        public ANA_Company Company { get; set; }
        public List<ANA_Company> Companies { get; set; }

    }
}
