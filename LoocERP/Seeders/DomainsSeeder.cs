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
    public static class DomainsSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Domain>().HasData(
                /* Documenti relativi al settaggio del profilo utente*/
                new Domain
                    {
                        Id = new Guid("45A09B15-1BC4-4244-A56C-DA2E417ADBEB"),
                        Name = "Carta D'Identita",
                        Tipo = "document_profile",
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    },
                 new Domain{
                        Id = new Guid("C66CD9E8-6773-44ED-A3F8-B6A8CA9ABF05"),
                        Name = "Patente",
                        Tipo = "document_profile",
                        MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                    },                    
                new Domain{
                    Id = new Guid("7A8447F6-33D1-4527-AD63-423AE67212E4"),
                    Name = "Passaporto",
                    Tipo = "document_profile",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },


                //Documenti visibili sulla pagina documenti
                new Domain{
                    Id = new Guid("9F6D3370-302A-4D34-AB92-D64A476392AC"),
                    Name = "Documento Generico",
                    Tipo = "document_index",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },

                //Titolo di studio
                new Domain{
                    Id = new Guid("99C7150B-2424-4E9A-AF01-1C37152D1175"),
                    Name = "Diploma",
                    Tipo = "document_titolo_studio",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },                                
                //... TODO aggiungere quelli corretti

                //Codice Livello Contratto Lavorativo
                new Domain{
                    Id = new Guid("628DA834-F730-464B-A533-A08C6E2099F5"),
                    Code = "M3",
                    Name = "METALMECCANICO LIVELLO 3",
                    Tipo = "contract_user_level",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },
                //... TODO aggiungere quelli corretti

                //Codice Tipo Retribuzione Contratto Lavorativo
                new Domain{
                    Id = new Guid("E7C5A7E5-80F6-40E5-8566-DA409765F004"),
                    Code = "OR",
                    Name = "Oraria",
                    Tipo = "contract_user_tipo_retribuzione",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },

                //Codice Tipo Orario Lavoro Contratto Lavorativo
                new Domain{
                    Id = new Guid("012256F5-4C8B-4DD0-8A78-23E9D2EDF665"),
                    Code = "FP",
                    Name = "Tempo pieno",
                    Tipo = "contract_user_tipo_orario",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },

                //Codice Tipo Contratto Lavorativo
                new Domain{
                    Id = new Guid("D6273C9B-A459-4457-9F1E-AE96F224F0B8"),
                    Code = "TE",
                    Name = "Tempo determinato",
                    Tipo = "contract_user_tipologia",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },

                //Codice Tipo Contratto Lavorativo
                new Domain{
                    Id = new Guid("C1AD61B4-3EB4-4A92-9A37-6267FCA03618"),
                    Code = "TE",
                    Name = "Tempo determinato",
                    Tipo = "contract_user_tipologia",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },
                
                //Codice Numero di Legge Contratto Lavorativo
                new Domain{
                    Id = new Guid("BE3FFFD7-E1A9-4E24-9D97-9C1FF9A9573D"),
                    Code = "68",
                    Name = "68",
                    Tipo = "contract_user_law_number",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                },

                //Codice Categoria Contratto Lavorativo
                new Domain{
                    Id = new Guid("44730230-FB83-48E2-9EBC-CE6B4FAB55B0"),
                    Code = "Impiegato",
                    Name = "Impiegato",
                    Tipo = "contract_user_category",
                    MultiTenantId = new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"),
                }               
            );            
        }
    }
}
