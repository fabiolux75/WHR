using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels
{
    public class UserNotificationCreateViewModel
    {

        public List<IdentityRole> roles { get; set; }
        public List<Models.AppUser> users { get; set; }

    }
}
