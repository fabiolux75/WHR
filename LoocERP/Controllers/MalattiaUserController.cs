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
    public class MalattiaUserController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public MalattiaUserController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("MalattiaUser.Show")]
        public async Task<IActionResult> Index(string? userId = null, string? redirectUrl = null)
        {

            

            var user = await userManager.GetUserAsync(HttpContext.User);
             
            MalattiaUserViewModel model = new MalattiaUserViewModel();   
            model.MalattiaUser.UserId = userId;
                                                         
            Func<MalattiaUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.MalattiaUserList = _context.Set<MalattiaUser>().Include(x => x.VisitaMedica).Where(whereClause).ToList();
            
            model.redirectUrl = redirectUrl;

            return View(model);
        }


        [HttpPost]
        [Authorize("MalattiaUser.Create")]
        public async Task<IActionResult> CreateAsync(MalattiaUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                
                Document d = new Document();
                // Saves the role in the underlying AspNetRoles table
                model.MalattiaUser.Id = new Guid();               
                model.MalattiaUser.MultiTenantId = user.MultiTenantId;
               
                _context.Set<MalattiaUser>().Add(model.MalattiaUser);
                _context.SaveChanges();
                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Malattia/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.MalattiaUser.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    model.MalattiaUser.FileName = trustedFileNameForFileStorage;

                    _context.Set<MalattiaUser>().Update(model.MalattiaUser);
                    _context.SaveChanges();
                }

                if (model.redirectUrl != null) return Redirect(model.redirectUrl);                
            }
                        
            return Redirect(model.redirectUrl);
        }


        [Authorize("MalattiaUser.Edit")]
        public async Task<IActionResult> Edit(string Id, string? userId = null, string? redirectUrl = null)
        {
            MalattiaUserViewModel model = new MalattiaUserViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(userId == null) userId = user.Id;

            model.MalattiaUser = _context.Set<MalattiaUser>().Include(x => x.VisitaMedica)
                .Where(c => c.Id.ToString().ToLower().Equals(Id.ToString().ToLower()) )
                .FirstOrDefault();

            ViewBag.fileDownload = "/Upload/Malattia/" + model.MalattiaUser.FileName;

            return View(model);
        }


        [HttpPost]
        [Authorize("MalattiaUser.Edit")]
        public async Task<IActionResult> EditAsync(MalattiaUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                
                
                MalattiaUser d = _context.Set<MalattiaUser>().Where( d => d.Id == model.MalattiaUser.Id).FirstOrDefault();

                d.Tipologia = model.MalattiaUser.Tipologia;
                d.TipoEvento = model.MalattiaUser.TipoEvento;
                d.ValidFrom = model.MalattiaUser.ValidFrom;
                d.ValidTo = model.MalattiaUser.ValidTo;
                d.Certificato = model.MalattiaUser.Certificato;
                d.CertificatoDiRiferimento = model.MalattiaUser.CertificatoDiRiferimento;
                d.DataRilascioCertificato = model.MalattiaUser.DataRilascioCertificato;
                d.DataConsegnaCertificato = model.MalattiaUser.DataConsegnaCertificato;
                d.DataPartoPresunta = model.MalattiaUser.DataPartoPresunta;
                d.DataPartoEffettiva = model.MalattiaUser.DataPartoEffettiva;
                d.NomeFiglio = model.MalattiaUser.NomeFiglio;
                d.Medico = model.MalattiaUser.Medico;

                d.FileName = model.MalattiaUser.FileName;
                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Malattia/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.MalattiaUser.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    d.FileName = trustedFileNameForFileStorage;
                }
        
               _context.Set<MalattiaUser>().Update(d);

                _context.SaveChanges();                
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);
                
            }

            if(model.redirectUrl != null) return Redirect(model.redirectUrl);
            return Redirect(model.redirectUrl);
        }

    }
        
}