using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoocERP.Models;
using LoocERP.Data;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using LoocERP.Helpers;

namespace LoocERP.Controllers
{
    //Dimensione massima per l'upload di file
    public static class FileSize
    {
        public static long maxsize = 5242880;
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorizationService AuthorizationService;

        public HomeController(
            UserManager<AppUser> userManager,
            ILogger<HomeController> logger, 
            ApplicationDBContext context,
             IAuthorizationService AuthorizationService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            this.AuthorizationService = AuthorizationService;
        }

        //Prima pagina di atterraggio
        public async Task<IActionResult> IndexAsync()
        {

            if (!(await AuthorizationService.AuthorizeAsync(User, "Home.Show")).Succeeded)
            {
                return View("IndexMain");
            }

            //COMMESSE
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
           

            ViewBag.NumeroCommesse = _context.C_Projects
                .Where(x => (x.MultiTenantId != null )? x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()) : true)
                .Where(x => x.Active == 1).Count();

            //CANTIERI
            ViewBag.NumeroCantieri = _context.Set<Cantiere>()
                .Where(x => x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()))
                .Where(x => x.Active == true).Count();

            //UTENTI INTERNI
            ViewBag.NumeroUtentiInterni = _userManager.Users
                .Include(x => x.Company)
                .Where(x => x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()))
                .Where(x => x.isEmployee == 1)
                .Where(x => x.Company.isExternal == false).Count();

            //UTENTI esterni
            ViewBag.NumeroUtentiEsterni = _userManager.Users
                .Include(x => x.Company)
                .Where(x => x.MultiTenantId.ToString().ToLower().Equals(user.MultiTenantId.ToString().ToLower()))
                .Where(x => x.isEmployee == 1)
                .Where(x => x.Company.isExternal == true).Count();

            //            var cInt = c.Where(x => x.Company.isExternal == false);
            //            var cEst = c.Where(x => x.Company.isExternal == true);
            //            ViewBag.NumeroUtentiInterni = cInt.Count();
            //            ViewBag.NumeroUtentiEsterni = cEst.Count();


            //var vettore = _context.C_ANA_Companies.Where(v => v.ID.ToString().Equals(codice)).FirstOrDefault();

            return View();
        }
        public IActionResult IndexMain()
        {
            return View();
        }
        public IActionResult IndexSettings()
        {
            return View();
        }

        public IActionResult IndexHR()
        {
            return View();
        }

        public async Task<IActionResult> IndexHRAnagraficaAsync()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.GestioneAnagrafica, "Gestione Anagrafica",false);
            return View();
        }
        public async Task<IActionResult> IndexHRProjectAsync()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.GestioneCommesse, "Gestione Commesse",false);
            return View();
        }
        public async Task<IActionResult> IndexGestioneMovimentoAsync()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.GestioneMovimento, "Gestione Movimento",false);
            return View();
        }
        public IActionResult IndexLogisticaSupporto()
        {
            return View();
        }

        public IActionResult IndexManutenzione()
        {
            return View();
        }
        public IActionResult IndexPianiManutenzione()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }                

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
