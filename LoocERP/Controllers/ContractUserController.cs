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
    public class ContractUserController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public ContractUserController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize("ContractUser.Show")]
        public async Task<IActionResult> Index(string? userId = null, string? redirectUrl = null)
        {                        
            var user = await userManager.GetUserAsync(HttpContext.User);
             
            ContractUserViewModel model = new ContractUserViewModel();   
            model.ContractUser.UserId = userId;
            model.ContractUser.Stato = 1; //default attivo
                                                         
            Func<ContractUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.ContractUserList = _context.Set<ContractUser>().Where(whereClause).OrderByDescending(c => c.Stato).ToList();

            model.LevelList = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_level")).OrderBy(c => c.Name).ToList();
            model.RetribuzioneTypeList = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_tipo_retribuzione")).OrderBy(c => c.Name).ToList();
            model.OreDiLavoroTypeList = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_tipo_orario")).OrderBy(c => c.Name).ToList();
            model.ContractTypeList = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_tipologia")).OrderBy(c => c.Name).ToList();
            model.LawNumberTypeList = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_law_number")).OrderBy(c => c.Name).ToList();
            model.CategoryCodeList = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_category")).OrderBy(c => c.Name).ToList();

            model.redirectUrl = redirectUrl;

            ViewBag.nContrattiAttivi = _context.Set<ContractUser>().Where(whereClause).Where( c => c.Stato == 1).Count();

            return View(model);
        }

        [HttpPost]
        [Authorize("ContractUser.Create")]
        public async Task<IActionResult> CreateAsync(ContractUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                
                Document d = new Document();
                // Saves the role in the underlying AspNetRoles table
                model.ContractUser.Id = new Guid();
                model.ContractUser.Stato = 1; //Contratto Attivo
               
                model.ContractUser.MultiTenantId = user.MultiTenantId;

                _context.Set<ContractUser>().Add(model.ContractUser);
                _context.SaveChanges();

                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Contract/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.ContractUser.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    model.ContractUser.FileName = trustedFileNameForFileStorage;

                    _context.Set<ContractUser>().Update(model.ContractUser);
                    _context.SaveChanges();
                }

                if (model.redirectUrl != null) return Redirect(model.redirectUrl);                
            }
                        
            return RedirectToAction("Index");
        }

        [Authorize("ContractUser.Edit")]
        public async Task<IActionResult> Edit(string Id, string? userId = null, string? redirectUrl = null)
        {
            ContractUserViewModel model = new ContractUserViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(userId == null) userId = user.Id;

            model.ContractUser = _context.Set<ContractUser>().Include(c=>c.Level)
                .Where(c => c.Id.ToString().ToLower().Equals(Id.ToString().ToLower()) )
                .FirstOrDefault();

            Func<ContractUser, bool> whereClause = (a => true); //default le prendo tutte
            if (userId != null) whereClause = (a => a.UserId.ToString().ToLower().Equals(userId.ToString().ToLower()));
            model.ContractUserList = _context.Set<ContractUser>().Where(whereClause).ToList();

            var l = _context.Set<Domain>().Where(c => c.Tipo.Equals("contract_user_level")).Where(c => c.ParentId != null).OrderBy(c => c.Name).FirstOrDefault();
            model.hasParentLevel = (l == null ? false : true);

            model.LevelList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_level") ).OrderBy(c => c.Name).ToList();
            model.RetribuzioneTypeList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_tipo_retribuzione") ).OrderBy(c => c.Name).ToList();
            model.OreDiLavoroTypeList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_tipo_orario") ).OrderBy(c => c.Name).ToList();
            model.ContractTypeList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_tipologia") ).OrderBy(c => c.Name).ToList();
            model.LawNumberTypeList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_law_number") ).OrderBy(c => c.Name).ToList();
            model.CategoryCodeList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_category") ).OrderBy(c => c.Name).ToList();
            model.redirectUrl = redirectUrl;

            ViewBag.fileDownload = "/Upload/Contract/" + model.ContractUser.FileName;

            return View(model);
        }

        [HttpPost]
        [Authorize("ContractUser.Edit")]
        public async Task<IActionResult> EditAsync(ContractUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                

                ContractUser d = _context.Set<ContractUser>().Where( d => d.Id == model.ContractUser.Id).FirstOrDefault();   
                d.LevelId = model.ContractUser.LevelId;
                d.RetribuzioneTypeId = model.ContractUser.RetribuzioneTypeId;
                d.OreDiLavoroTypeId = model.ContractUser.OreDiLavoroTypeId;
                d.ContractTypeId = model.ContractUser.ContractTypeId;
                d.LawNumberTypeId = model.ContractUser.LawNumberTypeId;
                d.CategoryCodeId = model.ContractUser.CategoryCodeId;
                d.ValidFrom = model.ContractUser.ValidFrom;
                d.ValidTo = model.ContractUser.ValidTo;
                d.MesiRinnovo = model.ContractUser.MesiRinnovo;

                d.FileName = model.ContractUser.FileName;
                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Contract/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.ContractUser.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    d.FileName = trustedFileNameForFileStorage;
                }

                _context.Set<ContractUser>().Update(d);

                _context.SaveChanges();                
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);
                
            }

            if(model.redirectUrl != null) return Redirect(model.redirectUrl);
            return RedirectToAction("Index");
        }

        [Authorize("ContractUser.Edit")]
        public async Task<IActionResult> Close(string Id, string? userId = null, string? redirectUrl = null)
        {
            ContractUserViewModel model = new ContractUserViewModel();
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(userId == null) userId = user.Id;

            model.ContractUser = _context.Set<ContractUser>().Where(c => c.Id.ToString().ToLower().Equals(Id.ToString().ToLower()) ).FirstOrDefault();

            model.FineRapportoList = _context.Set<Domain>().Where(c=> c.Tipo.Equals("contract_user_fine_rapporto") ).OrderBy(c => c.Name).ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize("ContractUser.Edit")]
        public async Task<IActionResult> CloseAsync(ContractUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                

                ContractUser d = _context.Set<ContractUser>().Where( d => d.Id == model.ContractUser.Id).FirstOrDefault();   
                d.FineRapportoId = model.ContractUser.FineRapportoId;
                d.ValidTo = model.ContractUser.ValidTo;
                d.FineContrattoNota = model.ContractUser.FineContrattoNota;
                d.Stato = model.ContractUser.Stato;
        
               _context.Set<ContractUser>().Update(d);

                _context.SaveChanges();                
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);
                
            }

            if(model.redirectUrl != null) return Redirect(model.redirectUrl);
            return RedirectToAction("Index");
        }

    }
        
}