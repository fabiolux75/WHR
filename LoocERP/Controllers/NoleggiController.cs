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
    public class NoleggiController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public NoleggiController(ApplicationDBContext context,
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
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaNoleggi, "Visualizzazione Noleggi");
            return View();
        }


        public async Task<IActionResult> ManageOptions()
        {            
            return View();
        }


        public async Task<JsonResult> ajaxOptionsAsync()
        {

            DateTime date = DateTime.Now;

            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            var r = _context.C_NoleggiOptions.Where(c => c.MultiTenantId == user.MultiTenantId).ToList();


            return Json(
                new { data = r }
            );
        }

        [HttpGet]
        public async Task<JsonResult> ajaxDeleteOptionAsync(Guid? id)
        {
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();


            var noleggio = _context.C_NoleggiOptions.Where(c => c.Id == id).FirstOrDefault();


            _context.C_NoleggiOptions.Remove(noleggio);

            _context.SaveChanges();

            return Json(
                new { }
            );
        }
        
        public async Task<JsonResult> ajaxAddOptionAsync(NoleggiOption noleggiOption)
        {
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            noleggiOption.Id = Guid.NewGuid();
            noleggiOption.MultiTenantId = user.MultiTenantId;

            _context.C_NoleggiOptions.Add(noleggiOption);

            _context.SaveChanges();

            return Json(
                new { }
            );
        }



        public IActionResult Create()
        {
            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();
            var r = _context.C_NoleggiOptions.Where(c => c.MultiTenantId == user.MultiTenantId).ToList();

            NoleggioViewModel model = new NoleggioViewModel();
            model.noleggiOptions = r;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(NoleggioViewModel model,List<Guid> options)
        {

            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();



            Noleggio noleggio = new Noleggio
            {
                Id = Guid.NewGuid(),
                EndDate = model.noleggio.EndDate,
                StartDate = model.noleggio.StartDate,
                ProtocolNumber = model.noleggio.ProtocolNumber,
                MultiTenantId = user.MultiTenantId,
                Company_id = model.noleggio.Company_id,
            };
            foreach(var option in options)
            {
                _context.C_NoleggioNoleggiOptions.Add(new NoleggioNoleggiOptions
                {
                    Id = Guid.NewGuid(),
                    NoleggioId = noleggio.Id,
                    NoleggioOptionId = option,
                });
            }
            _context.C_Noleggi.Add(noleggio);
            _context.SaveChanges();

            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? NoleggioId = null)
        {
            NoleggioViewModel model = new NoleggioViewModel();
            var noleggio = _context.Set<Noleggio>()
                                .Where(c => c.Id.ToString().Equals(NoleggioId))
                                .FirstOrDefault();

            var r = _context.C_NoleggioNoleggiOptions.Include(c=> c.NoleggiOption).Where(c => c.NoleggioId == new Guid(NoleggioId)).Select(c => c.NoleggiOption).ToList();

            model.noleggio = noleggio;
            model.noleggiOptions = r;


            model.VettoriAssociati = _context.Set<VettoreAssegnazione>()
                                            .Include(c => c.Vettore)
                                            .Where(c => c.NoleggioId == noleggio.Id)
                                            .ToList();


            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ajaxGetOptionsAsync(Guid? Id = null)
        {
            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();



            var nMine =  _context.C_NoleggioNoleggiOptions.Include(c=> c.NoleggiOption).Where(c => c.NoleggioId == Id).Select(c => c.NoleggiOption).ToList();
            var r = _context.C_NoleggiOptions.Where(c => c.MultiTenantId == user.MultiTenantId)
                        .Select(
                            c => new { 
                                Id = c.Id,
                                Name = c.Name,
                                Selected = nMine.Contains(c),
                            });

            return Json(
                new { data = r  }
            );
        }

        [HttpPost]
        public async Task<IActionResult> ajaxUpdateOptionsAsync(List<Guid> options,Guid? Id = null)
        {

           var opp=  _context.C_NoleggioNoleggiOptions.Where(c => c.NoleggioId == Id).ToList();


            foreach(var v in opp)
            {
                _context.C_NoleggioNoleggiOptions.Remove(v);
            }
            _context.SaveChanges();

            foreach(var o in options)
            {
                _context.C_NoleggioNoleggiOptions.Add(
                    new NoleggioNoleggiOptions
                    {
                        Id= Guid.NewGuid(),
                        NoleggioId = Id,
                        NoleggioOptionId = o,
                    }
                    );
            }

            _context.SaveChanges();
            return Json(
                new { }
            );
        }


        public async Task<JsonResult> ajaxGetAziendeFornitori(string? q = "")
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);


            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_ANA_Companies
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(c => c.isSupplier == true)
                .Where(item => (q != null) ? item.RagioneSociale.ToUpper().Contains(q.ToUpper()) : true)
                .Select(x => new Select2ViewModel
                {
                    id = x.ID.ToString(),
                    text = x.RagioneSociale,
                })
                .OrderBy(x => x.text)
                .ToList();

            return Json(
                new
                {
                    results = r
                }
           );
        }




        [HttpPost]
        public async Task<IActionResult> EditAsync(NoleggioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var noleggio = _context.C_Noleggi.Where(c => c.Id == model.noleggio.Id).FirstOrDefault();

                noleggio.ProtocolNumber = model.noleggio.ProtocolNumber;
                noleggio.EndDate = model.noleggio.EndDate;
                noleggio.StartDate = model.noleggio.StartDate;

                _context.SaveChanges();
            }

            return View(model);
        }




        public async Task<JsonResult> ajaxVettoriAssegnatiAsync(Guid? Id = null)
        {

            DateTime date = DateTime.Now;

            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            var r = _context.Set<VettoreAssegnazione>()
                                            .Include(c => c.Vettore)
                                            .ThenInclude(c => c.ModelloVettore)
                                            .ThenInclude(c => c.modelloInfo)
                                            .Include(c => c.Vettore)
                                            .ThenInclude(c => c.ModelloVettore)
                                            .ThenInclude(c => c.Marca)
                                            .Include(c => c.Vettore)
                                            .ThenInclude(c => c.ModelloVettore)
                                            .ThenInclude(c => c.modelloInfo)
                                            .Where(c => c.NoleggioId == Id)
                                            .Select(c => new { 
                                                Id = c.Id,
                                                img = (c.Vettore.ModelloVettore != null && c.Vettore.ModelloVettore.modelloInfo != null) ? c.Vettore.ModelloVettore.modelloInfo.Img : "",
                                                modello = (c.Vettore.ModelloVettore != null && c.Vettore.ModelloVettore.modelloInfo != null) ? Regex.Match(c.Vettore.ModelloVettore.modelloInfo.Descr, "(?<=<IT>)(.*)(?=</IT>)").Value : "",
                                                marca = (c.Vettore.ModelloVettore != null && c.Vettore.ModelloVettore.Marca != null) ? c.Vettore.ModelloVettore.Marca.Descr : "",
                                                Targa = c.Vettore.Targa,
                                            })
                                            .ToList();


            return Json(
                new { data = r }
            );
        }


        public async Task<JsonResult> ajaxIndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<Noleggio>()
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .ToList();

            return Json(
                new { data = r }
            );
        }


        public async Task<JsonResult> ajaxNoleggiListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.Set<Noleggio>()
                .Where(x => x.ProtocolNumber.ToString().Contains(q))
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = "" + x.ProtocolNumber.ToString(),
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }



        public async Task<JsonResult> ajaxVettoriSelect2Async(Guid? id = null, string search = null)
        {

            var leasing = _context.C_VettoreAssegnazione.Where(c => c.NoleggioId == id).FirstOrDefault();

            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.ANA_VETTORI
                .Include(c => c.VettoreAssegnazione)
                .ThenInclude(c => c.Noleggio)
                .Include(c => c.VettoreAssegnazione)
                .ThenInclude(c => c.Noleggio)
                .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                .Where(item => (search != null) ? item.Targa.ToUpper().Contains(search.ToUpper()) : true)
                
                .Where(c =>

                   (c.VettoreAssegnazione != null && c.VettoreAssegnazione.Count() > 0) ?

                   (

                   false

                   )
                   :
                   true
                )
                
                .Select(x => new Select2ViewModel
                {
                    id = x.Codice.ToString(),
                    text = x.Targa.Trim(),
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }




        public async Task<JsonResult> ajaxAddVettoreAsync(int IdVettore, Guid? IdNoleggio = null)
        {

            var noleggio = _context.C_Noleggi.Where(c => c.Id == IdNoleggio).FirstOrDefault();



            int found = _context.C_VettoreAssegnazione
                .Include(c => c.Leasing)
                .Include(c => c.Noleggio)
                .Where(c => c.CodiceVettore == IdVettore &&
               (
                   (c.Leasing != null) ? c.Leasing.EndDate <= noleggio.EndDate : false
                   ||
                   (c.Noleggio != null) ? c.Noleggio.EndDate <= noleggio.EndDate : false
               ))
                .Count();



            if (found == 0)
            {
                VettoreAssegnazione vettoreAssegnazione = new VettoreAssegnazione();
                vettoreAssegnazione.CodiceVettore = IdVettore;
                vettoreAssegnazione.NoleggioId = IdNoleggio;
                vettoreAssegnazione.LeasingId = null;
                _context.C_VettoreAssegnazione
                    .Add(vettoreAssegnazione);

                _context.SaveChanges();

                return Json(new { result = "OK" });
            }







            return Json(new
            {
                result = "KO",
                reason = "Vettore già in un leasing o noleggio",
            });
        }


        public async Task<JsonResult> ajaxRemoveVettoreAsync(string idAssegnazione = null)
        {


            var v = _context.C_VettoreAssegnazione.Where(c => c.Id == new Guid(idAssegnazione)).FirstOrDefault();
            if(v != null)
            {
                _context.C_VettoreAssegnazione.Remove(v);
                _context.SaveChanges();
            }
            return Json(new { result = "OK" });
        }
    }
}