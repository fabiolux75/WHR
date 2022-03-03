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
    public class RelFabbisognoSicurezza : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public RelFabbisognoSicurezza(ApplicationDBContext context
                                ,UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }
                
        public async Task<JsonResult> getSpecTurnoAsync(String turnoId)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var specializzazioni = (from sr in _context.C_Rel_FabbisognoSicurezza
                        join s in _context.C_Specializzazioni
                        on sr.SpecializzazioneId equals s.ID
                        where sr.TurnoId == new Guid(turnoId)
                        && sr.MultiTenantId == user.MultiTenantId
                        select new { 
                            s.ID,
                            s.Codice,
                            s.Descrizione,
                            sr.Quantita
                        
                        }).ToList();



            return Json(new {
                specializzazioni
            });
        }



        /// <summary>
        /// Companies <c>assignSelectCantiereUser</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>        
        public async Task<IActionResult> assignFabbisognoSicurezzaAsync(string cantiereId, [FromForm] ICollection<Rel_FabbisognoSicurezza> FabbisognoSicurezza,String turnoId)
        {
            string[] list = new string[] { };
            List<Rel_FabbisognoSicurezza> n = new List<Rel_FabbisognoSicurezza>();
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            foreach (var r in FabbisognoSicurezza) {
                Rel_FabbisognoSicurezza fr = _context.C_Rel_FabbisognoSicurezza.Where(c => c.Id.ToString().ToLower() == r.Id.ToString().ToLower()).FirstOrDefault();
                if (fr != null) //se esiste aggiorno
                {
                    fr.CantiereId = r.CantiereId;
                    fr.SpecializzazioneId = r.SpecializzazioneId;
                    fr.TurnoId = new Guid(turnoId);
                    //fr.SpecializzazioneDesc = r.Specializzazione.Descrizione;
                    fr.Quantita = r.Quantita;
                    _context.C_Rel_FabbisognoSicurezza.Update(fr);
                    n.Add(fr);
                }
                else { //creo nuovo
                    fr = new Rel_FabbisognoSicurezza();
                    fr.Id = r.Id;
                    fr.CantiereId = r.CantiereId;
                    fr.SpecializzazioneId = r.SpecializzazioneId;
                    fr.TurnoId = new Guid(turnoId);
                    fr.Quantita = r.Quantita;
                    fr.MultiTenantId = user.MultiTenantId;
                    _context.C_Rel_FabbisognoSicurezza.Add(fr);
                    n.Add(fr);
                }                
            }
            _context.SaveChanges();
            
            return LocalRedirect("/Cantiere/edit?id="+ cantiereId);
        }

        /*
         from sr in _context.C_Rel_FabbisognoSicurezza
                                    join s in _context.C_Specializzazioni
                                    on sr.SpecializzazioneId equals s.ID
                                    where sr.TurnoId == new Guid(turnoId)
                                    && sr.MultiTenantId == user.MultiTenantId
                                    select new
                                    {
                                        s.ID,
                                        s.Codice,
                                        s.Descrizione,
                                        sr.Quantita
         */

        public async Task<JsonResult> ajaxGetSpecTurnoAsync(String turnoId, String userid)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            var specializzazioni = (from sr in _context.C_Rel_SpecializzazioniUser
                                    join s in _context.C_Rel_FabbisognoSicurezza
                                    on sr.SpecializzazioneId equals s.SpecializzazioneId
                                    join mu in _context.C_Specializzazioni
                                    on sr.SpecializzazioneId equals mu.ID
                                    where s.TurnoId == new Guid(turnoId)
                                    && sr.UserId == userid
                                    && sr.MultiTenantId == user.MultiTenantId
                                    && sr.SpecializzazioneId == s.SpecializzazioneId
                                    select new
                                    {
                                        mu.ID,
                                        mu.Codice,
                                        mu.Descrizione,
                                        s.Quantita,
                                    }).ToList();



            return Json(new
            {
                data = specializzazioni
            });
        }

    }
}