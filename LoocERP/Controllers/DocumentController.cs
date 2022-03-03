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
    public class DocumentController : Controller
    {
        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public DocumentController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger,
                                    IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize("Document.Show")]
        [Route("Document", Name ="DocumentIndex")]
        public async Task<IActionResult> Index(
                string? tipo = "document_index", //tipologia del tipo di documenti - Dominio
                DocumentGroup? DocumentGroup = DocumentGroup.DocumentArea, // Area dei documenti
                string? redirectUrl="DocumentIndex",  //dove ridirezionare
                string? userId = null
            )
        {                        
            var user = await userManager.GetUserAsync(HttpContext.User);
            

            DocumentViewModel model = new DocumentViewModel();                        
                                                
            model.DocumentList = _context.Set<Document>().Include( x => x.DocumentType )
                .Where(c => c.DocumentGroup == DocumentGroup)
                .Where(c => c.UserId == userId)
                .ToList();
                
            model.redirectUrl = redirectUrl;
            model.area = tipo;
            model.DocumentTypeList = _context.Set<Domain>().Where( c => c.Tipo == tipo ).ToList();            
            model.Document.DocumentGroup = DocumentGroup;
            model.Document.UserId = userId;
            model.area = tipo;

            ViewBag.Title = "Documento";
            ViewBag.sNumero = "Numero";
            ViewBag.sName = "Nome Documento";
            ViewBag.sDescription = "Descrizione Documento";
            ViewBag.sValidFrom = "Valido dal";
            ViewBag.sValidTo = "Valido al";
            ViewBag.sDocumentTypeId = "Tipologia di documento";            
            ViewBag.sDetails = "Dettagli";
            if(tipo == "document_profile"){
                ViewBag.sDetails = "Dettagli(es. Tipo patente, ...)";
            }
            if(tipo == "document_titolo_studio"){
                ViewBag.Title = "Titolo di Studio";
                ViewBag.sNumero = "Codice";
                ViewBag.sName = "Titolo di studio";
                ViewBag.sDescription = "Descrizione Titolo di studio";
                ViewBag.sValidFrom = "Data inizio corso";
                ViewBag.sValidTo = "Data di conseguimento";
                ViewBag.sDocumentTypeId = "Livello studio";  
                ViewBag.sDetails = "Altre informazioni";
            }

            return View(model);
        }


        [HttpPost]
        [Authorize("Document.Create")]
        public async Task<IActionResult> CreateAsync(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                Document d = new Document();
                // Saves the role in the underlying AspNetRoles table
                d.Id = new Guid();
                d.Name = model.Document.Name;
                d.Numero = model.Document.Numero;
                d.Description = model.Document.Description;
                d.ValidFrom = model.Document.ValidFrom;
                d.ValidTo = model.Document.ValidTo;
                d.DocumentGroup = model.Document.DocumentGroup;
                d.DocumentTypeId = model.Document.DocumentTypeId;
                d.Details = model.Document.Details;
                d.UserId = model.Document.UserId ?? null;
                d.MultiTenantId = user.MultiTenantId;

                _context.Set<Document>().Add(d);
                _context.SaveChanges();

                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Documents/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = d.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    d.FileName = trustedFileNameForFileStorage;

                    _context.Set<Document>().Update(d);
                    _context.SaveChanges();
                }

                if (model.redirectUrl != null) return Redirect(model.redirectUrl);
            }

            return RedirectToAction("Index");
        }


        [Authorize("Document.Edit")]
        public async Task<IActionResult> Edit(string Id, 
                    //string area="document_index"
                    string? tipo = "document_index", //tipologia del tipo di documenti - Dominio
                    DocumentGroup? DocumentGroup = DocumentGroup.DocumentArea, // Area dei documenti
                    string? redirectUrl="DocumentIndex",  //dove ridirezionare
                    string? userId = null
                )
        {
            DocumentViewModel model = new DocumentViewModel();
            model.area = tipo;
            //model.DocumentList = _context.Set<Document>().Where(c => c.DocumentGroup == DocumentGroup).ToList();
            model.redirectUrl = redirectUrl;
            model.DocumentTypeList = _context.Set<Domain>().Where( c => c.Tipo == tipo ).ToList();            
            model.Document.DocumentGroup = DocumentGroup;
            model.Document.UserId = userId;
            
            
            //EditDocumentViewModel model = new EditDocumentViewModel(_context, area);
            //Prendo il document corrente
            model.Document = _context.Set<Document>().Where(c => c.Id.ToString().ToLower().Equals(Id.ToLower())).FirstOrDefault();
            
            ViewBag.Title = "Documento";
            ViewBag.sNumero = "Numero";
            ViewBag.sName = "Nome Documento";
            ViewBag.sDescription = "Descrizione Documento";
            ViewBag.sValidFrom = "Valido dal";
            ViewBag.sValidTo = "Valido al";
            ViewBag.sDocumentTypeId = "Tipologia di documento";
            ViewBag.sDetails = "Dettagli";
            if(tipo == "document_profile"){
                ViewBag.sDetails = "Dettagli(es. Tipo patente, ...)";
            }
            if(tipo == "document_titolo_studio"){
                ViewBag.Title = "Titolo di studio";
                ViewBag.sNumero = "Codice";
                ViewBag.sName = "Titolo di studio";
                ViewBag.sDescription = "Descrizione Titolo di studio";
                ViewBag.sValidFrom = "Data inizio corso";
                ViewBag.sValidTo = "Data di conseguimento";
                ViewBag.sDocumentTypeId = "Livello studio";  
                ViewBag.sDetails = "Altre informazioni";
            }

            ViewBag.fileDownload = "/Upload/Documents/" + model.Document.FileName;

            return View(model);
        }


        [HttpPost]
        [Authorize("Document.Edit")]
        public async Task<IActionResult> EditAsync(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                

                Document d = _context.Set<Document>().Where( d => d.Id == model.Document.Id).FirstOrDefault();                                
                d.Name = model.Document.Name;
                d.Numero = model.Document.Numero;
                d.Description = model.Document.Description;
                d.ValidFrom = model.Document.ValidFrom;
                d.ValidTo = model.Document.ValidTo;
                d.DocumentGroup = model.Document.DocumentGroup;
                d.DocumentTypeId = model.Document.DocumentTypeId;
                d.Details = model.Document.Details;
                d.UserId = model.Document.UserId??null;
                d.MultiTenantId = user.MultiTenantId;

                d.FileName = model.Document.FileName;
                if (model.FileUpload != null && model.FileUpload.ContentType == "application/pdf" && model.FileUpload.Length <= FileSize.maxsize)
                {
                    string projectRootPath = _hostingEnvironment.WebRootPath;
                    string _targetFilePath = projectRootPath + "/Upload/Documents/";
                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(model.FileUpload.FileName);
                    var trustedFileNameForFileStorage = model.Document.Id.ToString() + Guid.NewGuid().ToString() + ".pdf";
                    var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FileUpload.CopyToAsync(fileSteam);
                    }

                    d.FileName = trustedFileNameForFileStorage;
                }


                _context.Set<Document>().Update(d);

                _context.SaveChanges();                
                if(model.redirectUrl != null) return Redirect(model.redirectUrl);
                
            }

            return RedirectToAction("Index");
        }


    }       
}