using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoocERP.Models;
using LoocERP.ViewModels;
using LoocERP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

//per Upload
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using LoocERP.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LoocERP.Controllers
{
    public class UserNotificationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public UserNotificationController(ApplicationDBContext context
                                , UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager,
             RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            this.roleManager = roleManager;
        }

        [Authorize("Comunicazioni.View")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);


            var model = _context.C_UserNotification.ToList();
            return View(model);
        }


        [Authorize("Comunicazioni.Create")]
        [HttpPost]
        public async Task<IActionResult> Create(List<Guid> roles,List<Guid> users,String message)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (roles.Count > 0)
            {
                foreach(Guid id in roles)
                {

                    var usrs = _context.UserRoles.Where(c => c.RoleId.ToLower().Trim() == id.ToString().ToLower()).Select(c => new Guid(c.UserId)).ToList();
                    foreach(var u in usrs)
                    {
                        if (!users.Contains(u))
                        {
                            users.Add(u);
                        }
                    }
                }
            }

            if(users.Count > 0)
            {
                foreach(var u in users)
                {
                    _context.C_UserNotification.Add(
                    new UserNotification
                    {
                        MessageText = message,
                        MultiTenantId = user.MultiTenantId,
                        Seen = 0,
                        Sounded = 0,
                        Tipologia = Typology.generica,
                        UserId = u.ToString(),
                    }
                    );
                }
                _context.SaveChanges();
            }

            return Redirect("Index");
        }

        [Authorize("Comunicazioni.Create")]
        public async Task<IActionResult> CreateAsync()
        {
            UserNotificationCreateViewModel model = new UserNotificationCreateViewModel();

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var roles = roleManager.Roles.ToList();
            var claims = _context.C_Claims.ToList();
            var users = _context.Users.Where(c => c.MultiTenantId == user.MultiTenantId).ToList();

            model.roles = roles;
            model.users = users;

            return View(model);
        }

        public async Task<JsonResult> ajaxIndexAsync()
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            try
            {                
                var r = _context.C_UserNotification
                    .Include(c =>c.User)
                    //.Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .OrderBy( c => c.DataCreazione)
                    .Select(
                        c => new
                        {
                            id = c.Id,
                            text = c.MessageText,
                            tipology = c.Tipologia.GetType().GetMember(c.Tipologia.ToString())
                                    .First()
                                    .GetCustomAttribute<DisplayAttribute>()
                                    .GetName(),
                            user = (c.User != null) ? c.User.FirstName + " "+ c.User.LastName : "",
                            seen = c.Seen,
                            sounded = c.Sounded,
                        }
                    )
                    .ToList();

                return Json(
                    new { data = r }
                );
            }
            catch (Exception e)
            {
                return Json(
                    new
                    {
                        data = new { }
                    }
                );
            }

        }

    }
}
