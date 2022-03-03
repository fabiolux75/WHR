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
using System.Text.RegularExpressions;
using LoocERP.Helpers;

namespace LoocERP.Controllers
{
    public class SpecializzazioneController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public SpecializzazioneController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        //[Authorize("Vettore.Show")]
        public async Task<IActionResult> IndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaSpecializzazione, "Visualizzazione Specializzazione");
            return View();
        }

       
        public async Task<IActionResult> Create()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.CreazioneSpecializzazione, "Creazione Specializzazione");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Specializzazione model)
        {

            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();


            model.MultiTenantId = user.MultiTenantId;

            model.ID = Guid.NewGuid();

            _context.C_Specializzazioni.Add(model);


            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid? SpecializzazioneId = null)
        {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.EditSpecializzazione, "Edit Specializzazione");


            var specializzazione = _context.C_Specializzazioni
                                .Where(c => c.ID == SpecializzazioneId)
                                .FirstOrDefault();

            return View(specializzazione);
        }





        [HttpPost]
        public async Task<IActionResult> EditAsync(Specializzazione model)
        {
            if (ModelState.IsValid)
            {
                var specializzazione = _context.C_Specializzazioni
                                .Where(c => c.ID == model.ID)
                                .FirstOrDefault();
                specializzazione.Codice = model.Codice;
                specializzazione.Descrizione = model.Descrizione;
                specializzazione.Name = model.Name;



                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public async Task<JsonResult> ajaxIndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_Specializzazioni
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Select( c => new {
                    c.ID,
                    c.Codice,
                    c.Name,
                    c.Descrizione,
                })
                .ToList();

            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxRemoveSpecializzazioneAsync(string idSpecializzazione = null)
        {


            var v = _context.C_Specializzazioni.Where(c => c.ID == new Guid(idSpecializzazione)).FirstOrDefault();
            if (v != null)
            {
                _context.C_Specializzazioni.Remove(v);
                _context.SaveChanges();
            }
            return Json(new { result = "OK" });
        }

    }
}