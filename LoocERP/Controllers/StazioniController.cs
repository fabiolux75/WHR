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
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;


namespace LoocERP.Controllers
{
    public class StazioniController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public StazioniController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }

        public async Task<JsonResult> ajaxIndexAsync(string? CantiereId = null)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_StazioniCantiere
                .Include(c => c.Stazione)
                .Where(
                c => c.MultiTenantId == user.MultiTenantId && c.CantiereId.ToString().Equals(CantiereId)
                ).Select(
                c => new
                {
                    c.Id,
                    Stazione = (c.Stazione != null) ? c.Stazione.Nome : "",
                    c.StartValidity,
                    c.EndValidity,
                    c.KmCopertura,
                    c.Latitude,
                    c.Longitude,
                }
                ).ToList();

            return Json(
                new { data = r }
            );
        }


        [HttpPost]
        //[Authorize("Turni.Create")]
        public async Task<IActionResult> CreateAsync(StazioniCantiere model)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            model.MultiTenantId = user.MultiTenantId;

            _context.C_StazioniCantiere.Add(model);

            _context.SaveChanges();



            return RedirectToAction("edit", "Cantiere", new { id = model.CantiereId });
        }

        public async Task<JsonResult> ajaxCantiereStazioneListAsync(DateTime dateTime, String? q = "")
        {

           

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_Stazioni
                .Where(c => c.Nome.ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = x.Nome.Trim(),
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }
    }
}
