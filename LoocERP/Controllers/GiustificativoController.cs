using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Data;
using LoocERP.Models;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ServiceStack;

namespace LoocERP.Controllers
{
    public class GiustificativoController : Controller
    {
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public GiustificativoController(ApplicationDBContext context
                                 , UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Lista Giustificativi
        /// </summary>  
        public async Task<JsonResult> ajaxIndex(int type = 0, string term = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<Giustificativo>()
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(c => c.type == type)
                .Where(c => (String.IsNullOrEmpty(term) || c.Code.Contains(term)) || 
                             (String.IsNullOrEmpty(term) || c.Name.Contains(term)) 
                            )
                .Select(x => new
                {
                    id = x.Id,
                    //text = x.Cantiere.Name
                    text = '(' + x.Code + ") - " + x.Name
                })
             .ToList();

            return Json(
                new { results = r }
            ); 
        }
    }
}