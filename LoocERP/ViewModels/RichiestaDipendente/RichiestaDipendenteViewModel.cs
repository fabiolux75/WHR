using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class RichiestaDipendenteViewModel
    {
        public RichiestaDipendenteViewModel()
        {
            RichiestaDipendente = new RichiestaDipendente();
            RichiestaDipendenteList = new List<RichiestaDipendente>();
            GiustificativiList = new List<Giustificativo>();
        }

        public RichiestaDipendente RichiestaDipendente { get; set; }
        public List<RichiestaDipendente> RichiestaDipendenteList { get; set; }
        public List<Giustificativo> GiustificativiList { get; set; }

    }
}
