using System.Reflection.Metadata;
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
using System.Globalization;
using Microsoft.AspNetCore.Authorization;


namespace LoocERP.Controllers
{
    public class RelTurnoUserS : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public RelTurnoUserS(ApplicationDBContext context
                                , UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        [Authorize("RelTurnoUser.Show")]
        public async Task<IActionResult> IndexAsync(DateTime? WorkDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            if (WorkDate == null) WorkDate = DateTime.Now.Date;

            //ViewBag.WorkDate = WorkDate.Value.ToString("MM/dd/yyyy"); //data usata per la lettura dei dati
            //ViewData["TurnoId"] = TurnoId;

            var cantieri = _context.C_Cantieri.Where(c => c.MultiTenantId == user.MultiTenantId).ToList();

            ViewBag.WorkDate = WorkDate.Value;

            return View(cantieri);
        }


        [HttpPost]
        [Authorize("RelTurnoUser.Show")]
        public async Task<IActionResult> IndexAsync(String cantiereId, String turnoId,String userId, DateTime? WorkDate , DateTime dataInizio, DateTime dataFine)
        {
            var datafinecantiere = _context.C_Cantieri
                .Where(c => c.Id == new Guid(cantiereId))
                .Select(c => c.EndDate).FirstOrDefault();
                
            dataFine = DateTime.Parse(datafinecantiere.ToString());

            if(WorkDate == null)
            {
                WorkDate = DateTime.Today;
            }


            dataInizio = WorkDate.Value; //dal giorno selezionato fino a fine cantiere
            

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var turniUs = _context.C_Rel_TurniUsers.Where(
                rtu => rtu.UserId == userId
                        &&
                       rtu.WorkDate >= dataInizio
                        &&
                       rtu.WorkDate <= dataFine
                        && rtu.MultiTenantId == user.MultiTenantId
                ).ToList();

            foreach(var t in turniUs)
            {
                _context.C_Rel_TurniUsers.Remove(t);
                _context.SaveChanges();
            }

            

            while (dataInizio <= dataFine)
            {
                _context.C_Rel_TurniUsers.Add(
                    new Rel_TurnoUser
                    {
                        UserId = userId,
                        TurnoId = new Guid(turnoId),
                        WorkDate = dataInizio,
                        MultiTenantId = user.MultiTenantId
                    }
                );
                dataInizio = dataInizio.AddDays(1);
            }

            _context.SaveChanges();
           
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Companies <c>assignSelectCantiereUser</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>        
        [Authorize("RelTurnoUser.Create")]
        public async Task<IActionResult> assignSelectCantiereUserAsync(string? TurnoId = null, DateTime? WorkDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.TurnoId = TurnoId;
            
            if (WorkDate == null) WorkDate = DateTime.Now.Date;
            ViewBag.WorkDate = WorkDate.Value.ToString("MM/dd/yyyy");

            Turno turno = _context.C_Turni
                .Where(
                    x => x.Id.ToString().ToLower().Equals(TurnoId.ToString().ToLower())
                    && x.MultiTenantId == x.MultiTenantId
                )
                .FirstOrDefault();

            ViewData["Turno"] = turno;

            return View();
        }


        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxRemoveAsync(string UserId, string TurnoId, DateTime WorkDate)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var result = "ko";
            var message = "Errore generico nel salvataggio";
            try
            {
                var model = _context.C_Rel_TurniUsers
                    .Where(a => a.MultiTenantId == user.MultiTenantId)
                    .Where(a => a.UserId.ToString().ToLower().Equals(UserId.ToString().ToLower()))
                    .Where(a => a.TurnoId.ToString().ToLower().Equals(TurnoId.ToString().ToLower()))
                    .Where(a => DateTime.Compare(a.WorkDate, WorkDate) == 0)
                    .FirstOrDefault();
                if (model != null) //esiste aggiorno
                {
                    _context.C_Rel_TurniUsers.Remove(model);
                    _context.SaveChanges();
                }
                result = "ok";
                message = "";
            }
            catch (Exception e)
            {
                result = "ko";
                message = "Eccezione";
            }

            return Json(
              new
              {
                  result = result,
                  message = message,
                  data = new { }
              }
            );
        }


        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxAssignAsync(string UserId, string TurnoId, DateTime WorkDate)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var result = "ko";
            var message = "Errore generico nel salvataggio";
            try
            {
                var r = _context.C_Rel_TurniUsers
                    .Where(a => a.MultiTenantId == user.MultiTenantId)
                    .Where(a => a.UserId.ToString().ToLower().Equals(UserId.ToString().ToLower()))
                    .Where(a => DateTime.Compare(a.WorkDate, WorkDate) == 0)
                    .FirstOrDefault();
                if (r != null) //esiste aggiorno
                {
                    r.TurnoId = Guid.Parse(TurnoId);
                    _context.SaveChanges();
                }
                else
                {
                    Rel_TurnoUser model = new Rel_TurnoUser();
                    model.UserId = UserId;
                    model.TurnoId = Guid.Parse(TurnoId);
                    model.WorkDate = WorkDate;
                    model.MultiTenantId = user.MultiTenantId;
                    _context.C_Rel_TurniUsers.Add(model);
                    _context.SaveChanges();
                }
                result = "ok";
                message = "";
            }
            catch (Exception e)
            {
                result = "ko";
                message = "Eccezione";
            }

            return Json(
              new
              {
                  result = result,
                  message = message,
                  data = new { }
              }
            );
        }


        /// <summary>
        /// Ritorna la lista di tutti i cantieri e i turni
        /// </summary> 
        [HttpGet]
        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxProjectCantiereTurnoAsync(DateTime? WorkDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var users = (
                            from u in _context.Set<AppUser>() where u.isEmployee == 1 && u.MultiTenantId == user.MultiTenantId
                            from tu in _context.Set<Rel_TurnoUser>()
                                .Where( c => c.MultiTenantId == user.MultiTenantId)
                                .Where(tu => u.Id.Equals(tu.UserId) &&  DateTime.Compare(tu.WorkDate, (WorkDate ?? DateTime.Now.Date)) == 0 )
                                .DefaultIfEmpty() 
                            from t in _context.Set<Turno>()
                                .Where( c => c.MultiTenantId == user.MultiTenantId)
                                .Where(t => t.Id == tu.TurnoId).DefaultIfEmpty()
                            from c in _context.Set<Cantiere>()
                                .Where( c => c.MultiTenantId == user.MultiTenantId)
                                .Where(c => c.Id == t.CantiereId).DefaultIfEmpty()
                            from p in _context.Set<Project>()
                                .Where( c => c.MultiTenantId == user.MultiTenantId)
                                .Where(p => p.Id == c.ProjectId).DefaultIfEmpty()
                            select new
                            {
                                u.Id,
                                u.FirstName,
                                u.LastName,
                                u.Email,
                                Cantiere = "Progetto: "+(p.Name??"-")+" || Cantiere: "+(c.Name??"-"),
                                CantiereName = (c.Name??null),
                                CommessaName = (p.Name??null),
                            }                        
                        ).OrderBy(o => o.CommessaName).ToList();

            return Json(new { data = users });            
        }


        [HttpGet]
        public async Task<JsonResult> ajaxTurniAsync(DateTime? WorkDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_Turni
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Include(c => c.Cantiere)
                .Select( c=> new 
                {
                    id = c.Id,
                    name = c.Name,
                    oraInizio = c.OraInizio,
                    oraFine = c.OraFine,
                    cantiere = c.Cantiere.Name
                })
                .ToList();

            return Json(
               new { data = r }
            );

        }

        public async Task<JsonResult> ajaxTurniCantiereAsync(DateTime? WorkDate = null,String cantiereId = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_Turni                
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(c => c.CantiereId == new Guid(cantiereId))
                .ToList();

            return Json(
                new { data = r }
            );           
        }
        
    }
}