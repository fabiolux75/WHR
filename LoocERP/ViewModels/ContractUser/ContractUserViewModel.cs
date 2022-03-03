using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class ContractUserViewModel
    {
        public ContractUserViewModel()
        {
            ContractUser = new ContractUser();
            ContractUserList = new List<ContractUser>();
            LevelList = new List<Domain>();
            RetribuzioneTypeList = new List<Domain>();

            OreDiLavoroTypeList = new List<Domain>();
            ContractTypeList = new List<Domain>();
            LawNumberTypeList = new List<Domain>();
            CategoryCodeList = new List<Domain>();
            FineRapportoList = new List<Domain>();
        }

        public ContractUser ContractUser { get; set; }
        public List<ContractUser> ContractUserList { get; set; }
        public List<Domain> LevelList { get; set; }
        public List<Domain> RetribuzioneTypeList { get; set; }

        public List<Domain> OreDiLavoroTypeList { get; set; }
        public List<Domain> ContractTypeList { get; set; }
        public List<Domain> LawNumberTypeList { get; set; }
        public List<Domain> CategoryCodeList { get; set; }
        
        public List<Domain> FineRapportoList { get; set; }
        public string redirectUrl { get; set; }

        //File per Upload
        [Display(Name = "File da caricare online...")]
        public IFormFile? FileUpload { get; set; }

        public bool hasParentLevel { get; set; }
    }
}
