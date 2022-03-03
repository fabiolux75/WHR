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


namespace LoocERP.Controllers
{
    public class RelMansioneUserController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public RelMansioneUserController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }


        [Authorize("RelMansioneUser.Show")]
        public async Task<IActionResult> Index(string? userId = null, string? redirectUrl = null)
        {                        
            var user = await userManager.GetUserAsync(HttpContext.User);
            
            RelMansioneUserViewModel model = new RelMansioneUserViewModel();
            
            model.RelMansioneUser.UserId = userId;
                                                         
            Func<Rel_MansioneUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.RelMansioneUserList = _context.Set<Rel_MansioneUser>().Include(x => x.Mansione).Where(whereClause).ToList();
            model.MansioneList = _context.Set<Mansione>().OrderBy(c => c.Descrizione).ToList();
            model.MansioneMacchinaList = _context.Set<MansioneMacchina>().OrderBy(c => c.Description).ToList();
            model.redirectUrl = redirectUrl;
            return View(model);
        }


        [HttpPost]
        [Authorize("RelMansioneUser.Create")]
        public async Task<IActionResult> CreateAsync(RelMansioneUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                
                //Document d = new Document();
                // Saves the role in the underlying AspNetRoles table
                model.RelMansioneUser.Id = new Guid();
                
                model.RelMansioneUser.MultiTenantId = user.MultiTenantId;

                model.RelMansioneUser.DataAssegnazione = DateTime.Now;

               _context.Set<Rel_MansioneUser>().Add(model.RelMansioneUser);

                _context.SaveChanges();
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);                
            }
                        
            return Redirect(model.redirectUrl);
        }


        [Authorize("RelMansioneUser.Edit")]
        public async Task<IActionResult> Edit(string Id, string? userId = null, string? redirectUrl = null)
        {
            RelMansioneUserViewModel model = new RelMansioneUserViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(userId == null) userId = user.Id;

            model.RelMansioneUser = _context.Set<Rel_MansioneUser>()
                .Where(c => c.Id.ToString().ToLower().Equals(Id.ToString().ToLower()) )
                .FirstOrDefault();

            Func<Rel_MansioneUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.RelMansioneUserList = _context.Set<Rel_MansioneUser>().Where(whereClause).ToList();
            model.MansioneList = _context.Set<Mansione>().OrderBy(c => c.Descrizione).ToList();
            model.MansioneMacchinaList = _context.Set<MansioneMacchina>().OrderBy(c => c.Description).ToList();
            model.redirectUrl = redirectUrl;

            return View(model);
        }

        [HttpPost]
        [Authorize("RelMansioneUser.Edit")]
        public async Task<IActionResult> EditAsync(RelMansioneUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                
                Rel_MansioneUser d = _context.Set<Rel_MansioneUser>().Where( d => d.Id == model.RelMansioneUser.Id).FirstOrDefault();   
                d.MansioneId = model.RelMansioneUser.MansioneId;
                d.MansioneMacchinaId = model.RelMansioneUser.MansioneMacchinaId;
                d.DataInizioAttivita = model.RelMansioneUser.DataInizioAttivita;
                d.DataFineAttivita = model.RelMansioneUser.DataFineAttivita;
                d.SempreValido = model.RelMansioneUser.SempreValido;
                d.LivelloCompetenza = model.RelMansioneUser.LivelloCompetenza;

                _context.Set<Rel_MansioneUser>().Update(d);

                _context.SaveChanges();                
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);               
            }

            if(model.redirectUrl != null) return Redirect(model.redirectUrl);
            return Redirect(model.redirectUrl);
        }
    }        
}