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
    public class LeasingController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public LeasingController(ApplicationDBContext context,
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
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaLeasing, "Visualizzazione Leasing");
            return View();
        }

        public async Task<JsonResult> ajaxLeasingListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.Set<VettoreLeasing>()
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

        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Create(LeasingViewModel model)
        {

            AppUser user = _context.Users.Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();



            VettoreLeasing leasing = new VettoreLeasing
            {
                Id = Guid.NewGuid(),
                EndDate = model.leasing.EndDate,
                StartDate = model.leasing.StartDate,
                ProtocolNumber = model.leasing.ProtocolNumber,
                MultiTenantId = user.MultiTenantId,
                Company_id = user.IDCompany,
            };

            _context.C_VettoreLeasing.Add(leasing);
            _context.SaveChanges();

            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? LeasingId = null)
        {
            LeasingViewModel model = new LeasingViewModel();
            var noleggio = _context.Set<VettoreLeasing>()
                                .Where(c => c.Id.ToString().Equals(LeasingId))
                                .FirstOrDefault();

            model.leasing = noleggio;

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> EditAsync(LeasingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var noleggio = _context.C_VettoreLeasing.Where(c => c.Id == model.leasing.Id).FirstOrDefault();

                noleggio.ProtocolNumber = model.leasing.ProtocolNumber;
                noleggio.EndDate = model.leasing.EndDate;
                noleggio.StartDate = model.leasing.StartDate;

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
                                            .Where(c => c.LeasingId == Id)
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

            var r = _context.Set<VettoreLeasing>()
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .ToList();

            return Json(
                new { data = r }
            );
        }


        public async Task<JsonResult> ajaxVettoriSelect2Async(Guid? id = null,string search = null)
        {

            var leasing = _context.C_VettoreLeasing.Where(c => c.Id == id).FirstOrDefault();

            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.ANA_VETTORI
                .Include(c => c.VettoreAssegnazione)
                .ThenInclude( c=> c.Leasing)
                .Include(c => c.VettoreAssegnazione)
                .ThenInclude( c => c.Noleggio)
                .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                .Where(item => (search != null) ? item.Targa.ToUpper().Contains(search.ToUpper()) : true)
                .Where( c=> 
                
                    (c.VettoreAssegnazione != null && c.VettoreAssegnazione.Count() > 0) ?

                    (
                    
                    c.VettoreAssegnazione.Where(c =>
                        (c.Noleggio != null) ? c.Noleggio.EndDate <= leasing.EndDate : false
                        ||
                        (c.Leasing != null) ? c.Leasing.EndDate <= leasing.EndDate : false
                        ).Count() == 0 
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

        


        public async Task<JsonResult> ajaxAddVettoreAsync(int IdVettore,Guid? IdLeasing = null)
        {

            var leasing = _context.C_VettoreLeasing.Where(c => c.Id == IdLeasing).FirstOrDefault();



            int found = _context.C_VettoreAssegnazione
                .Include(c => c.Leasing)
                .Include(c => c.Noleggio)
                .Where(c => c.CodiceVettore == IdVettore &&
               (
                   (c.Leasing != null) ? c.Leasing.EndDate <= leasing.EndDate : false
                   ||
                   (c.Noleggio != null) ? c.Noleggio.EndDate <= leasing.EndDate : false
               ))
                .Count();



            if(found == 0)
            {
                VettoreAssegnazione vettoreAssegnazione = new VettoreAssegnazione();
                vettoreAssegnazione.CodiceVettore = IdVettore;
                vettoreAssegnazione.NoleggioId = null;
                vettoreAssegnazione.LeasingId = IdLeasing;
                _context.C_VettoreAssegnazione
                    .Add(vettoreAssegnazione);

                _context.SaveChanges();

                return Json(new { result = "OK" });
            }



            



            return Json(new { 
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