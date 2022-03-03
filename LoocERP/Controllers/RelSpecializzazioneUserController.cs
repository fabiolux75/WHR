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
    public class RelSpecializzazioneUserController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public RelSpecializzazioneUserController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        [Authorize("RelSpecializzazioneUser.Show")]
        public async Task<IActionResult> Index(string? userId = null, string? redirectUrl = null)
        {                        
            var user = await userManager.GetUserAsync(HttpContext.User);
            
            RelSpecializzazioneUserViewModel model = new RelSpecializzazioneUserViewModel();
            
            model.RelSpecializzazioneUser.UserId = userId;
                                                         
            Func<Rel_SpecializzazioneUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.RelSpecializzazioneUserList = _context.Set<Rel_SpecializzazioneUser>().Include(x => x.Specializzazione).Include(x => x.Corsi).Where(whereClause).ToList();
            model.SpecializzazioneList = _context.Set<Specializzazione>().OrderBy(c => c.Descrizione).ToList();
            model.redirectUrl = redirectUrl;
            return View(model);
        }


        [HttpPost]
        [Authorize("RelSpecializzazioneUser.Create")]
        public async Task<IActionResult> CreateAsync(RelSpecializzazioneUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                
                Document d = new Document();
                // Saves the role in the underlying AspNetRoles table
                model.RelSpecializzazioneUser.Id = new Guid();
                
                model.RelSpecializzazioneUser.MultiTenantId = user.MultiTenantId;

               _context.Set<Rel_SpecializzazioneUser>().Add(model.RelSpecializzazioneUser);

                _context.SaveChanges();

                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Specializzazioni/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.RelSpecializzazioneUser.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    model.RelSpecializzazioneUser.FileName = trustedFileNameForFileStorage;

                    _context.Set<Rel_SpecializzazioneUser>().Update(model.RelSpecializzazioneUser);
                    _context.SaveChanges();
                }

                if (model.redirectUrl != null) return Redirect(model.redirectUrl);                
            }
                        
            return Redirect(model.redirectUrl);
        }


        [Authorize("RelSpecializzazioneUser.Edit")]
        public async Task<IActionResult> Edit(string Id, string? userId = null, string? redirectUrl = null)
        {
            RelSpecializzazioneUserViewModel model = new RelSpecializzazioneUserViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(userId == null) userId = user.Id;

            model.RelSpecializzazioneUser = _context.Set<Rel_SpecializzazioneUser>()
                .Where(c => c.Id.ToString().ToLower().Equals(Id.ToString().ToLower()) )
                .FirstOrDefault();

            Func<Rel_SpecializzazioneUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.RelSpecializzazioneUserList = _context.Set<Rel_SpecializzazioneUser>().Include(x => x.Corsi).Where(whereClause).ToList();
            model.SpecializzazioneList = _context.Set<Specializzazione>().OrderBy(c => c.Descrizione).ToList();
            model.redirectUrl = redirectUrl;

            ViewBag.fileDownload = "/Upload/Specializzazioni/" + model.RelSpecializzazioneUser.FileName;

            return View(model);
        }


        [HttpPost]
        [Authorize("RelSpecializzazioneUser.Edit")]
        public async Task<IActionResult> EditAsync(RelSpecializzazioneUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                

                Rel_SpecializzazioneUser d = _context.Set<Rel_SpecializzazioneUser>().Where( d => d.Id == model.RelSpecializzazioneUser.Id).FirstOrDefault();   
                d.Code = model.RelSpecializzazioneUser.Code;
                d.Name = model.RelSpecializzazioneUser.Name;
                d.Description = model.RelSpecializzazioneUser.Description;
                d.StartDate = model.RelSpecializzazioneUser.StartDate;
                d.EndDate = model.RelSpecializzazioneUser.EndDate;
                d.ValidFrom = model.RelSpecializzazioneUser.ValidFrom;
                d.ValidTo = model.RelSpecializzazioneUser.ValidTo;
                d.ReleasedFrom = model.RelSpecializzazioneUser.ReleasedFrom;
                d.ReleasedAt = model.RelSpecializzazioneUser.ReleasedAt;
                d.Vote = model.RelSpecializzazioneUser.Vote;
                d.isPromosso = model.RelSpecializzazioneUser.isPromosso;
                d.SpecializzazioneId = model.RelSpecializzazioneUser.SpecializzazioneId;

                d.FileName = model.RelSpecializzazioneUser.FileName;
                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Specializzazioni/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.RelSpecializzazioneUser.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    d.FileName = trustedFileNameForFileStorage;
                }


                _context.Set<Rel_SpecializzazioneUser>().Update(d);

                _context.SaveChanges();                
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);
                
            }

            if(model.redirectUrl != null) return Redirect(model.redirectUrl);
            return Redirect(model.redirectUrl);
        }

    }
        
}