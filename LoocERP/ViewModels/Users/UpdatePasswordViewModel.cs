using LoocERP.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class UpdatePasswordViewModel
    {
        public UpdatePasswordViewModel()
        {                       
        }
        [Required]
        public string UserId { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma Password")]
        [Compare("Password", ErrorMessage = "La password di conferma non è identica")]
        public string ConfirmPassword { get; set; }
    }
}
