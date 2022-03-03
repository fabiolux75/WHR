using LoocERP.Data;
using LoocERP.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LoocERP.Seeders
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();
                _context.Database.EnsureCreated();

                UsersSeeder.Seed(userManager, roleManager, _context).Wait();
                //UsersSeeder.Seed(userManager, roleManager, _context).Wait();
            }
        }
    }
}