using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class CreateVisitaMedicaViewModel
    {
        public CreateVisitaMedicaViewModel()
        {
            VisitaMedica = new VisitaMedica();
            VisitaMedicaList = new List<VisitaMedica>();
            MalattiaUserList = new List<MalattiaUser>();
            RichiedentiList = new List<ANA_Company>();

        }

        public VisitaMedica VisitaMedica { get; set; }
        public List<VisitaMedica> VisitaMedicaList { get; set; }
        public List<MalattiaUser> MalattiaUserList { get; set; }
        public List<ANA_Company> RichiedentiList { get; set; }

        // [Display(Name = "Richiedente")]
        // public Guid CompanyID {get; set; }

    }
}
