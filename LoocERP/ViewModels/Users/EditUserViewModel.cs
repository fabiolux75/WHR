using LoocERP.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Companies = new List<ANA_Company>();
            User = new AppUser();
            UpdatePasswordViewModel = new UpdatePasswordViewModel();
            DocumentViewModel = new DocumentViewModel();
        }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma Password")]
        [Compare("Password", ErrorMessage = "La password di conferma non è identica")]
        public string ConfirmPassword { get; set; }

        public AppUser User { get; set; }
        public List<ANA_Company> Companies { get; set; }
        public List<Mansione> MansionesAssegnati { get; set; }
        public List<Specializzazione> SpecializzazionesAssegnate { get; set; }

        //public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
        public IList<Tuple<String, String, bool>> Mansioni { get; set; }
        public IList<Tuple<String, String, bool>> Specializzazioni { get; set; }

        public UpdatePasswordViewModel UpdatePasswordViewModel { get; set; }
        //[Display(Name = "Foto profilo")]
        //public IFormFile ProfilePicture { get; set; }    
    
        public DocumentViewModel DocumentViewModel { get; set; }

        public List<ANA_Company> CompaniesInternal { get; set; }

        public bool enableEsecutore { get; set; }
        public bool isParking { get; set; }
        public String email { get; set; }
    }
}
