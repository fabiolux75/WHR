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
    public class RichiestaDipendenteController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public RichiestaDipendenteController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("RichiestaDipendente.Show")]
        public async Task<IActionResult> Index()
        {                        
            return View();
        }

        [HttpGet]
        [Authorize("RichiestaDipendente.Create")]
        public async Task<IActionResult> Create()
        {
            RichiestaDipendenteViewModel model = new RichiestaDipendenteViewModel();

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            

            model.GiustificativiList = _context.Set<Giustificativo>()
                    .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .ToList();

            model.RichiestaDipendente.Data = DateTime.Now;

            return View(model);
        }

        [HttpPost]
        [Authorize("RichiestaDipendente.Create")]
        public async Task<IActionResult> CreateAsync(RichiestaDipendenteViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                if (model.RichiestaDipendente.TipologiaRichiesta.Equals(1))
                {
                    model.RichiestaDipendente.RichiestaIl = null;
                    model.RichiestaDipendente.NumeroOre = 0;
                }
                else if (model.RichiestaDipendente.TipologiaRichiesta.Equals(2))
                {
                    model.RichiestaDipendente.RichiestaDal = null;
                    model.RichiestaDipendente.RichiestaAl = null;
                    model.RichiestaDipendente.NumeroGiorni = 0;
                }

                RichiestaDipendente vm = new RichiestaDipendente
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    TipologiaRichiesta = model.RichiestaDipendente.TipologiaRichiesta,
                    RichiestaId = model.RichiestaDipendente.RichiestaId,
                    Data = model.RichiestaDipendente.Data,
                    RichiestaDal = model.RichiestaDipendente.RichiestaDal,
                    RichiestaAl = model.RichiestaDipendente.RichiestaAl,
                    RichiestaIl = model.RichiestaDipendente.RichiestaIl,
                    NumeroGiorni = model.RichiestaDipendente.NumeroGiorni,
                    NumeroOre = model.RichiestaDipendente.NumeroOre,
                    Stato = model.RichiestaDipendente.Stato,
                    MultiTenantId = user.MultiTenantId
                };
                // Saves the role in the underlying AspNetRoles table
                _context.Set<RichiestaDipendente>().Add(vm);
                var salvato = _context.SaveChanges();
                
                if (salvato > 0)
                {
                    return View("index");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize("RichiestaDipendente.Edit")]
        public async Task<IActionResult> Edit(string? IdRichiesta = null)
        {
            RichiestaDipendenteViewModel model = new RichiestaDipendenteViewModel();

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<RichiestaDipendente, bool> whereRichiesta = (a => true); //default sempre vera
            if (IdRichiesta != null) whereRichiesta = (c => c.Id.ToString().Equals(IdRichiesta));

            model.RichiestaDipendente = _context.Set<RichiestaDipendente>()
                .Include(c => c.Giustificativo)
                .Where(whereRichiesta)
                .Where(c => (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .FirstOrDefault();

            
            model.GiustificativiList = _context.Set<Giustificativo>()
                    .Where(c => (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize("RichiestaDipendente.Edit")]
        public async Task<IActionResult> EditAsync(RichiestaDipendenteViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                if (model.RichiestaDipendente.TipologiaRichiesta.Equals(1))
                {
                    model.RichiestaDipendente.RichiestaIl = null;
                    model.RichiestaDipendente.NumeroOre = 0;
                }
                else if (model.RichiestaDipendente.TipologiaRichiesta.Equals(2))
                {
                    model.RichiestaDipendente.RichiestaDal = null;
                    model.RichiestaDipendente.RichiestaAl = null;
                    model.RichiestaDipendente.NumeroGiorni = 0;
                }

                RichiestaDipendente cor = _context.Set<RichiestaDipendente>()
                    .Where(c => c.Id.Equals(model.RichiestaDipendente.Id))
                    .FirstOrDefault();


                //cor.Id = model.RichiestaDipendente.Id;
                cor.TipologiaRichiesta = model.RichiestaDipendente.TipologiaRichiesta;
                cor.RichiestaId = model.RichiestaDipendente.RichiestaId;
                cor.Data = model.RichiestaDipendente.Data;
                cor.RichiestaDal = model.RichiestaDipendente.RichiestaDal;
                cor.RichiestaAl = model.RichiestaDipendente.RichiestaAl;
                cor.RichiestaIl = model.RichiestaDipendente.RichiestaIl;
                cor.NumeroGiorni = model.RichiestaDipendente.NumeroGiorni;
                cor.NumeroOre = model.RichiestaDipendente.NumeroOre;
                cor.Stato = model.RichiestaDipendente.Stato;
                cor.MultiTenantId = user.MultiTenantId;
                

                _context.Set<RichiestaDipendente>().Update(cor);
                var salvato = _context.SaveChanges();

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
            
            
            Func<RichiestaDipendente, bool> whereClauseStato = (a => true); //default le prendo tute
            if (stato != 1) whereClauseStato = (a => a.Stato.Equals(stato));
            
            var r = _context.Set<RichiestaDipendente>()
                .Include(c => c.User)
                .Include(c => c.Giustificativo)
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(whereClauseStato)
                .Select( c => new{
                    id = c.Id,
                    tipologia = c.TipologiaRichiesta.ToString(),
                    giustificativo = c.Giustificativo.Name,
                    userFullName = c.User.FirstName + " " + c.User.LastName,
                    stato = c.Stato.ToString(),
                })
                .ToList();

            return Json(
                new { data = r }
            );
        }


    }    

}
