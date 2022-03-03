using LoocERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoocERP.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {            
            ClaimsSeeder.Seed(modelBuilder);            
            MultiTenantSeeder.Seed(modelBuilder);
            DomainsSeeder.Seed(modelBuilder);
            GiustificativiSeeder.Seed(modelBuilder);
            MansioniSeeder.Seed(modelBuilder);
            MansioniMacchinaSeeder.Seed(modelBuilder);
            SpecializzazioniSeeder.Seed(modelBuilder);
            CompaniesSeeder.Seed(modelBuilder);
            //UsersSeeder.Seed(modelBuilder);  

        }
    }
}