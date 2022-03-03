using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Companies = new List<ANA_Company>();
            User = new AppUser();
        }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Conferma Password")]
        [Compare("Password",ErrorMessage = "La password di conferma non è identica")]
        public string ConfirmPassword { get; set; }
        
        public AppUser User { get; set; }

        public List<ANA_Company> Companies { get; set; }
    }
}
