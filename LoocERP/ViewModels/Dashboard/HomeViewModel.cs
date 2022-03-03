using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels.Dashboard
{
    public class HomeViewModel
    {        
        [Display(Name = "Numero di Cantieri")]
        public string nCantieri { get; set; }
    }
}