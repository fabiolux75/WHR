using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class AssignUserCantieriViewModel
    {
        public AssignUserCantieriViewModel()
        {            
            MansioneList = new List<Mansione>();
        }        
        public List<Mansione> MansioneList { get; set; }
    }
}