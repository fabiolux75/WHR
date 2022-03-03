using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class EditRoleViewModel
    {   
        public string RoleId { get; set; }
        public string Title { get; set; }

        public List<Models.UserClaim> Claims { get; set; }
    }
}
