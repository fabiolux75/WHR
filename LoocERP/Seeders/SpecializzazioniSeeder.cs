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
    public static class SpecializzazioniSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specializzazione>().HasData(
                    new Specializzazione { ID = new Guid("2c7615cd-4aab-44b4-8b72-ee2e18c83f1a"), Codice = "S001", Name = "ABILITAZIONE PRIMO SOCCORSO", Descrizione = "ABILITAZIONE PRIMO SOCCORSO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("9144a055-579e-4964-b5e1-59aa8380625a"), Codice = "S002", Name = "ABILITAZIONE ANTINCENDIO", Descrizione = "ABILITAZIONE ANTINCENDIO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("810c4a73-3ed2-4aa0-b38f-af03a23b60ef"), Codice = "S003", Name = "ABILITAZIONE SALDATURA ALLUMINOTERMICA", Descrizione = "ABILITAZIONE SALDATURA ALLUMINOTERMICA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("1a80cdbc-4f8e-4321-970b-cd0b98b2ba4a"), Codice = "S004", Name = "ABILITAZIONE PROT CANTIERE", Descrizione = "ABILITAZIONE PROT CANTIERE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("3584324a-d1e9-43e5-b7be-821ead97d14e"), Codice = "S005", Name = "ABILITAZIONE GUIDA MEZZI", Descrizione = "ABILITAZIONE GUIDA MEZZI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("696dddac-2c53-4997-aa47-387f7582c490"), Codice = "S006", Name = "PAT. C", Descrizione = "PAT. C", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("52d76fe6-5e4d-4caf-af7b-dbb018c90a23"), Codice = "S007", Name = "PROT. CANTIERI FC", Descrizione = "PROT. CANTIERI FC", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("5118b6a7-34d3-4540-baf3-efa0da4192b0"), Codice = "S008", Name = "ABILITAZIONE PRIMI FORMATORI", Descrizione = "ABILITAZIONE PRIMI FORMATORI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("96ea3e72-3887-4f84-9129-078899bccbef"), Codice = "S009", Name = "ABILITAZIONE ULTRASUONI", Descrizione = "ABILITAZIONE ULTRASUONI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("b7739c92-b1a6-4828-92f6-244ecffc45eb"), Codice = "S010", Name = "ARMDITTE", Descrizione = "ARMDITTE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("e48e0d9d-f20b-4c02-a77b-7f0c88f28771"), Codice = "S011", Name = "ADD.PONTEGGIO", Descrizione = "ADD.PONTEGGIO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("804f759b-b1bf-4545-9e33-a1356411968c"), Codice = "S012", Name = "ABILITAZIONE SALDATURA MATERIALI", Descrizione = "ABILITAZIONE SALDATURA MATERIALI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("979c452b-2498-460d-95a0-db69bdf61c47"), Codice = "S013", Name = "TECNICO ORGANI SICUREZZA", Descrizione = "TECNICO ORGANI SICUREZZA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("03904a23-65bc-487c-bc58-54c098631700"), Codice = "S014", Name = "PAT. D", Descrizione = "PAT. D", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("72a1d36a-774c-44c7-9b96-3fb6f8769ea1"), Codice = "S015", Name = "PAT. D+E", Descrizione = "PAT. D+E", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("b629950f-2b8e-4f16-9dfe-1dd2c5fb3235"), Codice = "S016", Name = "ATTESTATO ESCAVATORISTA", Descrizione = "ATTESTATO ESCAVATORISTA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("da8037af-735b-49eb-b21d-2a08805d5515"), Codice = "S017", Name = "ATTESTATO MULETTO", Descrizione = "ATTESTATO MULETTO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("ddbd8756-e8c6-44ae-8abc-a4176479b776"), Codice = "S018", Name = "PREPOSTO DI CANTIERE", Descrizione = "PREPOSTO DI CANTIERE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("d5b9923f-a0ae-4d32-a2f8-de3609480169"), Codice = "S019", Name = "PATENTINO FGAS", Descrizione = "PATENTINO FGAS", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("854d9e50-f28a-4ea5-baa1-5f330de34aea"), Codice = "S020", Name = "ATTESTATO SCORTA E TOLTA TENSIONE", Descrizione = "ATTESTATO SCORTA E TOLTA TENSIONE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("b77e31b0-ccfa-4f36-9993-6aee022eb2e0"), Codice = "S021", Name = "ATTESTATO GRU PER AUTOCARRO", Descrizione = "ATTESTATO GRU PER AUTOCARRO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                    new Specializzazione { ID = new Guid("d5dc6b26-7dbd-49cf-91e5-3f44bcb7322f"), Codice = "S022", Name = "ATTESTATO PONTI E VIADOTTI", Descrizione = "ATTESTATO PONTI E VIADOTTI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") }
                );
        }
    }
}
