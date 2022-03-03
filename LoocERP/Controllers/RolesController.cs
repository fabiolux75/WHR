using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoocERP.Models;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoocERP.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly Data.ApplicationDBContext context;

        public RolesController(
            Data.ApplicationDBContext context
            , RoleManager<IdentityRole> roleManager
            ,UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }


        [HttpGet]
        [Authorize("Roles.Show")]
        public async Task<IActionResult> Index()
        {
            var roles = roleManager.Roles;

            List<RoleClaimsViewModel> ruoli = new List<RoleClaimsViewModel>();

            foreach (var role in roles.ToList())
            {
                var cls = await roleManager.GetClaimsAsync(role);
                ruoli.Add(
                        new RoleClaimsViewModel
                        {
                            RoleId = role.Id,
                            Title = role.Name,
                            Claims = cls,   
                        }
                    ) ;
                
            }
            return View(ruoli);
        }


        [HttpGet]
        [Authorize("Roles.Create")]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [Authorize("Roles.Create")]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "roles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        [Authorize("Roles.Edit")]
        public async Task<IActionResult> Edit( string id = null)
        {
            var claims =  context.C_Claims.ToList();

            List<Models.UserClaim> claimList = new List<Models.UserClaim>();

            var role = await roleManager.FindByIdAsync(id);
            var roleClaims = await roleManager.GetClaimsAsync(role);

            foreach(var claim in claims)
            {
                var a = roleClaims.Where(r => r.Value.Equals(claim.title)).ToList();

                var c = new UserClaim
                {
                    ClaimDescription = claim.description,
                    ClaimTitle = claim.title,
                    ClaimType = claim.type,
                    IsSelected = false,
                };

                if (a.Count > 0)
                {
                    c.IsSelected = true;
                }

                claimList.Add(c);
            }

            var model = new EditRoleViewModel
            {
                RoleId = id,
                Title = role.Name,
                Claims = claimList
            };

            return View(model);
        }


        [HttpPost]
        [Authorize("Roles.Show")]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            var roleClaims = await roleManager.GetClaimsAsync(role);

            try
            {
                IdentityResult result = null;

                foreach (var c in roleClaims)
                {
                    result = await roleManager.RemoveClaimAsync(role, c);
                }

                var selectedClaims = model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimTitle));

                foreach (var c in selectedClaims)
                {
                    result = await roleManager.AddClaimAsync(role, c);
                }

                if (result != null && result.Succeeded)
                {
                    return RedirectToAction("index", "roles");
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }

            return View(model);
        }


    }
}