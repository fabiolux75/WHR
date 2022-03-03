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
using Microsoft.AspNetCore.Authorization;

namespace LoocERP.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public ProjectController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }

        [Authorize("Project.Show")]
        public IActionResult Index(string MessageType=null, string MessageTitle = "", string Message = "")
        {
            Project model = new Project();
            ViewBag.Message = Message;
            ViewBag.MessageType = MessageType;
            ViewBag.MessageTitle = MessageTitle;
            return View(model);
        }

        [HttpPost]
        [Authorize("Project.Create")]
        public async Task<IActionResult> CreateAsync(Project model)
        {
            if (ModelState.IsValid)
            {
                // Saves the role in the underlying AspNetRoles table
                model.Id = new Guid();
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                model.MultiTenantId = (user.MultiTenantId??null);

                _context.C_Projects.Add(model);
                _context.SaveChanges();               
                return RedirectToAction("index", new { MessageType = "Success", MessageTitle = "Progetto", Message = "Progetto aggiunto con successo" });            
            }
            return RedirectToAction("index", new { MessageType = "Info", MessageTitle = "Progetto", Message = "Errore il progetto non è stato caricato correttamente" });            
        }


        [HttpPost]
        [Authorize("Project.Edit")]
        public async Task<IActionResult> EditAsync(Project model)
        {
            var cp = _context.Set<Project>().Where(c => c.Id.ToString().ToLower() == model.Id.ToString().ToLower()).FirstOrDefault();
            if (model.Id != null)
            {
                cp.Id = model.Id;
                cp.Name = (model.Name ?? null);
                cp.Description = (model.Description ?? null);
                cp.Codice = (model.Codice ?? null);
                cp.StartDate = (model.StartDate ?? null);
                cp.EndDate = (model.EndDate ?? null);
                cp.Active = (model.Active ?? null);
                _context.SaveChanges();
            }

            return RedirectToAction("index", "Cantiere", new { IdProject = model.Id });

        }

        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Lista turni di lavoro
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        public async Task<JsonResult> ajaxIndexAsync(int? active = 1)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            Func<Project, bool> whereClauseActive = (a => true); //default le prendo tute
            if (active == 0 || active == 1) whereClauseActive = (a => a.Active == active);

            var r = _context.C_Projects
                .Where(c => (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(whereClauseActive)
                .ToList();

            return Json(
                new { data = r }
            );
        }
    }
        
}