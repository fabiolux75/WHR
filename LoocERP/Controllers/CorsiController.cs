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
    public class CorsiController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CorsiController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("Corsi.Show")]
        public async Task<IActionResult> Index()
        {                        
           //var user = await userManager.GetUserAsync(HttpContext.User);
            
            //Corsi model = new Corsi();

            //Func<Corsi, bool> whereClause = (a => true); //default le prendo tutte
            //model.SpecializzazioneList = _context.Set<Specializzazione>().OrderBy(c => c.Descrizione).ToList();
           // model.redirectUrl = redirectUrl;
            return View(); //model
        }

        [HttpGet]
        [Authorize("Corsi.Create")]
        public async Task<IActionResult> Create()
        {
            CreateCorsiViewModel model = new CreateCorsiViewModel();

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            

            model.SpecializzazioneList = _context.Set<Specializzazione>()
                    .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .ToList();


            return View(model);
        }

        [HttpPost]
        [Authorize("Corsi.Create")]
        public async Task<IActionResult> CreateAsync(CreateCorsiViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (parentID != null) model.Company.ParentID = new Guid(parentID);

                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                Corsi cor = new Corsi
                {
                    ID = Guid.NewGuid(),
                    Code = model.Corso.Code,
                    Name = model.Corso.Name,
                    Description = model.Corso.Description,
                    StartDate = model.Corso.StartDate,
                    EndDate = model.Corso.EndDate,
                    ValidFrom = model.Corso.ValidFrom,
                    ValidTo = model.Corso.ValidTo,
                    ReleasedFrom = model.Corso.ReleasedFrom,
                    ReleasedAt = model.Corso.ReleasedAt,
                    Docente = model.Corso.Docente,
                    Stato = model.Corso.Stato,
                    SpecializzazioneId = model.Corso.SpecializzazioneId,
                    MultiTenantId = user.MultiTenantId
                };
                // Saves the role in the underlying AspNetRoles table
                _context.Set<Corsi>().Add(cor);
                var salvato = _context.SaveChanges();

                if (salvato > 0)
                {
                    return View("index");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize("Corsi.Edit")]
        public async Task<IActionResult> Edit(string? IdCorso = null)
        {
            CreateCorsiViewModel model = new CreateCorsiViewModel();

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<Corsi, bool> whereCorso = (a => true); //default sempre vera
            if (IdCorso != null) whereCorso = (c => c.ID.ToString().Equals(IdCorso));

            model.Corso = _context.Set<Corsi>()
                .Where(whereCorso)
                .FirstOrDefault();

            model.SpecializzazioneList = _context.Set<Specializzazione>()
                    .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .ToList();

            Func<Specializzazione, bool> whereSpecCurrent = (a => true); //default sempre vera
            whereSpecCurrent = (c => c.ID.Equals(model.Corso.SpecializzazioneId));

            model.SpecializzazioneCorrente = _context.Set<Specializzazione>()
                    .Where(c => (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .Where(whereSpecCurrent)
                    .FirstOrDefault();

            Func<Rel_SpecializzazioneUser, bool> whereRelSpecUser = (a => true); //default sempre vera
            whereRelSpecUser = (c => c.CorsoId.ToString().Equals(IdCorso));

            model.RelSpecializzazioneUserList = _context.Set<Rel_SpecializzazioneUser>().Include(c => c.User)
                .Where(whereRelSpecUser)
                .ToList();

            //ViewBag.IdCorso = IdCorso;

            return View(model);
        }

        [HttpPost]
        [Authorize("Corsi.Edit")]
        public async Task<IActionResult> EditAsync(CreateCorsiViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                Corsi cor = _context.Set<Corsi>()
                    .Where(c => c.ID.Equals(model.Corso.ID))
                    .FirstOrDefault();


                cor.ID = model.Corso.ID;
                cor.Code = model.Corso.Code;
                cor.Name = model.Corso.Name;
                cor.Description = model.Corso.Description;
                cor.StartDate = model.Corso.StartDate;
                cor.EndDate = model.Corso.EndDate;
                cor.ValidFrom = model.Corso.ValidFrom;
                cor.ValidTo = model.Corso.ValidTo;
                cor.ReleasedFrom = model.Corso.ReleasedFrom;
                cor.ReleasedAt = model.Corso.ReleasedAt;
                cor.Docente = model.Corso.Docente;
                cor.Stato = model.Corso.Stato;
                cor.SpecializzazioneId = model.Corso.SpecializzazioneId;
                cor.MultiTenantId = user.MultiTenantId;
                

                _context.Set<Corsi>().Update(cor);
                var salvato = _context.SaveChanges();

                //if (model.Corso.Stato == 0)
                //{
                    List<Rel_SpecializzazioneUser> listUtentiCorso = _context.Set<Rel_SpecializzazioneUser>()
                        .Where(c => c.MultiTenantId.Equals(user.MultiTenantId))
                        .Where(c => c.CorsoId.Equals(model.Corso.ID))
                        .ToList();

                    foreach (var item in listUtentiCorso)
                    {
                        item.StartDate = model.Corso.StartDate;
                        item.EndDate = model.Corso.EndDate;
                        item.ValidFrom = model.Corso.ValidFrom;
                        item.ValidTo = model.Corso.ValidTo;
                        item.ReleasedFrom = model.Corso.ReleasedFrom;
                        item.ReleasedAt = model.Corso.ReleasedAt;
                        item.SpecializzazioneId = model.Corso.SpecializzazioneId;
                        item.Code = model.Corso.Code;
                        item.Name = model.Corso.Name;
                        item.Description = model.Corso.Description;

                        _context.Set<Rel_SpecializzazioneUser>().Update(item);
                        _context.SaveChanges();
                    }


                //}

                if (salvato > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public async Task<JsonResult> ajaxIndexAsync(int? stato = 1)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<Corsi, bool> whereClauseStato = (a => true); //default le prendo tute
            if (stato != 1) whereClauseStato = (a => a.Stato == stato);

            var r = _context.Set<Corsi>()
                .Where(c=> (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()): true)
                .Where(whereClauseStato)
                .ToList();

            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxDeletePartecipante(string? id = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<Rel_SpecializzazioneUser, bool> whereClauseStato = (a => false); //default nessuno
            if (id != null) whereClauseStato = (a => a.Id == new Guid(id));

            var r = _context.Set<Rel_SpecializzazioneUser>()
                .Where(c => (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true )
                .Where(whereClauseStato)
                .FirstOrDefault();


            _context.Set<Rel_SpecializzazioneUser>().Remove(r);
            _context.SaveChanges();

            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxAddPartecipanti(Guid? IdSpec = null, string? NomeSpec= null, string? DescrSpec = null, string? CodSpec = null, Guid? IdCorso = null, string? IdUser = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            Rel_SpecializzazioneUser specializzazioneUtente = new Rel_SpecializzazioneUser
            {
                Id = Guid.NewGuid(),
                SpecializzazioneId = IdSpec,
                UserId = IdUser,
                MultiTenantId = user.MultiTenantId,
                Code = CodSpec,
                Name = NomeSpec,
                Description = DescrSpec,
                //Description = model.Corso.Description,
                //StartDate
                //EndDate
                //ValidFrom
                //ValidTo
                //ReleasedFrom 
                //ReleasedAt 
                //Vote
                //isPromosso
                CorsoId = IdCorso,
                //FileName
            };
            // Saves the role in the underlying AspNetRoles table
            var currentRel = _context.Set<Rel_SpecializzazioneUser>().Add(specializzazioneUtente);
            var salvato = _context.SaveChanges();

            if (salvato > 0)
            {
                return Json(
                    new
                    {
                        data = new
                        {
                            result = currentRel.Entity.Id,
                            result1 = currentRel.Entity.UserId
                        }
                    }
                );
            }

            return Json(
                new
                {
                    data = new
                    {
                        result = "ko"
                    }
                }
            );

        }

        public async Task<JsonResult> ajaxEditPartecipanti(Guid? IdRelSpecUser = null, string? Vote = null, bool? isPromosso = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            Rel_SpecializzazioneUser specializzazioneUtente = _context.Set<Rel_SpecializzazioneUser>()
                .Where(c => c.MultiTenantId.Equals(user.MultiTenantId))
                .Where(c => c.Id.Equals(IdRelSpecUser))
                .FirstOrDefault();

            specializzazioneUtente.Vote = Vote ?? specializzazioneUtente.Vote;
            specializzazioneUtente.isPromosso = isPromosso ?? specializzazioneUtente.isPromosso;

            // Saves the role in the underlying AspNetRoles table
            var currentRel = _context.Set<Rel_SpecializzazioneUser>().Update(specializzazioneUtente);
            var salvato = _context.SaveChanges();

            if (salvato > 0)
            {
                return Json(
                    new
                    {
                        data = new
                        {
                            result = "ok"
                        }
                    }
                );
            }

            return Json(
                new
                {
                    data = new
                    {
                        result = "ko"
                    }
                }
            );
        }


    }        
}