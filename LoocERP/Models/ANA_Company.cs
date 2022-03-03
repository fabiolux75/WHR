using System.Net.Security;
using System.Reflection.Metadata;
using LoocERP.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace LoocERP.Models
{

    public enum ModPagamento
    {


        [Display(Name = "Rimessa diretta")]
        Rimessa = 0,

        [Display(Name = "30 gg fine mese")]
        GG30 = 1,

        [Display(Name = "30 60 gg fine mese")]
        GG3060 = 2,

        [Display(Name = "30 60 90 gg fine mese")]
        GG306090 = 3,

    }
    public enum TipoPagamento
    {
        [Display(Name = "Assegno")]
        Assegno = 0,        

        [Display(Name = "Bonifico Bancario")]
        BonificoBancario = 1,        

        [Display(Name = "Contrassegno")]
        Contrassegno = 2,        

        [Display(Name = "Conto  corrente postale")]
        ContoCorrentePostale = 3,        

        [Display(Name = "Carta di credito")]
        CartaDiCredito = 4,        

        [Display(Name = "Pagherò")]
        Paghero = 5,        

        [Display(Name = "Pagamento anticipato")]
        PagamentoAnticipato = 6,        

        [Display(Name = "Contanti")]
        Contanti = 7,        

        [Display(Name = "R.I.D.")]
        RID = 8,        

        [Display(Name = "Rimessa diretta")]
        RimessaDiretta = 9,

        [Display(Name = "Ricevuta bancaria")]
        RicevutaBancaria = 10,

        [Display(Name = "Tratte")]
        Tratte = 11,

        [Display(Name = "Vaglia")]
        Vaglia = 12,

        [Display(Name = "PayPal")]
        PayPal = 13,

    }


    public enum Pagamento
    {
        [Display(Name = "Avvenuto")]
        Avvenuto = 1,
        [Display(Name = "Bonifico Bancario (pagato)")]
        BonificoBancariopagato = 2,
        [Display(Name = "Bonifico Bancario a 30 gg")]
        BonificoBancarioa30gg = 3,
        [Display(Name = "Bonifico Bancario a 60 gg df")]
        BonificoBancarioa60ggdf = 4,
        [Display(Name = "Bonifico Bancario a 60 gg df fm")]
        BonificoBancarioa60ggdffm = 5,
        [Display(Name = "Bonifico Bancario a 90 gg")]
        BonificoBancarioa90gg = 6,
        [Display(Name = "Effettuato")]
        Effettuato = 7,
        [Display(Name = "RIBA30-60.90 gg df")]
        RIBA306090ggdf = 8,
        [Display(Name = "RIBA120 gg df")]
        RIBA120ggdf = 9,
        [Display(Name = "RIBA30-60-90-120-150")]
        RIBA306090120150 = 10,
        [Display(Name = "RIBA150 gg fm")]
        RIBA150ggfm = 11,
        [Display(Name = "RIBA180 dd df")]
        RIBA180dddf = 12,
        [Display(Name = "RIBA120-150-180 gg fm")]
        RIBA120150180ggfm = 13,
        [Display(Name = "RIBAa 30 gg df")]
        RIBAa30ggdf = 14,
        [Display(Name = "RIBAa 30 gg df fm")]
        RIBAa30ggdffm = 15,
        [Display(Name = "RIBA30-60-90-120-150 gg df fm")]
        RIBA306090120150ggdffm = 16,
        [Display(Name = "RIBA30-60-90-120-150-180 fm")]
        RIBA306090120150180fm = 17,
        [Display(Name = "RIBA30-60 gg fm")]
        RIBA3060ggfm = 18,
        [Display(Name = "RIBA30-60-90 gg df fm")]
        RIBA306090ggdffm = 19,
        [Display(Name = "RIBA30-60-90-120 gg df")]
        RIBA306090120ggdf = 20,
        [Display(Name = "RIBA30-60-90-120 gg df fm")]
        RIBA306090120ggdffm = 21,
        [Display(Name = "RIBAa 60 gg df")]
        RIBAa60ggdf = 22,
        [Display(Name = "RIBAa 60 gg df fm")]
        RIBAa60ggdffm = 23,
        [Display(Name = "RIBA60-90-120-150-180 fm")]
        RIBA6090120150180fm = 24,
        [Display(Name = "RIBA60-90 gg fm")]
        RIBA6090ggfm = 25,
        [Display(Name = "RIBA60-90-120 gg df fm")]
        RIBA6090120ggdffm = 26,
        [Display(Name = "RIBA60-90 gg")]
        RIBA6090gg = 27,
        [Display(Name = "RIBAa 90 gg df fm")]
        RIBAa90ggdffm = 28,
        [Display(Name = "RIBA90-120 gg fm")]
        RIBA90120ggfm = 29,
        [Display(Name = "RIBA90-120-150 df fm")]
        RIBA90120150dffm = 30,
        [Display(Name = "Rimessa Diretta a vista")]
        RimessaDirettaavista = 31,
        [Display(Name = "Rimessa Diretta 30 gg df")]
        RimessaDiretta30ggdf = 32,
        [Display(Name = "Rimessa Diretta 30-60 gg fm")]
        RimessaDiretta3060ggfm = 33,
        [Display(Name = "Rimessa Diretta 60 gg")]
        RimessaDiretta60gg = 34,
        [Display(Name = "Tratta a 120 gg fm")]
        Trattaa120ggfm = 35,
        [Display(Name = "RI.BA a 90 gg df")]
        RIBAa90ggdf = 36,
        [Display(Name = "Pagamento a Scadenza")]
        PagamentoaScadenza = 37,
        [Display(Name = "Bonifico Bancario")]
        BonificoBancario = 38,
        [Display(Name = "Non Definito")]
        NonDefinito = 0,
    }

    public class ANA_Company
    {
        private DateTime? _createdAt; //AGGIUNTA
        private DateTime? _updatedAt;
        private DateTime? _deletedAt;
        private string _createdBy;
        private string _updatedBy;
        private string _deletedBy;

        public ANA_Company()
        {
            ID = Guid.NewGuid();
            active = true;
            isOfficina = false;
            isSupplier = false;
            isCustomer = false;
            isExternal = false;
            isCopiedOnLooc = false;
            /*
            MultiTenant = await auth
            using (var context = new MultiTenant())
            {
                var settings = context.Find("johndoe1987");
            }
            */
        }

        public DateTime? CreatedAt => _createdAt; //AGGIUNTA
        public DateTime? UpdatedAt => _updatedAt;
        public DateTime? DeletedAt => _deletedAt;
        public string? CreatedBy => _createdBy;
        public string? UpdatedBy => _updatedBy;
        public string? DeletedBy => _deletedBy;

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
                
        [Display(Name = "Codice")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")] 
        public string InternalCode { get; set; }
        

        [Required(ErrorMessage = "Campo richiesto",AllowEmptyStrings = false)]
        [Display(Name = "Ragione Sociale*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")] 
        public string RagioneSociale { get; set; }

        [Display(Name = "Partita Iva*")]
        [Required(ErrorMessage = "Campo richiesto")]
        [MaxLength(20, ErrorMessage = "Il campo non può superare 20 caratteri")]
        [Remote(action: "VerifyPiva", controller: "Companies")]
        public string PIva { get; set; }

        [Display(Name = "Codice Fiscale")]
        [MaxLength(20, ErrorMessage = "Il campo non può superare 20 caratteri")]
        public string? FiscalCode { get; set; }

        [Display(Name = "Email Pec")]
        [MaxLength(50, ErrorMessage = "Il campo non può superare 50 caratteri")]
        public string? EmailPec { get; set; }

        [Display(Name = "Codice SDI")]
        [MaxLength(20, ErrorMessage = "Il campo non può superare 20 caratteri")]
        public string? CodiceSdi { get; set; }

        [Display(Name = "Nazione")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Nazione { get; set; }

        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        [Display(Name = "Regione")]
        public string? Regione { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Provincia { get; set; }

        [Display(Name = "Città")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Citta { get; set; }

        [Display(Name = "Indirizzo")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Indirizzo { get; set; }


        [Display(Name = "Email Aziendale")]
        [MaxLength(50, ErrorMessage = "Il campo non può superare 50 caratteri")]
        public string? EmailAziendale { get; set; }

        [Display(Name = "Sito Web")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? SitoWeb { get; set; }

        [Display(Name = "Telefono")]
        [MaxLength(50, ErrorMessage = "Il campo non può superare 50 caratteri")]
        public string? Telefono { get; set; }

        [Display(Name = "Fax")]
        [MaxLength(50, ErrorMessage = "Il campo non può superare 50 caratteri")]
        public string? Fax { get; set; }

        [Display(Name = "Azienda Fornitore")]
        public bool? isSupplier { get; set; }

        [Display(Name = "Azienda Cliente")]
        public bool? isCustomer { get; set; }

        [Display(Name = "Azienda Officina")]
        public bool? isOfficina { get; set; }

        [Display(Name = "Azienda Esterna")]
        public bool? isExternal { get; set; }

        [Display(Name = "Azienda Padre")]
        [ForeignKey("Company")]
        public Guid? ParentID { get; set; }
        public virtual ANA_Company Parent { get; set; }

        [Display(Name = "Logo Azienda")]
        public byte[] Img { get; set; }        

        [Display(Name = "Attivazione azienda")]
        public bool active { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        [Display(Name = "Pagamento")]
        public Pagamento? Pagamento { get; set; }

        [Display(Name = "Banca")]
        public string? Banca { get; set; }

        [Display(Name = "IBAN")]
        public string? IBAN { get; set; }

        [Display(Name = "Azienda importata in Looc")]
        public bool isCopiedOnLooc { get; set; }
                
        [Display(Name = "Codice Datev")]
        public string DatevCode { get; set; }

        [Display(Name = "Tipo Pagamento")]
        public TipoPagamento? TipoPagamentoID { get; set; }        

        [Display(Name = "Modalità Pagamento")]
        public ModPagamento? ModalitaPagamentoID { get; set; }        

        public string? PagheCodAzienda { get; set; }
        public string? PagheCodFiliale { get; set; }

        [Display(Name = "CAP")]
        public string CAP { get; set; }


    }

}
