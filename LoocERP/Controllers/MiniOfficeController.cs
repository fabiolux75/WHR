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


namespace LoocERP.Controllers
{
    public class MiniOfficeController : Controller
    {
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public MiniOfficeController(ApplicationDBContext context
                                 , UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        //ritorna la lista delle nazioni - BLOCCHIAMO ALL'ITALIA
        public JsonResult getNazioni(string alpha3 = "ITA")
        {
            var r = new SFERA.MiniOffice.Elenchi.Nazioni().Elenco().Where(c => c.Codice_Alpha3 == alpha3).ToList();
            return Json(
                new
                {
                    data = r
                }
           );
        }

        //ritorna la lista delle regioni
        public JsonResult getRegioni()
        {
            var r = new SFERA.MiniOffice.Elenchi.Regioni().Elenco().ToList();
            return Json(
                new
                {
                    data = r
                }
           );
        }

        //ritorna la lista delle province
        public JsonResult getProvince(string regione = "")
        {
            var r = new SFERA.MiniOffice.Elenchi.Province().Elenco().Where(p => p.Regione == regione).ToList();
            return Json(
                new
                {
                    data = r
                }
           );
        }

        //ritorna la lista dei comuni
        public JsonResult getComuni(string provincia = "")
        {

            var tmp = new SFERA.MiniOffice.Elenchi.Comuni()
                .Elenco()
                .Where(c  => c.Provincia != null)
                .Where(c  => c.Provincia.Sigla == provincia)
                .Select(c => new
            {
                nome = c.Nome,
            }).Distinct();


            

            return Json(
                new
                {
                    data = tmp,
                }
                );

            

            var r = new SFERA.MiniOffice.Elenchi.CAP().Elenco()
                .Where(c => c.Provincia == provincia)
                .Select(
                    c => new
                    {
                        nome = c.Comune,
                    }
                    
                )
                .Distinct()
                .ToList();
 
            
            
            
            
            
            
            
            
            /*
            var r = new SFERA.MiniOffice.Elenchi.Comuni().Elenco()
                .Where(p => p.Provincia.Sigla == provincia )
                .ToList();
                */
            return Json(
                new
                {
                    data = r
                }
           );
        }

        public JsonResult getCaps(string comune = "")
        {




            var r = new SFERA.MiniOffice.Elenchi.CAP()
                
                .Elenco()
                .Where(c=> c.Comune == comune)
                .Select(c=> c.CAP)
                .ToList();
            //var r = new SFERA.MiniOffice.Elenchi.Comuni().Elenco().Where(c => c.Nome.ToLower().Equals(comune.ToLower())).ToList();



            return Json(
                new
                {
                    data = r
                }
           );

            /*
            var r = new SFERA.MiniOffice.Elenchi.Province().Elenco().Where(p => p.Regione == regione).ToList();
            return Json(
                new
                {
                    data = r
                }
           );
           */
        }
    }
}