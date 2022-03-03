using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Nome Ruolo")]
        public string RoleName { get; set; }
    }
}
