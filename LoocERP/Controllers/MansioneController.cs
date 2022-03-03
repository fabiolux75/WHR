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

namespace LoocERP.Controllers
{
    public class MansioneController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public MansioneController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> IndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? MansioneId = null)
        {

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            

            var Mansione = _context.C_Mansioni
                                .Where(c => c.ID == MansioneId)
                                .FirstOrDefault();

            return View(Mansione);
        }

        [HttpPost]
        public IActionResult createMacchina(MansioneMacchina model)
        {

            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();



            MansioneMacchina mm = new MansioneMacchina
            {
                Id = Guid.NewGuid(),
                MansioneId = model.MansioneId,
                Description = model.Description,
                Name = model.Name,
                Codice = model.Codice,
                MultiTenantId = user.MultiTenantId
            };

            _context.C_MansioneMacchina.Add(mm);
            _context.SaveChanges();


            return RedirectToAction("edit",new { MansioneId = model.MansioneId});
        }

        public async Task<JsonResult> ajaxMacchineAssegnateAsync(Guid? id)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_MansioneMacchina
                .Where(c => c.MansioneId == id)
                .Select(c => new {
                    c.Id,
                    c.Codice,
                    c.Name,
                    c.Description,
                })
                .ToList();

            return Json(
                new { data = r }
            );
        }
        
        public async Task<JsonResult> ajaxMacchineSpostamentoAsync(Guid? id)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_MansioneMacchina
                .Include( c => c.Mansione)
                .Where(c => c.MansioneId != id)
                .Select(c => new {
                    c.Id,
                    c.Codice,
                    c.Name,
                    c.Description,
                    Mansione = (c.Mansione != null) ? c.Mansione.Codice + " - " + c.Mansione.Name : "",
                })
                .ToList();

            return Json(
                new { data = r }
            );
        }



        public async Task<JsonResult> ajaxIndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_Mansioni
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Select(c => new {
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

        [HttpPost]
        public async Task<IActionResult> Create(string codice, string name,string Descrizione)
        {

            Mansione model = new Mansione();

            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();


            model.MultiTenantId = user.MultiTenantId;

            model.isAssignedAsDefault = 0;
            model.isEnabledManageHour = 0;
            model.Descrizione = Descrizione;
            model.Name = name;
            model.Codice = codice;

            model.ID = Guid.NewGuid();

            _context.C_Mansioni.Add(model);


            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> EditAsync(Guid? id,string codice, string name, string Descrizione)
        {
            if (ModelState.IsValid)
            {
               


                var Mansione = _context.C_Mansioni
                                .Where(c => c.ID == id)
                                .FirstOrDefault();
                Mansione.Codice = codice;
                Mansione.Descrizione = Descrizione;
                Mansione.Name = name;



                _context.SaveChanges();
            }

            return RedirectToAction("edit",new { MansioneId  = id});
        }


        public async Task<JsonResult> ajaxRemoveMacchinaMansioneAsync(Guid? idMacchina)
        {
            

            var macchina = _context.C_MansioneMacchina
                            .Where(c => c.Id == idMacchina)
                            .FirstOrDefault();

            var id = macchina.MansioneId;

            _context.C_MansioneMacchina.Remove(macchina);
            _context.SaveChanges();


            return Json(
               new { result = "OK" }
           );
        }


        public async Task<JsonResult> ajaxMoveMacchinaMansioneAsync(Guid? idMacchina, Guid? idMansione)
        {
            var car = _context.C_MansioneMacchina.Where(c => c.Id == idMacchina).FirstOrDefault();

            car.MansioneId = idMansione;

            _context.SaveChanges();


            return Json(
               new { result = "OK" }
           );
        }












        public async Task<JsonResult> ajaxRemoveMansioneAsync(string idMansione = null)
        {


            var v = _context.C_Mansioni.Where(c => c.ID == new Guid(idMansione)).FirstOrDefault();
            if (v != null)
            {
                _context.C_Mansioni.Remove(v);
                _context.SaveChanges();
            }
            return Json(new { result = "OK" });
        }





        public async Task<JsonResult> ajaxMansioneListAsync(String? q = null)
        {

            if (q == null) q = "";

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.Set<Mansione>()
                .Where(c => (c.Codice + " - " + c.Name ).ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.ID.ToString(),
                    text = x.Codice + " " + x.Name,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }





        /// <summary>
        /// Marsioni <c>ajaxIndex</c> 
        /// Lista di mansioni
        /// </summary>
        public async Task<JsonResult> ajaxIndexAsync_old(string type=null)
        {      
            


            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            try
            {
                var r = _context.C_Mansioni.Where(c => c.MultiTenantId == user.MultiTenantId);
                  
                /*Select 2 return */
                if (type == "select2") {
                    var s2 = r.Select(
                        x => new
                        {
                             id = x.ID,
                             text = x.Descrizione
                        }
                    );
                    return Json(
                        new { results = s2.ToList() }
                    );
                }
                
                return Json(
                    new { data = r.ToList() }
                );
            }catch (Exception) {
                return Json(
                    new {
                        data = new { }                            
                    }
                );                    
            }            
        }
    }
        
}