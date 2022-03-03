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
    public static class CompaniesSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Ventura
            modelBuilder.Entity<ANA_Company>().HasData(
                    new ANA_Company
                    {
                        ID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        RagioneSociale = "Gruppo Francesco Ventura",
                        PIva = "0",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi="",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",                        
                        active = true,   
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),                     
                    },
                    new ANA_Company
                    {
                        ID = new Guid("157CA439-3132-49A7-A620-3804DE61B1BE"),
                        RagioneSociale = "MedTech",
                        PIva = "1",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    },
                    new ANA_Company
                    {
                        ID = new Guid("4294DE0C-71DB-441B-8B52-9CC7E3F06C4C"),
                        RagioneSociale = "Ventura Mineraria",
                        PIva = "2",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    },
                    new ANA_Company
                    {
                        ID = new Guid("EFEE2AFB-D85E-4BC0-B065-A9C25D427ABE"),
                        RagioneSociale = "Fleet",
                        PIva = "2",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    },
                    new ANA_Company
                    {
                        ID = new Guid("D679BDBD-53DC-4A12-A3AD-CD31BC93366F"),
                        RagioneSociale = "Paola Real Estate",
                        PIva = "2",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    },
                    new ANA_Company
                    {
                        ID = new Guid("A1C60DBA-1F39-4687-9CE6-D2876AF4033D"),
                        RagioneSociale = "Francesco Ventura",
                        PIva = "2",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    }, new ANA_Company
                    {
                        ID = new Guid("111C6341-215F-4FBD-8641-EC468199219F"),
                        RagioneSociale = "Binaria",
                        PIva = "2",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("CF66EAFF-5955-4CE1-A92B-091215E298A5"),
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    }, 
                    // KRESEARCH
                    new ANA_Company                    
                    {
                        ID = new Guid("BC1EAB39-75D2-4763-999F-DBFA95878A6F"),
                        RagioneSociale = "Polo 4.0",
                        PIva = "0",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi="",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",                        
                        active = true,   
                        MultiTenantId = new Guid("46CE0547-FA27-4BCE-92D1-8A32E15FE95E"),                     
                    },
                    new ANA_Company
                    {
                        ID = new Guid("C9E8C9FA-A29C-418B-B136-E9905869E44B"),
                        RagioneSociale = "KRD",
                        PIva = "1",
                        FiscalCode = "",
                        EmailPec = "",
                        CodiceSdi = "",
                        Nazione = "Italia",
                        Regione = "Puglia",
                        Provincia = "Le",
                        Citta = "Lecce",
                        Indirizzo = "",
                        EmailAziendale = "",
                        SitoWeb = "",
                        Telefono = "",
                        Fax = "",
                        active = true,
                        ParentID = new Guid("BC1EAB39-75D2-4763-999F-DBFA95878A6F"),
                        MultiTenantId = new Guid("46CE0547-FA27-4BCE-92D1-8A32E15FE95E"),
                    }                    
                );

        }
    }
}
