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

namespace LoocERP.Controllers
{
    public class RemoteSetup : Controller
    {
        private readonly ILogger<RemoteSetup> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;
        
        public RemoteSetup(  ApplicationDBContext context
                            ,UserManager<AppUser> userManager
                            ,ILogger<RemoteSetup> logger)
        {

            _context = context;
            _logger = logger;
            userManager = userManager;
        }

        //Prima pagina di atterraggio
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Policy = "ManageUser")]
        public IActionResult Create()
        {
            return View();
        }        

        [HttpPost]
        public IActionResult Create(RemoteSetup? remotesetup)
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Policy = "ManageUser")]
        public IActionResult Edit() { 
            return View();
        }

        [HttpPost]
        public IActionResult Edit(string? id)
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Policy = "ManageUser")]
        public async Task<string> UpdateMapDeviceAsync(int id,string desc, string codVett,string codCli,char stato)
        {            
            var dev = _context.Devices.Where(d => d.idDevice == id).FirstOrDefault();
            dev.Description = desc;
            dev.CodiceCliente = codCli;
            dev.CodiceVettore = codVett;
            dev.StatoDevice = stato;

            _context.SaveChanges();

            return "ok";
        }


        [HttpGet]
        //[Authorize(Policy = "ManageUser")]
        public string update(int idDevice,string imeiDevice,int setupType,string telefono,string codiceVettore,string autista,string azienda,DateTime dataRichiesta,int id)
        {
            var rt = _context.RemoteSetup.Where(r => r.Id == id).FirstOrDefault();
            var user = _context.Users.Where(u => u.Id == autista).FirstOrDefault();
            rt.SetupType = setupType;
            rt.CodiceAutista = autista;

            var device = _context.Devices.Where(d => d.Id == idDevice).FirstOrDefault();

            if(device != null)
            {
                device.Description = String.Format("{0} - {1} {2}",rt.Id,user.LastName,user.FirstName);
            }




            _context.SaveChanges();
            return "ok";
        }





        /// <summary>
        /// Companies <c>ajaxIndex</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>
        /// <param>level</param> Filtra le aziende per livello
        /// <param>ParentID</param> Filtra le aziende per ParentId
        /// /// <param>ParentID</param> Filtra per il campo active
        public JsonResult ajaxIndex()
        {            
            //se definito filtro per classe prodotto            
            //Func<RemoteSetup, bool> whereClause = (c => true); //default sempre vera
            //if (ParentID != null) whereClause = (c => c.id == new Guid(Id));
            Func<RemoteSetupModel, bool> whereClause = (a => true); //default le prendo tute
            //if (active == 1) whereClauseEmployee = (a => a.isEmployee == isViewEmployee);

            List<RemoteSetupModel> r = new List<RemoteSetupModel>();
            r = _context.RemoteSetup
                .Where(whereClause)
                .ToList();

            var res = from rs in r
                      from d in _context.Devices.Where(c => rs.IdDevice == c.idDevice)
                      select new
                      {
                          rs.Id,
                          rs.IdDevice,
                          rs.ImeiDevice,
                          rs.LogoAz,
                          rs.ModelloVettore,
                          rs.NomeAutista,
                          rs.PrivacyFlag,
                          rs.Provenienza,
                          rs.RequestLatitude,
                          rs.RequestLongitude,
                          rs.RequestStatus,
                          rs.SetupType,
                          rs.SimSerialNumber,
                          rs.Stato,
                          rs.StatoAccount,
                          rs.Targa,
                          rs.Telefono,
                          rs.TelephoneNumber,
                          rs.TimeoutAccount,
                          rs.UriProd,
                          rs.UriTest,
                          rs.UserId,
                          rs.DataRichiesta,
                          rs.CodiceAziendaHr,
                          d.CodiceAutista,
                          d.CodiceCliente,
                          d.CodiceVettore,
                          d.Description,
                          d.StatoDevice

                      };
                      

            var devices = _context.Devices.ToList();
            return Json(
                new
                {
                    data = res
                }
           );
        }

    }
}
