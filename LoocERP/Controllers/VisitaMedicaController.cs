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
    public class VisitaMedicaController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public VisitaMedicaController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("VisitaMedica.Show")]
        public async Task<IActionResult> Index()
        {                        
            return View();
        }

        [HttpGet]
        [Authorize("VisitaMedica.Create")]
        public async Task<IActionResult> Create()
        {
            CreateVisitaMedicaViewModel model = new CreateVisitaMedicaViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize("VisitaMedica.Create")]
        public async Task<IActionResult> CreateAsync(CreateVisitaMedicaViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                VisitaMedica vm = new VisitaMedica
                {
                    ID = Guid.NewGuid(),
                    Tipologia = model.VisitaMedica.Tipologia,
                    TipoEvento = model.VisitaMedica.TipoEvento,
                    Description = model.VisitaMedica.Description,
                    StartDate = model.VisitaMedica.StartDate,
                    EndDate = model.VisitaMedica.EndDate,
                    Medico = model.VisitaMedica.Medico,
                    Stato = model.VisitaMedica.Stato,
                    MultiTenantId = user.MultiTenantId
                };
                // Saves the role in the underlying AspNetRoles table
                _context.Set<VisitaMedica>().Add(vm);
                var salvato = _context.SaveChanges();
                
                if (salvato > 0)
                {
                    return View("index");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize("VisitaMedica.Edit")]
        public async Task<IActionResult> Edit(string? IdVisitaMedica = null)
        {
            CreateVisitaMedicaViewModel model = new CreateVisitaMedicaViewModel();


            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            model.RichiedentiList = _context.Set<ANA_Company>()
            .Where(c => c.MultiTenantId == user.MultiTenantId)
            .OrderBy(c => c.RagioneSociale)
            .ToList();

            
            Func<VisitaMedica, bool> whereVisitaMedica = (a => true); //default sempre vera
            if (IdVisitaMedica != null) whereVisitaMedica = (c => c.ID.ToString().Equals(IdVisitaMedica));

            model.VisitaMedica = _context.Set<VisitaMedica>()
                .Where(whereVisitaMedica)
                .FirstOrDefault();

            Func<MalattiaUser, bool> whereMalattiaUser = (a => true); //default sempre vera
            whereMalattiaUser = (c => c.VisitaMedicaId.ToString().Equals(IdVisitaMedica));

            model.MalattiaUserList = _context.Set<MalattiaUser>().Include(c => c.User)
                .Where(whereMalattiaUser)
                .ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize("VisitaMedica.Edit")]
        public async Task<IActionResult> EditAsync(CreateVisitaMedicaViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                VisitaMedica cor = _context.Set<VisitaMedica>()
                    .Where(c => c.ID.Equals(model.VisitaMedica.ID))
                    .FirstOrDefault();


                cor.ID = model.VisitaMedica.ID;
                cor.Tipologia = model.VisitaMedica.Tipologia;
                cor.TipoEvento = model.VisitaMedica.TipoEvento;
                cor.Description = model.VisitaMedica.Description;
                cor.StartDate = model.VisitaMedica.StartDate;
                cor.EndDate = model.VisitaMedica.EndDate;
                cor.Medico = model.VisitaMedica.Medico;
                cor.Stato = model.VisitaMedica.Stato;
                cor.MultiTenantId = user.MultiTenantId;
                cor.CompanyId = model.VisitaMedica.CompanyId;
                

                _context.Set<VisitaMedica>().Update(cor);
                var salvato = _context.SaveChanges();

                //if (model.VisitaMedica.Stato == 0)
                //{
                    List<MalattiaUser> listUtentiCorso = _context.Set<MalattiaUser>()
                        .Where(c => c.MultiTenantId.Equals(user.MultiTenantId))
                        .Where(c => c.VisitaMedicaId.Equals(model.VisitaMedica.ID))
                        .ToList();

                    foreach (var item in listUtentiCorso)
                    {
                        item.ValidFrom = model.VisitaMedica.StartDate;
                        item.ValidTo = model.VisitaMedica.EndDate;
                        item.Medico = model.VisitaMedica.Medico;

                    _context.Set<MalattiaUser>().Update(item);
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

        public async Task<JsonResult> ajaxDeletePartecipante(string? id = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<MalattiaUser>()
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(a => (id != null) ? a.Id == new Guid(id) : true )
                .FirstOrDefault();


            _context.Set<MalattiaUser>().Remove(r);
            _context.SaveChanges();

            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxIndexAsync(int? stato = 1)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<VisitaMedica>()
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(a => (stato != 1) ? a.Stato == stato : true)
                .ToList();

            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxAddPartecipanti(Guid? IdVisitaMedica = null, string? IdUser = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            MalattiaUser visitaMedicaUtente = new MalattiaUser
            {
                Id = Guid.NewGuid(),
                UserId = IdUser,
                MultiTenantId = user.MultiTenantId,
                Tipologia = TipologiaMalattia.VisitaMedica,
                TipoEvento = TipoEvento.EventoSingolo,
                //Description = model.Corso.Description,
                //StartDate
                //EndDate
                //ValidFrom
                //ValidTo
                //ReleasedFrom 
                //ReleasedAt 
                //Vote
                //isPromosso
                VisitaMedicaId = IdVisitaMedica,
                //FileName
            };
            // Saves the role in the underlying AspNetRoles table
            var currentRel = _context.Set<MalattiaUser>().Add(visitaMedicaUtente);
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

        public async Task<JsonResult> ajaxEditPartecipanti(Guid? IdVisitaMedica = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            MalattiaUser visitaMedicaUtente = _context.Set<MalattiaUser>()
                .Where(c => c.MultiTenantId.Equals(user.MultiTenantId))
                .Where(c => c.Id.Equals(IdVisitaMedica))
                .FirstOrDefault();

            // Saves the role in the underlying AspNetRoles table
            var currentRel = _context.Set<MalattiaUser>().Update(visitaMedicaUtente);
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