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



namespace LoocERP.Controllers
{
    public class CantiereController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public CantiereController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }

        [Authorize("Cantiere.Show")]
        public IActionResult Index(string? IdProject = null, string? MessageType =null, string? MessageTitle = "", string? Message = "")
        {
            Cantiere model = new Cantiere();            
            ViewBag.Message = Message;
            ViewBag.MessageType = MessageType;
            ViewBag.MessageTitle = MessageTitle;
            ViewBag.ProjectId = IdProject;
            ViewData["Project"] = _context.C_Projects.Where(c => c.Id.ToString().ToLower().Equals(IdProject.ToString().ToLower()) ).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [Authorize("Cantiere.Create")]
        public async Task<IActionResult> Create(Cantiere model)
        {
            if (ModelState.IsValid)
            {                
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                // Saves the role in the underlying AspNetRoles table  
                Cantiere comp = new Cantiere
                {
                    Id = new Guid(),
                    Name = model.Name,
                    Description = model.Codice + "-" + model.Name,
                    Codice = (model.Codice ?? ""),
                    Latitude = (model.Latitude ?? null),
                    Longitude = (model.Longitude ?? null),
                    Round = (model.Round ?? null),
                    Nazione = (model.Nazione ?? ""),
                    Regione = (model.Regione ?? ""),
                    Provincia = (model.Provincia ?? ""),
                    Citta = (model.Citta ?? ""),
                    Indirizzo = (model.Indirizzo??""),
                    ProjectId = (model.ProjectId??null),
                    CapocantiereId = (model.CapocantiereId??null),
                    Active = (model.Active),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate.AddDays(1).AddTicks(-1),
                    MultiTenantId = (user.MultiTenantId??null)                    
                };           
                _context.C_Cantieri.Add(comp);
                _context.SaveChanges();

                return RedirectToAction(
                    "edit", 
                    new { id = comp.Id, 
                          MessageType = "Success", 
                          MessageTitle = "Cantiere",
                          Message = "Cantiere aggiunto con successo" 
                    }
                );            
            }

            var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return RedirectToAction("index", new { idProject = model.ProjectId, MessageType = "Error", MessageTitle = "Cantiere", Message = "Errore il cantiere non è stato caricato correttamente:" + message });            
        }


        [HttpGet]
        [Authorize("Cantiere.Edit")]
        public async Task<IActionResult> Edit(string id = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var model = _context.C_Cantieri.Where(c => c.Id.ToString().ToLower() == id.ToString().ToLower()).First();


            /*
            List<AppUser> employees = new List<AppUser>();
            List<Mansione> mansioni = new List<Mansione>();
            List<Rel_FabbisognoMansione> FabbisognoMansione = new List<Rel_FabbisognoMansione>();
            List<Rel_FabbisognoSicurezza> FabbisognoSicurezza = new List<Rel_FabbisognoSicurezza>();
            */


            List<AppUser> employees = _context.Users.Where(c => c.MultiTenantId == user.MultiTenantId &&  c.isEmployee == 1).ToList();
            List<Mansione> mansioni = _context.C_Mansioni.Where(c => c.MultiTenantId == user.MultiTenantId).OrderBy(c => c.Descrizione).ToList();
            
            
            
            List<Rel_FabbisognoMansione> FabbisognoMansione =
                _context.C_Rel_FabbisognoMansione
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Include(x => x.Mansione)
                .Where(c => c.CantiereId.ToString().ToLower() == id.ToString().ToLower())
                .ToList();

            List<Rel_FabbisognoSicurezza> FabbisognoSicurezza =
                 _context.C_Rel_FabbisognoSicurezza   
                 .Where(c => c.MultiTenantId == user.MultiTenantId)              
                 .Where(c => c.CantiereId.ToString().ToLower() == id.ToString().ToLower())
                 .Include(c => c.Specializzazione)
                 .ToList()
                 ;
                         
            List<Specializzazione> Specializzazioni = _context.C_Specializzazioni.Where(c => c.MultiTenantId == user.MultiTenantId).OrderBy(c => c.Descrizione).ToList();

            /*
            List<Rel_FabbisognoAssegnazione> FabbisognoAssegnazione =
                 _context.C_Rel_FabbisognoAssegnazione
                 .Where(c => c.CantiereId.ToString().ToLower() == id.ToString().ToLower())  
                 .Include( c => c.Mansione)
                 .Include( c => c.Specializzazione)
                 .ToList()
                 ;
            */

            List<Turno> Turni =
                 _context.C_Turni
                 .Where(c => c.CantiereId.ToString().ToLower() == id.ToString().ToLower())
                 .ToList()
                 ;

            ViewData["Turno"] = new Turno();
            ViewData["Turni"] = Turni;
            ViewData["Employees"] = employees;
            ViewData["Mansioni"] = mansioni;
            ViewData["FabbisognoMansione"] = FabbisognoMansione;
            ViewData["FabbisognoSicurezza"] = FabbisognoSicurezza;
            //ViewData["FabbisognoAssegnazione"] = FabbisognoAssegnazione;
            ViewData["Specializzazioni"] = Specializzazioni;
            ViewData["Cantiere"] = model;

            ViewBag.IdCantiere = id;
            return View(model);
        }


        [HttpPost]
        [Authorize("Cantiere.Edit")]
        public IActionResult Edit(Cantiere model)
        {
            var cantiere = _context.C_Cantieri
                .Include(c => c.Turni)
                .Where(c => c.Id.ToString().ToLower() == model.Id.ToString().ToLower())
                .Select(c => new { 
                    cp = c,
                    turni = c.Turni.Select( c => c.Id).ToList(),
                })
                .FirstOrDefault();

            var cp = cantiere.cp;


            var c_vs = _context.C_VettoreCantiere
                .Include(c => c.Vettore)
                .Where(s => s.CantiereId == cp.Id).OrderByDescending(c => c.WorkDate)
                .GroupBy(c => new { c.Vettore.Codice})
                .Select(
                    d => new { 
                        vettore = d.Key,
                        data = d.Max(c => c.WorkDate),
                    }
                )
                .ToList();
            var diff = EF.Functions.DateDiffDay(cp.EndDate, model.EndDate);

            


            

            if(diff > 0)
            {
                if (c_vs.Count > 0)
                {
                    foreach (var v in c_vs)
                    {
                        if (EF.Functions.DateDiffDay(cp.EndDate, v.data) == 0 || EF.Functions.DateDiffDay(cp.EndDate, v.data) == -1)
                        {
                            
                            for (int k = 0; k < diff; k++)
                            {
                                _context.C_VettoreCantiere.Add(
                                    new VettoreCantiere
                                    {
                                        Id = Guid.NewGuid(),
                                        VettoreId = v.vettore.Codice,
                                        CantiereId = model.Id,
                                        WorkDate = v.data.Value.AddDays(k + 1),
                                    }
                                    );
                            }
                        }
                    }
                }

                _context.SaveChanges();


                var users = _context.C_Rel_TurniUsers
                              .Where(c => c.MultiTenantId == cp.MultiTenantId)
                              .Where(c => cantiere.turni.Contains((Guid)c.TurnoId))
                              .Select(c => new
                              {
                                  c.MansioneId,
                                  c.SpecializzazioneId,
                                  c.UserId,
                                  c.TurnoId,
                              })
                              .Distinct()
                              .ToList();


                foreach (var user in users)
                {
                    for (int k = 0; k < diff; k++)
                    {
                        _context.C_Rel_TurniUsers.Add(
                                new Rel_TurnoUser
                                {
                                    Id = Guid.NewGuid(),
                                    WorkDate = cp.EndDate.AddDays(k + 1),
                                    MansioneId = user.MansioneId,
                                    TurnoId = user.TurnoId,
                                    MultiTenantId = cp.MultiTenantId,
                                    SpecializzazioneId = user.SpecializzazioneId,
                                    UserId = user.UserId,
                                }
                                );
                    }
                }
            }else if(diff < 0)
            {
                return RedirectToAction("edit", new { id = model.Id });
            }

            _context.SaveChanges();

            if (model.Id != null)
            {
                cp.CapocantiereId = model.CapocantiereId;
                cp.Nazione = (model.Nazione ?? null);
                cp.Regione = (model.Regione ?? null);
                cp.Provincia = (model.Provincia ?? null);
                cp.Citta = (model.Citta ?? null);
                cp.Indirizzo = (model.Indirizzo ?? null);
                cp.Description = (model.Description ?? null);
                cp.Name = (model.Name ?? null);
                cp.Latitude = (model.Latitude ?? null);
                cp.Longitude = (model.Longitude ?? null);
                cp.Round = (model.Round ?? null);
                cp.EndDate = model.EndDate;

                _context.SaveChanges();
            }

            return RedirectToAction("edit", new { id = model.Id });

        }




        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Lista turni di lavoro
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        public async Task<JsonResult> ajaxIndexAsync(bool? active = true,string? IdProject = null)
        {            
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            try
            {
                var r = _context.C_Cantieri
                    .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)                 
                    .Where(a => (active == true || active == false) ? ( a.Active == active) : true )
                    .Where(a => (IdProject != null) ? a.ProjectId == Guid.Parse(IdProject) : true)                    
                    .ToList();

                return Json(
                    new { data = r }
                );
            }
            catch (Exception e) {
                return Json(
                    new {
                        data = new { }                            
                    }
                );                    
            }            
        }
    }
        
}