using LoocERP.Data;
using LoocERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoocERP.Seeders
{
    public static class MultiTenantSeeder
    {        
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MultiTenant>().HasData(
                    new MultiTenant
                    {
                        Id = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                        LogoUrl = "/looc/images/loghi/logoventura200.png",
                        Name = "Gruppo Francesco Ventura"
                    },
                    new MultiTenant
                    {
                        Id = new Guid("46CE0547-FA27-4BCE-92D1-8A32E15FE95E"),
                        LogoUrl = "/looc/images/loghi/kresearch.png",
                        Name = "KResearch"
                    }
            );
        }
    }    
}
