using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Data;
using LoocERP.Models;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using CsvHelper;
using System.Globalization;
using DevExtreme.AspNet.Mvc.Builders.DataSources;
using CsvHelper.Configuration;
using ServiceStack;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using System.Text.Json;

namespace LoocERP.Controllers
{
    /// <summary>Class <c>CompaniesController</c>
    /// Gestione totale della classe Company
    /// </summary>
    ///
    [AllowAnonymous]
    public class CompaniesController : Controller
    {
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CompaniesController(ApplicationDBContext context
                                 ,UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        //[Authorize("Companies.Show")]
        public IActionResult Index(bool? isSupplier = false, bool? isCustomer = false, bool? isOfficina = false, bool? isExternal = true, bool? active = true)
        {
            //var companies = _context.C_ANA_Companies.Where(c => c.ParentID == null).ToList();
            ViewBag.isSupplier = isSupplier;
            ViewBag.isCustomer = isCustomer;
            ViewBag.isOfficina = isOfficina;
            ViewBag.isExternal = isExternal;
            ViewBag.active = active;
            return View();
        }

        /*Create con padre*/
        [HttpGet]
        //[Authorize("Companies.Create")]
        public async Task<IActionResult> Create(Guid? parentID = null, bool? isSupplier = false, bool? isCustomer = false, bool? isOfficina = false, bool? isExternal = false, bool? active = true)
        {
            CreateCompanyViewModel model = new CreateCompanyViewModel();
            if (parentID != null) //vuol dire che proviene da un padre
            {
                model.Company.ID = (Guid)parentID;
            }

            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            

            model.Companies = _context.C_ANA_Companies
                    .Where(c => (user.MultiTenantId != null) ?  c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()): true)
                    .Where(a => a.ParentID == parentID)
                    .Where(a => a.isExternal == isExternal)
                    .ToList();

            ViewBag.isSupplier = isSupplier;
            ViewBag.isCustomer = isCustomer;
            ViewBag.isOfficina = isOfficina;
            ViewBag.isExternal = isExternal;
            ViewBag.parentID = parentID;
            ViewBag.active = active;

            return View(model);
        }

        [HttpPost]
        //[Authorize("Companies.Create")]
        public async Task<IActionResult> CreateAsync(CreateCompanyViewModel model, string? parentID = null, bool? isSupplier = false, bool? isCustomer = false, bool? isOfficina = false, bool? isExternal = false, bool? active = true)
        {
            if (ModelState.IsValid)
            {
                if (parentID != null) model.Company.ParentID = new Guid(parentID);

                AppUser user = await userManager.GetUserAsync(HttpContext.User);

                ANA_Company comp = new ANA_Company
                {
                    InternalCode = model.Company.InternalCode ?? "",
                    Indirizzo = model.Company.Indirizzo,
                    PIva = model.Company.PIva,
                    Fax = model.Company.Fax,
                    Telefono = model.Company.Telefono,
                    Nazione = model.Company.Nazione,
                    Regione = model.Company.Regione,
                    Provincia = model.Company.Provincia,
                    Citta = model.Company.Citta,
                    EmailAziendale = model.Company.EmailAziendale,
                    EmailPec = model.Company.EmailPec,
                    FiscalCode = model.Company.FiscalCode,
                    CodiceSdi = model.Company.CodiceSdi,
                    RagioneSociale = model.Company.RagioneSociale,
                    SitoWeb = model.Company.SitoWeb,
                    ParentID = (model.Company.ParentID != null ? model.Company.ParentID : null),
                    MultiTenantId = (user.MultiTenantId ?? null),
                    isExternal = isExternal,
                    isSupplier = isSupplier,
                    isCustomer = isCustomer,
                    //isOfficina = model.Company.isOfficina
                    Banca = model.Company.Banca,
                    IBAN = model.Company.IBAN,
                    Pagamento = model.Company.Pagamento,
                    isCopiedOnLooc = model.Company.isCopiedOnLooc,  // ?? false
                    DatevCode = model.Company.DatevCode,
                    TipoPagamentoID = model.Company.TipoPagamentoID,
                    ModalitaPagamentoID = model.Company.ModalitaPagamentoID,
                    PagheCodAzienda = model.Company.PagheCodAzienda,
                    PagheCodFiliale = model.Company.PagheCodFiliale,
                    CAP = model.Company.CAP
                };
                // Saves the role in the underlying AspNetRoles table
                var currentCompanyHR = _context.C_ANA_Companies.Add(comp);

                _context.SaveChanges(user.Id, true);

                //------------------------------------------------------------------------------------------------------------------------------------------------------------------
                bool isCopiedOnLooc = model.Company.isCopiedOnLooc;// ?? false;
                if (isCopiedOnLooc) {
                    int currentTipo = 1;
                    string currentFL_Tipo = "F";
                    if (isCustomer.Value)
                    {
                        currentTipo = 0;
                        currentFL_Tipo = "C";
                    }
                    var azienda = _context.GOF_cliente_fornitore_esterno;
                    var azienda_rel = _context.gof_cliente_fornitore;

                    int codComune = 8205;
                    int codProvincia = 128;
                    int codRegione = 52;

                    if (model.Company.Citta != null) {
                        codComune = _context.COMUNI.Where(c => c.Descr == model.Company.Citta).Select(c => c.Codice).Single();
                        codProvincia = _context.COMUNI.Where(c => c.Codice == codComune).Select(c => c.CodPro).Single();
                        codRegione = _context.PROVINCE.Where(c => c.Codice == codProvincia).Select(c => c.CodReg).Single();
                    }                   

                    Looc_GOF_Cliente_Fornitore_esterno new_azienda = new Looc_GOF_Cliente_Fornitore_esterno
                    {
                        //Codice
                        CodFiscale = model.Company.FiscalCode,
                        RagSoc = model.Company.RagioneSociale,
                        PIVA = model.Company.PIva,
                        codRegione = codRegione,
                        codProvincia = codProvincia,
                        codComune = codComune,
                        Indirizzo = model.Company.Indirizzo ?? "Non definito",
                        cap = "",
                        Località = model.Company.Citta,
                        Telefono1 = model.Company.Telefono,
                        Telefono2 = "",
                        Cellulare = "",
                        Fax = "",
                        Email = model.Company.EmailAziendale,
                        Referente = "",
                        RefTelefono = "",
                        RefCellulare = "",
                        RefEmail = "",
                        DataCreazione = DateTime.Now,
                        OperatoreCreazione = user.FirstName,
                        DataModifica = DateTime.Now,
                        OperatoreModifica = user.FirstName, //modificare su DB se si vuole User.Id perchè troppo lungo
                                                            //SMOC = ""
                        IdAziendaHR = currentCompanyHR.Entity.ID,
                        MultiTenantId = currentCompanyHR.Entity.MultiTenantId
                    };

                    var currentCompanyLooc = azienda.Add(new_azienda);
                    _context.SaveChanges();


                    Looc_GOF_Cliente_Fornitore new_azienda_rel = new Looc_GOF_Cliente_Fornitore
                    {
                        //Codice
                        CodSettore = "",
                        //CodSottoSettore
                        CodCliente = _context.C_Multitenant.Where(c => c.Id == user.MultiTenantId).Select(c => c.LoocId).Single(), //FK
                        //CodClienteIdentita //FK
                        CodClienteEsternoIdentita = _context.GOF_cliente_fornitore_esterno.OrderByDescending(a => a.Codice).Select(a => a.Codice).FirstOrDefault(), //FK
                        Tipo = currentTipo,
                        FL_Tipo = currentFL_Tipo,    // Vincolo ([FL_Tipo] = 'C' or [FL_Tipo] = 'F')  
                        //Sconto
                        Pagamento = (int) model.Company.Pagamento.Value, //FK
                        Banca = model.Company.Banca,
                        IBAN = model.Company.IBAN
                        //codEsecutore
                    };

                    azienda_rel.Add(new_azienda_rel);

                    _context.SaveChanges();
                }
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------

                return RedirectToAction("index", new { isExternal = isExternal });
            }

            ViewBag.isSupplier = isSupplier;
            ViewBag.isCustomer = isCustomer;
            ViewBag.isOfficina = isOfficina;
            ViewBag.isExternal = isExternal;
            ViewBag.parentID = parentID;
            ViewBag.active = active;
            return View(model);
        }

        [HttpGet]
        //[Authorize("Companies.Edit")]
        public async Task<IActionResult> Edit(string id = null, string parentID = null, bool? isSupplier = false, bool? isCustomer = false, bool? isOfficina = false, bool isExternal = false, bool? active = true)
        {
            //utente corrente
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var company = _context.C_ANA_Companies.Where(c => c.ID == new Guid(id)).Include(c => c.Parent).First();

            var companies = _context.C_ANA_Companies.
                Where(
                    d => d.ID != company.ID
                    //  && ( (parentID == null ) || d.ID != new Guid(parentID)  )  
                    && d.ParentID == company.ID
                    && (d.MultiTenantId == user.MultiTenantId)
                )
                .Select(
                    c => new ANA_Company
                    {
                        ID = c.ID,
                        Nazione = c.Nazione,
                        Regione = c.Regione,
                        Provincia = c.Provincia,
                        Citta = c.Citta,
                        RagioneSociale = c.RagioneSociale,
                        EmailAziendale = c.EmailAziendale,
                        EmailPec = c.EmailPec,
                        CodiceSdi = c.CodiceSdi,
                        FiscalCode = c.FiscalCode,
                        Fax = c.Fax,
                        Indirizzo = c.Indirizzo,
                        ParentID = c.ParentID,
                        //Parent = c.Parent,
                        PIva = c.PIva,
                        SitoWeb = c.SitoWeb,
                        Telefono = c.Telefono,
                        isCustomer = c.isCustomer,
                        isSupplier = c.isSupplier,
                        isCopiedOnLooc = c.isCopiedOnLooc,
                        DatevCode = c.DatevCode,
                        TipoPagamentoID = c.TipoPagamentoID,
                        PagheCodAzienda = c.PagheCodAzienda,
                        PagheCodFiliale = c.PagheCodFiliale,
                        ModalitaPagamentoID = c.ModalitaPagamentoID,
                        CAP = c.CAP
                        //IsSelected = (cmps.Contains(c))
                    }
                ).ToList();

            EditCompanyViewModel vm = new EditCompanyViewModel();

            vm.Company.EmailAziendale = company.EmailAziendale;
            vm.Company.EmailPec = company.EmailPec;
            vm.Company.CodiceSdi = company.CodiceSdi;
            vm.Company.FiscalCode = company.FiscalCode;
            vm.Company.Fax = company.Fax;
            vm.Company.Nazione = company.Nazione;
            vm.Company.Regione = company.Regione;
            vm.Company.Provincia = company.Provincia;
            vm.Company.Citta = company.Citta;
            vm.Company.Telefono = company.Telefono;
            vm.Company.SitoWeb = company.SitoWeb;
            vm.Company.RagioneSociale = company.RagioneSociale;
            vm.Company.PIva = company.PIva;
            vm.Company.ParentID = company.ParentID;
            vm.Company.Indirizzo = company.Indirizzo;
            vm.Company.ID = company.ID;
            vm.Companies = (companies.Count() != 0 ? companies : new List<ANA_Company>());
            vm.Company.Parent = company.Parent;
            vm.Company.active = company.active;
            vm.Company.InternalCode = company.InternalCode;
            vm.Company.Banca = company.Banca;
            vm.Company.IBAN = company.IBAN;
            vm.Company.Pagamento = company.Pagamento;
            vm.Company.isCustomer = company.isCustomer;
            vm.Company.isSupplier = company.isSupplier;
            vm.Company.isCopiedOnLooc = company.isCopiedOnLooc;
            vm.Company.DatevCode = company.DatevCode;
            vm.Company.TipoPagamentoID = company.TipoPagamentoID;
            vm.Company.ModalitaPagamentoID = company.ModalitaPagamentoID;
            vm.Company.PagheCodFiliale = company.PagheCodFiliale;
            vm.Company.PagheCodAzienda = company.PagheCodAzienda;
            vm.Company.CAP = company.CAP;
            vm.isExternal = isExternal;
            //vm.Users = _.Where(a => Guid.Parse(a.IDCompany) == company.ID).ToList();

            ViewBag.isSupplier = isSupplier;
            ViewBag.isCustomer = isCustomer;
            ViewBag.isOfficina = isOfficina;
            ViewBag.isExternal = isExternal;
            ViewBag.active = active;

            return View(vm);
        }

        [HttpPost]
        //[Authorize("Companies.Edit")]
        public async Task<IActionResult> Edit(EditCompanyViewModel model)
        {
            var cp = _context.C_ANA_Companies.Where(c => c.ID == model.Company.ID).Include(c => c.Parent).FirstOrDefault();
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            if (cp.ID == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Company.ID} cannot be found";
                return View("NotFound");
            }
            else
            {
                cp.InternalCode = model.Company.InternalCode ?? "";
                cp.RagioneSociale = model.Company.RagioneSociale;
                cp.PIva = model.Company.PIva;
                cp.EmailPec = model.Company.EmailPec;
                cp.CodiceSdi = model.Company.CodiceSdi;
                cp.Nazione = model.Company.Nazione;
                cp.Regione = model.Company.Regione;
                cp.Provincia = model.Company.Provincia;
                cp.Citta = model.Company.Citta;
                cp.Indirizzo = model.Company.Indirizzo;
                cp.EmailAziendale = model.Company.EmailAziendale;
                cp.SitoWeb = model.Company.SitoWeb;
                cp.Telefono = model.Company.Telefono;
                cp.Fax = model.Company.Fax;
                cp.FiscalCode = model.Company.FiscalCode;
                cp.active = model.Company.active;
                cp.Banca = model.Company.Banca;
                cp.IBAN = model.Company.IBAN;
                cp.Pagamento = model.Company.Pagamento;
                cp.isCopiedOnLooc = model.Company.isCopiedOnLooc;
                cp.DatevCode = model.Company.DatevCode;
                cp.TipoPagamentoID = model.Company.TipoPagamentoID;
                cp.ModalitaPagamentoID = model.Company.ModalitaPagamentoID;
                cp.PagheCodAzienda = model.Company.PagheCodAzienda;
                cp.PagheCodFiliale = model.Company.PagheCodFiliale;
                cp.CAP = model.Company.CAP;


                model.Company.ParentID = cp.ParentID;

                var currentCompanyHR = _context.C_ANA_Companies.Update(cp);
                _context.SaveChanges(userManager.GetUserId(HttpContext.User), true);


                if (model.Company.isCopiedOnLooc) //se vero effettuo update/insert in Looc
                {
                    //bool esiste = false;
                    var checkAzienda = _context.GOF_cliente_fornitore_esterno.Where(c => c.MultiTenantId == currentCompanyHR.Entity.MultiTenantId).Where(c => c.IdAziendaHR == currentCompanyHR.Entity.ID).FirstOrDefault();
                    //if (checkAzienda != null)
                    //{
                    //    esiste = true;
                    //}

                    if (checkAzienda != null) { //update Looc
                        int currentTipo = 1;
                        string currentFL_Tipo = "F";
                        if (currentCompanyHR.Entity.isCustomer.Value)
                        {
                            currentTipo = 0;
                            currentFL_Tipo = "C";
                        }
                        var azienda = _context.GOF_cliente_fornitore_esterno.Where(c => c.MultiTenantId == currentCompanyHR.Entity.MultiTenantId).Where(c => c.IdAziendaHR == currentCompanyHR.Entity.ID).FirstOrDefault();
                        var azienda_rel = _context.gof_cliente_fornitore.Where(c => c.CodClienteEsternoIdentita == azienda.Codice).FirstOrDefault();

                        int codComune = 8205;
                        int codProvincia = 128;
                        int codRegione = 52;

                        if (model.Company.Citta != null)
                        {
                            codComune = _context.COMUNI.Where(c => c.Descr == model.Company.Citta).Select(c => c.Codice).FirstOrDefault();
                            codProvincia = _context.COMUNI.Where(c => c.Codice == codComune).Select(c => c.CodPro).FirstOrDefault();
                            codRegione = _context.PROVINCE.Where(c => c.Codice == codProvincia).Select(c => c.CodReg).FirstOrDefault();
                        }                        

                        //Codice
                        azienda.CodFiscale = model.Company.FiscalCode;
                        azienda.RagSoc = model.Company.RagioneSociale;
                        azienda.PIVA = model.Company.PIva;
                        azienda.codRegione = codRegione;
                        azienda.codProvincia = codProvincia;
                        azienda.codComune = codComune;
                        azienda.Indirizzo = model.Company.Indirizzo ?? "Non definito";
                        azienda.cap = "";
                        azienda.Località = model.Company.Citta;
                        azienda.Telefono1 = model.Company.Telefono;
                        azienda.Telefono2 = "";
                        azienda.Cellulare = "";
                        azienda.Fax = "";
                        azienda.Email = model.Company.EmailAziendale;
                        azienda.Referente = "";
                        azienda.RefTelefono = "";
                        azienda.RefCellulare = "";
                        azienda.RefEmail = "";
                        azienda.DataCreazione = DateTime.Now;
                        azienda.OperatoreCreazione = user.FirstName;
                        azienda.DataModifica = DateTime.Now;
                        azienda.OperatoreModifica = user.FirstName; //modificare su DB se si vuole User.Id perchè troppo lungo
                        //SMOC = ""
                        azienda.IdAziendaHR = currentCompanyHR.Entity.ID;
                        azienda.MultiTenantId = currentCompanyHR.Entity.MultiTenantId;


                        var currentCompanyLooc = _context.GOF_cliente_fornitore_esterno.Update(azienda);
                        _context.SaveChanges();


                        //Codice
                        azienda_rel.CodSettore = "";
                        //CodSottoSettore
                        azienda_rel.CodCliente = _context.C_Multitenant.Where(c => c.Id == user.MultiTenantId).Select(c => c.LoocId).FirstOrDefault(); //FK
                                                                                                                                               //CodClienteIdentita //FK
                        azienda_rel.CodClienteEsternoIdentita = azienda.Codice; //FK
                        azienda_rel.Tipo = currentTipo;
                        azienda_rel.FL_Tipo = currentFL_Tipo;    // Vincolo ([FL_Tipo] = 'C' or [FL_Tipo] = 'F')  
                                                                 //Sconto
                        azienda_rel.Pagamento = (int)model.Company.Pagamento.Value; //FK
                        azienda_rel.Banca = model.Company.Banca;
                        azienda_rel.IBAN = model.Company.IBAN;
                        //codEsecutore

                        _context.gof_cliente_fornitore.Update(azienda_rel);

                        _context.SaveChanges();
                    }
                    else //insert Looc
                    {
                        int currentTipo = 1;
                        string currentFL_Tipo = "F";
                        if (currentCompanyHR.Entity.isCustomer.Value)
                        {
                            currentTipo = 0;
                            currentFL_Tipo = "C";
                        }
                        var azienda = _context.GOF_cliente_fornitore_esterno;
                        var azienda_rel = _context.gof_cliente_fornitore;

                        int codComune = 8205;
                        int codProvincia = 128;
                        int codRegione = 52;


                        if (model.Company.Citta != null)
                        {
                            codComune = _context.COMUNI.Where(c => c.Descr == model.Company.Citta).Select(c => c.Codice).FirstOrDefault();
                            codProvincia = _context.COMUNI.Where(c => c.Codice == codComune).Select(c => c.CodPro).FirstOrDefault();
                            codRegione = _context.PROVINCE.Where(c => c.Codice == codProvincia).Select(c => c.CodReg).FirstOrDefault();
                        }                        

                        Looc_GOF_Cliente_Fornitore_esterno new_azienda = new Looc_GOF_Cliente_Fornitore_esterno
                        {
                            //Codice
                            CodFiscale = model.Company.FiscalCode,
                            RagSoc = model.Company.RagioneSociale,
                            PIVA = model.Company.PIva,
                            codRegione = codRegione,
                            codProvincia = codProvincia,
                            codComune = codComune,
                            Indirizzo = model.Company.Indirizzo ?? "Non definito",
                            cap = "",
                            Località = model.Company.Citta,
                            Telefono1 = model.Company.Telefono,
                            Telefono2 = "",
                            Cellulare = "",
                            Fax = "",
                            Email = model.Company.EmailAziendale,
                            Referente = "",
                            RefTelefono = "",
                            RefCellulare = "",
                            RefEmail = "",
                            DataCreazione = DateTime.Now,
                            OperatoreCreazione = user.FirstName,
                            DataModifica = DateTime.Now,
                            OperatoreModifica = user.FirstName, //modificare su DB se si vuole User.Id perchè troppo lungo
                                                                //SMOC = ""
                            IdAziendaHR = currentCompanyHR.Entity.ID,
                            MultiTenantId = currentCompanyHR.Entity.MultiTenantId
                        };

                        var currentCompanyLooc = azienda.Add(new_azienda);
                        _context.SaveChanges();


                        Looc_GOF_Cliente_Fornitore new_azienda_rel = new Looc_GOF_Cliente_Fornitore
                        {
                            //Codice
                            CodSettore = "",
                            //CodSottoSettore
                            CodCliente = _context.C_Multitenant.Where(c => c.Id == user.MultiTenantId).Select(c => c.LoocId).FirstOrDefault(), //FK
                                                                                                                                       //CodClienteIdentita //FK
                            CodClienteEsternoIdentita = _context.GOF_cliente_fornitore_esterno.OrderByDescending(a => a.Codice).Select(a => a.Codice).FirstOrDefault(), //FK
                            Tipo = currentTipo,
                            FL_Tipo = currentFL_Tipo,    // Vincolo ([FL_Tipo] = 'C' or [FL_Tipo] = 'F')  
                                                         //Sconto
                            Pagamento = (int)model.Company.Pagamento.Value, //FK
                            Banca = model.Company.Banca,
                            IBAN = model.Company.IBAN
                            //codEsecutore
                        };

                        azienda_rel.Add(new_azienda_rel);

                        _context.SaveChanges();
                    }
                }

                //return View(model);
                return RedirectToAction("Edit", new { id = cp.ID, isExternal = model.isExternal });
            }
        }

        [HttpPost]
        //[Authorize("Companies.Edit")]
        public async Task<IActionResult> updateImage([FromForm(Name = "companyImage")] IFormFile companyImage, bool isExternal, string codice = null)
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
            var vettore = _context.C_ANA_Companies.Where(v => v.ID.ToString().Equals(codice)).FirstOrDefault();
            vettore.Img = img;
            _context.SaveChanges();
            return RedirectToAction("Edit", new { id = codice , isExternal = isExternal});
        }

        [AllowAnonymous]
        [HttpGet]
        public FileResult getCompanyPicture(String id = null)
        {
            try
            {
                var vettore = _context.C_ANA_Companies.Where(v => v.ID.ToString().Equals(id)).FirstOrDefault();
                if (vettore != null && vettore.Img != null)
                {
                    return File(vettore.Img, "image/png");
                }
                else
                {
                    return File("~/theme/images/noImage.png", "image/png");
                }
            }
            catch(Exception e)
            {
                return File("~/theme/images/noImage.png", "image/png");
            }
           

        }

        /// <summary>
        /// Companies <c>ajaxIndex</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>
        /// <param>level</param> Filtra le aziende per livello
        /// <param>ParentID</param> Filtra le aziende per ParentId
        /// /// <param>ParentID</param> Filtra per il campo active
        public async Task<JsonResult> ajaxIndexAsync(string? ParentID = null, bool? active = true, int? isSupplier = null, int? isCustomer = null, bool? isOfficina = false, bool? isExternal = false)
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            Func<ANA_Company, bool> whereClauseParent = (c => c.ParentID == null); //default sempre vera
            if (ParentID != null) whereClauseParent = (c => c.ParentID == new Guid(ParentID));

            //Func<ANA_Company, bool> whereClauseActive = (a => true); //default le prendo tute
            Func<ANA_Company, bool> whereClauseActive = (a => a.active == active);

            Func<ANA_Company, bool> whereClauseisSupplier = ( c => true ); //default sempre vera
            if (isSupplier == 0 || isSupplier == 1) whereClauseisSupplier = (c => c.isSupplier == (isSupplier==1?true:false));

            Func<ANA_Company, bool> whereClauseisCustomer = (c => true); //default sempre vera
            if (isCustomer == 0 || isCustomer == 1) whereClauseisCustomer = (c => c.isCustomer == (isCustomer == 1 ? true : false));

            List<ANA_Company> r = new List<ANA_Company>();
            r = _context.C_ANA_Companies
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(whereClauseParent)
                .Where(whereClauseActive)
                .Where(whereClauseisSupplier)
                .Where(whereClauseisCustomer)
                .Where(a => a.isExternal == isExternal)
                .Select(x => new ANA_Company
                {
                    RagioneSociale = x.RagioneSociale,
                    PIva = x.PIva,
                    Citta = x.Citta,
                    active = x.active,
                    ParentID = x.ParentID,
                    isExternal = x.isExternal,
                    isSupplier = x.isSupplier,
                    isCustomer = x.isCustomer,
                    ID = x.ID,
                    isCopiedOnLooc = x.isCopiedOnLooc
                })
                .OrderBy(c => c.RagioneSociale)
                .ToList();

            return Json(
                new
                {
                    data = r
                }
           );
        }

        /// <summary>
        /// Companies <c>VerifyPiva</c> 
        /// Crea annotazione che consente di controllare se esiste o meno una partita IVA sul database
        /// </summary>        
        /// <param>Piva</param> Partita iva
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPiva(string Piva)
        {
            ANA_Company r = new ANA_Company();
            r = _context.C_ANA_Companies.Where(p => p.PIva == Piva).FirstOrDefault();
            if (r == null) return Json("Partita IVA già presente nel database");            

            return Json(true);
        }



        public async Task<JsonResult> ajaxAddVettoreAsync(int IdVettore, Guid? IdCompany = null)
        {

            /*
            var company = _context.C_ANA_Companies.Where(c => c.ID == IdCompany).FirstOrDefault();



            int found = _context.C_VettoreAssegnazione
                .Include(c => c.Leasing)
                .Include(c => c.Noleggio)
                .Where(c => c.CodiceVettore == IdVettore &&
               (
                   (c.Leasing != null) ? c.Leasing.EndDate <= leasing.EndDate : false
                   ||
                   (c.Noleggio != null) ? c.Noleggio.EndDate <= leasing.EndDate : false
               ))
                .Count();



            if (found == 0)
            {
                VettoreAssegnazione vettoreAssegnazione = new VettoreAssegnazione();
                vettoreAssegnazione.CodiceVettore = IdVettore;
                vettoreAssegnazione.NoleggioId = null;
                vettoreAssegnazione.LeasingId = IdLeasing;
                _context.C_VettoreAssegnazione
                    .Add(vettoreAssegnazione);

                _context.SaveChanges();

                return Json(new { result = "OK" });
            }




            */


            return Json(new
            {
                result = "KO",
                reason = "Vettore già in un leasing o noleggio",
            });
        }


        public async Task<JsonResult> ajaxRemoveVettoreAsync(string idAssegnazione = null)
        {


            var v = _context.C_VettoreAssegnazione.Where(c => c.Id == new Guid(idAssegnazione)).FirstOrDefault();
            if (v != null)
            {
                _context.C_VettoreAssegnazione.Remove(v);
                _context.SaveChanges();
            }
            return Json(new { result = "OK" });
        }


        public async Task<JsonResult> ajaxCompanySelect2Async(string? search = "")
        {
            //Filtro multitenant
            AppUser user = await userManager.GetUserAsync(HttpContext.User);


            List<Select2ViewModel> r = new List<Select2ViewModel>();
            r = _context.C_ANA_Companies
                .Where(c => (user.MultiTenantId != null) ? c.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(item => (search != null) ? item.RagioneSociale.ToUpper().Contains(search.ToUpper()) : true)
                .Select(x => new Select2ViewModel
                {
                    id = x.ID.ToString(),
                    text = x.RagioneSociale,
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

        [HttpPost]
        public async Task<IActionResult> ImportCsv (IFormFile file_csv) {

            string fileName = file_csv.FileName;
            
            string extension = Path.GetExtension(fileName);

            if (extension != ".csv" ) {
                return Json(new {  success = false, msg = "Attenzione: caricare file con estensione .csv"  });
            }

            if (file_csv == null || file_csv.Length == 0) {

                return Content("file not selected");
            } 

            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                        Delimiter = ";",
                };
            

            using var memoryStream = new MemoryStream(new byte[file_csv.Length]);
            await file_csv.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using (var reader =  new StreamReader(memoryStream))      
            using (var csvReader = new CsvReader(reader, config))
            {
                csvReader.Read();
                csvReader.ReadHeader();
                
                var records = csvReader.GetRecords<CompanyCsv>().ToList();

                var transaction = _context.Database.BeginTransaction();
                foreach (var record in records)
                 {
                    try
                    {


                            ANA_Company company = new ANA_Company();
                            company.RagioneSociale      = record.RagioneSociale;
                            company.PIva                = record.PIva;
                            company.FiscalCode          = record.FiscalCode;
                            company.EmailPec            = record.EmailPec;
                            company.EmailAziendale      = record.EmailAziendale;
                            company.CodiceSdi           = record.CodiceSdi;
                            company.Nazione             = "ITA";
                            company.Regione             = record.Regione.ToUpper();
                            company.Provincia           = record.Provincia.ToUpper();
                            company.Citta               = record.Citta;
                            company.CAP                 = record.CAP;
                            company.Indirizzo           = record.Indirizzo;
                            company.SitoWeb             = record.SitoWeb;
                            company.Telefono            = record.Telefono;
                            company.Fax                 = record.Fax;
                            company.isSupplier          = true;
                            company.isCustomer          = false;
                            company.isOfficina          = false;
                            company.ParentID            = null;
                            company.Img                 = null;
                            company.active              = true;
                            company.isExternal          = true;
                            company.Banca               = record.Banca;
                            company.IBAN                = record.IBAN;
                            company.DatevCode           = record.DatevCode;

                            if (record.TipoPagamentoID == null) {
                                company.TipoPagamentoID     = null;
                            } else {
                                TipoPagamento tipoPag = (TipoPagamento)Enum.Parse(typeof(TipoPagamento), record.TipoPagamentoID.ToString());
                                company.TipoPagamentoID     = tipoPag;

                            }

                            if (record.ModalitaPagamentoID == null){
                                company.ModalitaPagamentoID = null;
                            } else {
                                ModPagamento modPag   = (ModPagamento)Enum.Parse(typeof(ModPagamento), record.ModalitaPagamentoID.ToString());
                                company.ModalitaPagamentoID = modPag;
                            }

                            
                            company.MultiTenantId   = user.MultiTenantId;

                            _context.C_ANA_Companies.Add(company);
                            _context.SaveChanges();
                        //Console.WriteLine("Inserita: " + company.RagioneSociale);

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new {  success = false, msg = ex.Message  });

                    }
                }                
                    transaction.Commit(); 
            }        

            return Json(new {  success = true});

        }


        [HttpGet]
        public async Task<IActionResult> GetCompanies(int tipoUtente, DataSourceLoadOptions loadOptions) {


            var lookup = _context.C_ANA_Companies
                        .Where(p => p.isExternal.HasValue.Equals(true))
                        .Where(p => (tipoUtente == 1)? p.isCustomer == true : p.isSupplier == true)
                        .Select( x => new {
                             ID = x.ID,
                             Descrizione = x.RagioneSociale                            
                        });

            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }        
    }
}