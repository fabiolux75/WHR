using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class MalattiaUserViewModel
    {
        public MalattiaUserViewModel()
        {
            MalattiaUser = new MalattiaUser();
            MalattiaUserList = new List<MalattiaUser>();
        }

        public MalattiaUser MalattiaUser { get; set; }
         public List<MalattiaUser> MalattiaUserList { get; set; }
        public string redirectUrl { get; set; }

        //File per Upload
        [Display(Name = "File da caricare online...")]
        public IFormFile? FileUpload { get; set; }
    }
}
