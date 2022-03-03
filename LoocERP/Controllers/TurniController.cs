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
using Microsoft.EntityFrameworkCore;

namespace LoocERP.Controllers
{
    public class TurniController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public TurniController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }


        [Authorize("Turni.Show")]
        public IActionResult Index(string CantiereId = null, string MessageType=null, string MessageTitle = "", string Message = "")
        {
            Turno model = new Turno();
            ViewBag.Message = Message;
            ViewBag.MessageType = MessageType;
            ViewBag.MessageTitle = MessageTitle;
            ViewBag.CantiereId = CantiereId;
            return View(model);
        }


        [HttpPost]
        [Authorize("Turni.Create")]
        public async Task<IActionResult> CreateAsync(Turno model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                // Saves the role in the underlying AspNetRoles table
                model.Id = new Guid();
                model.dataInizio = Convert.ToDateTime("01-01-1900");
                model.dataFine = Convert.ToDateTime("01-01-2100");
                model.MultiTenantId = user.MultiTenantId;
                
                _context.C_Turni.Add(model);

                // Creo mansioni di default
                List<Mansione> MansioniDefault = _context.C_Mansioni.Where(c => c.isAssignedAsDefault == 1 && c.MultiTenantId == user.MultiTenantId ).ToList();
                foreach (var r in MansioniDefault) {          
                    Rel_FabbisognoMansione fr = new Rel_FabbisognoMansione();
                    fr.CantiereId = (Guid)model.CantiereId;
                    fr.MansioneId = r.ID;
                    fr.TurnoId = model.Id;
                    fr.Quantita = 1;        
                    fr.MultiTenantId = user.MultiTenantId;            
                    _context.C_Rel_FabbisognoMansione.Add(fr);
                }                    

                _context.SaveChanges();
                return RedirectToAction("edit", "Cantiere", new { id = model.CantiereId, MessageType = "Success", MessageTitle = "Turno", Message = "Turno aggiunto con successo" });
            }
            
            return RedirectToAction("edit", "Cantiere", new { id = model.CantiereId, MessageType = "Info", MessageTitle = "Turno", Message = "Errore il Turno non è stato caricato correttamente" });       
        }

        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Lista turni di lavoro
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        public async Task<JsonResult> ajaxIndexAsync(string? CantiereId = null)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<Turno, bool> whereClauseActive = (a => true); //default le prendo tute
            if (CantiereId != null) whereClauseActive = (a => a.CantiereId.ToString().ToLower().Equals(CantiereId.ToString().ToLower()));

            var r = _context.C_Turni
                .Where( c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()): true)
                .Where(whereClauseActive)
                .ToList();

            return Json(
                new { data = r }
            );
        }

        public JsonResult getTurnoById(Guid id)
        {
            var turno = _context.C_Turni.Include(c => c.Cantiere)
                .Where(t => t.Id == id)
                .Select(
                    c=> new
                    {
                        id = c.Id,
                        nome = c.Name,
                        cantiereId = c.CantiereId,
                        nomeCantiere = c.Cantiere.Name,
                    }
                )
                .FirstOrDefault();

            return Json(new { turno });
        }

        public JsonResult getTurno(String turnoId)
        {
            var turno = _context.C_Rel_TurniUsers.Where(t => t.TurnoId.ToString().ToLower().Equals(turnoId)).FirstOrDefault();

            return Json( new { turno });
        }

    }
        
}