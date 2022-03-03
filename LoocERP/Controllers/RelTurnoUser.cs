using System.Runtime.InteropServices.WindowsRuntime;
using System.ComponentModel;
using System;
using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LoocERP.Controllers
{
    public class RelTurnoUser : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public RelTurnoUser(ApplicationDBContext context
                                ,UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        [Authorize("RelTurnoUser.Show")]
        public async Task<IActionResult> IndexAsync(DateTime? WorkDate = null)
        {                       
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            ViewBag.WorkDate = WorkDate??DateTime.Now;
            AssignUserCantieriViewModel model = new AssignUserCantieriViewModel();
            model.MansioneList = 
                _context.Set<Mansione>()
                    .Where( x=>  x.MultiTenantId == user.MultiTenantId )
                    .ToList(); 

            return View(model);
        }


        [Authorize("RelTurnoUser.Create")]
        public IActionResult assignModalUserCantieri(DateTime? WorkDate = null)
        {                       
            ViewBag.WorkDate = WorkDate??DateTime.Now;
            return View();
        }


        /// <summary>
        /// Companies <c>assignModalSelectCantiereUser</c> 
        /// Torna lista di aziende legata al livello
        /// </summary>        
        [Authorize("RelTurnoUser.Create")]
        public async Task<IActionResult> assignModalSelectCantiereUserAsync(string? TurnoId = null, DateTime? WorkDate = null)
        {                      
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            
            ViewBag.TurnoId = TurnoId;
            if (WorkDate == null) WorkDate = DateTime.Now.Date;
            ViewBag.WorkDate = WorkDate.Value;

            Turno turno = _context.Set<Turno>()
                .Include(x=>x.Cantiere)
//includere per avere mansioni dell'utente
                .Where(
                    x => x.Id.ToString().ToLower().Equals(TurnoId.ToString().ToLower()) 
                    && x.MultiTenantId == user.MultiTenantId
                )
                .FirstOrDefault();
            ViewData["Turno"] = turno;

            return View();
        }


        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxRemoveAsync(string UserId, string TurnoId, DateTime WorkDate, DateTime dataStart, DateTime dataEnd)
        {
            var result = "ko";
            var message = "Errore generico nel salvataggio";
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var turniUs = _context.C_Rel_TurniUsers.Where(
                rtu => 
                       rtu.UserId == UserId
                        &&
                       rtu.WorkDate >= dataStart
                        &&
                       rtu.WorkDate <= dataEnd
                        &&
                       rtu.TurnoId == new Guid(TurnoId)
                        && rtu.MultiTenantId == user.MultiTenantId
                ).ToList();

            _context.C_Rel_TurniUsers.RemoveRange(turniUs);
            _context.SaveChanges();

            return Json(
              new
              {
                 
              }
            );
        }


        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxAssignRelAsync(string UserId, string TurnoId, DateTime WorkDate, DateTime dataStart, DateTime dataEnd,String? mansioneId,String specializzazioneId)
        {
            var result = "ko";
            var message = "Errore generico nel salvataggio";
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            Guid? mId = null;


            Turno t = _context.C_Turni.Include(c => c.Cantiere).Where(c => c.Id == new Guid(TurnoId)).FirstOrDefault();
            var Message = "";
            if(t != null)
            {
                _context.C_UserNotification.Add(
                    new UserNotification
                    {
                        MessageText = "Sei stato assegnato al cantiere : "+t.Cantiere.Name+"\nDalle "+t.OraInizio+" alle "+t.OraFine,
                        MultiTenantId = user.MultiTenantId,
                        Seen = 0,
                        Sounded = 0,
                        Tipologia = Typology.assignationCantiere,
                        UserId = UserId,
                    }
                    );
                _context.SaveChanges();
            }


            if(mansioneId != "undefined" && mansioneId != "null")
            {
                mId = new Guid(mansioneId);
            }

            Guid? sId = null;

            if (specializzazioneId != "undefined" && specializzazioneId != "null")
            {
                sId = new Guid(specializzazioneId);
            }


            var turniUs = 
            _context.C_Rel_TurniUsers.Where(
                rtu => rtu.UserId == UserId
                        &&
                        rtu.WorkDate >= dataStart
                        &&
                        rtu.WorkDate <= dataEnd
                        && rtu.MultiTenantId == user.MultiTenantId
                ).ToList();
            if(turniUs.Count > 0){ //elimino le assegnazione precedenti per utente e date
                _context.C_Rel_TurniUsers.RemoveRange(turniUs);
                _context.SaveChanges();
            }

            while (dataStart <= dataEnd)
            {           
                _context.C_Rel_TurniUsers.Add(
                    new Rel_TurnoUser
                        {
                            UserId = UserId,
                            TurnoId = new Guid(TurnoId),
                            WorkDate = dataStart,
                            SpecializzazioneId = sId,
                            MansioneId = mId,
                            MultiTenantId = user.MultiTenantId,
                        }
                    );

                dataStart = dataStart.AddDays(1);
            }

            _context.SaveChanges();
            return 
                Json(new{ });

        }


        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxAssignAsync(string UserId, string TurnoId, DateTime WorkDate,DateTime dataStart,DateTime dataEnd)
        {
            var result = "ko";
            var message = "Errore generico nel salvataggio";
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var turniUs = _context.C_Rel_TurniUsers.Where(
                rtu => rtu.UserId == UserId
                        &&
                       rtu.WorkDate >= dataStart
                        &&
                       rtu.WorkDate <= dataEnd
                        && rtu.MultiTenantId == user.MultiTenantId
                ).ToList();

            _context.C_Rel_TurniUsers.RemoveRange(turniUs);
            _context.SaveChanges();

            while (dataStart <= dataEnd)
            {
                _context.C_Rel_TurniUsers.Add(
                    new Rel_TurnoUser
                        {
                            UserId = UserId.ToUpper(),
                            TurnoId = new Guid(TurnoId),
                            WorkDate = dataStart,
                            MultiTenantId = user.MultiTenantId
                        }                    
                    );
                dataStart = dataStart.AddDays(1);
            }

            _context.SaveChanges();

            return 
                Json(new { });
        }


        /// <summary>
        /// Ritorna la lista di tutti i cantieri e i turni - Index Page
        /// </summary> 
        [Authorize("RelTurnoUser.Show")]
        public async Task<JsonResult> ajaxProjectCantiereTurnoAsync(DateTime? WorkDate = null)
        {     
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            if (WorkDate == null) WorkDate = DateTime.Now.Date;
            
            var r = (from e in _context.C_Cantieri
                     join p in _context.C_Projects on e.ProjectId equals p.Id
                     join t in _context.C_Turni on e.Id equals t.CantiereId 
                     where e.MultiTenantId.Equals(user.MultiTenantId)
                     where e.StartDate <= WorkDate
                     where e.EndDate >= WorkDate
                     select new
                     {
                         CantiereId = e.Id,
                         CantiereCode = e.Codice,
                         CantiereName = e.Name,
                         CantiereCitta = e.Citta,
                         CantiereIndirizzo = e.Indirizzo,
                         ProjectCode = p.Codice,
                         ProjectId = p.Id,
                         ProjectName = p.Name,
                         TurnoId = t.Id,
                         TurnoName = t.Name,
                     }).ToList();
                     //&& DateTime.Compare(c.WorkDate, wd) == 0 //c.WorkDate.Date.Equals(wd.ToString("MM-dd-yyyy HH:mm:ss")) 
            return Json(
              new
              {
                  data = r
              }
            );
        }


        /// <summary>
        /// Seleziona la lista degli utenti presenti e non presenti nel turno
        /// Torna lista di aziende legata al livello
        /// </summary>        
        [Authorize("RelTurnoUser.Create")]
        public async Task<JsonResult> ajaxSelectCantiereUserTurnoAsync(string TurnoId, DateTime WorkDate, int? isIncluded=0)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            //List<AppUser> r = new List<AppUser>();
            JsonResult data = null;
            String dt = WorkDate.Date.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                var queryinterna = _context.C_Rel_TurniUsers
                            .Where(c => c.MultiTenantId == user.MultiTenantId)
                            .Where(c => c.TurnoId == new Guid(TurnoId))
                            .Where(c => c.WorkDate == WorkDate)
                            .Select(c => new
                            {
                                id = c.UserId,
                                FirstName = c.User.FirstName,
                                LastName = c.User.LastName,
                                TurnoId = c.TurnoId,
                            })
                            .Distinct().ToList();

                if (isIncluded == 1)
                {
                    return Json(
                        new
                        {
                            data = queryinterna
                        }
                    );

                }

                var test = (from u in _context.Users
                           .Where(u => u.MultiTenantId == user.MultiTenantId)
                           .Where(u => u.isEmployee == 1)
                            join c in _context.C_Rel_TurniUsers.Where(c => c.WorkDate == WorkDate) on u.Id equals c.UserId into gj
                            from subc in gj.DefaultIfEmpty()
                            select new
                            {
                                id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                TurnoId = subc.TurnoId,
                            })
                            .Distinct().ToList();

                var right = test
                            .Where(u => queryinterna.All(x => x.id != u.id))
                            .Where(u => u.TurnoId != new Guid(TurnoId));

                return Json(
                    new
                    {
                        data = right
                    }
                );
            }
            catch (Exception e)
            {
                var a = e.Message;
            }

            return Json(
                new
                {
                    data
                }
            );

        }


        /// <summary>
        /// Pagina di Index delle assegnazioni
        /// All'interno ci sono dei JS che automaticamente assegnano le attività
        /// </summary>      
        [Authorize("RelTurnoUser.Show")]
        public async Task<JsonResult> ajaxUserIndexAsync(DateTime WorkDate)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);            

            var r = _context.Set<Rel_TurnoUser>()
                .Include ( x => x.User)
                .Include ( x => x.User.Rel_MansioneUser)                
                .Include ( x => x.Turno)
                .Include ( x => x.Turno.Cantiere)
                .Include ( x => x.Turno.Cantiere.Project)
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(a => DateTime.Compare(a.WorkDate, WorkDate) == 0)
                //.Where(a => a.UserId == "87ee2af6-7d6e-4a27-a2cc-02f5caba03c5") // filtro osso per test                                
                //.Where(a => a.UserId == "2ec753ce-4085-4eec-8eb7-4b80aa64ac7f") // filtro osso DE MAGISTRIS
                .Select( c => new{
                        //Id = c.Id,
                        workDate = c.WorkDate,
                        turnoId = c.TurnoId,                        
                        userId = new Guid(c.UserId),
                        FullName = c.User.LastName+" "+c.User.FirstName,
                        TurnoName = c.Turno.Name,
                        CantiereId = c.Turno.Cantiere.Id,
                        CantiereName = c.Turno.Cantiere.Name,
                        CantiereCode = c.Turno.Cantiere.Codice,
                        ProjectId = c.Turno.Cantiere.Project.Id,
                        ProjectName = c.Turno.Cantiere.Project.Name,
                        CantiereStartDate = c.Turno.Cantiere.StartDate,
                        CantiereEndDate = c.Turno.Cantiere.EndDate,
                        mansioneId = c.MansioneId,
                        specializzazioneId = c.SpecializzazioneId,
                        mansioniTurnoList = _context.Set<Rel_FabbisognoMansione>() // mansioni richieste per il turno   
                            .Where(c => c.MultiTenantId == user.MultiTenantId)                         
                            .Where ( u => u.TurnoId == c.TurnoId )
                            .Where ( u => u.CantiereId == c.Turno.CantiereId)
                            .Select( rmu => new {                        
                                MansioneId = rmu.Mansione.ID,
                                MansioneName = rmu.Mansione.Descrizione,
                                Quantita = rmu.Quantita
                            })
                            .ToList()
                        ,
                        mansioniUserTurnoList = _context.Set<Rel_TurnoUser>() // mansioni del turno assegnate all'utente
                            .Where( u => u.UserId == c.UserId )
                            .Where(c => c.MultiTenantId == user.MultiTenantId)
                            .Where(a => DateTime.Compare(a.WorkDate, WorkDate) == 0)
                            .Select(s=> s.Mansione)
                            .ToList()
                            ,
                        Rel_MansioneUser =    // mansioni che l'utente può ricoprire
                            c.User.Rel_MansioneUser                                    
                                .Select( rmu => new {                        
                                    MansioneId = rmu.Mansione.ID,
                                    MansioneName = rmu.Mansione.Descrizione
                                }
                        ),
                        specializzazioniTurnoList = _context.Set<Rel_FabbisognoSicurezza>() // mansioni richieste per il turno   
                            .Where(c => c.MultiTenantId == user.MultiTenantId)                         
                            .Where ( u => u.TurnoId == c.TurnoId )
                            .Where ( u => u.CantiereId == c.Turno.CantiereId)
                            .Select( rmu => new {                        
                                SpecializzazioneId = rmu.Specializzazione.ID,
                                SpecializzazioneName = rmu.Specializzazione.Descrizione,
                                Quantita = rmu.Quantita
                            })
                            .ToList()
                        ,
                        specializzazioniUserTurnoList = _context.Set<Rel_TurnoUser>() // specializzazioni del turno assegnate all'utente
                            .Where( u => u.UserId == c.UserId )
                            .Where(c => c.MultiTenantId == user.MultiTenantId)
                            .Where(a => DateTime.Compare(a.WorkDate, WorkDate) == 0)
                            .Select(s => s.Specializzazione )
                            .ToList()
                            ,                        
                        Rel_SpecializzazioneUser = // specializzazioni che l'utente può ricoprire
                            c.User.Rel_SpecializzazioneUser                                  
                                .Select( rmu => new {                                               
                                    SpecializzazioneId = rmu.Specializzazione.ID,
                                    SpecializzazioneName = rmu.Specializzazione.Descrizione
                                }
                        )
                    }
                )    
                .OrderBy ( x => x.ProjectName)
                .ToList();
        
            
            var r1 = r.GroupBy( b => new { userId = b.userId , turnoId = b.turnoId } )                    
                        .Select(
                                g => new { 
                                    key = g.Key, 
                                    data = g.FirstOrDefault(),                              
                                    count = g.Count() 
                            }
                        );            
            return Json(
                new { data = r1 }
            );           
        }        

        /// <summary>
        /// Torna la lista di Turni, assiciati alla data e il cantiere
        /// </summary>  
        public async Task<JsonResult> ajaxTurnoList(DateTime WorkDate, string cantiereId)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<Turno>()
                .Include( x=> x.Cantiere)
                .Include( x=> x.Cantiere.Project)
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(c => c.CantiereId == new Guid(cantiereId))
                //.Where(a => DateTime.Compare(a.E, WorkDate) == 0)
                .Select( x => new {
                    id = x.Id,                    
                    text = x.Name
                })                
                .ToList();

            return Json(
                new { results = r }
            ); 
        }   

        /// <summary>
        /// Torna la lista di Cantieri, assiciati alla data e il cantiere
        /// </summary>  
        public async Task<JsonResult> ajaxCantiereList(DateTime WorkDate, string projectId)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<Turno>()
                .Include( x=> x.Cantiere)
                .Include( x=> x.Cantiere.Project)
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Where(c => c.Cantiere.ProjectId == new Guid(projectId))
                .Select( x => new {
                    id = x.Cantiere.Id,
                    //text = x.Cantiere.Name
                    text = x.Cantiere.Codice +"("+x.Cantiere.Name+")"
                })                
                .ToList();

            return Json(
                new { results = r }
            ); 
        }

        /// <summary>
        /// Torna la lista di Project List, assiciati alla data e il cantiere
        /// </summary>  
        public async Task<JsonResult> ajaxProjectList(DateTime WorkDate)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.Set<Turno>()
                .Include( x=> x.Cantiere)
                .Include( x=> x.Cantiere.Project)
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                //.Where(a => DateTime.Compare(a.E, WorkDate) == 0)
                .Select( x => new {
                    id = x.Cantiere.Project.Id,
                    text = x.Cantiere.Project.Name
                })                
                .ToList();

            return Json(
                new { results = r }
            ); 
        }

        /// <summary>
        /// Aggiornamento dei turni sulla pagina di assegnazione giornaliera
        /// </summary>  
        [HttpGet]
        public async Task<JsonResult> ajaxUpdateTurnoUser(
                string turnoid, string userid, DateTime workdate, 
                string mansioniJson, string specializzazioniJson
            ){
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            Turno Turno = _context.Set<Turno>().Include(x => x.Cantiere).Where(c => c.Id == new Guid(turnoid)).FirstOrDefault();

            if (Turno == null) {
                return Json(
                        new
                        {
                            results = "ko"
                        }
                    );
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                //var mansioniList = Array.Pa.Parse(mansioniJson);
                var mansioniList = JsonConvert.DeserializeObject<string[]>(mansioniJson);
                var specializzazioniList = JsonConvert.DeserializeObject<string[]>(specializzazioniJson);            
                
                /* Rimuovo tutte le date dell'utente a partire dal giorno stesso*/

                var q = _context.Set<Rel_TurnoUser>()
                    .Include(x=> x.Turno)
                    .Include(x => x.Turno.Cantiere)
                    .Where(
                    rtu => 
                            rtu.UserId == userid
                            &&
                            rtu.WorkDate >= workdate                        
                    )
                    .OrderBy(c => c.WorkDate)
                    ;
                    //.ToArray();
                //DateTime dataEnd = q.LastOrDefault().WorkDate; //prendo l'ultima data assegnata
                
                var rem = q.ToList();


                _context.Set<Rel_TurnoUser>().RemoveRange(rem); //rimuovo le assegnazioni precedenti
                _context.SaveChanges();
                
                //prendo il valore massimo tra l'elenco delle mansioni e le specializzazione
                int nmax = (mansioniList.Count()<specializzazioniList.Count()?specializzazioniList.Count():mansioniList.Count());
                
                DateTime dataStart = workdate;
                
                while (dataStart <= Turno.Cantiere.EndDate  )
                {
                    for(int i=0;i<nmax;i++){   
                        Rel_TurnoUser Rel_TurnoUser = new Rel_TurnoUser();
                        Rel_TurnoUser.UserId = userid;
                        Rel_TurnoUser.TurnoId = new Guid(turnoid);
                        Rel_TurnoUser.WorkDate = dataStart;
                        Rel_TurnoUser.MultiTenantId = user.MultiTenantId;
                        if(i < mansioniList.Count()){
                            Rel_TurnoUser.MansioneId = new Guid(mansioniList[i]);
                        }
                        if(i < specializzazioniList.Count()){
                            Rel_TurnoUser.SpecializzazioneId = new Guid(specializzazioniList[i]);
                        }
                        _context.Set<Rel_TurnoUser>().Add(Rel_TurnoUser);                    
                    }
                    if (nmax == 0) { //default specia = null mansion = null
                        Rel_TurnoUser Rel_TurnoUser = new Rel_TurnoUser();
                        Rel_TurnoUser.UserId = userid;
                        Rel_TurnoUser.TurnoId = new Guid(turnoid);
                        Rel_TurnoUser.WorkDate = dataStart;
                        Rel_TurnoUser.MultiTenantId = user.MultiTenantId;
                        _context.Set<Rel_TurnoUser>().Add(Rel_TurnoUser);
                    }
                    dataStart = dataStart.AddDays(1);
                }
                _context.SaveChanges();  
                transaction.Commit(); 
                var r = "";

                return Json(
                    new { results = r }
                ); 
            }
            catch(Exception){
                transaction.Rollback();
                return Json(
                    new { 
                        results = "ko" 
                    }
                );
            }                                 
        }
        
    }
}