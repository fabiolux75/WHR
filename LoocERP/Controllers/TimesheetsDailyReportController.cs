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
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Text;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ServiceStack;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
// using ServiceStack;
// using Microsoft.AspNet.Identity;

namespace LoocERP.Controllers
{
    public class TimesheetsDailyReportController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;      
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public string CodiceOperatore { get; private set; }

        public TimesheetsDailyReportController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }


        [Authorize("TimesheetDailyReport.Show")]
        public async Task<IActionResult> IndexAsync(DateTime? WorkDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaTimeSheetDaily, "Visualizzazione timesheet daily");

            ViewBag.WorkDate = WorkDate??DateTime.Now;

            return View();
        }
        

        [HttpGet]
        public async Task<IActionResult> IndexMontlyAsync(DateTime? MontlyDate  = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.VisualizzaTimeSheetMontly, "Visualizzazione TimeSheet Mensile");

            if (MontlyDate == null) MontlyDate = DateTime.Now.Date;

            ViewBag.MontlyDate = MontlyDate.Value;

            return View();
        }


        public async Task<IActionResult> DailyReportAsync(string turnoId = null,DateTime? WorkDate = null, int WizardStep=0)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            DailyReportViewModel model = new DailyReportViewModel();
            model.TimeSheetDailyReportList = 
                _context.C_TimeSheetsDailyReport 
                    .Include( x => x.Turno )
                    .Include( x=> x.Turno.Cantiere)
                    .Include( x=> x.Giustificativo)
                    .Include( x=> x.Trasferta)
                    .Where( c=>c.MultiTenantId == user.MultiTenantId)                                    
                    .Where( c => c.TurnoId.ToString().ToLower().Equals( (turnoId!=null?turnoId.ToLower():c.TurnoId.ToString().ToLower()) ) )
                    .Where(tu => DateTime.Compare((DateTime) tu.WorkDate, (WorkDate ?? DateTime.Now.Date)) == 0 )
                    .Select( 
                        c => 
                            new DailyReportElementViewModel{                                
                                Turno = c.Turno,
                                User = c.User,
                                Cantiere = c.Turno.Cantiere,                        
                                Commessa = c.Turno.Cantiere.Project,    
                                TimeSheetDailyReport = new TimeSheetDailyReport{
                                    Id = c.Id,
                                    WorkDate = c.WorkDate,
                                    TurnoId = c.TurnoId,
                                    UserId = c.UserId,
                                    OreEffettive = c.OreEffettive,
                                    Ore = c.Ore,
                                    OreStraordinarie = c.OreStraordinarie,
                                    OreNotturne = c.OreNotturne,
                                    OreGalleria = c.OreGalleria,
                                    //OreTrasferta = c.OreTrasferta,
                                    Cigo = c.Cigo,
                                    Ec = c.Ec,
                                    Turno = c.Turno,
                                    User = c.User,
                                    Giustificativo = c.Giustificativo,
                                    GiustificativoId = c.GiustificativoId,
                                    Trasferta = c.Trasferta,
                                    TrasfertaId = c.TrasfertaId,
                                    StatoUtente = c.StatoUtente,                                
                                    StatoTurno = c.StatoTurno,
                                    Hour = c.Hour,
                                    TravelHour = c.TravelHour,
                                    GalleryHour = c.GalleryHour,
                                    EffectiveHour = c.EffectiveHour,
                                    NightHour = c.NightHour,
                                    OvertimeHour = c.OvertimeHour
                                }
                            }
                    )
                    .OrderBy(c => c.User.LastName)
                    .ToList();

            if(turnoId != null){
                model.Turno = _context.Set<Turno>().Where( c => c.Id.ToString().ToLower().Equals(turnoId.ToString().ToLower())).FirstOrDefault();            
                model.Cantiere = _context.Set<Cantiere>().Where(t => t.Id == model.Turno.CantiereId ).FirstOrDefault();
                model.Project = _context.Set<Project>().Where(t => t.Id == model.Cantiere.ProjectId).FirstOrDefault();
            }

            if (WizardStep == 1) return View("DailyReportPrint", model);
            
            ViewBag.WorkDate = WorkDate;
            ViewBag.TurnoId = turnoId;
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> export(String turnoId, DateTime? WorkDate, int WizardStep = 0)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            DailyReportViewModel model = new DailyReportViewModel();
            model.TimeSheetDailyReportList =
                _context.Set<TimeSheetDailyReport>()
                    .Include(x => x.Turno)
                    .Include(x => x.Turno.Cantiere)
                    .Include(x => x.Giustificativo)
                    .Include(x => x.Trasferta)
                    .Where(c => c.MultiTenantId == user.MultiTenantId)
                    .Where(c => c.TurnoId.ToString().ToLower().Equals((turnoId != null ? turnoId.ToLower() : c.TurnoId.ToString().ToLower())))
                    .Where(tu => DateTime.Compare((DateTime)tu.WorkDate, (WorkDate ?? DateTime.Now.Date)) == 0)
                    .Select(
                        c =>
                            new DailyReportElementViewModel
                            {
                                Turno = c.Turno,
                                User = c.User,
                                Cantiere = c.Turno.Cantiere,
                                Commessa = c.Turno.Cantiere.Project,
                                TimeSheetDailyReport = new TimeSheetDailyReport
                                {
                                    Id = c.Id,
                                    WorkDate = c.WorkDate,
                                    TurnoId = c.TurnoId,
                                    UserId = c.UserId,
                                    OreEffettive = c.OreEffettive,
                                    Ore = c.Ore,
                                    OreStraordinarie = c.OreStraordinarie,
                                    OreNotturne = c.OreNotturne,
                                    OreGalleria = c.OreGalleria,
                                    //OreTrasferta = c.OreTrasferta,
                                    Cigo = c.Cigo,
                                    Ec = c.Ec,
                                    Turno = c.Turno,
                                    User = c.User,
                                    Giustificativo = c.Giustificativo,
                                    GiustificativoId = c.GiustificativoId,
                                    Trasferta = c.Trasferta,
                                    TrasfertaId = c.TrasfertaId,
                                    StatoUtente = c.StatoUtente,
                                    StatoTurno = c.StatoTurno,
                                    Hour = c.Hour,
                                    TravelHour = c.TravelHour,
                                    GalleryHour = c.GalleryHour,
                                    EffectiveHour = c.EffectiveHour,
                                    NightHour = c.NightHour,
                                    OvertimeHour = c.OvertimeHour
                                }
                            }
                    )
                    .OrderBy(c => c.User.LastName)
                    .ToList();

            return Json(
                    new
                    {
                        data = new
                        {
                            result = model
                        }
                    }
                );
        }



        


        public ArchiveFile WriteCsvToMemory(AppUser user,ApplicationDBContext _context, string userId = null, DateTime? MontlyDate = null)
        {

            var usr = _context.Users.Where(c => c.Id == userId).FirstOrDefault();
            if (MontlyDate == null) MontlyDate = DateTime.Now.Date;
            var year = MontlyDate.Value.Year;
            var month = MontlyDate.Value.Month;

            DailyReportViewModel model = new DailyReportViewModel();

            model.TimeSheetDailyReportList =
                _context.C_TimeSheetsDailyReport
                .Include( c => c.Turno)
                .ThenInclude( c => c.Cantiere)
                .ThenInclude( c => c.Project)
                .Include(c => c.Turno)
                .Include(c => c.User)
                .Where(c => c.WorkDate.Value.Month == month) //filtro per mese
                .Where(c => c.WorkDate.Value.Year == year) //filtro per mese
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(c => c.UserId == userId)
                .Select(
                    c =>
                        new DailyReportElementViewModel
                        {
                            Turno = c.Turno,//_context.Set<Turno>().Where(t => t.Id == c.TurnoId).FirstOrDefault(),
                            User = c.User,//_context.Set<AppUser>().Where(t => t.Id == c.UserId).FirstOrDefault(),
                            Cantiere = c.Turno.Cantiere,//_context.Set<Cantiere>().Where(t => t.Id == c.Turno.CantiereId).FirstOrDefault(),
                            Commessa = c.Turno.Cantiere.Project, //_context.Set<Project>().Where(t => t.Id == c.Turno.Cantiere.ProjectId).FirstOrDefault(),
                            //Cantiere = c.Turno.Cantiere,                        
                            //Commessa = c.Turno.Cantiere.Project,    
                            TimeSheetDailyReport = new TimeSheetDailyReport
                            {
                                Id = c.Id,
                                WorkDate = c.WorkDate,
                                TurnoId = c.TurnoId,
                                UserId = c.UserId,
                                OreEffettive = c.OreEffettive,
                                Ore = c.Ore,
                                OreStraordinarie = c.OreStraordinarie,
                                OreNotturne = c.OreNotturne,
                                OreGalleria = c.OreGalleria,
                                //OreTrasferta = c.OreTrasferta,
                                Indennita = c.Indennita,
                                Cigo = c.Cigo,
                                Ec = c.Ec,
                                Turno = c.Turno,
                                User = c.User,
                                Giustificativo = c.Giustificativo,
                                GiustificativoId = c.GiustificativoId,
                                Trasferta = c.Trasferta,
                                TrasfertaId = c.TrasfertaId,
                                StatoUtente = c.StatoUtente,
                                StatoTurno = c.StatoTurno,
                                Hour = c.Hour,
                                TravelHour = c.TravelHour,
                                GalleryHour = c.GalleryHour,
                                EffectiveHour = c.EffectiveHour,
                                NightHour = c.NightHour,
                                OvertimeHour = c.OvertimeHour
                            }
                        }
                )
                .OrderBy(c => c.TimeSheetDailyReport.WorkDate.Value.Day)
                .ToList();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };

            ContractUser contract = null;

            if(model.TimeSheetDailyReportList.Count > 0)
                contract =  _context.C_ContractUser.Where(c => c.UserId == model.TimeSheetDailyReportList.FirstOrDefault().User.Id).OrderBy(c => c.ValidTo).FirstOrDefault();


            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, config))
            {
                csvWriter.WriteRecords(model.TimeSheetDailyReportList.Select( c => new { 
                    Nome = c.User.FirstName,
                    Cognome = c.User.LastName,
                    Commessa = c.Commessa.Name,
                    Cantiere = c.Cantiere.Name,
                    OreApp = c.TimeSheetDailyReport.EffectiveHour,
                    Ore = c.TimeSheetDailyReport.Hour,
                    Notturno = (c.TimeSheetDailyReport.NightHour != null) ? c.TimeSheetDailyReport.NightHour + "" : "00:00",
                    Straordinario = (c.TimeSheetDailyReport.OvertimeHour != null) ? c.TimeSheetDailyReport.OvertimeHour + "" : "00:00",
                    Galleria = (c.TimeSheetDailyReport.GalleryHour != null) ? c.TimeSheetDailyReport.GalleryHour + "" : "00:00",
                    CodiceTrasferta = (c.TimeSheetDailyReport.Trasferta != null ) ? c.TimeSheetDailyReport.Trasferta.Code : "",
                    Trasferta = (c.TimeSheetDailyReport.Trasferta != null) ? c.TimeSheetDailyReport.Trasferta.Description : "",
                    CodiceGuistificativo = (c.TimeSheetDailyReport.Giustificativo != null) ? c.TimeSheetDailyReport.Giustificativo.Code : "",
                    Guistificativo = (c.TimeSheetDailyReport.Giustificativo != null) ? c.TimeSheetDailyReport.Giustificativo.Description : "",
                    Indennita = c.TimeSheetDailyReport.Indennita,
                    cigo = c.TimeSheetDailyReport.Cigo,
                    Ec = c.TimeSheetDailyReport.Ec,
                    CodiceContratto = (contract != null ) ? contract.Code : "",
                    InizioContratto = (contract != null) ? contract.ValidFrom + "" : "",
                    FineContratto = (contract != null) ? contract.ValidTo + "" : "",
                }).ToList());
                streamWriter.Flush();

                ArchiveFile file = new ArchiveFile();
                file.FileBytes = memoryStream.ToArray();


                file.Name = usr.FirstName + " " + usr.LastName+" - "+ MontlyDate.ToString();
                file.Extension = "csv";
                return file;
            }
        }


        public async Task<IActionResult> expRILPRE(DateTime? MontlyDate = null) {


            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string tracciatoUtentiFinale = "";

            if (MontlyDate == null) MontlyDate = DateTime.Now.Date;
            var year = MontlyDate.Value.Year;
            var month = MontlyDate.Value.Month;

            var daysInMonth = DateTime.DaysInMonth(year, month);

            string emptyDay     = "0000    0000    0000    0000    0000    0000    0000";
            string emptyHour    = "    0000";


            var r = _context.Users
                                .Include(c => c.TimeSheetDailyReports)
                                .Include(c => c.Company)
                                .Where(c => c.MultiTenantId == user.MultiTenantId)
                                .Where(c => c.TimeSheetDailyReports.Where(c => c.WorkDate.Value.Month == month).Count() > 0)
                                .Where(c => c.TimeSheetDailyReports.Where(c => c.WorkDate.Value.Year == year).Count() > 0)
                                .Select(
                                    c => new
                                    {
                                        Id = c.Id,
                                        FirstName = c.FirstName,
                                        LastName = c.LastName,
                                        Matricola = c.matricola,
                                        CodiceAzienda = c.Company.PagheCodAzienda,
                                        CodiceFiliale = c.Company.PagheCodFiliale,
                                        TimeSheetDailyReports = c.TimeSheetDailyReports
                                            .Where(c => c.WorkDate.Value.Year == year)
                                            .Where(c => c.WorkDate.Value.Month == month)
                                            .Select(
                                            c => new
                                            {
                                                TimeSheetDailyReport = new
                                                {
                                                    Id = c.Id,
                                                    WorkDate = c.WorkDate,
                                                    UserId = c.UserId,
                                                    OreEffettive = c.OreEffettive,
                                                    Ore = c.Ore,
                                                    OreStraordinarie = c.OreStraordinarie,
                                                    OreNotturne = c.OreNotturne,
                                                    OreGalleria = c.OreGalleria,
                                                    Giustificativo = c.Giustificativo,
                                                    GiustificativoId = c.GiustificativoId,
                                                    Hour = c.Hour,
                                                    TravelHour = c.TravelHour,
                                                    GalleryHour = c.GalleryHour,
                                                    EffectiveHour = c.EffectiveHour,
                                                    NightHour = c.NightHour,
                                                    OvertimeHour = c.OvertimeHour
                                                }
                                            }
                                        )
                                    }
                                ).ToList();


            string inizioMese   = "01"+month.ToString().PadLeft(2,'0')+year.ToString().Substring(2);
            string fineMese     = daysInMonth.ToString()+month.ToString().PadLeft(2,'0')+year.ToString().Substring(2);
            string fileName     = year+month.ToString().PadLeft(2,'0')+"_TracciatoRILPRE.txt";

            foreach (var user_timesheet in r)
             {
                List<string> TracciatoRILPRE = new List<string>();
                for (int i = 0; i < daysInMonth; ++i) {
                    TracciatoRILPRE.Add(emptyDay);
                }                 
                
                string codiceAzienda    = user_timesheet.CodiceAzienda ?? "0000" ;
                string codiceFiliale    = user_timesheet.CodiceFiliale ?? "00" ;
                string matricola        = string.IsNullOrEmpty(user_timesheet.Matricola) ?  "000000000" : user_timesheet.Matricola.ToString().PadLeft(9,'0');
                
                foreach (var day in user_timesheet.TimeSheetDailyReports.ToArray()) {
                    int wd              = day.TimeSheetDailyReport.WorkDate.Value.Day - 1;
                    string ordinarie    = string.IsNullOrEmpty(day.TimeSheetDailyReport.Hour.ToString()) ? "0000" : day.TimeSheetDailyReport.Hour.Value.Hours.ToString().PadLeft(2,'0') + day.TimeSheetDailyReport.Hour.Value.Minutes.ToString().PadLeft(2,'0');
                    string over         = string.IsNullOrEmpty(day.TimeSheetDailyReport.OvertimeHour.ToString()) ? emptyHour : "STR " + day.TimeSheetDailyReport.OvertimeHour.Value.Hours.ToString().PadLeft(2,'0')+ day.TimeSheetDailyReport.OvertimeHour.Value.Minutes.ToString().PadLeft(2,'0');
                    string night        = string.IsNullOrEmpty(day.TimeSheetDailyReport.NightHour.ToString()) ? emptyHour : "LN  " + day.TimeSheetDailyReport.NightHour.Value.Hours.ToString().PadLeft(2,'0')+ day.TimeSheetDailyReport.NightHour.Value.Minutes.ToString().PadLeft(2,'0');
                    string gallery      = (string.IsNullOrEmpty(day.TimeSheetDailyReport.GalleryHour.ToString()) || day.TimeSheetDailyReport.GalleryHour.ToString().Equals("00:00:00"))  ? emptyHour : "IG  " + day.TimeSheetDailyReport.GalleryHour.Value.Hours.ToString().PadLeft(2,'0')+ day.TimeSheetDailyReport.GalleryHour.Value.Minutes.ToString().PadLeft(2,'0');
                    string extraGiust   = string.IsNullOrEmpty(day.TimeSheetDailyReport.Giustificativo?.Code.ToString()) ? emptyHour :  day.TimeSheetDailyReport.Giustificativo.Code.ToString().PadRight(4,' ') + "1000"; // Manca il campo sul DB

                    TracciatoRILPRE[wd] =  ordinarie + over + night + gallery + extraGiust + "    0000    0000";
                }

                tracciatoUtentiFinale += codiceAzienda+codiceFiliale+matricola+ string.Join("",TracciatoRILPRE)+"\r\n";

            }
            return File(Encoding.UTF8.GetBytes(tracciatoUtentiFinale.ToString()), "text/plain", fileName);

        }

        public async Task<IActionResult> exp(DateTime? MontlyDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            if (MontlyDate == null) MontlyDate = DateTime.Now.Date;
            var year = MontlyDate.Value.Year;
            var month = MontlyDate.Value.Month;



            var r = _context.Users
                            .Include(c => c.TimeSheetDailyReports)
                            .ThenInclude(c => c.Turno)
                            .ThenInclude(c => c.Cantiere)
                            .ThenInclude(c => c.Project)
                            .Include(c => c.TimeSheetDailyReports)
                            .ThenInclude(c => c.Turno)
                            .Where(c => c.MultiTenantId == user.MultiTenantId)
                            .Where(c => c.TimeSheetDailyReports.Where(c => c.WorkDate.Value.Month == month).Count() > 0)
                            .Where(c => c.TimeSheetDailyReports.Where(c => c.WorkDate.Value.Year == year).Count() > 0)
                            .Select(
                                c => new
                                {
                                    Id = c.Id,
                                    FirstName = c.FirstName,
                                    LastName = c.LastName,
                                    TimeSheetDailyReports = c.TimeSheetDailyReports
                                        .Where(c => c.WorkDate.Value.Year == year)
                                        .Where(c => c.WorkDate.Value.Month == month)
                                        .Select(
                                        c => new
                                        {
                                            Turno = c.Turno,
                                            Cantiere = c.Turno.Cantiere,
                                            Commessa = c.Turno.Cantiere.Project,
                                            TimeSheetDailyReport = new
                                            {
                                                Id = c.Id,
                                                WorkDate = c.WorkDate,
                                                TurnoId = c.TurnoId,
                                                UserId = c.UserId,
                                                OreEffettive = c.OreEffettive,
                                                Ore = c.Ore,
                                                OreStraordinarie = c.OreStraordinarie,
                                                OreNotturne = c.OreNotturne,
                                                OreGalleria = c.OreGalleria,
                                                Indennita = c.Indennita,
                                                Cigo = c.Cigo,
                                                Ec = c.Ec,
                                                Turno = c.Turno.Name,
                                                Giustificativo = c.Giustificativo,
                                                GiustificativoId = c.GiustificativoId,
                                                Trasferta = c.Trasferta,
                                                TrasfertaId = c.TrasfertaId,
                                                StatoUtente = c.StatoUtente,
                                                StatoTurno = c.StatoTurno,
                                                Hour = c.Hour,
                                                TravelHour = c.TravelHour,
                                                GalleryHour = c.GalleryHour,
                                                EffectiveHour = c.EffectiveHour,
                                                NightHour = c.NightHour,
                                                OvertimeHour = c.OvertimeHour
                                            }
                                        }
                                    )
                                }
                            ).ToList();


            List<ArchiveFile> files = new List<ArchiveFile>();

            foreach (var user_timesheet in r)
            {
                ArchiveFile ArcFile = new ArchiveFile();
                ArcFile.Name = user_timesheet.FirstName + " " + user_timesheet.LastName;
                ArcFile.Extension = "csv";


                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };

                ContractUser contract = _context.C_ContractUser.Where(c => c.UserId == user_timesheet.Id).OrderBy(c => c.ValidTo).FirstOrDefault();


                using (var memoryStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter, config))
                {
                    csvWriter.WriteRecords(user_timesheet.TimeSheetDailyReports.Select(c => new
                    {
                        Nome = user_timesheet.FirstName,
                        Cognome = user_timesheet.LastName,
                        Commessa = c.Commessa.Name,
                        Cantiere = c.Cantiere.Name,
                        OreApp = c.TimeSheetDailyReport.EffectiveHour,
                        Ore = c.TimeSheetDailyReport.Hour,
                        Notturno = (c.TimeSheetDailyReport.NightHour != null) ? c.TimeSheetDailyReport.NightHour + "" : "00:00",
                        Straordinario = (c.TimeSheetDailyReport.OvertimeHour != null) ? c.TimeSheetDailyReport.OvertimeHour + "" : "00:00",
                        Galleria = (c.TimeSheetDailyReport.GalleryHour != null) ? c.TimeSheetDailyReport.GalleryHour + "" : "00:00",
                        CodiceTrasferta = (c.TimeSheetDailyReport.Trasferta != null) ? c.TimeSheetDailyReport.Trasferta.Code : "",
                        Trasferta = (c.TimeSheetDailyReport.Trasferta != null) ? c.TimeSheetDailyReport.Trasferta.Description : "",
                        CodiceGuistificativo = (c.TimeSheetDailyReport.Giustificativo != null) ? c.TimeSheetDailyReport.Giustificativo.Code : "",
                        Guistificativo = (c.TimeSheetDailyReport.Giustificativo != null) ? c.TimeSheetDailyReport.Giustificativo.Description : "",
                        Indennita = c.TimeSheetDailyReport.Indennita,
                        cigo = c.TimeSheetDailyReport.Cigo,
                        Ec = c.TimeSheetDailyReport.Ec,
                        CodiceContratto = (contract != null) ? contract.Code : "",
                        InizioContratto = (contract != null) ? contract.ValidFrom + "" : "",
                        FineContratto = (contract != null) ? contract.ValidTo + "" : "",
                    }).ToList());
                    streamWriter.Flush();
                    ArcFile.FileBytes = memoryStream.ToArray();

                    files.Add(ArcFile);
                }
            }

            var package = GeneratePackage(files);

            var file = MontlyDate.ToString() + "-Export.zip";

            return File(package, "application/zip", file);

        }



        public async Task<FileStreamResult> ExportMontlyReport(string userId = null, DateTime? MontlyDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var result = WriteCsvToMemory(user,_context, userId,MontlyDate);
            var memoryStream = new MemoryStream(result.FileBytes);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = result.Name + "." + result.Extension };
        }


        private byte[] GeneratePackage(List<ArchiveFile> fileList)
        {
            byte[] result;

            using (var packageStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(packageStream, ZipArchiveMode.Create, true))
                {
                    foreach (var virtualFile in fileList)
                    {
                        //Create a zip entry for each attachment
                        var zipFile = archive.CreateEntry(virtualFile.Name + "." + virtualFile.Extension);
                        using (var sourceFileStream = new MemoryStream(virtualFile.FileBytes))
                        using (var zipEntryStream = zipFile.Open())
                        {
                            sourceFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }
                result = packageStream.ToArray();
            }

            return result;
        }



        public async Task<IActionResult> ExportAllMontlyReport(DateTime? MontlyDate = null)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);




            if (MontlyDate == null) MontlyDate = DateTime.Now.Date;
            var year = MontlyDate.Value.Year;
            var month = MontlyDate.Value.Month;

            DailyReportViewModel model = new DailyReportViewModel();

            var result = 
                _context.Set<TimeSheetDailyReport>()
                .Include(c => c.User)
                .Where(c => c.WorkDate.Value.Month == month) //filtro per mese
                .Where(c => c.WorkDate.Value.Year == year) //filtro per mese
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .GroupBy( c => new { c.UserId , c.User.FirstName,c.User.LastName})
                .Select(
                    c => new
                    {
                        c.Key.UserId,
                        c.Key.FirstName,
                        c.Key.LastName,
                        
                    }
                )
                .ToList();

            List<ArchiveFile> reports = new List<ArchiveFile>();


            foreach (var id in result)
            {
                reports.Add(WriteCsvToMemory(user, _context, id.UserId, MontlyDate));
            }

            var package = GeneratePackage(reports);

            var file = MontlyDate.ToString() + "-Export.zip";

            return File(package, "application/zip", file);

        }

        public async Task<IActionResult> MontlyReportAsync(string userId = null, DateTime? MontlyDate = null)
        {

            //ALTER TABLE CDI_TEST.dbo.C_TimeSheetsDailyReport ADD isApproved bit DEFAULT 0 NULL;
            AppUser user = await userManager.GetUserAsync(HttpContext.User);            
                        
            if (MontlyDate == null) MontlyDate = DateTime.Now.Date; 
            var year = MontlyDate.Value.Year;
            var month = MontlyDate.Value.Month;
            
            DailyReportViewModel model = new DailyReportViewModel();
            // var test = "0042f49f-05d6-4d95-bf6a-f97ffd2c8fbd";  // FORZARE ID PER TEST
            model.TimeSheetDailyReportList = 
                _context.Set<TimeSheetDailyReport>()  
                .Where(c => c.WorkDate.Value.Month == month) //filtro per mese
                .Where(c => c.WorkDate.Value.Year == year) //filtro per mese
                .Where( c =>c.MultiTenantId == user.MultiTenantId)
                .Where( c => c.UserId == userId )                                       
                .Select( 
                    c => 
                        new DailyReportElementViewModel{                                
                            Turno = _context.Set<Turno>().Where(t => t.Id == c.TurnoId ).FirstOrDefault(),
                            User = _context.Set<AppUser>().Where(t => t.Id == c.UserId ).FirstOrDefault(),
                            Cantiere = _context.Set<Cantiere>().Where(t => t.Id == c.Turno.CantiereId).FirstOrDefault(),
                            Commessa = _context.Set<Project>().Where(t => t.Id == c.Turno.Cantiere.ProjectId).FirstOrDefault(),
                            //Cantiere = c.Turno.Cantiere,                        
                            //Commessa = c.Turno.Cantiere.Project,  
                            
                            TimeSheetDailyReport = new TimeSheetDailyReport{
                                Id = c.Id,
                                WorkDate = c.WorkDate,
                                TurnoId = c.TurnoId,
                                UserId = c.UserId,
                                OreEffettive = c.OreEffettive,
                                Ore = c.Ore,
                                OreStraordinarie = c.OreStraordinarie,
                                OreNotturne = c.OreNotturne,
                                OreGalleria = c.OreGalleria,
                                //OreTrasferta = c.OreTrasferta,
                                Indennita = c.Indennita,
                                Cigo = c.Cigo,
                                Ec = c.Ec,
                                Turno = c.Turno,
                                User = c.User,
                                Giustificativo = c.Giustificativo,
                                GiustificativoId = c.GiustificativoId,
                                Trasferta = c.Trasferta,
                                TrasfertaId = c.TrasfertaId,
                                StatoUtente = c.StatoUtente,
                                StatoTurno = c.StatoTurno,
                                Hour = c.Hour,
                                TravelHour = c.TravelHour,
                                GalleryHour = c.GalleryHour,
                                EffectiveHour = c.EffectiveHour,
                                NightHour = c.NightHour,
                                OvertimeHour = c.OvertimeHour,
                                isApproved = c.isApproved
                            }
                        }
                )
                .OrderBy(c => c.TimeSheetDailyReport.WorkDate.Value.Day)
                .ToList();

            var isApproved = false;
            if (!model.TimeSheetDailyReportList.IsNullOrEmpty()){
                isApproved = model.TimeSheetDailyReportList[0].TimeSheetDailyReport.isApproved;
            } 

            model.Contract = _context.Set<ContractUser>().Where(a => a.UserId == userId).FirstOrDefault();
            model.Year = year;
            model.Month = month;
            model.User = await userManager.FindByIdAsync(userId);
            ViewBag.UserId = userId;
            ViewBag.MontlyDate = MontlyDate;
            ViewBag.isApproved = isApproved;

            return View(model);
        }

        public async Task<JsonResult> ajaxIndexMontlyReport(DateTime? MontlyDate = null){            
            
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            if (MontlyDate == null) MontlyDate = DateTime.Now.Date;              

            var users = (
                from c in 
                    (from u in _context.Set<AppUser>() where u.active == 1 && u.MultiTenantId == user.MultiTenantId
                        select 
                        new
                        {

                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            GiorniLavorativi = 
                                (from c in _context.Set<TimeSheetDailyReport>()
                                        .Where( c => c.UserId == u.Id)
                                        .Where( c => c.WorkDate.Value.Month == MontlyDate.Value.Month)
                                        .Where( c => c.WorkDate.Value.Year == MontlyDate.Value.Year)     
                                        select c.Id 
                                ).Count(),
                        }                                                                      
                    )
                    .Where( x=> x.GiorniLavorativi != 0)      
                    .OrderByDescending(x=> x.GiorniLavorativi)
                    select c
                )                                        
                .OrderBy(x=> x.FirstName)                
                .ToList();                    
                
            return Json(new { data = users });
        }

        public async Task<JsonResult> ajaxEditCellMontlyReport(string? Id, 
                            string? Ore=null, string? OreStraordinarie =null,
                            string? OreNotturne=null, string? OreGalleria=null, 
                            string? OreEffettive=null, decimal? Indennita = null,
                            string? Cigo=null, string? Ec=null,
                            string? GiustificativoId = null,
                            string? TrasfertaId = null, string? OreGiustificativo = null){
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            TimeSheetDailyReport TimeSheetDailyReport = 
                _context.Set<TimeSheetDailyReport>()                  
                .Where( c => c.Id.ToString().ToLower().Equals(Id.ToString().ToLower()) )                                       
                .Where( c =>c.MultiTenantId == user.MultiTenantId)
                .FirstOrDefault();

            bool isFound = false;
            try
            {
                if (Ore != null)
                {
                    TimeSheetDailyReport.Hour = TimeSpan.Parse(Ore);
                    isFound = true;
                }
                if (OreStraordinarie != null)
                {
                    TimeSheetDailyReport.OvertimeHour = TimeSpan.Parse(OreStraordinarie);
                    isFound = true;
                }
                if (OreEffettive != null)
                {
                    TimeSheetDailyReport.EffectiveHour = TimeSpan.Parse(OreEffettive);
                    isFound = true;
                }
                if (OreNotturne != null)
                {
                    TimeSheetDailyReport.NightHour = TimeSpan.Parse(OreNotturne);
                    isFound = true;
                }
                if (Indennita != null)
                {
                    TimeSheetDailyReport.Indennita = Indennita;
                    isFound = true;
                }
                if (OreGalleria != null)
                {
                    TimeSheetDailyReport.GalleryHour = TimeSpan.Parse(OreGalleria);
                    isFound = true;
                }                

                if  (GiustificativoId != null) {
                    if (!GiustificativoId.Equals("0"))  {
                        TimeSheetDailyReport.GiustificativoId = new Guid(GiustificativoId);
                        isFound = true;                        
                    } else {
                        TimeSheetDailyReport.GiustificativoId = null;
                        TimeSheetDailyReport.OreGiustificativo = null;
                        isFound = true;                    

                    }
                }

                if (OreGiustificativo != null)
                {
                    TimeSheetDailyReport.OreGiustificativo = TimeSpan.Parse(OreGiustificativo);
                    isFound = true;
                }                

                if (TrasfertaId != null)
                {
                    TimeSheetDailyReport.TrasfertaId = new Guid(TrasfertaId);
                    isFound = true;
                }
                if (Cigo != null)
                {
                    TimeSheetDailyReport.Cigo = Cigo;
                    isFound = true;
                }
                if (Ec != null)
                {
                    TimeSheetDailyReport.Ec = Ec;
                    isFound = true;
                }                
                if (isFound)
                {
                    _context.Set<TimeSheetDailyReport>().Update(TimeSheetDailyReport);
                    _context.SaveChanges();
                }

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
            catch(Exception e)
            {
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

        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Filtraggio per Lista
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello        
        public async Task<JsonResult> ajaxIndexAsync(DateTime? WorkDate = null)
        {            
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var r = 
            (
                from c in _context.Set<TimeSheetDailyReport>()
                .Where( c=>c.MultiTenantId == user.MultiTenantId)
                .Where(tu => DateTime.Compare((DateTime) tu.WorkDate, (WorkDate ?? DateTime.Now.Date)) == 0 )
                group c by c.TurnoId into g
                select 
                    new { 
                        TurnoId = g.Key.Value, 
                        TurnoName = _context.Set<Turno>().Where(t => t.Id == g.Key.Value).Select(x=>x.Name).FirstOrDefault(),
                        CantiereId = _context.Set<Turno>().Where(t => t.Id == g.Key.Value).Select(x=>x.Cantiere.Id).FirstOrDefault(),
                        CantiereName = _context.Set<Turno>().Where(t => t.Id == g.Key.Value).Select(x=>x.Cantiere.Name).FirstOrDefault(),
                        ProjectId = _context.Set<Turno>().Where(t => t.Id == g.Key.Value).Select(x=>x.Cantiere.Project.Id).FirstOrDefault(),
                        ProjectName = _context.Set<Turno>().Where(t => t.Id == g.Key.Value).Select(x=>x.Cantiere.Project.Name).FirstOrDefault(),                    
                        count = g.Count()
                }                            
            )
            .ToList();

            return Json(
                new { data = r }
            );
        }

        [HttpPost]
        public async Task<JsonResult> ajaxApprovaReport([FromBody] JsonElement parametri){
            

            var userId    = parametri.GetProperty("userId").ToString();
            var period    = parametri.GetProperty("period").ToString();

            var anno = DateTime.Parse(period).Year;
            var mese = DateTime.Parse(period).Month;


            string query = "UPDATE C_TimeSheetsDailyReport SET isApproved = 1 WHERE  MONTH(WorkDate) = @mese AND YEAR(WorkDate) = @anno AND UserId = @userId";

            try
            {
                var result = _context.Database.ExecuteSqlRaw(query, new SqlParameter("@mese", mese), new SqlParameter("@anno", anno), new SqlParameter("@userId", userId));    
            }
            catch (Exception ex)
            {
                return Json(new { esito =0, msg = ex.Message});
            }

            return Json(new { esito =1, msg = "Ok"});

        }
        
    }
}