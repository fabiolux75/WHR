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
using Microsoft.AspNetCore.Authorization;
using LoocERP.Helpers;

namespace LoocERP.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;      
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public string CodiceOperatore { get; private set; }

        public TimesheetController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }


        [Authorize("Timesheet.Show")]
        public async Task<IActionResult> IndexAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaTimeSheet, "Visualizzazione Time Sheet");

            if (endDate == null) endDate = DateTime.Now.Date;
            if (startDate == null) startDate = DateTime.Now.Date; //endDate.Value.AddMonths(-1);

            /*
            ViewData["startDate"] = startDate.Value.ToString("MM/dd/yyyy");
            ViewData["endDate"] = endDate.Value.ToString("MM/dd/yyyy");
            */
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View();
        }

        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Lista turni di lavoro
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        public async Task<JsonResult> ajaxIndexAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            if (endDate != null) endDate = endDate.Value.AddDays(1);

            //List<TimeSheetViewModel> r = new List<TimeSheetViewModel>();
            var r = _context.C_TimeSheets
                .Where(a => a.MultiTenantId == user.MultiTenantId)
                .Where(a => startDate <= a.DataLogin && endDate > a.DataLogin )
                .Select(
                    a => new // Use an anonymous type
                    {
                        Id = a.Id,
                        nomeOperatore = a.NomeOperatore,
                        CodiceOperatore = a.CodiceOperatore,
                        DataLogin = a.DataLogin,
                        OraLogin = a.OraLogin,
                        DataLogout = a.DataLogout,
                        OraLogout = a.OraLogout,
                        IdDevice = a.IdDevice,
                        CodiceVettore = a.CodiceVettore,
                        TargaVettore = a.TargaVettore,
                        CodiceAzienda = a.CodiceAzienda,
                        Evento = a.Evento,
                        Stato = a.Stato,
                        CodiceCantiere = a.CodiceCantiere,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude,
                        LoginFull = (a.DataLogin + a.OraLogin),
                        LogoutFull = (a.DataLogout != null ? (a.DataLogout + a.OraLogout): null),
                        EffectiveHour = (a.DataLogout != null ? ((a.DataLogout + a.OraLogout) - (a.DataLogin + a.OraLogin)).Value.Hours.ToString("00") : "-"),                                                
                        EffectiveMinute = (a.DataLogout != null ? ((a.DataLogout + a.OraLogout) - (a.DataLogin + a.OraLogin)).Value.Minutes.ToString("00") : "-"),
                        EffectiveSecond = (a.DataLogout != null ? ((a.DataLogout + a.OraLogout) - (a.DataLogin + a.OraLogin)).Value.Seconds.ToString("00") : "-")
                    })
                .OrderByDescending( a => a.DataLogin)
                .ToList();
                        

            return Json(
                new { data = r}
            );
        }
                
        [HttpPost]
        public async Task<JsonResult> ajaxModalSummaryAsync(int? id=null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var r = _context.C_TimeSheets
                .Where(a => a.MultiTenantId == user.MultiTenantId)
                .Where(a => a.Id.Equals(id) )
                .Select(
                    a => new // Use an anonymous type
                    {
                        Id = a.Id,
                        nomeOperatore = a.NomeOperatore,
                        CodiceOperatore = a.CodiceOperatore,
                        DataLogin = a.DataLogin,
                        OraLogin = a.OraLogin,
                        DataLogout = a.DataLogout,
                        OraLogout = a.OraLogout,
                        IdDevice = a.IdDevice,
                        CodiceVettore = a.CodiceVettore,
                        TargaVettore = a.TargaVettore,
                        CodiceAzienda = a.CodiceAzienda,
                        Evento = a.Evento,
                        Stato = a.Stato,
                        CodiceCantiere = a.CodiceCantiere,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude,
                        LoginFull = (a.DataLogin + a.OraLogin),
                        LogoutFull = (a.DataLogout != null ? (a.DataLogout + a.OraLogout) : null),
                        EffectiveHour = (a.DataLogout != null ? ((a.DataLogout + a.OraLogout) - (a.DataLogin + a.OraLogin)).Value.Hours.ToString("00") : "-"),
                        EffectiveMinute = (a.DataLogout != null ? ((a.DataLogout + a.OraLogout) - (a.DataLogin + a.OraLogin)).Value.Minutes.ToString("00") : "-"),
                        EffectiveSecond = (a.DataLogout != null ? ((a.DataLogout + a.OraLogout) - (a.DataLogin + a.OraLogin)).Value.Seconds.ToString("00") : "-")
                    })                    
                    .FirstOrDefault();

            return Json(
                new { data = r }
            );            
        }
        /*
        public JsonResult ajaxFullCalendar(
             DateTime? start = null
            ,DateTime? end = null)
        {
            if (start == null) {
                start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            }
            if (end == null)
            {
                end = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                end.Value.AddHours(23);
                end.Value.AddMinutes(59);
            }

            List<TimeSheet> r = new List<TimeSheet>();            
            r = _context.C_TimeSheet
                .Where(a => a.DataLogin > start && a.DataLogin < end)
                .ToList();                        
            List<FullCalendarEvent> r1 = new List<FullCalendarEvent>();

            foreach (var element in r)
            {
                FullCalendarEvent f = new FullCalendarEvent();
                f.id = (int)element.Id;                
                f.start = (DateTime)element.DataLogin + (TimeSpan)element.OraLogin;
                f.end = (DateTime)element.DataLogin + (TimeSpan)element.OraLogout;
                f.HourDiff = f.end - f.start;
                f.title = element.NomeOperatore + " - " +f.HourDiff;
                f.allDay = "";
                r1.Add(f);
            }
            return Json(
                r1
            );
        }
        */
    }
}