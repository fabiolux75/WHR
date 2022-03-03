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
    public static class ClaimsSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claims>().HasData(
                new Claims { ID = new Guid("74c41de4-cda7-4f2a-9be5-62ffa9dd728f"), title = "Users.Show", type = "Users.Show", description = "Visualizza Utenti" },
                new Claims { ID = new Guid("87887436-12d5-4311-900e-bd5b29d6cac6"), title = "Users.Create", type = "Users.Create", description = "Crea Utenti" },
                new Claims { ID = new Guid("4fb3636a-9a1c-45ce-8eb6-45d61eb721cb"), title = "Users.Edit", type = "Users.Edit", description = "Modifica Utenti" },
                new Claims { ID = new Guid("63a8d1f0-2a87-4277-b8f6-bfb287cd07b9"), title = "Cantiere.Show", type = "Cantiere.Show", description = "Visualizza Cantiere" },
                new Claims { ID = new Guid("21fe8b3a-db8b-494b-8c90-e636cbd6d21b"), title = "Cantiere.Create", type = "Cantiere.Create", description = "Crea Cantiere" },
                new Claims { ID = new Guid("bed0e6c4-40d1-40b8-98d9-ad74d9eb69d8"), title = "Cantiere.Edit", type = "Cantiere.Edit", description = "Modifica Cantiere" },
                new Claims { ID = new Guid("190df19f-08a6-4a20-926b-6f8285ce9665"), title = "Companies.Show", type = "Companies.Show", description = "Visualizza Aziende" },
                new Claims { ID = new Guid("81a716cf-fdad-47a4-9ad7-0915416bb1d0"), title = "Companies.Create", type = "Companies.Create", description = "Crea Aziende" },
                new Claims { ID = new Guid("c49eba26-8736-4a8d-aec1-cce7641eef8c"), title = "Companies.Edit", type = "Companies.Edit", description = "Modifica Aziende" },
                new Claims { ID = new Guid("ba385d95-89f5-4a9c-bb54-516756438550"), title = "Project.Show", type = "Project.Show", description = "Visualizza Commesse" },
                new Claims { ID = new Guid("2210dbd6-3893-45b7-8484-389c89277ae0"), title = "Project.Create", type = "Project.Create", description = "Crea Commesse" },
                new Claims { ID = new Guid("27b11d30-c698-4256-a9ff-64b1d32261f4"), title = "Project.Edit", type = "Project.Edit", description = "Modifica Commesse" },
                new Claims { ID = new Guid("6fdcda12-61db-4c9e-91a6-5d686a9a936e"), title = "ContractUser.Show", type = "ContractUser.Show", description = "Visualizza Contratti Utente" },
                new Claims { ID = new Guid("d155a8c2-82eb-4610-aebb-652cb580b78d"), title = "ContractUser.Create", type = "ContractUser.Create", description = "Crea Contratti Utente" },
                new Claims { ID = new Guid("6a4024b4-550a-4278-8ed0-abf7d594566c"), title = "ContractUser.Edit", type = "ContractUser.Edit", description = "Modifica Contratti Utente" },
                new Claims { ID = new Guid("66b6b76f-a1ea-4cd1-8301-9ca9319a9cf6"), title = "Document.Show", type = "Document.Show", description = "Visualizza Documenti Utente" },
                new Claims { ID = new Guid("711ae8b0-0eef-4d10-a6cf-94119e0b97ee"), title = "Document.Create", type = "Document.Create", description = "Crea Documenti Utente" },
                new Claims { ID = new Guid("77115fc4-7158-4674-86a0-43becc95b524"), title = "Document.Edit", type = "Document.Edit", description = "Modifica Documenti Utente" },
                new Claims { ID = new Guid("1b937324-9854-4012-a6d7-ee9081b3b4f6"), title = "MalattiaUser.Show", type = "MalattiaUser.Show", description = "Visualizza Malattie Utente" },
                new Claims { ID = new Guid("39a6f905-4ac8-4bc5-b4d2-bbf8bc464b78"), title = "MalattiaUser.Create", type = "MalattiaUser.Create", description = "Crea Malattie Utente" },
                new Claims { ID = new Guid("0d7f74d3-1dfe-4ef7-a16e-fcb04b999d84"), title = "MalattiaUser.Edit", type = "MalattiaUser.Edit", description = "Modifica Malattie Utente" },
                new Claims { ID = new Guid("a38c153f-1e09-4584-b0f8-8ebc482810cc"), title = "RelTurnoUser.Show", type = "RelTurnoUser.Show", description = "Visualizza Turno Utente, RelTurnoUser" },
                new Claims { ID = new Guid("71ba2680-71c9-49a3-bd7c-c0904688a79c"), title = "RelTurnoUser.Create", type = "RelTurnoUser.Create", description = "Crea Turno Utente, RelTurnoUser" },
                new Claims { ID = new Guid("9c06626d-3812-4532-9860-b7587adb02b4"), title = "Roles.Show", type = "Roles.Show", description = "Visualizza Ruoli" },
                new Claims { ID = new Guid("d59dc4e5-a585-47bc-b393-95766b0428b1"), title = "Roles.Create", type = "Roles.Create", description = "Crea Ruoli" },
                new Claims { ID = new Guid("b7c4dd9e-7af3-4a03-b465-2387ce77fbf0"), title = "Roles.Edit", type = "Roles.Edit", description = "Modifica Ruoli" },
                new Claims { ID = new Guid("c763d52e-fa93-4060-81a9-52675c071679"), title = "Timesheet.Show", type = "Timesheet.Show", description = "Visualizza Scheda Attività, Timesheet" },
                new Claims { ID = new Guid("bbf29f0f-4f67-4359-bcef-457bc4931268"), title = "Timesheet.Create", type = "Timesheet.Create", description = "Crea Scheda Attività, Timesheet" },
                new Claims { ID = new Guid("3924c0d8-056d-478a-a9b5-1cee51b14882"), title = "Timesheet.Edit", type = "Timesheet.Edit", description = "Modifica Scheda Attività, Timesheet" },
                new Claims { ID = new Guid("3b6acf0e-3a0f-4f62-ac66-0e845f9f5136"), title = "TimesheetDailyReport.Show", type = "TimesheetDailyReport.Show", description = "Visualizza Scheda Attività Giornaliera, TimesheetDailyReport" },
                new Claims { ID = new Guid("fe69536b-b4ef-49ac-a7eb-999895aac265"), title = "TimesheetDailyReport.Create", type = "TimesheetDailyReport.Create", description = "Crea Scheda Attività Giornaliera, TimesheetDailyReport" },
                new Claims { ID = new Guid("a8a0e2a8-90cd-495e-8704-d392a25ecff0"), title = "TimesheetDailyReport.Edit", type = "TimesheetDailyReport.Edit", description = "Modifica Scheda Attività Giornaliera, TimesheetDailyReport" },
                new Claims { ID = new Guid("f888c968-58ef-4f8d-8034-9759f325e974"), title = "Turni.Show", type = "Turni.Show", description = "Visualizza Turni" },
                new Claims { ID = new Guid("37984e75-d281-404b-afa5-29a63489b915"), title = "Turni.Create", type = "Turni.Create", description = "Crea Turni" },
                new Claims { ID = new Guid("8e3dc15b-0cd0-4987-9f9b-0f61c76b18c7"), title = "Turni.Edit", type = "Turni.Edit", description = "Modifica Turni" },
                new Claims { ID = new Guid("703359a7-ce97-4f58-ba6e-fe9ac5479d55"), title = "RelMansioneUser.Show", type = "RelMansioneUser.Show", description = "Visualizza Mansioni Utente, RelMansioneUser" },
                new Claims { ID = new Guid("1fa660ea-f47b-49a1-ab93-e335bf8cd8ea"), title = "RelMansioneUser.Create", type = "RelMansioneUser.Create", description = "Crea Mansioni Utente, RelMansioneUser" },
                new Claims { ID = new Guid("56a001ac-5007-4cf3-8a8e-b419e42c4917"), title = "RelMansioneUser.Edit", type = "RelMansioneUser.Edit", description = "Modifica Mansioni Utente, RelMansioneUser" },
                new Claims { ID = new Guid("cca2bc88-4311-41fc-8a9e-a80b5202b6ef"), title = "RelSpecializzazioneUser.Show", type = "RelSpecializzazioneUser.Show", description = "Visualizza Specializzazione Utente, RelSpecializzazioneUser" },
                new Claims { ID = new Guid("a54e611f-839b-42ce-82ea-fed1cc2d6e50"), title = "RelSpecializzazioneUser.Create", type = "RelSpecializzazioneUser.Create", description = "Crea Specializzazione Utente, RelSpecializzazioneUser" },
                new Claims { ID = new Guid("7f34d9f3-a1da-4656-858c-c90884070ba2"), title = "RelSpecializzazioneUser.Edit", type = "RelSpecializzazioneUser.Edit", description = "Modifica Specializzazione Utente, RelSpecializzazioneUser" },
                new Claims { ID = new Guid("5f8bd6bf-f38d-4699-b416-25fe86f732ce"), title = "Corsi.Show", type = "Corsi.Show", description = "Visualizza Corsi" },
                new Claims { ID = new Guid("b992734e-8e2e-4195-95dc-6d55a830d370"), title = "Corsi.Create", type = "Corsi.Create", description = "Crea Corsi" },
                new Claims { ID = new Guid("557f59de-75a4-480f-8d31-e4ba6888e516"), title = "Corsi.Edit", type = "Corsi.Edit", description = "Modifica Corsi" },
                new Claims { ID = new Guid("9F806088-A3D5-430E-ACF6-0EE2CCD579F5"), title = "VisitaMedica.Show", type = "VisitaMedica.Show", description = "Visualizza Visite Mediche" },
                new Claims { ID = new Guid("6AA38659-A818-4BF3-994F-2BC3F27983F3"), title = "VisitaMedica.Create", type = "VisitaMedica.Create", description = "Crea Visite Mediche" },
                new Claims { ID = new Guid("20387C78-55B1-4A3F-8521-16BAB36AA896"), title = "VisitaMedica.Edit", type = "VisitaMedica.Edit", description = "Modifica Visite Mediche" },
                new Claims { ID = new Guid("A49FF5ED-725B-4D01-8388-F289405D6EBA"), title = "RichiestaDipendente.Show", type = "RichiestaDipendente.Show", description = "Visualizza Richieste Dipendenti" },
                new Claims { ID = new Guid("B1AFE985-EFDB-4A15-A8FE-BAB5BD15C11A"), title = "RichiestaDipendente.Create", type = "RichiestaDipendente.Create", description = "Crea Richiesta Dipendentee" },
                new Claims { ID = new Guid("A4888C9A-4694-433A-8C09-2A99712FAEA1"), title = "RichiestaDipendente.Edit", type = "RichiestaDipendente.Edit", description = "Modifica Richiesta Dipendente" }                
                );
        }
    }
}
