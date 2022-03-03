using LoocERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Data
{    
    public class EnumDBContext : DbContext
    {
        public EnumDBContext(DbContextOptions<EnumDBContext> options) : base(options) { }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Document> Document { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AppUser>()
                .Property(p => p.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v));

            modelBuilder
                .Entity<Document>()
                .Property(p => p.DocumentGroup)
                .HasConversion(
                    v => v.ToString(),
                    v => (DocumentGroup)Enum.Parse(typeof(DocumentGroup), v));                    
        }
    }
}
