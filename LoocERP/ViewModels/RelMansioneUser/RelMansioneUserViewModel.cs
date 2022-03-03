using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class RelMansioneUserViewModel
    {
        public RelMansioneUserViewModel()
        {
            RelMansioneUser = new Rel_MansioneUser();
            RelMansioneUserList = new List<Rel_MansioneUser>();
            MansioneList = new List<Mansione>();
            MansioneMacchinaList = new List<MansioneMacchina>();
        }

        public Rel_MansioneUser RelMansioneUser { get; set; }
         public List<Mansione> MansioneList { get; set; }
        public List<MansioneMacchina> MansioneMacchinaList { get; set; }
        public List<Rel_MansioneUser> RelMansioneUserList { get; set; }
        public string redirectUrl { get; set; }
    }
}
