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
    public class ResourceRequestController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public ResourceRequestController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
        public async Task<IActionResult> EditAsync(Guid? id)
        {
            
            ResourceRequest r = new ResourceRequest();
            if(id != null)
            {
                r = _context
                    .C_ResourceRequest
                    .Include(c => c.dettagli)
                    .ThenInclude(c => c.macchina)
                    .Include(c => c.dettagli)
                    .ThenInclude(c => c.Specializzazione)
                    .Include(c => c.dettagli)
                    .ThenInclude(c => c.Cantiere)
                    .Include(c => c.dettagli)
                    .ThenInclude(c => c.mansione)
                    .Include(c => c.Turno)
                    .ThenInclude(c => c.Cantiere)
                    .Include(c => c.Turno)
                    .ThenInclude(c => c.TurniUser)
                    .Where(c => c.Id == id).FirstOrDefault();

            }
            if(r.dettagli == null)
            {
                r.dettagli = new List<ResourceRequestDetails>();
            }




            return View(r);
        }



        public async Task<JsonResult> getUsersDisponibiliSpecializzazione(Guid? Id)
        {

            var request = _context
                .C_ResourceRequestDetails
                .Include(c => c.ResourceRequest)
                .Where(c => c.Id == Id).FirstOrDefault();

            var res = _context
                        .C_Rel_SpecializzazioniUser
                        .Include(c => c.User)
                        .Where(c => c.SpecializzazioneId == request.SpecializzazioneId)
                        .Where(
                            c => _context.C_Rel_TurniUsers.Where(
                                    d => d.UserId == c.UserId && d.MansioneId == request.MansioneId && EF.Functions.DateDiffDay(d.WorkDate, request.ResourceRequest.WorkDate) == 0
                                ).Count() == 0
                        )
                        .Select(c => new
                        {
                            Id = c.User.Id,
                            firstname = c.User.FirstName,
                            lastname = c.User.LastName,
                            inizio = c.StartDate.Value.ToString("dd/MM/yyyy"),
                            fine = c.EndDate.Value.ToString("dd/MM/yyyy"),
                        })
                        .ToList();

            return Json(new
            {
                result = "OK",
                data = res,
            }
                );
        }

        public async Task<JsonResult> getUsersDisponibiliMansione(Guid? Id)
        {

            var request = _context
                .C_ResourceRequestDetails
                .Include(c => c.ResourceRequest)
                .Where(c => c.Id == Id).FirstOrDefault();

            var res = _context
                        .C_Rel_MansioniUser
                        .Include(c => c.User)
                        .Where(c => c.MansioneId == request.MansioneId)
                        .Where(c => (request.MacchinaId != null) ? c.MansioneMacchinaId == request.MacchinaId : true)
                        .Where(
                            c => _context.C_Rel_TurniUsers.Where(
                                    d => d.UserId == c.UserId && d.MansioneId == request.MansioneId && EF.Functions.DateDiffDay(d.WorkDate, request.ResourceRequest.WorkDate) == 0
                                ).Count() == 0
                        )
                        .Select(c => new
                        {
                            Id = c.User.Id,
                            firstname = c.User.FirstName,
                            lastname = c.User.LastName,
                            inizio = c.DataInizioAttivita.Value.ToString("dd/MM/yyyy"),
                            fine = c.DataFineAttivita.Value.ToString("dd/MM/yyyy"),
                        })
                        .ToList();

            return Json(new
            {
                result = "OK",
                data = res,
            }
                );
        }

        public async Task<JsonResult> assignUserMansione(Guid? IdRichiesta,string idUser)
        {

            var req = _context.C_ResourceRequestDetails
                .Include(c=> c.ResourceRequest)
                .Where(c => c.Id == IdRichiesta)
                .FirstOrDefault();


            _context.C_Rel_TurniUsers
                       .Add(
                            new Rel_TurnoUser
                            {
                                Id = Guid.NewGuid(),
                                MansioneId = req.MansioneId,
                                TurnoId = req.ResourceRequest.TurnoId,
                                WorkDate = req.ResourceRequest.WorkDate.Value,
                                UserId = idUser,
                            }
                        );

            _context.SaveChanges();


            return Json(new
            {
                result = "OK",
            }
                    );
        }

        public async Task<JsonResult> ajaxSommario(DateTime? selected)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var requests = _context.C_ResourceRequest.Where(c => EF.Functions.DateDiffDay(c.WorkDate, selected) == 0).ToList();


            int bozze = requests.Where(c => c.Stato == StatoRequest.bozza).Count();
            int inCarico = requests.Where(c => c.Stato == StatoRequest.inCarico).Count();
            int inviata = requests.Where(c => c.Stato == StatoRequest.inviata).Count();
            int completata = requests.Where(c => c.Stato == StatoRequest.completata).Count();

            return Json(new
            {
                result = "OK",
                data = new
                {
                    bozze,
                    inCarico,
                    inviata,
                    completata,
                    totale = requests.Count,
                }
            }
                    );
        }

         public async Task<JsonResult> assignUserSpecializzazione(Guid? IdRichiesta,string idUser)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var req = _context.C_ResourceRequestDetails
                .Include(c=> c.ResourceRequest)
                .ThenInclude(c => c.Turno)
                .Where(c => c.Id == IdRichiesta)
                .FirstOrDefault();


            var turno = req.ResourceRequest.Turno;


            var turniDopo = _context.C_Rel_TurniUsers
                .Include(c => c.Turno)
                .Where(c => c.UserId == idUser)
                .Where(c => c.TurnoId == turno.Id)
                .Where(c => EF.Functions.DateDiffDay(req.ResourceRequest.WorkDate, turno.dataFine) >= 0)
                .Select( c=> c.Turno)
                .ToList();

            foreach( var t in turniDopo)
            {
                _context.C_Turni.Remove(t);
            }



            
            for(int k = 0; k < EF.Functions.DateDiffDay(req.ResourceRequest.WorkDate, turno.dataFine); k++)
            {
                _context.C_Rel_TurniUsers
                    .Add(
                        new Rel_TurnoUser
                        {
                            Id = Guid.NewGuid(),
                            SpecializzazioneId = req.SpecializzazioneId,
                            WorkDate = req.ResourceRequest.WorkDate.Value.AddDays(k),
                            TurnoId = turno.Id,
                            UserId = idUser,
                            MultiTenantId = user.MultiTenantId
                        }
                    );
            }


            _context.SaveChanges();


            return Json(new
            {
                result = "OK",
            }
                    );
        }




        public async Task<JsonResult> getUtentiAssociatiMansione(Guid? Id)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var request = _context
                .C_ResourceRequestDetails
                .Include(c => c.ResourceRequest)
                .Where(c => c.Id == Id).FirstOrDefault();

            var res = _context.C_Rel_TurniUsers
                .Include(c => c.User)
                .Where(
                    c => 
                            c.MansioneId == request.MansioneId
                            && 
                            EF.Functions.DateDiffDay(request.ResourceRequest.WorkDate, c.WorkDate) == 0
                            &&
                            c.TurnoId == request.ResourceRequest.TurnoId
                )
                .Select(c => new
                {
                    Id = c.User.Id,
                    firstname = c.User.FirstName,
                    lastname = c.User.LastName,
                })
                .ToList();

            return Json(new
            {
                result = "OK",
                data = res,
            }
                );

        }


        public async Task<JsonResult> getUtentiAssociatiSpecializzazione(Guid? Id)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var request = _context
                .C_ResourceRequestDetails
                .Include(c => c.ResourceRequest)
                .Where(c => c.Id == Id).FirstOrDefault();

            var res = _context.C_Rel_TurniUsers
                .Include(c => c.User)
                .Where(
                    c =>
                            c.SpecializzazioneId == request.SpecializzazioneId
                            &&
                            EF.Functions.DateDiffDay(request.ResourceRequest.WorkDate, c.WorkDate) == 0
                            &&
                            c.TurnoId == request.ResourceRequest.TurnoId
                )
                .Select(c => new
                {
                    Id = c.User.Id,
                    firstname = c.User.FirstName,
                    lastname = c.User.LastName,
                })
                .ToList();

            return Json(new
            {
                result = "OK",
                data = res,
            }
                );

        }



        [HttpPost]
        public async Task<JsonResult> InsertRequestBozza(ResourceRequest request)
        {
            if (request.Id != null)
            {
                var r = _context.C_ResourceRequest
                            .Include(c => c.dettagli)
                            .Where(c => c.Id == request.Id)
                            .FirstOrDefault();
                

                var esistenti = (request.dettagli != null ) ? request.dettagli.Where(c => c.Id != null).ToList() : new List<ResourceRequestDetails>();
                var nonesistenti = (request.dettagli != null) ? request.dettagli.Where(c => c.Id == null).ToList(): new List<ResourceRequestDetails>();

                foreach (var dettaglio in r.dettagli)
                {
                    if(esistenti.Where(c => c.Id == dettaglio.Id).Count() == 0)
                    {
                        _context.C_ResourceRequestDetails.Remove(dettaglio);
                    }

                }

                foreach (var dettaglio in nonesistenti)
                {
                    dettaglio.Id = Guid.NewGuid();
                    dettaglio.ResourceRequestId = r.Id;
                    _context.C_ResourceRequestDetails.Add(dettaglio);
                }

                _context.SaveChanges();

                r.WorkDate = request.WorkDate;
                r.Descr = request.Descr;
                r.TurnoId = request.TurnoId;
                if (request.Stato != null)
                {
                    r.Stato = request.Stato;
                }

                _context.SaveChanges();


                return Json(new
                {
                    result = "OK",
                    data = request.Id.ToString(),
                }
                );
            }



            request.Id = Guid.NewGuid();

            if(request.dettagli == null)
            {
                request.dettagli = new List<ResourceRequestDetails>();
            }

            foreach (var dettaglio in request.dettagli)
            {
                dettaglio.Id = Guid.NewGuid();
                _context.C_ResourceRequestDetails.Add(dettaglio);
            }
            if(request.Stato == null)
            {
                request.Stato = StatoRequest.bozza;
            }
           
            _context.C_ResourceRequest.Add(request);


            _context.SaveChanges();

            return Json(new
            {
                result = "OK",
                data = request.Id.ToString(),
            }
                );
        }

        [HttpGet]
        public IActionResult ajaxSetStato(Guid? Id, StatoRequest? stato)
        {

            var request = _context.C_ResourceRequest.Where(c => c.Id == Id).FirstOrDefault();

            if(request != null)
            {
                request.Stato = stato;
            }
            _context.SaveChanges();


            return Json(new
            {
                result = "OK",
            }
               );
        }



        [HttpGet]
        public IActionResult ajaxIndex(StatoRequest? stato, DateTime? selected)
        {

            var result = _context.C_ResourceRequest
                        .Include(c => c.dettagli)
                        .Where(c => c.Stato == stato)
                        .Where(c => EF.Functions.DateDiffDay(c.WorkDate, selected) == 0)
                        .Select(d => new { 
                            Id = d.Id,
                            nome = d.Descr,
                            nMansioni = d.dettagli.Where(c => c.MansioneId != null).Count(),
                            nSpecializzazioni = d.dettagli.Where(c => c.SpecializzazioneId != null).Count(),
                        })
                        .ToList();


            return Json(new 
            {
                data =  result,
            }
                );
        }


        public async Task<JsonResult> ajaxTurniListAsync(Guid? cantiereId,String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_Turni
                .Where(x => x.CantiereId == cantiereId)
                .Where(u => (u.Name).ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = "" + x.Name,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }

        public async Task<JsonResult> ajaxCantiereListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_Cantieri
                .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(u => (u.Name).ToLower().Contains(q.ToLower()) || (u.Codice).ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = x.Codice + " - " + x.Name,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }
        public async Task<JsonResult> ajaxSpecializzazioniListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_Specializzazioni
                .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(u => (u.Name ).ToLower().Contains(q.ToLower()) || (u.Descrizione).ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.ID.ToString(),
                    text = ""+x.Name,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }

        public async Task<JsonResult> ajaxMansioniListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_Mansioni
                .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(u => (u.Name ).ToLower().Contains(q.ToLower()) || (u.Descrizione).ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.ID.ToString(),
                    text = ""+x.Name,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }
        public async Task<JsonResult> ajaxMacchineListAsync(Guid? idMansione, String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_MansioneMacchina
                .Where(u => (u.Name ).ToLower().Contains(q.ToLower()))
                .Where(u => u.MansioneId == idMansione)
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = ""+x.Name,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }

    }
}