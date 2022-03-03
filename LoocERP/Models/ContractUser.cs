using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
   
    public class ContractUser
    {
        public ContractUser()
        {
            Id = Guid.NewGuid();
            MesiRinnovo = 0;
        }        

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
  
        [Display(Name = "Numero Protocollo")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string? Code { get; set; }
  
        [Display(Name = "Livello")]
        [ForeignKey("Domain")]
        public Guid? LevelId { get; set; }
        public virtual Domain Level { get; set; }

        [Display(Name = "Tipo di retribuzione")]
        [ForeignKey("Domain")]
        public Guid? RetribuzioneTypeId { get; set; }
        public virtual Domain RetribuzioneType { get; set; }

        [Display(Name = "Tipologia ore di lavoro")]
        [ForeignKey("Domain")]
        public Guid? OreDiLavoroTypeId { get; set; }
        public virtual Domain OreDiLavoroType { get; set; }

        [Display(Name = "Tipo di contratto")]
        [ForeignKey("Domain")]
        public Guid? ContractTypeId { get; set; }
        public virtual Domain ContractType { get; set; }

        [Display(Name = "Codice per assunzioni speciali")]
        [ForeignKey("Domain")]
        public Guid? LawNumberTypeId { get; set; }
        public virtual Domain LawNumberType { get; set; }

        [Display(Name = "Qualifica lavoratore")]
        [ForeignKey("Domain")]
        public Guid? CategoryCodeId { get; set; }
        public virtual Domain CategoryCode { get; set; }
        
        [Display(Name = "Motivo cessazione")]
        [ForeignKey("Domain")]
        public Guid? FineRapportoId { get; set; }
        public virtual Domain FineRapporto { get; set; }     

        [Display(Name = "Data Inizio Validità")]        
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]        
        public DateTime? ValidFrom { get; set; }
        
        [Display(Name = "Scadenza/Data Fine Validità")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? ValidTo { get; set; }
        
        //0 = Chiuso, 1 = Attivo,  
        [Display(Name = "Stato")]
        public int? Stato { get; set; }

        [Display(Name = "Note fine contratto")]
        [MaxLength(2000, ErrorMessage = "Lunghezza massima 2000 caratteri")]
        public string? FineContrattoNota { get; set; }
  
        [ForeignKey("AppUser")]
        public String? UserId { get; set; }
        public virtual AppUser User { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        //Nome file per DB
        [Display(Name = "Nome file carcato")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string? FileName { get; set; }

        [Display(Name = "Indennita Mese")]
        [Range(0, float.MaxValue, ErrorMessage = "Inserire un numero corretto")]
        [DisplayFormat(DataFormatString = "{0:n2} €", ApplyFormatInEditMode = true, NullDisplayText = "0.00 €")]

        public double? IndennitaMese {get; set;}

        [Range(0, Int32.MaxValue)]
        [Display(Name = "Mesi di rinnovo")]
        public int? MesiRinnovo { get; set; }



    }
}
