using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Data;
using LoocERP.Models;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LoocERP.Controllers
{
    public class MultiTenantController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public MultiTenantController(ApplicationDBContext context
                                ,UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }
       
        [AllowAnonymous]
        [HttpGet]
        public async Task<FileResult> getLogoPicture(String id = null)
        {
            var r = _context.C_Multitenant.Where(v => v.Id.ToString().Equals(id)).FirstOrDefault();
            if (r != null && r.LogoUrl != null)
            {
                return File(r.LogoUrl, "image/png");
            }            

            return File("~/looc/images/loghi/kresearch.png", "image/png");
        }
    }
}