using LoocERP.Data;
using LoocERP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoocERP.Seeders
{
    public static class UsersSeeder
    {
        /*      
        public static async Task Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MultiTenant>().HasData(
                new MultiTenant
                {
                    Id = new Guid("4760FF77-8F35-459B-A791-1774EF428F7D"),
                    Name = "Gruppo Francesco Ventura",
                    LogoUrl = "/looc/images/loghi/logoventura200.png"                        
                }
            );
            
            //Data.ApplicationDBContext _context;
            //UserManager<AppUser> userManager;
            AppUser user = new AppUser
            {
                Id = "A1AA1907-8020-46B0-9A49-AE62835903C7",
                FirstName = "Amministratore",
                LastName = "kresearch",
                UserName = "test@looc.com",
                Email = "info@kresearch.it",
                IDCompany = "d93144fb-2922-43f0-a0fc-238d3b0a923c"
            };

            IdentityRole role = new IdentityRole
            {
                Name = "administrator",
                NormalizedName = "administrator",
            };
            
            if (await roleManager.FindByNameAsync("administrator") == null)
            {
                var res = await roleManager.CreateAsync(role);

                await roleManager.AddClaimAsync(role, new Claim("ManageUser", "ManageUser"));
                await roleManager.AddClaimAsync(role, new Claim("ManageRoles", "ManageRoles"));
            }

            IdentityResult result = await UserManager.CreateAsync(user, "Produzione.15");
            var roleResult = await userManager.AddToRoleAsync(user, role.Name);
        }
        */
        public static async Task Seed(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext _context)
        {

            /* Ventura */
            AppUser user = new AppUser
            {
                Id = "29da0a72-9e03-466e-b329-c0f85f81fd96",
                FirstName = "Amministratore",
                LastName = "Ventura",
                UserName = "test@looc.com",
                Email = "info@kresearch.it",
                IDCompany = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb")
            };

            IdentityRole role = new IdentityRole
            {
                Id = "3b1e03be-4d19-4be1-bb94-6a98094eca32",
                Name = "Administrator",
                NormalizedName = "Administrator",
            };

            if (await roleManager.FindByNameAsync("Administrator") == null)
            {
                var res = await roleManager.CreateAsync(role);

                await roleManager.AddClaimAsync(role, new Claim("Users.Show", "Users.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Users.Create", "Users.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Users.Edit", "Users.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("Cantiere.Show", "Cantiere.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Cantiere.Create", "Cantiere.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Cantiere.Edit", "Cantiere.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("Companies.Show", "Companies.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Companies.Create", "Companies.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Companies.Edit", "Companies.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("Project.Show", "Project.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Project.Create", "Project.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Project.Edit", "Project.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("ContractUser.Show", "ContractUser.Show"));
                await roleManager.AddClaimAsync(role, new Claim("ContractUser.Create", "ContractUser.Create"));
                await roleManager.AddClaimAsync(role, new Claim("ContractUser.Edit", "ContractUser.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("Document.Show", "Document.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Document.Create", "Document.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Document.Edit", "Document.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("MalattiaUser.Show", "MalattiaUser.Show"));
                await roleManager.AddClaimAsync(role, new Claim("MalattiaUser.Create", "MalattiaUser.Create"));
                await roleManager.AddClaimAsync(role, new Claim("MalattiaUser.Edit", "MalattiaUser.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("RelTurnoUser.Show", "RelTurnoUser.Show"));
                await roleManager.AddClaimAsync(role, new Claim("RelTurnoUser.Create", "RelTurnoUser.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Roles.Show", "Roles.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Roles.Create", "Roles.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Roles.Edit", "Roles.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("Timesheet.Show", "Timesheet.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Timesheet.Create", "Timesheet.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Timesheet.Edit", "Timesheet.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("TimesheetDailyReport.Show", "TimesheetDailyReport.Show"));
                await roleManager.AddClaimAsync(role, new Claim("TimesheetDailyReport.Create", "TimesheetDailyReport.Create"));
                await roleManager.AddClaimAsync(role, new Claim("TimesheetDailyReport.Edit", "TimesheetDailyReport.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("Turni.Show", "Turni.Show"));
                await roleManager.AddClaimAsync(role, new Claim("Turni.Create", "Turni.Create"));
                await roleManager.AddClaimAsync(role, new Claim("Turni.Edit", "Turni.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("RelMansioneUser.Show", "RelMansioneUser.Show"));
                await roleManager.AddClaimAsync(role, new Claim("RelMansioneUser.Create", "RelMansioneUser.Create"));
                await roleManager.AddClaimAsync(role, new Claim("RelMansioneUser.Edit", "RelMansioneUser.Edit"));
                await roleManager.AddClaimAsync(role, new Claim("RelSpecializzazioneUser.Show", "RelSpecializzazioneUser.Show"));
                await roleManager.AddClaimAsync(role, new Claim("RelSpecializzazioneUser.Create", "RelSpecializzazioneUser.Create"));
                await roleManager.AddClaimAsync(role, new Claim("RelSpecializzazioneUser.Edit", "RelSpecializzazioneUser.Edit"));
            }

            IdentityResult result = await userManager.CreateAsync(user, "Produzione.15");
            var roleResult = await userManager.AddToRoleAsync(user, role.Name);

            /* KResearch */
            /*
            AppUser user1 = new AppUser
            {
                Id = "e7452e17-f8cf-4f16-aaa3-fe27b1d5a50e",  
                FirstName = "Amministratore",
                LastName = "kresearch",
                UserName = "info@kresearch.it",
                Email = "info@kresearch.it",
                IDCompany = "C9E8C9FA-A29C-418B-B136-E9905869E44B",
                MultiTenantId = new Guid("46CE0547-FA27-4BCE-92D1-8A32E15FE95E")
            };            

            IdentityResult result1 = await userManager.CreateAsync(user1, "Produzione.15");
            var roleResult1 = await userManager.AddToRoleAsync(user1, role.Name);
            */
        }
    }
}
