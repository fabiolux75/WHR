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

namespace LoocERP.Controllers
{
    public class DomainController : Controller
    {
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public DomainController(ApplicationDBContext context
                                 , UserManager<AppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        //ritorna la lista delle nazioni - BLOCCHIAMO ALL'ITALIA
        public JsonResult getList(string tipo = null, Guid? parentId = null)
        {
            Func<Domain, bool> whereClauseTipo = (a => true); //default le prendo tute
            if (tipo != null) whereClauseTipo = (a => a.Tipo == tipo);

            Func<Domain, bool> whereClauseParentId = (a => true); //default le prendo tute
            if (tipo != null) whereClauseParentId = (a => a.ParentId == parentId);

            var r = _context.Set<Domain>()
                    .Where(whereClauseTipo)
                    .Where(whereClauseParentId)
                    .ToList();

            return Json(
                new
                {
                    data = r
                }
           );
        }
        public JsonResult getChild(Guid? Id = null)
        {
            Func<Domain, bool> whereClause = (a => true); //default le prendo tute
            if (Id != null) whereClause = (a => a.ParentId == Id);
            
            var r = _context.Set<Domain>()
                    .Where(whereClause)
                    .ToList();

            return Json(
                new
                {
                    data = r
                }
           );
        }
    }
}