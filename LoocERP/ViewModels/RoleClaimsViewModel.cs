using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class RoleClaimsViewModel
    {

        public string RoleId { get; set; }
        public string Title { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}
