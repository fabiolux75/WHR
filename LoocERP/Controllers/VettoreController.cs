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
    public class VettoreController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public VettoreController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("Vettore.Show")]
        public async Task<IActionResult> Index(VettoreTableViewModel model)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaVettori, "Visuallizzazione Vettori");



            ViewBag.settore = model.settore;
            ViewBag.sottosettore = model.sottoSettore;
            ViewBag.targa = model.targa;
               
            
            if(model.settore == "MA.STR")
            {
                return View("IndexStradali");
            }
            if(model.settore == "INDUST")
            {
                return View("IndexIndustriali");
            }
            if(model.settore == "ATTREZ")
            {
                return View("IndexAttrezzature");
            }
            


            return View();
        }

        public async Task<JsonResult> ajaxGetVettoriSintexAsync(VettoreTableViewModel model)
        {

            DateTime date = DateTime.Now;
            if (model.startTime != null)
            {
                date = DateTime.Parse(model.startTime);
            }

            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();


            int vettoriTotal = _context.ANA_VETTORI
                                .Where(c => (model.settore != null) ? c.CodSettore == model.settore : true)
                                .Where(c => (model.sottoSettore != null) ? c.CodSottoSett == model.sottoSettore : true)
                                .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                                .Count();


            //c.GOF_Scheda.stato
            int inManutenzione = _context.ANA_VETTORI
                                    .Include(c => c.GOF_Scheda)
                                    
                                    .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                                    .Where(c => (model.settore != null) ? c.CodSettore == model.settore : true)
                                    .Where(c => (model.sottoSettore != null) ? c.CodSottoSett == model.sottoSettore : true)
                                    .Where(c => c.GOF_Scheda.FirstOrDefault().stato == 1)
                                    .Count();

            int inParcheggio = _context.C_VettoreParcheggio
                                    .Include(c => c.Vettore)
                                    .Where(c => c.Vettore.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                                    .Where(c => (model.settore != null) ? c.Vettore.CodSettore == model.settore : true)
                                    .Where(c => (model.sottoSettore != null) ? c.Vettore.CodSottoSett == model.sottoSettore : true)
                                    .Where(c => c.WorkDate >= date)
                                    .Count();

            int vettoriAssegnati = _context.ANA_VETTORI
                                    .Include(c => c.VettoreUsers)
                                    .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                                    .Where(c => (model.settore != null) ? c.CodSettore == model.settore : true)
                                    .Where(c => (model.sottoSettore != null) ? c.CodSottoSett == model.sottoSettore : true)
                                    .Join(_context.C_VettoreUser, cu => cu.Codice, p => p.VettoreId, (cu, p) => new { cu, p })
                                    .Where(c => c.p.StartDate <= date && c.p.EndDate >= date)
                                    .Count();

            /*
            int vettoriAssegnati = _context.C_VettoreCantiere
                                    .Include(c => c.Vettore)
                                    .Where(c => c.Vettore.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                                    .Where(vu => EF.Functions.DateDiffDay(vu.WorkDate, date) == 1 || EF.Functions.DateDiffDay(vu.WorkDate, date) == -1)//(vu.WorkDate - date).Value.TotalDays == 0)
                                    .ToList()
                                    .GroupBy(c => c.VettoreId)
                                    .Count();

            */


            return Json(new { data = new { 
                tot = vettoriTotal,
                inManutenzione = inManutenzione,
                inParcheggio = inParcheggio,
                assegnati = vettoriAssegnati
            } });
        }





        /// <summary>
        /// <c>ajax</c> 
        /// Metodo per il ritorno degli utenti con autoassociate
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        // [Authorize("Vettore.Show")]
        public async Task<JsonResult> ajaxIndexAsync(VettoreTableViewModel model)
        {

            DateTime date = DateTime.Now;
            if (model.startTime != null)
            {
                date = DateTime.Parse(model.startTime);
            }

            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            var r = _context.ANA_VETTORI
                    .AsNoTracking()
                    .Include(c => c.ModelloVettore)
                    .ThenInclude(c => c.modelloInfo)
                    .Include( c => c.Noleggio)
                    .Include(c => c.ModelloVettore)
                    .ThenInclude(c => c.Marca)
                    //.Include(c => c.MultiTenant)
                    .Include(c => c.VettoreAssegnazione)
                    .ThenInclude(c => c.Noleggio)
                    .Include(c => c.VettoreAssegnazione)
                    .ThenInclude(c => c.Leasing)
                    .Include(c => c.VettoreSettore)
                    .Include(c => c.CodiceSottoSettore)
                    .Include(c => c.ModelloVettore)
                    .ThenInclude(c => c.modelloInfo)
                    .Include(c => c.GOF_Scheda)
                    .Include(c => c.MedAnaVettoriInfo)
                    .Include(c => c.VettoreParcheggi)
                    .ThenInclude(c => c.Parcheggio)
                    .Include(c => c.VettoreCantieri)
                    .ThenInclude(c => c.cantiere)
                    .Include(c => c.VettoreUsers)
                    .ThenInclude(u => u.User)
                    .ThenInclude(u => u.TimeSheetDailyReports)
                    .ThenInclude(u => u.Turno)
                    .ThenInclude(u => u.Cantiere)
                    .Include( c => c.company)
                    .Include(c => c.VettoreUsers)
                    .ThenInclude( c => c.User)
                    .ThenInclude(c => c.Malattie)
                    .Where(c =>  c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                    .Where(c => (model.settore!= null) ? c.CodSettore == model.settore : true)
                    .Where(c => (model.sottoSettore != null) ? c.CodSottoSett == model.sottoSettore : true)
                    .Where(c => c.MedAnaVettoriInfo.Cancellato == 0)
                    .Where(c => c.ModelloVettore.modelloInfo.Cancellato == 0)
                    .Select(c => new
                    {
                        Codice = c.Codice,
                        Targa = c.Targa.Trim(),
                        posti = c.Nposti,
                        CodModello = c.CodModello,
                        Property = (c.Noleggio != null && c.Noleggio.company != null) ? c.Noleggio.company.RagioneSociale : "",
                        Descr = c.Descr,
                        CodCliente = c.CodCliente,
                        CodSettoreDesc = (c.VettoreSettore != null ? c.VettoreSettore.Descr : ""),
                        CodSottoSettoreDesc = (c.CodiceSottoSettore != null ? c.CodiceSottoSettore.Descr : ""),
                        CodSottoSett = c.CodSottoSett,
                        CodSettore = c.CodSettore,
                        img = (c.ModelloVettore != null && c.ModelloVettore.modelloInfo != null) ? c.ModelloVettore.modelloInfo.Img : "",
                        modello = (c.ModelloVettore != null && c.ModelloVettore.modelloInfo != null) ? c.ModelloVettore.modelloInfo.Descr : "",
                        marca = (c.ModelloVettore != null && c.ModelloVettore.Marca != null) ? c.ModelloVettore.Marca.Descr : "",
                        Company = (c.company != null) ? c.company : null,
                        Users = c.VettoreUsers
                                .Where(vu => vu.StartDate <= date && vu.EndDate >= date)
                                .Select(vu => new
                                {
                                    Id = vu.User.Id,
                                    firstname = vu.User.FirstName,
                                    lastname = vu.User.LastName,
                                    fullname = vu.User.FirstName + " " + vu.User.LastName,
                                    endDate = vu.EndDate,
                                    cantiere = (vu.User.TimeSheetDailyReports.First().Turno.Cantiere.Name != null) ? vu.User.TimeSheetDailyReports.First().Turno.Cantiere.Name : "",
                                    codCantiere = (vu.User.TimeSheetDailyReports.First().Turno.Cantiere.Codice != null) ? vu.User.TimeSheetDailyReports.First().Turno.Cantiere.Codice : "",
                                    MalattiaUser = vu.User.Malattie.Where(vu => EF.Functions.DateDiffDay(vu.ValidTo, date) < 0).Count() > 0,
                                }),
                        Cantieri = c.VettoreCantieri
                                    .Where(vu => EF.Functions.DateDiffDay(vu.WorkDate,date) == 1 || EF.Functions.DateDiffDay(vu.WorkDate, date) == -1 )//(vu.WorkDate - date).Value.TotalDays == 0)
                                    .OrderBy( m => c.VettoreCantieri.Max(c => c.WorkDate))
                                    //.Where(m => c.VettoreCantieri.Max(c => c.WorkDate) >= date && c.VettoreCantieri.Min(c => c.WorkDate) <= date)
                                    .Take(1)
                                    .Select(m => new {
                                        Id = m.cantiere.Id,
                                        codice = m.cantiere.Codice,
                                        Name = m.cantiere.Name,
                                        endDate = c.VettoreCantieri.Max(c => c.WorkDate)
                                    }),
                        parcheggi = c.VettoreParcheggi
                            //.Where(c => c.WorkDate >= date)
                            .Where(vu => vu.WorkDate <= date && vu.EndDate >= date)
                            .Select(
                            c => new {
                                Id = c.Parcheggio.Id,
                                Name = c.Parcheggio.Name,
                                Date = c.WorkDate,
                            }
                            ),
                        scheda = c.GOF_Scheda.Select( c => new
                        {
                            Codice = c.Codice,
                            stato = c.stato,

                        }).FirstOrDefault(),
                        vettoreAssegnazione = c.VettoreAssegnazione
                                            .Select( c=> new { 
                                                id = c.Id,
                                                noleggio = (c.Noleggio != null) ? c.Noleggio.ProtocolNumber.ToString() : "",
                                                leasing = (c.Leasing != null) ? c.Leasing.ProtocolNumber.ToString() : "",
                                            })
                                            .FirstOrDefault()

                    })
                    .ToList();


            return Json(
                new { data = r }
            );
        }


        public async Task<JsonResult> ajaxVettoriUserHistory(int codiceVettore)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_VettoreUser.Include(c => c.User)
                .Where(c => c.User.MultiTenantId == user.MultiTenantId)
                .Where(c => c.VettoreId == codiceVettore)
                
                .Select(
                    c => new
                    {
                        Nome = c.User.FirstName + " " + c.User.LastName,
                        Inizio = c.StartDate,
                        Fine = c.EndDate,

                    }
                )
                .OrderBy(c => c.Inizio)
                .ToList();


            return Json(
                new { data = r }
            );
        }


        public async Task<JsonResult> ajaxAssociaCompany(int codiceVettore,Guid? IdCompany)
        {
            return Json(
                new { 
                    result = "OK"
                }
            );
        }



        public async Task<JsonResult> ajaxVettoriCantiereHistory(int codiceVettore)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_VettoreCantiere.Include(c => c.cantiere)
                .Where(c => c.cantiere.MultiTenantId == user.MultiTenantId)
                .Where(c => c.VettoreId == codiceVettore)
                .GroupBy(d => new {
                    d.cantiere.Id,
                    Name = d.cantiere.Description
                })
                .Select(
                    e => new
                    {
                        e.Key.Id,
                        e.Key.Name,
                        from = e.Min(c => c.WorkDate),
                        to =  e.Max(c => c.WorkDate),
                    }
                )
                .ToList();


            return Json(
                new { data = r }
            );
        }

        public async Task<JsonResult> ajaxVettoriParkingHistory(int codiceVettore)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_VettoreParcheggio
                .Include(c => c.Parcheggio)
                .Include(c => c.Vettore)
                .Where( c => true /*&& c.Vettore != null && c.Vettore.CodCliente == user.MultiTenant.LoocId*/)
                .Where(c => c.VettoreId == codiceVettore)
                .Select(
                    c => new
                    {
                        name = (c.Parcheggio != null) ? c.Parcheggio.Name : "",
                        startDate = c.WorkDate,
                    }
                ).ToList();


            return Json(
                new { data = r }
            );
        }


        public async Task<JsonResult> ajaxUserVettoreListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.Set<AppUser>()
                .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where( u => (u.FirstName + " " + u.LastName).ToLower().Contains(q.ToLower()))
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = x.FirstName + " " + x.LastName,
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }
        //[Authorize("VettoreUser.Show")]
        public async Task<IActionResult> VettoreUser(int isParking=0)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.Logout, "Visualizzazione Personale");

            ViewBag.isParking = isParking;
            return View();
        }



        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Lista turni di lavoro
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        public async Task<JsonResult> ajaxVettoreUserAsync(string q = null)
        {
            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r =  _context.ANA_VETTORI
                //.Include (x => x.CodiceSettore)
                //.Include (x => x.CodiceSottoSettore)
                .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))            
                .Where(item => (q != null) ? item.Targa.ToUpper().Contains(q.ToUpper()) : true)    
                .Select(x => new Select2ViewModel
                {
                    id = x.Codice.ToString(),
                    text = x.Targa.Trim() ,//+ " (" + x.Targa.Trim()+")",
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }



        public async Task<JsonResult> ajaxAddCompanyAsync(int codVettore,Guid? IdCompany)
        {

            var vettore = _context.ANA_VETTORI.Where(c => c.Codice == codVettore).FirstOrDefault();

            if(vettore != null)
            {
                vettore.IdCompany = IdCompany;

                _context.SaveChanges();

                 return Json(
                    new { 
                        results = "OK" ,
                    }
                );
            }



            return Json(
                new { 
                    results = "KO" ,
                    data = "Vettore non trovato"
                }
            );
        }
        public async Task<JsonResult> ajaxRemoveCompanyAsync(int codVettore)
        {

            var vettore = _context.ANA_VETTORI.Where(c => c.Codice == codVettore).FirstOrDefault();

            if(vettore != null)
            {
                vettore.IdCompany = null;

                _context.SaveChanges();

                 return Json(
                    new { 
                        results = "OK" ,
                    }
                );
            }



            return Json(
                new { 
                    results = "KO" ,
                    data = "Vettore non trovato"
                }
            );
        }



        /// <summary>
        /// Companies <c>ajaxIndex</c> 
        /// Torna lista utenti e vettore
        /// </summary>
        /// <param>level</param> Filtra le aziende per livello
        /// <param>ParentID</param> Filtra le aziende per ParentId        
        /// <param>type</param> 0 = tutti, 1 = Employee
        public async Task<JsonResult> ajaxUserVettoreAsync(String? startTime=null,int isParking=-1)
        {
            DateTime startDate = DateTime.Now;
            if(startTime != null)
            {
                startDate = DateTime.Parse(startTime);
            }
            

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
           

            Func<AppUser, bool> whereisParking = (x => true); //default sempre vera
            if (isParking == 0 || isParking == 1) whereisParking = (x => x.isParking == isParking );


            /*
            var draw = Request.Query["draw"].FirstOrDefault();
            // Skiping number of Rows count
            var start = Request.Query["start"].FirstOrDefault();
            // Paging Length 10,20
            var length = Request.Query["length"].FirstOrDefault();
            // Sort Column Name
            var sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)
            var sortColumnDirection = Request.Query["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)
            var searchValue = Request.Query["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            */


            try
            {




                var usersList = _context.Users
                                    .Include(u => u.TimeSheetDailyReports)
                                    .ThenInclude(u => u.Turno)
                                    .ThenInclude(u => u.Cantiere)
                                    .Include(u => u.vettori)
                                    .ThenInclude( v => v.Vettore )
                                    .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                                    .Where(x => (isParking == 0 || isParking == 1) ?  x.isParking == isParking : true)
                                    .Select(x => new
                                    {
                                        Id = x.Id,
                                        FirstName = x.FirstName,
                                        LastName = x.LastName,
                                        Fullname = x.FirstName + " " + x.LastName,
                                        PhoneNumber = x.PhoneNumber,
                                        InternalCode = x.InternalCode,
                                        isParking = x.isParking == 1 ,
                                        DataModifica = x.DataModifica,
                                        Vettori = x.vettori
                                            .Where(c => DateTime.Compare((DateTime)c.StartDate, startDate) <= 0)
                                            .Where(c => DateTime.Compare((DateTime)c.EndDate, startDate) >= 0)
                                            .Select( v => new {
                                                VettoreId = v.VettoreId,
                                                Targa = v.Vettore.Targa,
                                                EndDate = v.EndDate, 
                                            }),
                                        cantiere = x.TimeSheetDailyReports
                                                .Where(r => EF.Functions.DateDiffDay(r.WorkDate, startDate) == 1 || EF.Functions.DateDiffDay(r.WorkDate, startDate) == -1)
                                                .Select( t => new
                                                {
                                                    nomeCantiere = (t.Turno.Cantiere != null) ? t.Turno.Cantiere.Name : "",
                                                    nomeTurno = t.Turno.Name,
                                                    
                                                }).FirstOrDefault(),
                                    })
                                    .OrderByDescending(x=>x.isParking);




            
                /*
                //da sistemare sort
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {

                }


                if (!string.IsNullOrEmpty(searchValue))
                {
                    usersList = usersList.Where(m => m.FirstName == searchValue || m.LastName == searchValue);
                }
                */


                return Json(
                    new
                    {
                        data = usersList.ToList(),
                        //recordsTotal = recordsTotal,
                        //recordsFiltered = recordsTotal,
                    }
                );
                
            }
            catch(Exception e)
            {
                return Json(
                    new
                    {
                       ko = e.Message
                    }
               );
            }
            
        }

        [HttpPost]
        public async Task<JsonResult> ajaxVettoreUserAssignAsync(String UserId, String CodiceVettore, DateTime StartDate, DateTime EndDate )
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            try{


                var parks = _context.C_VettoreParcheggio.Where(c => EF.Functions.DateDiffDay(c.EndDate, EndDate) < 0 && EF.Functions.DateDiffDay(c.WorkDate, EndDate) > 0 && c.VettoreId == Int32.Parse(CodiceVettore)).ToList();



                parks.ForEach(c =>
                {
                    c.EndDate = StartDate.AddDays(-1);
                });

                _context.SaveChanges();

                //_context.C_VettoreParcheggio.RemoveRange(_context.C_VettoreParcheggio.Where(c => c.WorkDate > StartDate && c.VettoreId == Int32.Parse(CodiceVettore)));

                



                var vettori = _context.C_VettoreUser.Where(
                    x => x.EndDate > StartDate && x.VettoreId == Int32.Parse(CodiceVettore)
                    ).ToList();
                vettori.ForEach(
                    v => v.EndDate = StartDate
                    );
                _context.SaveChanges();
                

                VettoreUser model = new VettoreUser();     
                model.Id = new Guid();
                model.UserId = UserId;
                model.VettoreId = Int32.Parse(CodiceVettore);
                model.StartDate = StartDate;
                model.EndDate = EndDate;
            
                _context.Set<VettoreUser>().Add(model);
                _context.SaveChanges();                               
            }catch(Exception e ){
                return Json(new { 
                    errore = e.Message
                }) ; 
            }                            
            return Json("ok");            
        }


        /* AREA CANTIERE */

        [HttpPost]
        public async Task<JsonResult> ajaxVettoreCantiereAssignAsync(String CantiereId, String CodiceVettore, DateTime WorkDate)
        {
            try
            {
                Cantiere c = _context.C_Cantieri.Where(c => c.Id == new Guid(CantiereId)).FirstOrDefault();

                _context.C_VettoreCantiere.RemoveRange(_context.C_VettoreCantiere.Where(vc => vc.WorkDate >= WorkDate && vc.WorkDate <= c.EndDate && vc.VettoreId == Int32.Parse(CodiceVettore)));

                await _context.SaveChangesAsync();

                var days = (c.EndDate - WorkDate).TotalDays;

                for (int k = 0; k < days; k++)
                {

                    DateTime date = WorkDate;

                    if(k > 0)
                    {
                        date = new DateTime(WorkDate.AddDays(k).Year, WorkDate.AddDays(k).Month, WorkDate.AddDays(k).Day, 0, 0, 0);
                    }

                    await _context.C_VettoreCantiere.AddAsync(
                    new VettoreCantiere
                    {
                        Id = Guid.NewGuid(),
                        cantiere = c,
                        VettoreId = Int32.Parse(CodiceVettore),
                        WorkDate = date
                    }
                    );
                       
                   
                }
                await _context.SaveChangesAsync();

                /* Vedo se è al parcheggio per resettare la data*/
                VettoreParcheggio p = 
                    _context.C_VettoreParcheggio                    
                    .Where(c => c.VettoreId == Int32.Parse(CodiceVettore))
                    .Where(c => c.WorkDate <= WorkDate && WorkDate <= c.EndDate)                    
                    .FirstOrDefault();
                
                if( p != null){
                    DateTime tmp = WorkDate;                
                    p.EndDate = WorkDate.AddSeconds(-1);
                    _context.C_VettoreParcheggio.UpdateRange(p);
                    _context.SaveChanges();
                }
                
                /*

                var days = (c.EndDate - WorkDate).TotalDays;

                for(int k = 0; k < days; k++)
                {
                    await _context.C_VettoreCantiere.AddAsync(
                        new VettoreCantiere
                        {
                            Id = new Guid(),
                            cantiere = c,
                            VettoreId = Int32.Parse(CodiceVettore),
                            WorkDate = WorkDate.AddDays(k)
                        }
                        );
                }
                await _context.SaveChangesAsync();
                */
            }
            catch (Exception e)
            {
                return Json(new
                {
                    errore = e.Message
                });
            }
            return Json("ok");
        }

        public async Task<JsonResult> ajaxCantiereParcheggioAsync(String? startTime)
        {
            DateTime start = DateTime.Now;
            if (startTime != null)
            {
                start = DateTime.Parse(startTime);
            }

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            try
            {

                var r = _context.C_Parcheggio
                        .Include(p => p.VettoriParcheggio)
                        .ThenInclude( p => p.Vettore)
                        .Select(
                            p => new
                            {
                                p.Id,
                                p.Name,
                                p.Coordinates,
                                vettori = p.VettoriParcheggio.Select(
                                    v => new {
                                        v.Vettore.Targa,
                                        v.WorkDate,
                                    }
                                )
                            }
                        );
                    


                return Json(
                    new
                    {
                        data = r
                    }
                );
            }
            catch (Exception e)
            {
                return Json(
                    new
                    {
                        ko = e.Message
                    }
               );
            }

        }

        public async Task<JsonResult> ajaxCantiereVettoreAsync(String? startTime, String? cantiereId)
        {
            DateTime start = DateTime.Now;
            if (startTime != null)
            {
                start = DateTime.Parse(startTime);
            }

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            try
            {
                
                var r = _context.C_Cantieri
                    .Include( p => p.Project)
                    .Include( c=> c.VettoreCantieres)
                    .ThenInclude( vc => vc.cantiere)
                    .Include(c => c.VettoreCantieres)
                    .ThenInclude(vc => vc.Vettore)
                    .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .Where(c => c.EndDate >= start)
                    .Where(x => (cantiereId != null) ? x.Id == new Guid(cantiereId) : true).ToList()
                    .Select(
                        x => new
                        {
                            Id = x.Id,
                            name = x.Name,
                            description = x.Description,
                            progetto = (x.Project != null) ? x.Project.Name : "" ,
                            Vettori = (x.VettoreCantieres != null ) ? x.VettoreCantieres

                                        .Where(c => x.VettoreCantieres.Max(c => c.WorkDate) >= start && x.VettoreCantieres.Min(c => c.WorkDate) <= start)//c.cantiere.EndDate >= start && c.WorkDate >= start)
                                        .GroupBy(
                                            m => new {
                                                m.VettoreId,
                                                m.Vettore.Targa,
                                            }
                                        )
                                        .Select(
                                                o =>
                                                new
                                                {
                                                    VettoreId = o.Key.VettoreId,
                                                    Targa = o.Key.Targa,
                                                    EndDate = x.VettoreCantieres.Max(x => x.WorkDate)
                                                }
                                            ) : null,
                            /*
                           
                            */
                        }
                            );                  
                    
                
                return Json(
                    new
                    {
                        data = r
                    }
                );
              
            }
            catch (Exception e)
            {
                return Json(
                    new
                    {
                        ko = e.Message
                    }
               );
            }

        }


        /* AREA PARCHEGGIO */
        [HttpPost]
        public async Task<JsonResult> ajaxParkingAsync(String UserId, int CodiceVettore, DateTime EndDate,String parkingSelect)
        {

            var f = _context.C_VettoreUser.Where(
                c => c.UserId == UserId && c.VettoreId == CodiceVettore
                ).FirstOrDefault();
            f.EndDate = EndDate.AddDays(-1);


            _context.C_VettoreUser.Add(
                new VettoreUser
                {
                    UserId = parkingSelect,
                    VettoreId = CodiceVettore,
                    StartDate = EndDate,
                    EndDate = EndDate.AddYears(50),
                }
                );


            _context.SaveChanges();



            return Json("ok");
        }

        public async Task<JsonResult> ajaxInsertParcheggio(String name,String coordinate) {
            Parcheggio newPark = new Parcheggio();

            newPark.Id = Guid.NewGuid();
            newPark.Name = name;
            newPark.Coordinates = coordinate;

            _context.C_Parcheggio.Add(newPark);


            _context.SaveChanges();



            return Json(new { result = "OK" });

        }

        [HttpPost]
        public async Task<JsonResult> ajaxEditNposti(int CodiceVettore, int nposti)
        {
            var vettore = _context.ANA_VETTORI.Where(c => c.Codice == CodiceVettore).FirstOrDefault();

            if(vettore != null)
            {
                vettore.Nposti = nposti;
                _context.SaveChanges();

                return Json(new
                {
                    result = "OK",
                    data = "Vettore salvato",
                });

            }

            return Json(new
            {
                result = "KO",
                data = "Vettore non trovato",
            });

        }


        [HttpPost]
        public async Task<JsonResult> ajaxVettoreParcheggioAssignAsync(String parkId, String CodiceVettore, DateTime workDate)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            try
            {
                
                await _context.C_VettoreCantiere.Where(v => v.VettoreId == Int32.Parse(CodiceVettore) && v.WorkDate > workDate).ForEachAsync(
                    c =>
                    {
                        c.WorkDate = workDate.AddSeconds(-1);
                    }
                    );
                var r = _context.C_VettoreCantiere
                        .Where(v => v.VettoreId == Int32.Parse(CodiceVettore) && v.WorkDate >= workDate)
                        .ToList();

                _context.C_VettoreCantiere.RemoveRange(r);

                await _context.SaveChangesAsync();

                await _context.C_VettoreUser.Where(v => v.VettoreId == Int32.Parse(CodiceVettore) && v.EndDate > workDate).ForEachAsync(
                    c =>
                    {
                        c.EndDate = workDate.AddSeconds(-1);
                    }
                    );

                await _context.SaveChangesAsync();


                var parks = _context.C_VettoreParcheggio.Where(c => EF.Functions.DateDiffDay(c.EndDate, workDate) < 0 && EF.Functions.DateDiffDay(c.WorkDate, workDate) > 0 && c.VettoreId == Int32.Parse(CodiceVettore)).ToList();

                if(parks.Count == 0)
                {
                    var vp = new VettoreParcheggio();
                    vp.Id = Guid.NewGuid();
                    vp.ParcheggioId = new Guid(parkId);
                    vp.VettoreId = Int32.Parse(CodiceVettore);
                    vp.WorkDate = workDate;
                    vp.EndDate = workDate.AddYears(50);

                    _context.C_VettoreParcheggio.Add(vp);
                }

                

                await _context.SaveChangesAsync();


                /*

                var vettori = _context.C_VettoreUser.Where(
                    x => x.EndDate > StartDate && x.VettoreId == Int32.Parse(CodiceVettore)
                    ).ToList();
                vettori.ForEach(
                    v => v.EndDate = StartDate
                    );
                _context.SaveChanges();


                VettoreUser model = new VettoreUser();
                model.Id = new Guid();
                model.UserId = UserId;
                model.VettoreId = Int32.Parse(CodiceVettore);
                model.StartDate = StartDate;
                model.EndDate = EndDate;

                _context.Set<VettoreUser>().Add(model);
                _context.SaveChanges();

                */
            }
            catch (Exception e)
            {
                return Json(new
                {
                    errore = e.Message
                });
            }
            return Json("ok");
        }

       // [Authorize("VettoreCantiere.Show")]
        public async Task<IActionResult> VettoreCantiere()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.Logout, "Visualizzazione cantieri");
            return View();
        }


        //[Authorize("VettoreParcheggio.Show")]
        public async Task<IActionResult> VettoreParcheggio()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.Logout, "Visualizzazione parcheggi");
            return View();
        }
        //[Authorize("VettoreParcheggio.Show")]
        public async Task<JsonResult> ajaxParcheggioAsync(string q = null)
        {
            
            var r =  _context.Users
                    .Where(
                    u => u.isParking == 1 && (u.FirstName + " " + u.LastName).Contains(q)
                    ).ToList().Select(
                    x =>
                    new Select2ViewModel
                    {
                        id = x.Id,
                        text = x.FirstName +" " + x.LastName,
                    }
                    ).ToList();
            return Json(
                new { results = r }
            );
        }

        public async Task<JsonResult> ajaxParkVettoreListAsync(String? q = null)
        {

            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_Parcheggio
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = x.Name.Trim(),
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }

        //[Authorize("VettoreCantiere.Show")]
        public async Task<JsonResult> ajaxVettoreCantiereAsync(string q = null)
        {
            //Filtro multitenant
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();


            Func<Vettore, bool> whereSearching = (a => true); //default sempre vera
            if (q != null) whereSearching = (item => item.Targa.ToUpper().Contains(q.ToUpper()));

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.Set<Vettore>()
                //.Include (x => x.CodiceSettore)
                //.Include (x => x.CodiceSottoSettore)
                .Where(c => c.CodCliente.Trim().Equals(user.MultiTenant.LoocId))
                .Where(whereSearching)
                .Select(x => new Select2ViewModel
                {
                    id = x.Codice.ToString(),
                    text = x.Targa.Trim(),//+ " (" + x.Targa.Trim()+")",
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }

        public async Task<JsonResult> ajaxCantiereVettoreListAsync(DateTime dateTime, String? q = null)
        {
            
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
           
            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.Set<Cantiere>()
                .Where(x => (x.MultiTenantId != null) ? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where( c => c.Name.ToLower().Contains(q.ToLower()))
                .Where( c => c.StartDate < dateTime && c.EndDate > dateTime)
                .Select(x => new Select2ViewModel
                {
                    id = x.Id.ToString(),
                    text = x.Name.Trim(),
                })
                .ToList();

            return Json(
                new { results = r }
            );
        }

        /* ALTRE */

        [AllowAnonymous]
        [HttpGet]
        public async Task<FileResult> getPictureSemaforo(int codiceVettore)
        {

            var sc = _context.ANA_VETTORI
                            .Include(c => c.GOF_Scheda)
                            .Where(c => c.Codice == codiceVettore)
                            .Select(c => new
                            {
                                scheda = (c.GOF_Scheda != null) ? new
                                {
                                    Codice = c.GOF_Scheda.FirstOrDefault().Codice,
                                    stato = c.GOF_Scheda.FirstOrDefault().stato,
                                    
                                } :
                               null,
                               
                            }).FirstOrDefault();




            var webClient = new WebClient();
            byte[] imageBytes = null;


            //(c.GOF_Scheda.Codice != null && c.GOF_Scheda.stato != 2 && c.GOF_Scheda.stato != 3 && c.GOF_Scheda.stato != 4 && c.GOF_Scheda.stato != 5)


            //operativo = (c.GOF_Scheda.Codic != null && c.GOF_Scheda.stato != 2 && c.GOF_Scheda.stato != 3 && c.GOF_Scheda.stato != 4 && c.GOF_Scheda.stato != 5)


            if (sc == null)
            {
                imageBytes = webClient.DownloadData("wwwroot/looc/images/various/semaforoVerde32.png");
            }

            
            else if (sc.scheda == null) {
                imageBytes = webClient.DownloadData("wwwroot/looc/images/various/semaforoVerde32.png");
            }
            else if (sc.scheda.stato == 1)
            {
                imageBytes = webClient.DownloadData("wwwroot/looc/images/various/semaforoRosso32.png");
            }
            else
            {
                imageBytes = webClient.DownloadData("wwwroot/looc/images/various/semaforoGiallo32.png");
            }
            
            return File(imageBytes, "image/png");
            
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<FileResult> getPicture(string file)
        {
            /*
            var file = _context.ANA_VETTORI
                    .Include(c => c.ModelloVettore)
                    .ThenInclude(c => c.modelloInfo)
                    .Where( c=> c.Codice == codiceVettore)
                    .Select(c => c.ModelloVettore.modelloInfo.Img).FirstOrDefault();
                    */
            
            var webClient = new WebClient();
            byte[] imageBytes;
            try
            {
                imageBytes = webClient.DownloadData("http://looc.kresearch.it/PackageGovernance/ServiziModellazione/ModellazioneVettori/ImgModelliVettore/" + file);
                return File(imageBytes, "image/png");
            }
            catch (Exception)
            {

            }


            imageBytes = webClient.DownloadData("http://looc.kresearch.it/PackageGovernance/ServiziModellazione/ModellazioneVettori/ImgModelliVettore/vettore.jpg");
            return File(imageBytes, "image/png");
            

        }

    }        
}