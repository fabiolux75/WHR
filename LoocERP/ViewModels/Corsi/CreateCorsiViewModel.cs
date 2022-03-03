using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class CreateCorsiViewModel
    {
        public CreateCorsiViewModel()
        {
            Corso = new Corsi();
            CorsiList = new List<Corsi>();
            SpecializzazioneCorrente = new Specializzazione();
            SpecializzazioneList = new List<Specializzazione>();
            RelSpecializzazioneUserList = new List<Rel_SpecializzazioneUser>();
        }

        public Corsi Corso { get; set; }
        public List<Corsi> CorsiList { get; set; }
        public Specializzazione SpecializzazioneCorrente { get; set; }
        public List<Specializzazione> SpecializzazioneList { get; set; }
        public List<Rel_SpecializzazioneUser> RelSpecializzazioneUserList { get; set; }

    }
}
