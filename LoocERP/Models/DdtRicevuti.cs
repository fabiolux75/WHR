using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoocERP.Models
{
    public class DdtRicevuti
    {

        [Key]
        public Guid Id { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime DataDDT { get; set; }


        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }


        [ForeignKey("Mittente")]
        public Guid IdMittente { get; set; }

        public virtual  ANA_Company Mittente { get; set; }
        public int? NumeroProgressivoDocumento { get; set; }
        public ICollection<DdtDettaglioRicevuti> Dettagli  { get; set; }

        public string CantiereCodice  { get; set; }
        public string CantiereName  { get; set; }

        public int? ModalitaPagamentoID { get; set; }
        public virtual ModalitaPagamento ModalitaPagamento { get; set; }


        public int? TipoPagamentoID { get; set; }

        public virtual TipoPagamentoDatev TipoPagamento { get; set; }

        public string Suffisso { get; set; }

        public DateTime? DataExpDatev { get; set; }

        public string ProjectCodice  { get; set; }
        public string ProjectName  { get; set; }        
       

    }
}
