using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class TimeSheetDailyReport
    {

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        [Display(Name = "Giorno Lavorativo")]                
        public DateTime? WorkDate { get; set; }

        [Display(Name = "Operatore")]
        [ForeignKey("AppUser")]
        public String UserId { get; set; }        
        public virtual AppUser User { get; set; }

        [Display(Name = "Turno")]
        [ForeignKey("Turno")]
        public Guid? TurnoId { get; set; }
        public virtual Turno Turno { get; set; }

        [Display(Name = "Ore Effettive")]
        public decimal? OreEffettive { get; set; }

        [Display(Name = "Ore")]
        public decimal? Ore { get; set; }

        [Display(Name = "Ore Straordinarie")]
        public decimal? OreStraordinarie { get; set; }

        [Display(Name = "Ore Notturne")]
        public decimal? OreNotturne { get; set; }

        [Display(Name = "Cigo")]
        public string? Cigo { get; set; }

        [Display(Name = "EC")]
        public string? Ec { get; set; }

        [Display(Name = "Stato Turno")]
        public int? StatoTurno { get; set; } //0 Chiuso - 1 Aperto

        [Display(Name = "Ore Galleria")]
        public decimal? OreGalleria { get; set; }

        [Display(Name = "Stato Utente")]
        public int? StatoUtente { get; set; } //0 Chiuso - 1 Aperto

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }

        //[Display(Name = "Ore Trasferta")]
        // public decimal? OreTrasferta { get; set; }

        [Display(Name = "Giustificativo")]
        [ForeignKey("Giustificativo")]
        public Guid? GiustificativoId { get; set; }
        public virtual Giustificativo Giustificativo { get; set; }
        public Guid? TrasfertaId { get; set; }
        public virtual Giustificativo Trasferta { get; set; }

        [Display(Name = "Indennita?")]
        public decimal? Indennita { get; set; }

        [Display(Name = "EffectiveHour")]
        public TimeSpan? EffectiveHour { get; set; }

        [Display(Name = "Hour")]
        public TimeSpan? Hour { get; set; }

        [Display(Name = "OvertimeHour")]
        public TimeSpan? OvertimeHour { get; set; }

        [Display(Name = "NightHour")]
        public TimeSpan? NightHour { get; set; }

        [Display(Name = "GalleryHour")]
        public TimeSpan? GalleryHour { get; set; }

        [Display(Name = "TravelHour")]
        public TimeSpan? TravelHour { get; set; }


        //[Display(Name = "Tipo")]
        //public int type { get; set; }

        [Display(Name = "Auto Chiusura")]
        public int? isAutoClosed { get; set; }

        public bool isApproved { get; set; }   

        public TimeSpan? OreGiustificativo { get; set; }     

    }
}
