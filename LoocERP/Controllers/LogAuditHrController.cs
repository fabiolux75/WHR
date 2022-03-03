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

namespace LoocERP.Controllers
{
    public class LogAuditHrController : Controller
    {

        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public LogAuditHrController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ajaxIndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

           
            var r = _context.C_LogAuditHR
                .Include(c => c.User)
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Select(c => new { 
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    Date = c.EventDate,
                    Detail = c.Details,
                })
                .ToList();

            return Json(
                new { data = r }
            );
        }
    }
}