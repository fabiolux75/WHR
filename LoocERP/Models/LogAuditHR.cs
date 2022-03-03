using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class LogAuditHR
    {

        public enum LogAuditHREventType
        {
            NotDefined, //0
            Error, //1
            Login, //2
            Logout, //3
            GestioneMovimento,
            GestioneAnagrafica,
            GestioneCommesse,
            VisualizzaVettori,
            VisualizzaCantiere,
            VisualizzaPersonale,
            VisualizzaParcheggio,
            VisualizzaNoleggi,
            VisualizzaLeasing,
            VisualizzaCorsi,
            VisualizzaVisiteMediche,
            VisualizzaRichiesteAssenza,
            VisualizzaProgetti,
            VisualizzaPianificazioneRisose,
            VisualizzaTimeSheet,
            VisualizzaTimeSheetDaily,
            VisualizzaTimeSheetMontly,
            VisualizzaSpecializzazione,
            CreazioneSpecializzazione,
            EditSpecializzazione,   

        }

        public LogAuditHR()
        {
        }
        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [ForeignKey("AppUser")]        
        public String? UserId { get; set; }        
        public virtual AppUser User { get; set; }    
    
        [Display(Name = "Data evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EventDate { get; set; }


        [Display(Name = "Tipologia evento")]
        public LogAuditHREventType? EventType { get; set; }



        [Display(Name = "Dettagli")]
        public string? Details { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }
        
    }
}