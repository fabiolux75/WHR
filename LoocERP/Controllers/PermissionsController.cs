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
using System.Data.Entity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LoocERP.Controllers
{
    public class PermissionsController : Controller
    {

        private readonly ILogger<TimesheetController> _logger;
        private readonly Data.ApplicationDBContext _context;
        private readonly UserManager<AppUser> userManager;

        public PermissionsController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager,
                                    ILogger<TimesheetController> logger)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<JsonResult> ajaxIndexAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);

            var r = _context.C_Claims
                    
                    .ToList();

            return Json(
                new { data = r }
            );
        }
        
        public async Task<JsonResult> ajaxCreateAsync(string title, string type, string description)
        {
            Claims c = new Claims
            {
                title = title,
                description = description,
                type = type,
                ID = Guid.NewGuid(),
            };

            _context.C_Claims.Add(c);

            _context.SaveChanges();


            return Json(
                new { result = "ok" }
            );
        }


    }






    //define permissions for each action grouped by a feature area. using constants because we will use these later in attributes, which require constant expressions.
    public static class Permissions
    {
        public static class Home //Dashboards
        {
            public const string View = "Permissions.Home.View";
            public const string Create = "Permissions.Home.Create";
            public const string Edit = "Permissions.Home.Edit";
            public const string Delete = "Permissions.Home.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }
    }

    
    public class CustomClaimTypes
    {
        public const string Permission = "permission";
    }

    //Permission Requirement: class that will hold the permission to be evaluated.
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }

    //Permission Policy Provider: dynamically create a policy with the appropriate requirement as it's needed during runtime.
    internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // There can only be one policy provider in ASP.NET Core.
            // We only handle permissions related policies, for the rest
            /// we will use the default provider.
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        // Dynamically creates a policy with a requirement that contains the permission.
        // The policy name must match the permission that is needed.
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permissions", StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }

            // Policy is not for permissions, try the default provider.
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        //interfaccia impletata altrimenti errore su IAuthorizationPolicyProvider
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return ((IAuthorizationPolicyProvider)FallbackPolicyProvider).GetFallbackPolicyAsync();
        }

    }

}