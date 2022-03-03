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
    public static class MansioniSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mansione>().HasData(
                                new Mansione
                                {
                                    ID = new Guid("5B0491E8-3FE6-4240-A4AA-9B4A89B6B0D1"),
                                    Codice = "MAN000",
                                    Name = "Responsabile gestione ore turno",
                                    Descrizione = "Responsabile gestione ore turno",
                                    isAssignedAsDefault = 1,
                                    isEnabledManageHour = 1,
                                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb")
                                },
                                new Mansione { ID = new Guid("8b2b8e6d-b7f5-4d1b-abc1-c625da10e99f"), Codice = "MAN001", Name = "ADDETTA PULIZIE", Descrizione = "ADDETTA PULIZIE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("9eb584c8-a372-49b9-8290-4a2c70ed8513"), Codice = "MAN002", Name = "ADDETTO PONTEGGI", Descrizione = "ADDETTO PONTEGGI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("70c797fa-9909-4266-a1ab-00e74fa1ac6e"), Codice = "MAN003", Name = "ADDETTO RILIEVI", Descrizione = "ADDETTO RILIEVI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("02703a2a-18ec-4c09-81bd-c36478837f70"), Codice = "MAN004", Name = "ASPP", Descrizione = "ASPP", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b90642fe-1684-402b-b429-757f7df85e9c"), Codice = "MAN005", Name = "ASSISTENTE DI CANTIERE E ADDETTO ALLA QUALITA'", Descrizione = "ASSISTENTE DI CANTIERE E ADDETTO ALLA QUALITA'", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("6f612119-1704-47c2-92a5-a8327b0391f9"), Codice = "MAN006", Name = "ASSISTENTE MECCANICO", Descrizione = "ASSISTENTE MECCANICO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("7177a0ae-f046-43c2-85cd-42ceb4b84c30"), Codice = "MAN007", Name = "AUTISTA", Descrizione = "AUTISTA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("ed278b71-093a-49b1-9eea-bb31a24127e8"), Codice = "MAN008", Name = "AUTISTA CAMION", Descrizione = "AUTISTA CAMION", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("88619738-1336-4b98-bff7-4c8baa8e4c3f"), Codice = "MAN009", Name = "AUTISTA DUMPER", Descrizione = "AUTISTA DUMPER", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("72ed47c4-4286-4c34-a867-291b85bdb5d5"), Codice = "MAN010", Name = "CAPO CANTIERE", Descrizione = "CAPO CANTIERE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("2fe3364f-d8cf-4425-95d6-87f085e89bae"), Codice = "MAN011", Name = "CAPO SQUADRA", Descrizione = "CAPO SQUADRA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("8803244b-741f-4606-a0f7-6d8483a1f1c5"), Codice = "MAN012", Name = "CARRI GRUETTE", Descrizione = "CARRI GRUETTE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("8bb6e628-c02d-41d3-9726-8f7a9775c96f"), Codice = "MAN013", Name = "CARRI RISANATRICE", Descrizione = "CARRI RISANATRICE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("bebb005d-1b5a-4858-9fde-ddb5b608287f"), Codice = "MAN014", Name = "CARRI WINDHOFF", Descrizione = "CARRI WINDHOFF", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("e8e942db-a381-4da0-8843-e849cbf5c6e8"), Codice = "MAN015", Name = "CARROZZIERE", Descrizione = "CARROZZIERE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("79842194-de56-4abe-bd07-540951051edf"), Codice = "MAN016", Name = "DIRETTORE DI CANTIERE", Descrizione = "DIRETTORE DI CANTIERE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("c7cb2e01-2ec7-42f4-9619-562c1a69258c"), Codice = "MAN017", Name = "DIRETTORE TECNICO", Descrizione = "DIRETTORE TECNICO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("96e446c3-353a-49f2-8957-50dff43928e8"), Codice = "MAN018", Name = "DIREZIONE", Descrizione = "DIREZIONE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("301c4883-fe51-4339-813a-d7231fd2b98d"), Codice = "MAN019", Name = "ELETTRICISTA", Descrizione = "ELETTRICISTA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b8aeb862-474f-45a2-819a-1604c9272d01"), Codice = "MAN020", Name = "GUARDIANO", Descrizione = "GUARDIANO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b435269b-f845-4979-a51a-49dbb077e00f"), Codice = "MAN021", Name = "IMPIANTI ELETTRICI", Descrizione = "IMPIANTI ELETTRICI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("8c96b155-e015-4116-a3ca-a3dd366fa038"), Codice = "MAN022", Name = "IMPIANTO DI ILLUMINAZIONE", Descrizione = "IMPIANTO DI ILLUMINAZIONE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("a3158132-8698-4987-8f80-068b2828a6c5"), Codice = "MAN023", Name = "MECCANICO", Descrizione = "MECCANICO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("bc68716a-634a-429f-97eb-08e5d2140d6d"), Codice = "MAN024", Name = "OPERAIO", Descrizione = "OPERAIO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b7eb46f4-dbe5-49fe-a253-d078b6848e14"), Codice = "MAN025", Name = "OPERAIO - MANUTENZIONE RISANATRICI E CARRI", Descrizione = "OPERAIO - MANUTENZIONE RISANATRICI E CARRI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("44aec8ba-48d8-49b2-a42c-b8ba80b3c508"), Codice = "MAN026", Name = "OPERAIO CARRI RISANATRICE", Descrizione = "OPERAIO CARRI RISANATRICE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b3211b66-685d-4522-8996-2c04b73c6ab5"), Codice = "MAN027", Name = "OPERAIO CAVA", Descrizione = "OPERAIO CAVA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("72590be4-38aa-432b-a2dd-73784b32cf7c"), Codice = "MAN028", Name = "OPERAIO MAGAZZINO", Descrizione = "OPERAIO MAGAZZINO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("330944a0-a6ce-4cd7-9f08-2e84729b6c62"), Codice = "MAN029", Name = "OPERAIO TRAMVIA", Descrizione = "OPERAIO TRAMVIA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b06b1a62-da0b-43ed-add8-89439476ad72"), Codice = "MAN030", Name = "OPERAIO TRENO RINNOVATORE", Descrizione = "OPERAIO TRENO RINNOVATORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("c28da311-584a-4eae-a1ce-4a3884d0f921"), Codice = "MAN031", Name = "OPERATORE CARICATORE", Descrizione = "OPERATORE CARICATORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("fbd7a922-cb24-4add-8cef-a5fc0da2b81b"), Codice = "MAN032", Name = "OPERATORE CARICATORE DA PIAZZALE", Descrizione = "OPERATORE CARICATORE DA PIAZZALE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("58e546e7-57ea-4e2b-bf68-9bcb20744a67"), Codice = "MAN033", Name = "OPERATORE CARRELLO WINDHOFF", Descrizione = "OPERATORE CARRELLO WINDHOFF", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("63d8d6eb-3a4c-4949-b0bf-a5c8c12fcb79"), Codice = "MAN034", Name = "OPERATORE COMPATTATRICE", Descrizione = "OPERATORE COMPATTATRICE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("567c5203-09f9-4699-9696-773cc0c90370"), Codice = "MAN035", Name = "OPERATORE ESCAVATORE", Descrizione = "OPERATORE ESCAVATORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("e988f39c-e3f5-4497-97dc-43a1839faad9"), Codice = "MAN036", Name = "OPERATORE ESCAVATORE FINO A 80 QT", Descrizione = "OPERATORE ESCAVATORE FINO A 80 QT", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), Codice = "MAN037", Name = "OPERATORE LIVELLISTA", Descrizione = "OPERATORE LIVELLISTA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), Codice = "MAN038", Name = "OPERATORE LOCOMOTORE", Descrizione = "OPERATORE LOCOMOTORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("f6999d2a-28d9-4134-a280-a24ece5e8a2c"), Codice = "MAN039", Name = "OPERATORE PALA", Descrizione = "OPERATORE PALA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("71be202e-5de3-4623-b2e8-db24a2f67594"), Codice = "MAN040", Name = "OPERATORE PINSE E SCAMBI", Descrizione = "OPERATORE PINSE E SCAMBI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("ea052b6c-42f4-4b8c-91d8-bfd4f22ab189"), Codice = "MAN041", Name = "OPERATORE PORTALE RINNOVO SCART RIDOTTO", Descrizione = "OPERATORE PORTALE RINNOVO SCART RIDOTTO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("5385c641-9a4e-4201-a47b-722209f41f40"), Codice = "MAN042", Name = "OPERATORE PORTALINI", Descrizione = "OPERATORE PORTALINI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("baa2a9bb-e87d-41f4-8a1f-bbd356f225c7"), Codice = "MAN043", Name = "OPERATORE PORTALINI SCAMBI", Descrizione = "OPERATORE PORTALINI SCAMBI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), Codice = "MAN044", Name = "OPERATORE PROFILATRICE", Descrizione = "OPERATORE PROFILATRICE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), Codice = "MAN045", Name = "OPERATORE RINCALZATORE", Descrizione = "OPERATORE RINCALZATORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("7511ac24-b75b-445a-abf2-3f871204ecbd"), Codice = "MAN046", Name = "OPERATORE RINCALZATORE PINSE E SCAMBI", Descrizione = "OPERATORE RINCALZATORE PINSE E SCAMBI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("860a8bdd-4d2b-488b-ab08-748c4914a042"), Codice = "MAN047", Name = "OPERATORE RISANATRICE", Descrizione = "OPERATORE RISANATRICE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("428769de-f4d9-41f3-b205-647666ddc864"), Codice = "MAN048", Name = "OPERATORE RISANATRICE ESTERNO", Descrizione = "OPERATORE RISANATRICE ESTERNO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), Codice = "MAN049", Name = "OPERATORE SALDATRICE SCINTILLIO", Descrizione = "OPERATORE SALDATRICE SCINTILLIO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("942ed7b9-15d2-4241-ad71-ad17ed3ff2e8"), Codice = "MAN050", Name = "OPERATORE TRENO RINNOVATORE", Descrizione = "OPERATORE TRENO RINNOVATORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("6bf78f3c-46af-4fd7-a7b2-b5d86bc2e77f"), Codice = "MAN051", Name = "PORTALINI SCAMBI", Descrizione = "PORTALINI SCAMBI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("da843ef1-eea3-415b-b3be-89794fa4eff9"), Codice = "MAN052", Name = "RSPP", Descrizione = "RSPP", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("b12da303-a228-4ae1-a460-ec8d4afe6a98"), Codice = "MAN053", Name = "SALDATORE AD ARCO", Descrizione = "SALDATORE AD ARCO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("0be565d8-cd9d-411f-906e-394a0314ad28"), Codice = "MAN054", Name = "SALDATORE ALLUMINOTERMICO", Descrizione = "SALDATORE ALLUMINOTERMICO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("df84bfd4-c35d-439c-9870-8aaac2bf8412"), Codice = "MAN055", Name = "SALDATORE CAVA", Descrizione = "SALDATORE CAVA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("96133186-46ad-4f61-a56f-a8a9106e1a9b"), Codice = "MAN056", Name = "SALDATORE OFFICINA", Descrizione = "SALDATORE OFFICINA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("5d8c8f1a-a290-44b8-9ec5-3ea70f48389c"), Codice = "MAN057", Name = "SCARTAMENTISTA", Descrizione = "SCARTAMENTISTA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("abafe7bb-9b77-4fd1-9c2b-03eda91ba992"), Codice = "MAN058", Name = "TORNITORE", Descrizione = "TORNITORE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("afb65cd1-1b1b-4f87-90b4-e135e7ef2f35"), Codice = "MAN059", Name = "UFFICIO AFFARI GENERALI", Descrizione = "UFFICIO AFFARI GENERALI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("f94c7c83-6c7b-4496-9e1a-5e0654a681a6"), Codice = "MAN060", Name = "UFFICIO AMMINISTRAZIONE", Descrizione = "UFFICIO AMMINISTRAZIONE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("10dddfa4-a6a1-4c54-9a64-549aa13ef92f"), Codice = "MAN061", Name = "UFFICIO APPROVIGIONAMENTO", Descrizione = "UFFICIO APPROVIGIONAMENTO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("c5d1aab6-70fd-4a25-a565-fd089e8a8eed"), Codice = "MAN062", Name = "UFFICIO CONTABILITA LAVORI", Descrizione = "UFFICIO CONTABILITA LAVORI", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("d47bdb67-19db-4bb0-b560-408bb5d61de0"), Codice = "MAN063", Name = "UFFICIO CONTROLLO DI GESTIONE", Descrizione = "UFFICIO CONTROLLO DI GESTIONE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("5d6b8b30-852c-479a-92d6-30aaf2537165"), Codice = "MAN064", Name = "UFFICIO GARE", Descrizione = "UFFICIO GARE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("e4a48625-1ab9-4526-9933-273b34fc8faf"), Codice = "MAN065", Name = "UFFICIO INFRASTRUTTURE", Descrizione = "UFFICIO INFRASTRUTTURE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("7fb2c0f5-7369-4f41-a4a6-74d2605097a9"), Codice = "MAN066", Name = "UFFICIO LOGISTICA PERSONALE", Descrizione = "UFFICIO LOGISTICA PERSONALE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("e7c4eb60-d9ed-461e-9882-248de34b86ad"), Codice = "MAN067", Name = "UFFICIO PAGHE", Descrizione = "UFFICIO PAGHE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("1b25c42f-304b-44b5-8fbb-438db071bf49"), Codice = "MAN068", Name = "UFFICIO PERSONALE", Descrizione = "UFFICIO PERSONALE", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("15be68e6-3a4a-49ba-aaf5-4cf3334c79ae"), Codice = "MAN069", Name = "UFFICIO PROTOCOLLO", Descrizione = "UFFICIO PROTOCOLLO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("7724d562-9c72-4d8e-a3bc-bf09157f16fd"), Codice = "MAN070", Name = "UFFICIO QUALITA", Descrizione = "UFFICIO QUALITA", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("bcd5ba2f-1b27-412d-b642-8e6097425c34"), Codice = "MAN071", Name = "UFFICIO SISTEMA INFORMATIVO", Descrizione = "UFFICIO SISTEMA INFORMATIVO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") },
                                new Mansione { ID = new Guid("dc930044-acfb-47e8-8eb0-0e2dae104502"), Codice = "MAN072", Name = "UFFICIO TECNICO", Descrizione = "UFFICIO TECNICO", MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb") }


                );


        }
    }
}
