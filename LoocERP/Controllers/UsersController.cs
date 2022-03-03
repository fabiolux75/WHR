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
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using System.Text.Json;
using CollectionFilteringNetCoreSample;

namespace LoocERP.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context
                                , UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            FilterByCollectionPropertyHelper.RegisterFor<ReportUsers, Mansione>(r => r.Mansioni, m => m.Descrizione);
        }

        [Authorize("Users.Show")]
        public IActionResult IndexExt(int? isEmployee = 0, int? type = 0) {

            ViewBag.isEmployee = isEmployee;
            ViewBag.type = type;

            return View();
        }

        [Authorize("Users.Show")]
        public IActionResult Index(int? isEmployee = 0, int? type = 0)
        {
            // isEmployee = 0 tutti/admin
            // isEmployee = 1 dipendenti
            // type = 0 int
            // type = 1 int isSupplier
            // type = 2 int isCustomer
            // type = 3 ext
            // type = 4 ext isSupplier
            // type = 5 ext isCustomer
            ViewBag.isEmployee = isEmployee;
            ViewBag.type = type;
            return View();
        }

        [Authorize("Users.Create")]
        [HttpGet]
        public async Task<IActionResult> CreateAsync(int? isEmployee = 1, int? type = 0)
        {
            ViewBag.type = type;           

            CreateViewModel model = new CreateViewModel();

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<ANA_Company, bool> whereClauseActive = (a => a.active == true);
            Func<ANA_Company, bool> whereClauseParentID = (a => a.ParentID != null);

            Func<ANA_Company, bool> whereClauseType = (a => a.isExternal == false);     // type = 0 int

            if (type == 1)   // type = 1 int isSupplier
            {
                whereClauseType = (a => a.isExternal == false && a.isSupplier == true);
            }
            else if (type == 2)   // type = 2 int isCustomer
            {
                whereClauseType = (a => a.isExternal == false && a.isCustomer == true);
            }
            else if (type == 3)   // type = 3 ext // se esterna può essere padre (parentID == null)
            {
                whereClauseType = (a => a.isExternal == true);
                whereClauseParentID = (a => true);
            }
            else if (type == 4)   // type = 4 ext isSupplier
            {
                whereClauseType = (a => a.isExternal == true && a.isSupplier == true);
                whereClauseParentID = (a => true);
            }
            else if (type == 5)   // type = 5 ext isCustomer
            {
                whereClauseType = (a => a.isExternal == true && a.isCustomer == true);
                whereClauseParentID = (a => true);
            }

            model.Companies = _context.C_ANA_Companies
                            .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                            .Where(whereClauseType)
                            .Where(whereClauseActive)
                            .Where(whereClauseParentID)
                            .ToList();
            model.type = type;
            return View(model);
        }

        [Authorize("Users.Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model, int? type = 0)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);

                // Copy data from RegisterViewModel to AppUser
                AppUser usernew = new AppUser();
                usernew = model.User;
                usernew.UserName = model.User.Email;
                usernew.active = 1;
                usernew.MultiTenantId = currentUser.MultiTenantId;

                usernew.isEmployee = (model.User.isEmployee == -1) ? 0 : model.User.isEmployee;

                // Store user data in AspNetUsers database table
                //var result = await userManager.CreateAsync(user, model.Password);
                var result = await userManager.CreateAsync(usernew);

                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController

                if (result.Succeeded)
                {
                    return RedirectToAction("edit", "users", new { id = usernew.Id });
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            // Reimposta la lista Companies
            int? tipotemp = model.type;

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            Func<ANA_Company, bool> whereClauseActive = (a => a.active == true);
            Func<ANA_Company, bool> whereClauseParentID = (a => a.ParentID != null);
            Func<ANA_Company, bool> whereClauseType = (a => a.isExternal == false);     // type = 0 int
            if (tipotemp == 1)   // type = 1 int isSupplier
            {
                whereClauseType = (a => a.isExternal == false && a.isSupplier == true);
            }
            else if (tipotemp == 2)   // type = 2 int isCustomer
            {
                whereClauseType = (a => a.isExternal == false && a.isCustomer == true);
            }
            else if (tipotemp == 3)   // type = 3 ext // se esterna può essere padre (parentID == null)
            {
                whereClauseType = (a => a.isExternal == true);
                whereClauseParentID = (a => true);
            }
            else if (tipotemp == 4)   // type = 4 ext isSupplier
            {
                whereClauseType = (a => a.isExternal == true && a.isSupplier == true);
                whereClauseParentID = (a => true);
            }
            else if (tipotemp == 5)   // type = 5 ext isCustomer
            {
                whereClauseType = (a => a.isExternal == true && a.isCustomer == true);
                whereClauseParentID = (a => true);
            }

            model.Companies = _context.C_ANA_Companies
                            .Where( c  => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                            .Where(whereClauseType)
                            .Where(whereClauseActive)
                            .Where(whereClauseParentID)
                            .ToList();
            model.type = tipotemp;
            //return RedirectToAction("Create", "Users", new { type = Request.Form["type"] }); // MANTIENE LA LISTA AZIENDE MA NON FA VEDERE MESSAGGI DI ERRORE 
            return View(model);
            //return RedirectToAction("Create", "Users", type); NON SALVA I VALORI GIà INSERITI - INSERIRE PASSAGGIO DI TYPE nel create view
        }

        [Authorize("Users.Edit")]
        public async Task<IActionResult> Edit(string id, int? isEmployee = 1, int? isExternal = 0, int? type = 0)
        {
            //AppUser user = await userManager.GetUserAsync(HttpContext.User);

            AppUser user = _context.Users.Where(u => u.Id.ToLower().Equals(id.ToLower())).FirstOrDefault();


            var mansioniAssegnate = (from ma in _context.C_Rel_MansioniUser
                                     join m in _context.C_Mansioni on ma.MansioneId equals m.ID
                                     where ma.UserId == id && ma.MultiTenantId == user.MultiTenantId
                                     select m.ID
                                    ).ToList();

            var specializzazioniAssegnate = (from ma in _context.C_Rel_SpecializzazioniUser
                                             join m in _context.C_Specializzazioni on ma.SpecializzazioneId equals m.ID
                                             where ma.UserId == id
                                                    && ma.MultiTenantId == user.MultiTenantId
                                             select m.ID
                                                ).ToList();

            var mansioni = _context.C_Mansioni.Where(c => c.MultiTenantId == user.MultiTenantId).Select(
                    m => new Tuple<String, String, bool>(
                        m.ID.ToString(), m.Descrizione, mansioniAssegnate.Contains(m.ID)
                        )
                ).ToList();

            var specializzazioni = _context.C_Specializzazioni.Where(c => c.MultiTenantId == user.MultiTenantId).Select(
                    m => new Tuple<String, String, bool>(
                        m.ID.ToString(), m.Descrizione, specializzazioniAssegnate.Contains(m.ID)
                        )
                ).ToList();


            EditUserViewModel model = new EditUserViewModel();

            model.email = user.Email;

            model.enableEsecutore = (user.CodiceEsecutore != null && user.CodiceEsecutore != "");
            model.isParking = user.isParking == 1;


            model.User = _context.Users.Where(c => c.MultiTenantId == user.MultiTenantId).Where(c => c.Id.Equals(id)).FirstOrDefault();
            //model.Companies = _context.C_ANA_Companies.Where(c => c.MultiTenantId == user.MultiTenantId).Where(c => c.ParentID != null).ToList();
            model.Companies = _context.C_ANA_Companies
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(   
                    c=>
                        (c.ParentID != null && c.isExternal == false) ||
                        (c.ParentID == null && c.isExternal == true)
                )
                .Where(c => c.isExternal == (isExternal == 1))
                .ToList();
            model.Roles = await userManager.GetRolesAsync(model.User);
            model.Mansioni = mansioni;
            model.Specializzazioni = specializzazioni;
            model.DocumentViewModel = new DocumentViewModel();

            model.CompaniesInternal = _context.C_ANA_Companies
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(c =>c.ParentID != null && c.isExternal == false)
                .ToList();
           

            ViewBag.id = id;
            ViewBag.isEmployee = isEmployee;
            ViewBag.type = type;

            return View(model);
        }

        [Authorize("Users.Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, List<String> Mansioni, List<String> Specializzazioni, int isEmployee = 1, string tab = null, int? isExternal = 0)
        {
            //UTENTE
            var user = await userManager.FindByIdAsync(model.User.Id);


            model.User.Email = model.email;


            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.User.Id} cannot be found";
                return View("NotFound");
            }
            if (tab == null) return RedirectToAction("edit", "users", new { id = user.Id });

            if (tab == "edit_looc")
            {
                user.TipoPatente = model.User.TipoPatente;
                user.NumeroPatente = model.User.NumeroPatente;
                user.NfcCode = model.User.NfcCode ?? user.NfcCode;
                user.CodiceCartaCarb = model.User.CodiceCartaCarb;
                user.CodiceEsecutore = "";
                user.isParking = model.isParking?1:0;
                try
                {

                    if (model.enableEsecutore)
                    {


                        if (user.CodiceEsecutore == null || user.CodiceEsecutore == "")
                        {


                            var ie = _context.INTERV_ESECUTORI.Where(m => m.Uid.ToLower().Equals(user.Id.ToString().ToLower())).FirstOrDefault();



                            if (ie == null)
                            {
                                var multitenent = _context.C_Multitenant.Where(
                                    m => m.Id == user.MultiTenantId
                                ).FirstOrDefault();


                                int codEsecutore = _context.INTERV_ESECUTORI.Max(m => m.Codice) + 1;


                                user.CodiceEsecutore = "" + codEsecutore;


                                
                                _context.INTERV_ESECUTORI.Add(
                                    new INTERV_ESECUTORI
                                    {
                                        CodCliente = multitenent.LoocId,
                                        FlgInterno = user.isExternal == 1,
                                        Codice = codEsecutore,
                                        Descr = user.FirstName + "." + user.LastName, 
                                        DataCreazione = DateTime.Now, 
                                        DataModifica = DateTime.Now,
                                        Uid = user.Id,
                                        Note = "",
                                        OperatoreCreazione = "E.ma",
                                        OperatoreModifica = "E.ma",
                                        codSettore = "",
                                        codSottosettore = ""
                                    }
                                );

                                _context.SaveChanges();

                                Int64 codiceInt = _context.MED_INTERV_ESECUTORI_INFO.Max(m => m.Codice) + 1;

                                _context.MED_INTERV_ESECUTORI_INFO.Add(
                                    new MED_INTERV_ESECUTORI_INFO{ 
                                        AccessoAccettazione = true,
                                        Cancellato = 0,
                                        Codice = codiceInt,
                                        CodEsecutore = codEsecutore,
                                        CostoOrario = 0,
                                        DataCreazione = DateTime.Now,
                                        DataModifica = DateTime.Now,
                                        Descr = "<IT>Esecutore</IT><EN>Executor</EN>",
                                        OperatoreCreazione = "E.ma",
                                        OperatoreModifica = "E.ma",
                                        password = "",
                                        Username = "",
                                    }
                                    );

                                _context.SaveChanges();




                                /*

                                String query = @"INSERT INTO CDI.dbo.INTERV_ESECUTORI
                            (
                                CodCliente, 
                                codSettore, 
                                codSottosettore, 
                                FlgInterno, 
                                Codice, 
                                Descr, 
                                DataCreazione, 
                                DataModifica,
                                OperatoreModifica,
                                OperatoreCreazione,
                                Note,
                                Uid
                            )
                            VALUES
                            (	
                                '" + multitenent.LoocId + @"', 
	                            '',
	                            '',
	                            '" + user.isExternal + @"', 
	                            '" + codEsecutore + @"',
	                            '" + user.FirstName + @"." + user.LastName + @"', 
	                            GETDATE(), 
	                            GETDATE(),
	                            '',
	                            '',
	                            '',
                                '" + user.Id + @"'
                            );";

                                _context.Database.ExecuteSqlCommand(
                                    query
                                    );

                                int codiceInfo = _context.MED_INTERV_ESECUTORI_INFO.Max(m => m.Codice) + 1;

                                query = @"INSERT INTO CDI.dbo.INTERV_ESECUTORI 
                                            (
                                                Codice,
                                                CodEsecutore,
                                                Descr,
                                                Cancellato,
                                                DataCreazione,
                                                DataModifica
                                            )
                                            VALUES
                                            (
                                            " + codiceInfo + @",
                                            " + codEsecutore + @",
                                            '" + user.FirstName + @"." + user.LastName + @"',
                                            0,
                                             GETDATE(), 
	                                        GETDATE()
                                            )";
                                _context.Database.ExecuteSqlCommand(
                                    query
                                    );
                                    */

                            }
                            else
                            {
                                user.CodiceEsecutore = ie.Codice + "";
                            }
                        }

                    }
                }
                catch (Exception e) {
                    string a = e.Message;
                }



            }
            else if (tab == "edit_altreinformazioni")
            {
                user.IBAN = model.User.IBAN;
                user.matricola = model.User.matricola;
                user.Note = model.User.Note;
                user.StimaOraria = model.User.StimaOraria;
                user.StimaOrariaGalleria = model.User.StimaOrariaGalleria;
                user.StimaOrariaStraordinaria = model.User.StimaOrariaStraordinaria;
                user.StimaOrariaNotturna = model.User.StimaOrariaNotturna;
                user.isEnabledSupplierOrderConfirm = model.User.isEnabledSupplierOrderConfirm;
                user.isEnabledSupplierBonifico = model.User.isEnabledSupplierBonifico;
                user.maxCostSupplierOrderConfirm = model.User.maxCostSupplierOrderConfirm;

            }
            else if (tab == "edit_profile")
            {
                //profile
                List<Rel_MansioneUser> mans = _context.C_Rel_MansioniUser.Where(m => m.UserId == model.User.Id).ToList();
                _context.C_Rel_MansioniUser.RemoveRange(_context.C_Rel_MansioniUser.Where(m => m.UserId == model.User.Id));                
                //_context.C_Rel_SpecializzazioniUser.RemoveRange(_context.C_Rel_SpecializzazioniUser.Where(m => m.UserId == model.User.Id));
                foreach (var m in Mansioni)
                {
                    _context.C_Rel_MansioniUser.Add(new Rel_MansioneUser
                    {
                        MansioneId = new Guid(m),
                        UserId = model.User.Id
                    });
                }
                _context.SaveChanges();

                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.Email = model.User.Email;
                user.UserName = Guid.NewGuid().ToString();
                if (user.Email != null)
                    user.UserName = model.User.Email;
                user.PhoneNumber = model.User.PhoneNumber;
                user.Gender = model.User.Gender;
                user.IDCompany = model.User.IDCompany;
                user.CodiceFiscale = model.User.CodiceFiscale;
                user.Nazione = model.User.Nazione;
                user.Provincia = model.User.Provincia;
                user.Regione = model.User.Regione;
                user.Citta = model.User.Citta;
                user.Cap = model.User.Cap;
                user.Indirizzo = model.User.Indirizzo;
                user.isEmployee = isEmployee;//      
                user.InternalCode = model.User.InternalCode;
                user.InternalCompanyReferenceId = model.User.InternalCompanyReferenceId;
                user.ComuneNascita = model.User.ComuneNascita;
                user.DataNascita = model.User.DataNascita;
                user.CellularNumber = model.User.CellularNumber;
                user.PrivateNumber = model.User.PrivateNumber;
            }

            //common
            user.DataModifica = System.DateTime.Now;
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            user.IDModificatoDa = currentUser.Id;


            _context.SaveChanges();
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("edit", "users", new { id = user.Id, isEmployee = isEmployee });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("edit", "users", new { id = user.Id });

        }

        [Authorize("Users.Edit")]
        [HttpPost]
        public async Task<IActionResult> updateImage([FromForm(Name = "companyImage")] IFormFile companyImage, string codice = null)
        {
            var Image = companyImage;
            byte[] img = null;
            if (Image != null)

            {
                if (Image.Length > 0)

                //Convert Image to byte and save to database

                {

                    byte[] p1 = null;
                    using (var fs1 = Image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    img = p1;

                }
            }

            var user = await userManager.FindByIdAsync(codice);
            user.ProfilePicture = img;
            user.DataModifica = System.DateTime.Now;
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            user.IDModificatoDa = currentUser.Id;

            _context.SaveChanges();
            await userManager.UpdateAsync(user);
            return RedirectToAction("edit", new { id = codice });
        }

        [Authorize("Users.Edit")]
        [HttpPost]
        public async Task<IActionResult> updatePassword(EditUserViewModel model)
        {

            //UpdatePasswordViewModel model = modelin.UpdatePasswordViewModel;
            //var user = await userManager.FindByIdAsync(model.UserId);
            var user = await userManager.FindByIdAsync(model.User.Id);
            user.DataModifica = System.DateTime.Now;
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            user.IDModificatoDa = currentUser.Id;
            // compute the new hash string
            //var newPassword = userManager.PasswordHasher.HashPassword(user, model.Password);            
            var newPassword = new PasswordHasher<AppUser>().HashPassword(user, model.UpdatePasswordViewModel.Password);
            user.PasswordHash = newPassword;
            user.EmailConfirmed = true;

            var result = await userManager.UpdateAsync(user);

            //if (result.Succeeded)
            //{
                return RedirectToAction("edit", "users", new { id = user.Id });
            //}
            //return RedirectToAction("edit", new { id = "9610c381-85fa-4a88-b92e-ad8f5d92704a" });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<FileResult> getProfilePicture(String id = null)
        {
            if (id != null)
            {
                var user = await userManager.FindByIdAsync(id);
                if (user != null && user.ProfilePicture != null)
                {
                    return File(user.ProfilePicture, "image/png");
                }
            }

            return File("~/theme/images/user_icon.png", "image/png");

        }

        [Authorize("Users.Edit")]
        [HttpPost]
        public async Task<IActionResult> assignRole(List<string> ruoli, string id = null)
        {
            var user = userManager.FindByIdAsync(id).Result;
            var roles = userManager.GetRolesAsync(user).Result.ToList();

            foreach (var r in roles)
            {
                await userManager.RemoveFromRoleAsync(user, r);
            }

            foreach (var r in ruoli)
            {
                await userManager.AddToRoleAsync(user, r);
            }
            return RedirectToAction("edit", "Users", new { id = id });
        }

        /*
        public String test()
        {
            String query = @"INSERT INTO CDI.dbo.INTERV_ESECUTORI
                            (
                                CodCliente, 
                                codSettore, 
                                codSottosettore, 
                                FlgInterno, 
                                Codice, 
                                Descr, 
                                DataCreazione, 
                                DataModifica,
                                OperatoreModifica,
                                OperatoreCreazione,
                                Note 
                            )
                            VALUES
                            (	'SMOEUR', 
	                            '',
	                            '',
	                            0, 
	                            (select max(Codice)+1 from INTERV_ESECUTORI),
	                            'test.test', 
	                            GETDATE(), 
	                            GETDATE(),
	                            '',
	                            '',
	                            ''
                            );";
            return "test!";
        }

        */

        /// <summary>
        /// Companies <c>ajaxIndex</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>
        /// <param>level</param> Filtra le aziende per livello
        /// <param>ParentID</param> Filtra le aziende per ParentId        
        /// <param>type</param> 0 = tutti, 1 = Employee
        public async Task<JsonResult> ajaxIndexAsync(int? isEmployee = 1, string? IdCompany = null, string? ParentID = null, int? type = -1)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            try
            {
                List<AppUser> r = new List<AppUser>();

                //var roles = userManager.GetUsersInRoleAsync("administrator").Result;
                //var ok = roles.Contains(user);

                var ret = _context.Users
                    .Include(x => x.Company)
                    .Include(c => c.ContractUsers)
                    //.Include(x => x.Rel_MansioneUser)
                    //.ThenInclude( m => m.Select(c  => c.Mansione))
                    .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                    .Where(a => (ParentID != null) ? a.Company.ParentID == new Guid(ParentID) : true)
                    .Where(a => (isEmployee == 1) ? a.isEmployee == isEmployee : true)
                    .Where(a => (IdCompany != null) ? a.IDCompany == new Guid(IdCompany) : true)
                    .Where(a => (type == 0) ? a.Company.isExternal == false : true)
                    .Where(a => (type == 1) ? (a.Company.isExternal == false && a.Company.isSupplier == true) : true)
                    .Where(a => (type == 2) ? (a.Company.isExternal == false && a.Company.isCustomer == true) == false : true)
                    .Where(a => (type == 3) ? (a.Company.isExternal == true) : true)
                    .Where(a => (type == 4) ? (a.Company.isExternal == true && a.Company.isSupplier == true) : true)
                    .Where(a => (type == 5) ? (a.Company.isExternal == true && a.Company.isCustomer == true) : true)
                    //.Where(a => (ok) ? true : a.IDCompany == user.IDCompany)
                    .Select(
                        x => new
                            {
                                Id = x.Id,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                PhoneNumber = x.PhoneNumber,
                                InternalCode = x.InternalCode,
                                DataModifica = x.DataModifica,
                                Citta = x.Citta,
                                contract = x.ContractUsers
                                            .OrderBy(c => c.ValidTo).Select(
                                    x => new
                                    {
                                        x.MesiRinnovo,
                                        x.ValidFrom,
                                        x.ValidTo,
                                        x.Stato
                                    }
                                    ).First()
                            }
                        )
                    .ToList();

                    /*
                    .Select(x => new AppUser
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNumber = x.PhoneNumber,                        
                        InternalCode = x.InternalCode,
                    })
                    .ToList();
                    */
                return Json(
                    new
                    {
                        data = ret
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


        public async Task<JsonResult> ajaxUsersSelect2Async(string? search = "")
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
           Func<AppUser, bool> whereSearching = (a => true); //default sempre vera
           if (search != null) whereSearching = (item => item.FirstName.ToUpper().Contains(search.ToUpper()));//(item => search.ToString().ToLower().Any(search => item.FirstName.ToLower().Contains(search.ToString().ToLower())));

            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = userManager.Users
                .Where(c => (user.MultiTenantId != null)? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(item => (search != null) ? item.FirstName.ToUpper().Contains(search.ToUpper()) : true)
                .Select(x => new Select2ViewModel
                {
                    id = x.Id,
                    text = x.FirstName + " " + x.LastName,
                })
                .OrderBy(x => x.text)
                .ToList();

            return Json(
                new
                {
                    results = r
                }
           );
        }


        [HttpGet]
         public async Task<IActionResult> GetUsers(DataSourceLoadOptions loadOptions, int? isEmployee = 1, string? IdCompany = null, string? ParentID = null, int? type = -1) {
        
             AppUser user = await userManager.GetUserAsync(HttpContext.User);


            //https://entityframeworkcore.com/knowledge-base/57437000/many-to-many-ef-core---unable-to-determine-the-relationship

           /* var res = _context.Users
             .Include(z => z.Rel_MansioneUser)
             .Include(x => x.Company)
             .Include(c => c.ContractUsers)
             .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
             .Where(a => (ParentID != null) ? a.Company.ParentID == new Guid(ParentID) : true)
             .Where(a => (isEmployee == 1) ? a.isEmployee == isEmployee : true)
             .Where(a => (IdCompany != null) ? a.IDCompany == new Guid(IdCompany) : true)
             .Where(a => (type == 0) ? a.Company.isExternal == false : true)
             .Where(a => (type == 1) ? (a.Company.isExternal == false && a.Company.isSupplier == true) : true)
             .Where(a => (type == 2) ? (a.Company.isExternal == false && a.Company.isCustomer == true) == false : true)
             .Where(a => (type == 3) ? (a.Company.isExternal == true) : true)
             .Where(a => (type == 4) ? (a.Company.isExternal == true && a.Company.isSupplier == true) : true)
             .Where(a => (type == 5) ? (a.Company.isExternal == true && a.Company.isCustomer == true) : true)
             .Select(
                 x => new
                 {
                     Id = x.Id,
                     // Mansioni = String.Join(", ",x.Rel_MansioneUser.Select(x => x.Mansione.Descrizione).ToList()),
                     Mansioni = x.Rel_MansioneUser.Select(x => x.Mansione.Descrizione),
                     MansioniDue = string.Join(", ", x.Rel_MansioneUser.Select(x => x.Mansione.Descrizione)),
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     PhoneNumber = x.PhoneNumber,
                     InternalCode = x.InternalCode,
                     DataModifica = x.DataModifica,
                     DataNascita = x.DataNascita,
                     Citta = x.Citta,
                     contract = x.ContractUsers
                                 .OrderBy(c => c.ValidTo).Select(
                         x => new
                         {
                             x.MesiRinnovo,
                             x.ValidFrom,
                             x.ValidTo,
                             x.Stato
                         }
                         ).First()
                 }
                 );*/


            var res = _context.Users
             .Include(z => z.Rel_MansioneUser)
             .Include(x => x.Company)
             .Include(c => c.ContractUsers)
             .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
             .Where(a => (ParentID != null) ? a.Company.ParentID == new Guid(ParentID) : true)
             .Where(a => (isEmployee == 1) ? a.isEmployee == isEmployee : true)
             .Where(a => (IdCompany != null) ? a.IDCompany == new Guid(IdCompany) : true)
             .Where(a => (type == 0) ? a.Company.isExternal == false : true)
             .Where(a => (type == 1) ? (a.Company.isExternal == false && a.Company.isSupplier == true) : true)
             .Where(a => (type == 2) ? (a.Company.isExternal == false && a.Company.isCustomer == true) == false : true)
             .Where(a => (type == 3) ? (a.Company.isExternal == true) : true)
             .Where(a => (type == 4) ? (a.Company.isExternal == true && a.Company.isSupplier == true) : true)
             .Where(a => (type == 5) ? (a.Company.isExternal == true && a.Company.isCustomer == true) : true)
             .Select(
                 x => new ReportUsers
                 {
                    Id = x.Id,
                    // Mansioni = String.Join(", ",x.Rel_MansioneUser.Select(x => x.Mansione.Descrizione).ToList()),
                    Mansioni = x.Rel_MansioneUser.Select(x => new Mansione{
                                                                    Descrizione = x.Mansione.Descrizione
                                                                }),
                    //MansioniDue = string.Join(", ",x.Rel_MansioneUser.Select(x => x.Mansione.Descrizione)),                                                                
                    //MansioniDue = "PIPPO,PLUTO",                                                                
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    InternalCode = x.InternalCode,
                    DataModifica = x.DataModifica,
                    DataNascita = x.DataNascita,
                    Citta = x.Citta,
                    contract = x.ContractUsers
                                .OrderBy(c => c.ValidTo)
                                .Select(
                                        x => new ContractUser
                                        {
                                            MesiRinnovo = x.MesiRinnovo,
                                            ValidFrom = x.ValidFrom,
                                            ValidTo = x.ValidTo,
                                            Stato = x.Stato
                                        }
                                        ).First()
                    
                 }
                 );

            
            // var sql = ((System.Data.Objects.ObjectQuery)query).ToTraceString();
            // foreach (var item in res)
            // {

            //    foreach (var mm in item.Mansioni)
            //    {
            //         item.MansioniDue = "TEST";
            //         var b = 0;
            //     }
            // }

            //var q = res.AsQueryable();

            /*if (loadOptions.Filter != null && loadOptions.Filter[0].ToString() == "Mansioni"){
                var test = res;
                res = res.Where(z => z.Mansioni.Contains("OP"));
            }*/

            /*loadOptions.Filter = null;*/


//https://supportcenter.devexpress.com/ticket/details/t1011408/how-to-filter-datagrid-by-a-column-bound-to-a-collection
            //https://supportcenter.devexpress.com/ticket/details/t637631/filtering-by-column-bound-to-many-to-many-relation
            //https://supportcenter.devexpress.com/Ticket/Details/T603821/how-to-filter-grid-by-column-bound-to-many-to-many-relation
            //https://supportcenter.devexpress.com/ticket/details/t1011408/how-to-filter-datagrid-by-a-column-bound-to-a-collection

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            

             return Json(await DataSourceLoader.LoadAsync(res, loadOptions), new JsonSerializerOptions
             {
                 PropertyNamingPolicy = null,
             });
        }        

    }
}
