using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Data;
using LoocERP.Models;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization;

namespace LoocERP.Controllers
{
    public class RelFabbisognoMansione : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public RelFabbisognoMansione(ApplicationDBContext context
                                ,UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }



        public async Task<JsonResult> getSpecTurno(String turnoId)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var specializzazioni = (from sr in _context.C_Rel_FabbisognoMansione
                                    join s in _context.C_Mansioni
                                    on sr.MansioneId equals s.ID
                                    where sr.TurnoId == new Guid(turnoId)
                                    && sr.MultiTenantId == user.MultiTenantId
                                    select new
                                    {
                                        s.ID,
                                        s.Codice,
                                        s.Descrizione,
                                        sr.Quantita

                                    }).ToList();



            return Json(new
            {
                specializzazioni
            });
        }

        /*
         from sr in _context.C_Rel_FabbisognoMansione
                                    join s in _context.C_Mansioni                                    
                                    on sr.MansioneId equals s.ID
                                    join mu in _context.C_Rel_MansioniUser
                                    on sr.MansioneId equals mu.MansioneId
                                    where sr.TurnoId == new Guid(turnoId)
                                    && sr.MultiTenantId == user.MultiTenantId
                                    && mu.MultiTenantId == user.MultiTenantId
                                    && mu.MansioneId == sr.MansioneId
         */

        public async Task<JsonResult> ajaxGetSpecTurnoAsync(String turnoId, String userid)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            var mansioni = (from sr in _context.C_Rel_MansioniUser
                                    join s in _context.C_Rel_FabbisognoMansione
                                    on sr.MansioneId equals s.MansioneId
                                    join mu in _context.C_Mansioni
                                    on sr.MansioneId equals mu.ID
                                    where s.TurnoId == new Guid(turnoId)
                                    && sr.UserId == userid
                                    && sr.MultiTenantId == user.MultiTenantId
                                    && sr.MansioneId == s.MansioneId
                            select new
                                    {
                                        mu.ID,
                                        mu.Codice,
                                        mu.Descrizione,
                                        s.Quantita,
                                       /* DipendentiAssegnati = (from c in _context.C_Rel_TurniUsers
                                                                                        where 
                                                                                           c.TurnoId == sr.TurnoId&&
                                                                                           c.WorkDate.Equals( DateTime.Now)
                                                                                        && c.MansioneId == s.ID
                                                                                        select c.Id
                                                ).Count()*/
                                    }).ToList();



            return Json(new
            {
                data = mansioni
            });
        }

        /// <summary>
        /// Companies <c>assignSelectCantiereUser</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>        
        public async Task<IActionResult> assignFabbisognoMansioneAsync(string cantiereId, [FromForm] ICollection<Rel_FabbisognoMansione> FabbisognoMansione,String turnoId)
        {            
            List<Rel_FabbisognoMansione> n = new List<Rel_FabbisognoMansione>();            
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            foreach (var r in FabbisognoMansione) {
                Rel_FabbisognoMansione fr = _context.C_Rel_FabbisognoMansione.Where(c => c.Id.ToString().ToLower() == r.Id.ToString().ToLower()).FirstOrDefault();
                if (fr != null) //se esiste aggiorno
                {
                    fr.CantiereId = r.CantiereId;
                    fr.MansioneId = r.MansioneId;
                    fr.TurnoId = new Guid(turnoId);
                    //fr.MansioneDesc = r.Mansione.Descrizione??"";
                    fr.Quantita = r.Quantita;                 
                    _context.C_Rel_FabbisognoMansione.Update(fr);
                    n.Add(fr);
                }
                else { //creo nuovo
                    fr = new Rel_FabbisognoMansione();
                    fr.Id = r.Id;
                    fr.CantiereId = r.CantiereId;
                    fr.MansioneId = r.MansioneId;
                    fr.TurnoId = new Guid(turnoId);
                    //fr.MansioneDesc = r.Mansione.Descrizione ?? "";
                    fr.Quantita = r.Quantita;
                    fr.MultiTenantId = user.MultiTenantId;   
                    _context.C_Rel_FabbisognoMansione.Add(fr);
                    n.Add(fr);
                }                
            }
            
            _context.SaveChanges();
            return LocalRedirect("/Cantiere/edit?id="+ cantiereId);
        }

        
    }
}