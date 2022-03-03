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
    public static class GiustificativiSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Giustificativo>().HasData(                
                new Giustificativo
                {
                    Id = new Guid("2E6A4B1D-A2D3-4DE9-AE0F-483768C4D874"), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEA"),
                    Code = "SER",
                    Name = "SERVIZIO",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = new Guid("124C98B5-A5E5-48AE-B052-EBCB6E6A3A08"), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEB"),
                    Code = "TIM",
                    Name = "PRESENZA SENZA TIMBRATURE",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                /*
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEC"),
                    Code = "ANR",
                    Name = "ASSENZA NON RETRIBUITA",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                }, 
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBED"),
                    Code = "TRA",
                    Name = "MISSIONE ITALIA",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEE"),
                    Code = "TES",
                    Name = "MISSIONE ESTERO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADAEA"),
                    Code = "PGL",
                    Name = "PRESTAZIONE IN GIORNATA NON LAVORATIVA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEF"),
                    Code = "PFI",
                    Name = "PRESTAZIONE IN GIORNATA FESTIVA INFRASETTIMANALE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADAEB"),
                    Code = "INL",
                    Name = "INTERVENTO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADAEC"),
                    Code = "ROL",
                    Name = "RECUPERO ORE LAVORATE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADAEE"),
                    Code = "RCO",
                    Name = "RIPOSO COMPENSATIVO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADAED"),
                    Code = "STR",
                    Name = "STRAORDINARIO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADAEF"),
                    Code = "STM",
                    Name = "STRAORDINARIO MANUALE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                 new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEA"),
                    Code = "NRI",
                    Name = "MAGGIOR PRESTAZIONE NON RICONOSCIUTA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                 new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEB"),
                    Code = "CSE",
                    Name = "PARTECIPAZIONE A CORSI/SEMINARI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                 new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEC"),
                    Code = "CSA",
                    Name = "PARTECIPAZIONE A CORSI RESIDENZIALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                 new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBED"),
                    Code = "CPP",
                    Name = "CORSO PREVENZIONE E PROTEZIONE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBET"),
                    Code = "FOR",
                    Name = "CORSO DI FORMAZIONE IN GIORNATA FESTIVA/SEMIFESTIVA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },                
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEU"),
                    Code = "ROF",
                    Name = "RECUPERO ORARIO FORMAZIONE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEV"),
                    Code = "SCP",
                    Name = "SCIOPERO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEZ"),
                    Code = "ASS",
                    Name = "ASSEMBLEA RETRIBUITA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFA"),
                    Code = "ASF",
                    Name = "ASSEMBLEA FUORI ORARIO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFB"),
                    Code = "VDA",
                    Name = "VISITA MEDICA D.LGS. N. 81/08",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                */
                new Giustificativo
                {
                    Id = new Guid("D0EF1825-A2AA-429B-B16C-C39035BB4874"), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFC"),
                    Code = "FER",
                    Name = "FERIE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                /*
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFD"),
                    Code = "EXF",
                    Name = "PERMESSO EX FESTIVITA’",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFE"),
                    Code = "PE2",
                    Name = "PERMESSO RETRIBUITO EX FESTIVITA' CARIVE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFF"),
                    Code = "PRU",
                    Name = "PERMESSO RETRIBUITO EX CRUP",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFG"),
                    Code = "PFE",
                    Name = "PERMESSO RETRIBUITO PREMIO FEDELTA' EX BPDA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                */
                new Giustificativo
                {
                    Id = new Guid("060274BA-E860-473F-BD0D-73D7DDF7FFEA"), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFH"),
                    Code = "PCR",
                    Name = "PERMESSO CONTRATTUALE RETRIBUITO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                /*
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFI"),
                    Code = "PPI",
                    Name = "BREVE PERMESSO RETRIBUITO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFL"),
                    Code = "NRE",
                    Name = "PERMESSO NON RETRIBUITO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFM"),
                    Code = "PEC",
                    Name = "PERMESSO CON RECUPERO ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFN"),
                    Code = "RBO",
                    Name = "RECUPERO BANCA ORE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFO"),
                    Code = "SM1",
                    Name = "PERMESSO STUDIO GIORNO DELL’ESAME MEDIA INFERIORE/SUPERIORE ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFP"),
                    Code = "SM2",
                    Name = "PERMESSO STUDIO ANNUALE GIORNATA INTERA MEDIA/SUPERIORE (8 gg.) ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFQ"),
                    Code = "SM3",
                    Name = "PERMESSO STUDIO FRAZIONATO MEDIA INFERIORE/SUPERIORE (8 gg.) ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFR"),
                    Code = "SM4",
                    Name = "ASPETTATIVA NON RETRIBUITA PER STUDIO MEDIA INFERIORE/SUPERIORE (30 gg.)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFS"),
                    Code = "UN1",
                    Name = "PERMESSO STUDIO ESAMI UNIVERSITA’",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFT"),
                    Code = "UN2",
                    Name = "PERMESSO STUDIO ESAME LAUREA (6 gg.)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFU"),
                    Code = "UN3",
                    Name = "PERMESSO STUDIO ESAME LAUREA MAGISTRALE (4 gg.) ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFV"),
                    Code = "UN4",
                    Name = "ASPETTATIVA NON RETRIBUITA PER ESAME LAUREA (180 gg.)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGA"),
                    Code = "ML5",
                    Name = "CONGEDO NON RETRIBUITO PER FORMAZIONE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGB"),
                    Code = "PT1",
                    Name = "PERMESSO RETRIBUITO TRASLOCO ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGC"),
                    Code = "PT2",
                    Name = "PERMESSO RETRIBUITO TRASLOCO DISPOSTO D’UFFICIO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGD"),
                    Code = "PEL",
                    Name = "PERMESSO RETRIBUITO ELEZIONI ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGE"),
                    Code = "ESL",
                    Name = "GIORNO ELETTORALE CON RIPOSO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBFZ"),
                    Code = "ESR",
                    Name = "SABATO ELETTORALE RETRIBUITO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },

                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGF"),
                    Code = "RCE",
                    Name = "RIPOSO COMPENSATIVO PER ELEZIONI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGG"),
                    Code = "LRA",
                    Name = "RICHIAMO ALLE ARMI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGH"),
                    Code = "LEP",
                    Name = "CONGEDO POST SERVIZIO MILITARE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGI"),
                    Code = "PPC",
                    Name = "PERMESSO RETRIBUITO PROTEZIONE CIVILE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGL"),
                    Code = "FON",
                    Name = "PERMESSI PER RIUNIONI ORGANI SOCIALI FONDI PENSIONE E CASSA SANITARIA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGM"),
                    Code = "RDS",
                    Name = "PERMESSO RETRIBUITO GIORNALIERO DONAZIONE SANGUE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGN"),
                    Code = "PDO",
                    Name = "PERMESSO RETRIBUITO ORARIO DONAZIONE SANGUE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGO"),
                    Code = "PDM",
                    Name = "PERMESSO RETRIBUITO GIORNALIERO DONAZIONE MIDOLLO OSSEO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGP"),
                    Code = "POD",
                    Name = "PERMESSO RETRIBUITO ORARIO DONAZIONE MIDOLLO OSSEO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGQ"),
                    Code = "LMA",
                    Name = "CONGEDO MATRIMONIALE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGR"),
                    Code = "AP5",
                    Name = "ASPETTATIVA NON RETRIBUITA PER MOTIVI PERSONALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGS"),
                    Code = "NRP",
                    Name = "PERMESSO NON RETRIBUITO MOTIVI PERSONALI/FAMILIARI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGT"),
                    Code = "NRM",
                    Name = "PERMESSO NON RETRIBUITO MALATTIA FAMILIARI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGU"),
                    Code = "ML3",
                    Name = "PERMESSO RETRIBUITO PER GRAVE INFERMITA’",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGV"),
                    Code = "PLU",
                    Name = "PERMESSO RETRIBUITO PER LUTTO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBGZ"),
                    Code = "PCP",
                    Name = "PERMESSI RETRIBUITI CONTROLLI PRENATALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHA"),
                    Code = "PRF",
                    Name = "PERMESSO RETRIBUITO NASCITA FIGLIO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHB"),
                    Code = "MAA",
                    Name = "MATERNITA’ INTERDIZIONE ANTICIPATA ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHC"),
                    Code = "MAT",
                    Name = "CONGEDO DI MATERNITA’/PATERNITA’ ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHD"),
                    Code = "MPP",
                    Name = "CONGEDO PARENTALE (astensione facoltativa)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHE"),
                    Code = "ML1",
                    Name = "CONGEDO PARENTALE NON INDENNIZZATO (astensione facoltativa)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHF"),
                    Code = "MP3",
                    Name = "ASPETTATIVA NON RETRIBUITA PER PUERPERIO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHG"),
                    Code = "MAD",
                    Name = "CONGEDO MATERNITA’/PATERNITA’ PER ADOZIONE/AFFIDAMENTO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHH"),
                    Code = "MAI",
                    Name = "ADOZIONE E AFFIDAMENTO PREADOTTIVO INTERNAZIONALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHI"),
                    Code = "MAF",
                    Name = "CONGEDO PARENTALE PER ADOZIONE/AFFIDAMENTO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHM"),
                    Code = "ML0",
                    Name = "CONGEDO PARENTALE PER ADOZIONE/AFFIDAMENTO NON INDENNIZZATO ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHN"),
                    Code = "MFI",
                    Name = "MALATTIA FIGLIO FINO A 3 ANNI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHO"),
                    Code = "ML2",
                    Name = "MALATTIA FIGLIO DAI 3 AGLI 8 ANNI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHP"),
                    Code = "MR1",
                    Name = "RIPOSO GIORNALIERO LAVORATRICE MADRE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHQ"),
                    Code = "ML6",
                    Name = "RIPOSO GIORNALIERO LAVORATRICE MADRE – PARTO PLURIMO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHR"),
                    Code = "HA1",
                    Name = "PROLUNGAMENTO CONGEDO PARENTALE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHS"),
                    Code = "HA2",
                    Name = "PERMESSO ORARIO ASSISTENZA FIGLIO PORTATORE DI HANDICAP GRAVE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHT"),
                    Code = "HA3",
                    Name = "PERMESSO GIORNALIERO ASSISTENZA PORTATORE DI HANDICAP GRAVE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHU"),
                    Code = "HA6",
                    Name = "PERMESSO ORARIO PORTATORE DI HANDICAP GRAVE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHV"),
                    Code = "HA7",
                    Name = "PERMESSO GIORNALIERO PORTATORE DI HANDICAP GRAVE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBHZ"),
                    Code = "RDL",
                    Name = "RECUPERO PERMESSO MR1/HA2/HA6",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },


                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIA"),
                    Code = "ML7",
                    Name = "CONGEDO STRAORDINARIO RETRIBUITO D.LGS. N. 151/01",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                */
                new Giustificativo
                {
                    Id = new Guid("37017210-62A6-4ECF-BA7F-409DB3E8F333"), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIB"),
                    Code = "MAL",
                    Name = "MALATTIA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = new Guid("F36477E6-B3AE-48DB-A65B-63D1CDD2F8C0"), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIC"),
                    Code = "MIN",
                    Name = "INFORTUNIO SUL LAVORO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                }
                /*
                ,
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBID"),
                    Code = "AMN",
                    Name = "ASPETTATIVA NON RETRIBUITA PER MALATTIA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIE"),
                    Code = "MNR",
                    Name = "MALATTIA NON RETRIBUITA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIF"),
                    Code = "ATA",
                    Name = "ASPETTATIVA NON RETRIBUITA PER LAVORATORI TOSSICODIPENDENTI/ALCOLISTI E FAMILIARI CHE LI ASSISTONO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIG"),
                    Code = "MAD",
                    Name = "CONGEDO MATERNITA’/PATERNITA’ PER ADOZIONE/AFFIDAMENTO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIH"),
                    Code = "PVM",
                    Name = "PERMESSO RETRIBUITO VISITA MEDICA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBII"),
                    Code = "PDI",
                    Name = "PERMESSO RETRIBUITO PER DIALISI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIM"),
                    Code = "NCT",
                    Name = "PERMESSO NON RETRIBUITO PER CURE TERMALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIN"),
                    Code = "NIC",
                    Name = "CONGEDO RETRIBUITO PER LAVORATORI MUTILATI ED INVALIDI CIVILI L. n. 118/71",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIO"),
                    Code = "ACN",
                    Name = "ASPETTATIVA NON RETRIBUITA PER CARICA PUBBLICA MEMBRI DEL PARLAMENTO NAZIONALE/EUROPEO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIP"),
                    Code = "ACR",
                    Name = "ASPETTATIVA NON RETRIBUITA PER CARICA PUBBLICA MEMBRI DI ASSEMBLEE REGIONALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIQ"),
                    Code = "ACP",
                    Name = "ASPETTATIVA NON RETRIBUITA PER CARICA PUBBLICA - AMMINISTRATORI DI ENTI LOCALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIR"),
                    Code = "CP1",
                    Name = "PERMESSI RETRIBUITI PER CARICA PUBBLICA (24 h)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIS"),
                    Code = "CP2",
                    Name = "PERMESSI RETRIBUITI PER CARICA PUBBLICA (48h)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIT"),
                    Code = "CPC",
                    Name = "PERMESSO RETRIBUITO PER CARICA PUBBLICA PER LA GIORNATA DEL CONSIGLIO/RIUNIONE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIU"),
                    Code = "CPN",
                    Name = "PERMESSI NON RETRIBUITI PER CARICA PUBBLICA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIV"),
                    Code = "PO1",
                    Name = "PERMESSO RETRIBUITO PER CONSIGLIERI DI PARI OPPORTUNITA’ (50 h.)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBIZ"),
                    Code = "PO2",
                    Name = "PERMESSO RETRIBUITO PER CONSIGLIERI DI PARI OPPORTUNITA’ (30 h.)",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLA"),
                    Code = "PO3",
                    Name = "PERMESSO NON RETRIBUITO PER CONSIGLIERI DI PARI OPPORTUNITA’ ",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLB"),
                    Code = "PRI",
                    Name = "PERMESSO RETRIBUITO CROCE ROSSA ITALIANA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLC"),
                    Code = "NGP",
                    Name = "PERMESSO NON RETRIBUITO PER GIUDICE POPOLARE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLD"),
                    Code = "ACS",
                    Name = "ASPETTATIVA PER CARICA SINDACALE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLE"),
                    Code = "CED",
                    Name = "PERMESSI SINDACALI A CEDOLE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLF"),
                    Code = "CRA",
                    Name = "PERMESSI COORDINATORI DELLE RR.SS.AA DI AREA ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLG"),
                    Code = "RS2",
                    Name = "PERMESSI ORARI RSA IN PIAZZA CON PIU’ DI 200 DIPENDENTI ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLH"),
                    Code = "RS3",
                    Name = "PERMESSI RSA PER RIUNIONE ORGANO DI COORDINAMENTO ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLI"),
                    Code = "RS4",
                    Name = "PERMESSI NON RETRIBUITI RSA/SEGR. ORG. COORD. PER CONGRESSI E CONVEGNI ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLM"),
                    Code = "SC1",
                    Name = "PERMESSI SEGR. ORG. COORD. NON IN DISTACCO PIENO",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLN"),
                    Code = "SC2",
                    Name = "PERMESSI SEGR. ORG. COORD. NON IN DISTACCO PIENO PER RIUNIONI ORG. DI COORD.",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLO"),
                    Code = "SC3",
                    Name = "PERMESSI SEGR. ORG. COORD. IN DISTACCO PIENO/DISTACCHI EX ACCORDO 29.12.2004",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLP"),
                    Code = "SI9",
                    Name = "PERMESSI PER INCONTRI SEMESTRALI/ANNUALI/CONTROLLI STRAORDINARI/INCONTRI AZIENDALI",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLR"),
                    Code = "SNR",
                    Name = "PERMESSI SINDACALI NON REGOLARIZZATI DA CEDOLE",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLS"),
                    Code = "FGR",
                    Name = "PARTECIPAZIONE OO.SS. AI -FOCUS GROUP- ",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                },
                new Giustificativo
                {
                    Id = Guid.NewGuid(), //Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBLT"),
                    Code = "RLS",
                    Name = "PERMESSI PER I RAPPRESENTANTI DEI LAVORATORI PER LA SICUREZZA",                    
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    Active = true,
                }          
                */      
            );            
        }
    }
}
