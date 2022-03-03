using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class RelSpecializzazioneUserViewModel
    {
        public RelSpecializzazioneUserViewModel()
        {
            RelSpecializzazioneUser = new Rel_SpecializzazioneUser();
            RelSpecializzazioneUserList = new List<Rel_SpecializzazioneUser>();
            SpecializzazioneList = new List<Specializzazione>();
        }

        public Rel_SpecializzazioneUser RelSpecializzazioneUser { get; set; }
         public List<Specializzazione> SpecializzazioneList { get; set; }
        public List<Rel_SpecializzazioneUser> RelSpecializzazioneUserList { get; set; }
        public string redirectUrl { get; set; }

        //File per Upload
        [Display(Name = "File da caricare online...")]
        public IFormFile? FileUpload { get; set; }

    }
}
