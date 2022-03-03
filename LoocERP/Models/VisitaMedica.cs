using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class VisitaMedica
    {
        public VisitaMedica()
        {
            ID = Guid.NewGuid();
            Stato = 1;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid ID { get; set; }

        [Display(Name = "Tipologia")]
        public int Tipologia { get; set; }

        [Display(Name = "Tipo Evento")]
        public int TipoEvento { get; set; }

        [MaxLength(4000)]
        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        [Display(Name = "Data Inizio Visita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Data Fine Visita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        [MaxLength(200)]
        [Display(Name = "Medico")]
        public string Medico { get; set; }

        [Display(Name = "Stato della visita")]
        public int Stato { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }


        [ForeignKey("ANA_Company")] //ALTER TABLE CDI_TEST.dbo.C_VisiteMediche ADD CompanyId uniqueidentifier NULL;
        [Display(Name = "Richiedente")]
        public Guid? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual ANA_Company  Richiedente { get; set; }


    }
}
