using LoocERP.Models;
using LoocERP.Seeders;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using LoocERP.Models.Sdi;

namespace LoocERP.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        //private readonly UserManager<AppUser> userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
           : base(options)
        {
            //_httpContextAccessor = httpContextAccessor;
            //this.userManager = userManager;
        }

        public ApplicationDBContext(string ConnectionString)
            : base(SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), ConnectionString).Options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);            
            builder.Entity<Rel_TurnoUser>().HasOne(s => s.User);
            //builder.Entity<Rel_TurnoUser>().HasOne(s => s.Turno);
            builder.Entity(typeof (Rel_TurnoUser))
            .HasOne(typeof (Turno), "Turno")
            .WithMany()
            .HasForeignKey("TurnoId")
            .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
            
            builder.Entity<TimeSheetDailyReport>().HasOne(s => s.User);
            builder.Entity<TimeSheetDailyReport>().HasOne(s => s.Turno);

            // Workaround per far funzionare il filtro di ricerca sulle colonne di tipo "data"
            builder.Entity<SdiDoc>(entity =>
            {
                entity.Property(e => e.DataCreazione).HasColumnType("datetime");
                entity.Property(e => e.DataDoc).HasColumnType("datetime");
            });


            //builder.Entity<AppUser>().HasMany(c => c.ContractUsers).WithOne(c => c.AppUser).HasForeignKey(c => c.UserId);



            /*
            builder.Entity<GOF_scheda>().HasOne(v => v.Vettore).WithMany(c => c.GOF_Scheda).HasForeignKey<GOF_scheda>(v => v.codVettore);

            builder.Entity<Vettore>().HasMany(v => v.GOF_Scheda).WithOne(v => v.Vettore).HasForeignKey<GOF_scheda>(v => v.codVettore);
            */


            builder.Entity<MedAnaVettoriInfo>().HasOne(v => v.vettore).WithOne(v => v.MedAnaVettoriInfo).HasForeignKey<MedAnaVettoriInfo>(v => v.CodAnaVettore);

            builder.Entity<Rel_TurnoUser>().HasOne(c => c.Turno).WithMany(c => c.TurniUser);


            builder.Entity<AppUser>().HasOne(s => s.MultiTenant);            
            builder.Entity<ANA_Company>().HasOne(s => s.MultiTenant);

            builder.Entity<ANA_Company>()
            .Property(post => post.CreatedAt)
            .HasField("_createdAt");
            builder.Entity<ANA_Company>()
            .Property(post => post.UpdatedAt)
            .HasField("_updatedAt");
            builder.Entity<ANA_Company>()
            .Property(post => post.DeletedAt)
            .HasField("_deletedAt");
            builder.Entity<ANA_Company>()
            .Property(post => post.CreatedBy)
            .HasField("_createdBy");
            builder.Entity<ANA_Company>()
            .Property(post => post.UpdatedBy)
            .HasField("_updatedBy");
            builder.Entity<ANA_Company>()
            .Property(post => post.DeletedBy)
            .HasField("_deletedBy");

            //builder.Entity<INTERV_ESECUTORI>().HasNoKey().ToView(null);
            //builder.Entity<MED_INTERV_ESECUTORI_INFO>().HasNoKey().ToView(null);

            builder.Entity<Looc_GOF_Magazzino>().HasKey(table => new {table.CodCliente, table.Codice, table.codSettore, table.codSottosettore, table.CodMarca, table.codTipoArticolo});

            builder.Entity<SdiDocDdt>().HasKey(e => new { e.SdiDocId, e.DdtId });
            
            DatabaseSeeder.Seed(builder);
        }

        public int SaveChanges()
        {
            OnBeforeSaving(null);
            return base.SaveChanges();
        }
        public int SaveChanges(string utente, bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving(utente);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        private void OnBeforeSaving(string currentuser)
        {

            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                //if (entry.Entity is ANA_Company)
                //{
                try
                {
                    switch (entry.State)
                    {

                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = DateTime.Now;
                            entry.CurrentValues["UpdatedAt"] = DateTime.UnixEpoch;
                            entry.CurrentValues["DeletedAt"] = DateTime.UnixEpoch;
                            entry.CurrentValues["CreatedBy"] = currentuser;
                            entry.CurrentValues["UpdatedBy"] = null;
                            entry.CurrentValues["DeletedBy"] = null;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                            entry.CurrentValues["UpdatedBy"] = currentuser;
                            break;

                        case EntityState.Deleted:
                            entry.CurrentValues["DeletedAt"] = DateTime.Now;
                            entry.CurrentValues["DeletedBy"] = currentuser;
                            break;
                    }
                }
                catch (Exception e) {
                    break;
                }
                //}
            }
        }

        public DbSet<Models.MultiTenant> C_Multitenant { get; set; }
        public DbSet<Models.Domain> C_Domains { get; set; }
        public DbSet<Models.Rel_TurnoUser> C_Rel_TurniUsers { get; set; }
        public DbSet<Models.ANA_Company> C_ANA_Companies { get; set; }
        /* Definizione Anagrafica */
        public DbSet<Models.Claims> C_Claims { get; set; }
        public DbSet<Models.TimeSheet> C_TimeSheets { get; set; }
        public DbSet<Models.Project> C_Projects { get; set; }
        public DbSet<Models.Cantiere> C_Cantieri { get; set; }
        public DbSet<Models.Turno> C_Turni { get; set; }
        public DbSet<Models.Mansione> C_Mansioni { get; set; }
        public DbSet<Models.Specializzazione> C_Specializzazioni { get; set; }
        public DbSet<Models.Rel_MansioneUser> C_Rel_MansioniUser { get; set; }
        public DbSet<Models.Rel_SpecializzazioneUser> C_Rel_SpecializzazioniUser { get; set; }
        public DbSet<Models.Rel_FabbisognoMansione> C_Rel_FabbisognoMansione { get; set; }
        public DbSet<Models.Rel_FabbisognoSicurezza> C_Rel_FabbisognoSicurezza { get; set; }
        public DbSet<Models.TimeSheetDailyReport> C_TimeSheetsDailyReport { get; set; }
        public DbSet<Models.Document> C_Documents { get; set; }
        public DbSet<Models.ContractUser> C_ContractUser { get; set; }
        public DbSet<Models.MalattiaUser> C_MalattiaUser { get; set; }
        public DbSet<Models.Giustificativo> C_Giustificativi { get; set; }
        public DbSet<Models.MansioneMacchina> C_MansioneMacchina { get; set; }
        public DbSet<Models.Corsi> C_Corsi { get; set; }
        public DbSet<Models.VisitaMedica> C_VisiteMediche { get; set; }
        public DbSet<Models.RichiestaDipendente> C_RichiesteDipendenti { get; set; }
        public DbSet<Models.VettoreUser> C_VettoreUser { get; set; }
        public DbSet<Models.VettoreCantiere> C_VettoreCantiere { get; set; }
        public DbSet<Models.LogAuditHR> C_LogAuditHR { get; set; }
        

        public DbSet<Models.RemoteSetupModel> RemoteSetup { get; set; } // già presente vecchio looc
        public DbSet<Models.Device> Devices { get; set; } // già presente vecchio looc       
        public DbSet<Models.Vettore> ANA_VETTORI { get; set; } // già presente vecchio looc               
        public DbSet<Models.VettoreSettore> SETTORI { get; set; } // già presente vecchio looc
        public DbSet<Models.VettoreSottoSettore> SOTTO_SETTORI { get; set; } // già presente vecchio looc
        public DbSet<Models.INTERV_ESECUTORI> INTERV_ESECUTORI { get; set; } // già presente vecchio looc
        public DbSet<Models.MED_INTERV_ESECUTORI_INFO> MED_INTERV_ESECUTORI_INFO { get; set; } // già presente vecchio looc
        public DbSet<Models.Looc_GOF_Cliente_Fornitore_esterno> GOF_cliente_fornitore_esterno { get; set; } // già presente nel vecchio looc
        public DbSet<Models.Looc_GOF_Cliente_Fornitore> gof_cliente_fornitore { get; set; } // già presente nel vecchio looc
        public DbSet<Models.Looc_Comuni> COMUNI { get; set; } // già presente nel vecchio looc
        public DbSet<Models.Looc_Province> PROVINCE { get; set; } // già presente nel vecchio looc
        public DbSet<Models.Looc_Regioni> REGIONI { get; set; } // già presente nel vecchio looc        
        public DbSet<Models.ModelloVettore> MODELLO_VETTORI { get; set; } // già presente nel vecchio looc        
        public DbSet<Models.ModelloVettoreInfo> MED_MODELLO_VETTORI_INFO { get; set; } // già presente nel vecchio looc        
        public DbSet<Models.GOF_scheda> GOF_scheda { get; set; } // già presente nel vecchio looc        
        public DbSet<Models.Parcheggio> C_Parcheggio { get; set; }     
        public DbSet<Models.VettoreParcheggio> C_VettoreParcheggio { get; set; }     
        public DbSet<Models.MedAnaVettoriInfo> MED_ANA_VETTORI_INFO { get; set; }     
        public DbSet<Models.MedFamVettoriInfo> MED_FAM_VETTORI_INFO { get; set; }     
        public DbSet<Models.Marche> MARCHE { get; set; }     
        public DbSet<Models.Noleggio> C_Noleggi { get; set; }     
        public DbSet<Models.VettoreAssegnazione> C_VettoreAssegnazione { get; set; }     
        public DbSet<Models.VettoreLeasing> C_VettoreLeasing { get; set; }     
        public DbSet<Models.Sdi.SdiDoc> C_SdiDoc { get; set; }     
        public DbSet<Models.Ddt> C_DDT { get; set; }     
        public DbSet<Models.DdtRicevuti> C_DDT_ricevuti { get; set; }     
        public DbSet<Models.DdtDettaglio> C_DDT_Dettaglio { get; set; }     
        public DbSet<Models.DdtDettaglioRicevuti> C_DDT_Dettaglio_Ricevuti { get; set; }     
        public DbSet<Models.StazioniCantiere> C_StazioniCantiere { get; set; }     
        public DbSet<Models.Stazione> C_Stazioni { get; set; }     
        public DbSet<Models.ResourceRequest> C_ResourceRequest { get; set; }     
        public DbSet<Models.ResourceRequestDetails> C_ResourceRequestDetails { get; set; }     
        public DbSet<Models.ModalitaPagamento> C_ModalitaPagamento { get; set; }     
        public DbSet<Models.TipoPagamentoDatev> C_TipoPagamento { get; set; }     
        public DbSet<Models.UserNotification> C_UserNotification { get; set; }     
        public DbSet<Models.NoleggiOption> C_NoleggiOptions { get; set; }             
        public DbSet<Models.NoleggioNoleggiOptions> C_NoleggioNoleggiOptions { get; set; }             
        public DbSet<Models.Articolo> C_MAG_ARTICOLI { get; set; }             
        public DbSet<Models.Um> C_UM { get; set; }             
        public DbSet<Models.Looc_GOF_Magazzino> GOF_magazzino { get; set; }
        public DbSet<Models.Looc_GOF_Categoria> GOF_Categoria { get; set; }
        public DbSet<Models.Looc_GOF_um> GOF_um { get; set; }
        public DbSet<Models.Looc_GOF_Aliquota> GOF_aliquota { get; set; }
        public DbSet<Models.Sdi.SdiDocDdt> C_SdiDocDdt { get; set; }
        public DbSet<Models.AppUser> AspNetUsers { get; set; }
        public DbSet<Models.TipiDocumentoFE> FE_TipiDocumento { get; set; }
    }
}
