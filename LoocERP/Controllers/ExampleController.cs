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
    public class ExampleController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public ExampleController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("Example.Show")]
        public async Task<IActionResult> Index()
        {
            return View(); //model
        }

        [HttpGet]
        [Authorize("Example.Create")]
        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize("Example.Create")]
        public async Task<IActionResult> CreateAsync(/*CreateCorsiViewModel model*/)
        {
            
            return View();
        }

        [HttpGet]
        [Authorize("Example.Edit")]
        public async Task<IActionResult> Edit(string? IdExample = null)
        {
            

            return View();
        }

        [HttpPost]
        [Authorize("Corsi.Edit")]
        public async Task<IActionResult> EditAsync()
        {
            return View();
        }

        public async Task<JsonResult> ajaxIndexAsync(int? stato = 1)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            Func<Corsi, bool> whereClauseStato = (a => true); //default le prendo tute
            if (stato != 1) whereClauseStato = (a => a.Stato == stato);

            var r = _context.Set<Corsi>()
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
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
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(whereClauseStato)
                .FirstOrDefault();


            _context.Set<Rel_SpecializzazioneUser>().Remove(r);
            _context.SaveChanges();

            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxAddPartecipanti(Guid? IdSpec = null, string? NomeSpec = null, string? DescrSpec = null, string? CodSpec = null, Guid? IdCorso = null, string? IdUser = null)
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