using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models
{
    public class UserClaim
    {
        public string ClaimTitle { get; set; }
        public string ClaimDescription { get; set; }
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }        

    }
}
