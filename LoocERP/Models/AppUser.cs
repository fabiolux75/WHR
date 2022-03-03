using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public enum Gender
    {
        Uomo,
        Donna
    }

    public class AppUser : IdentityUser
    {        
        public AppUser()
        {
            this.DataModifica = System.DateTime.Now;
            if (this.DataCreazione == null) this.DataCreazione = System.DateTime.Now;   
            this.active = 1;
            this.isEmployee = 0;
            this.Gender = 0; //0 men - 1 women  
        }

        [Display(Name = "Codice")]
        [MaxLength(50, ErrorMessage = "Lunghezza massima 50 caratteri")]
        public string? InternalCode { get; set; }

        [Required(ErrorMessage = "E' necessario indicare un Nome")]
        [Display(Name = "Nome*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "E' necessario indicare un Cognome")]
        [Display(Name = "Cognome*")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "E' necessario indicare un numero di telefono")]
        [Display(Name = "Numero Telefono principale*")]
        [MaxLength(50, ErrorMessage = "Lunghezza massima 50 caratteri")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Numero Telefono Cellulare")]
        [MaxLength(50, ErrorMessage = "Lunghezza massima 50 caratteri")]
        public string? CellularNumber { get; set; }

        [Display(Name = "Numero Telefono Privato")]
        [MaxLength(50, ErrorMessage = "Lunghezza massima 50 caratteri")]
        public string? PrivateNumber { get; set; }

        [Display(Name = "E' un risorsa")]
        public int? isEmployee { get; set; }

        [Display(Name = "E' una risorsa esterna?")]
        public int? isExternal { get; set; }


        [Display(Name = "Picture")]        
        public byte[] ProfilePicture { get; set; }

        [Display(Name = "Azienda")]
        [ForeignKey("ANA_Company")]
        [Required(ErrorMessage = "E' necessario indicare l'azienda di appartenza")]
        public Guid? IDCompany { get; set; }
        //[NotMapped]
        [ForeignKey("IDCompany")]
        public virtual ANA_Company Company { get; set; }


        [Display(Name = "Attivazione azienda")]
        public int? active { get; set; }

        [Display(Name = "Sesso")]
        public Gender? Gender { get; set; }

        [Display(Name = "Codice Fiscale")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? CodiceFiscale { get; set; }

        [Display(Name = "Codice NFC")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? NfcCode { get; set; }

        [Display(Name = "Codice Carta Carburante")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? CodiceCartaCarb { get; set; }

        [Display(Name = "Nazione Residenza")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Nazione { get; set; }

        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        [Display(Name = "Regione Residenza")]
        public string? Regione { get; set; }

        [Display(Name = "Provincia Residenza")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Provincia { get; set; }

        [Display(Name = "Città Residenza")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Citta { get; set; }

        [Display(Name = "Cap Residenza")]
        [MaxLength(50, ErrorMessage = "Il campo non può superare 200 caratteri")]        
        public string? Cap { get; set; }

        [Display(Name = "Indirizzo Residenza")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Indirizzo { get; set; }

        [Display(Name = "Tipo Patente")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? TipoPatente { get; set; }

        [Display(Name = "N° Patente")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? NumeroPatente { get; set; }

        [Display(Name = "DataScadenza Patente")]
        [MaxLength(200, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public DateTime? DataScadenzaPatente { get; set; }

        /* Creazione */
        public DateTime? DataCreazione { get; set; }

        [ForeignKey("AppUser")]
        public string IDCreatoDa { get; set; }
        [NotMapped]
        public virtual AppUser CreatoDa { get; set; }
        
        //public virtual ICollection<AppUser> CreatoDa { get; set; }

        /* Modifica */
        public DateTime? DataModifica { get; set; }

        [ForeignKey("AppUser")]
        public string IDModificatoDa { get; set; }
        public virtual AppUser ModificatoDa { get; set; }
        //[NotMapped]
        //public virtual ICollection<AppUser> ModificatoDa { get; set; }

        /* Eliminazione */
        public DateTime? DataEliminazione { get; set; }

        [ForeignKey("AppUser")]
        public string IDEliminatoDa { get; set; }
        [NotMapped]
        public virtual AppUser EliminatoDa { get; set; }

        [NotMapped]
        public virtual List<Rel_MansioneUser> MansioniUsers { get; set; }
        
        //public string CodiceDipendente { get; internal set; }
         [Display(Name = "IBAN")]
        public string IBAN { get; set; }
        //public string ScadenzaDocumento { get; internal set; }

        [Display(Name = "Matricola amministrativa")]
        public string matricola { get; set; }
                
        [Display(Name = "Note")]
        [MaxLength(2000, ErrorMessage = "Il campo non può superare 200 caratteri")]
        public string? Note { get; set; }

        [Display(Name = "Stima Oraria")]
        public decimal? StimaOraria { get; set; }
        [Display(Name = "Stima Oraria Straordinaria")]
        public decimal? StimaOrariaStraordinaria { get; set; }
        [Display(Name = "Stima Oraria Galleria")]
        public decimal? StimaOrariaGalleria { get; set; }
        [Display(Name = "Stima Oraria Notturna")]
        public decimal? StimaOrariaNotturna { get; set; }
        
        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }
                
        //TORNA LA LISTA DELLE MANSIONI COLLEGATE ALL'UTENTE
        public virtual ICollection<Rel_MansioneUser> Rel_MansioneUser {get; set; }
        public virtual ICollection<Rel_SpecializzazioneUser> Rel_SpecializzazioneUser {get; set; }

        [Display(Name = "Azienda Referente")]
        public Guid? InternalCompanyReferenceId { get; set; }
        public virtual ANA_Company InternalCompanyReference { get; set; }

        [Display(Name = "Comune di nascita")]
        [MaxLength(100, ErrorMessage = "Lunghezza massima 50 caratteri")]
        public string? ComuneNascita { get; set; }

        [Display(Name = "Data di nascita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataNascita { get; set; }
        public String? CodiceEsecutore { get; set; }
        public int? isParking { get; set; }
        public virtual IEnumerable<VettoreUser> vettori { get; set; }
        public virtual IEnumerable<TimeSheetDailyReport> TimeSheetDailyReports { get; set; }
        public virtual List<ContractUser> ContractUsers { get; set; }
        public virtual List<Rel_TurnoUser> TurniUser { get; set; }
        public string LoocUsername { get; set; }
        public string LoocPassword { get; set; }

        [Display(Name = "Abilitato alla gestione")]
        public bool isEnabledSupplierOrderConfirm { get; set; }

        [Display(Name = "Abilitato ai bonifici")]
        public bool isEnabledSupplierBonifico { get; set; }
        
        [Display(Name = "Limite massimo di costo gestito")] 
        public int? maxCostSupplierOrderConfirm { get; set; }

        public virtual List<MalattiaUser> Malattie { get; set; }



    }
}
