using LoocERP.Data;
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

namespace LoocERP.Models
{
    public enum TipoEvento
    {
        Inizio, //0
        Continuazione, //1
        Ricaduta, //2
        EventoSingolo, //3
    }

    public enum TipologiaMalattia    {
        Malattia, //0
        Infortunio, //1
        VisitaMedica, //2
    }

    public class MalattiaUser
    {
        public MalattiaUser()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Tipologia Malattia")]
        public TipologiaMalattia Tipologia { get; set; }

        //Area Certificato
        [Display(Name = "Tipo Evento")]
        public TipoEvento TipoEvento{ get; set; }

        [Display(Name = "Data Inizio")]        
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]        
        public DateTime? ValidFrom { get; set; }
        
        [Display(Name = "Data Fine")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? ValidTo { get; set; }
        
        //Area Certificato
        [Display(Name = "Certificato")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string? Certificato{ get; set; }
          
        [Display(Name = "Certificato di riferimento")]
        [MaxLength(200, ErrorMessage = "Lunghezza massima 200 caratteri")]
        public string? CertificatoDiRiferimento { get; set; }
        
        [Display(Name = "Data Rilascio Certificato")]        
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]        
        public DateTime? DataRilascioCertificato { get; set; }
        
        [Display(Name = "Data Consegna Certificato")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? DataConsegnaCertificato { get; set; }
        
        //Parto
        [Display(Name = "Data parto Presunta")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? DataPartoPresunta { get; set; }

        //Parto
        [Display(Name = "Data parto effettiva")]  
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime? DataPartoEffettiva { get; set; }
  
        //Figlio
        [Display(Name = "Nome Figlio (se dovuta ad esso)")]    
        public string? NomeFiglio { get; set; }

        //Nome Medico
        [Display(Name = "Nome Medico")]    
        public string? Medico { get; set; }

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

        [Display(Name = "Visita Medica Id")]
        [ForeignKey("VisitaMedicaId")]
        public Guid? VisitaMedicaId { get; set; }
        public virtual VisitaMedica VisitaMedica { get; set; }


    }
}
