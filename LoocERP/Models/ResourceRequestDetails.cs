using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class ResourceRequestDetails
    {

		public Guid? Id { get; set; }

		[ForeignKey("Mansione")]
		public Guid? MansioneId { get; set; }
		public virtual Mansione mansione { get; set; }


		[ForeignKey("Macchina")]
		public Guid? MacchinaId { get; set; }
		public virtual MansioneMacchina macchina { get; set; }

		[ForeignKey("Macchina")]
		public Guid? SpecializzazioneId { get; set; }
		public virtual Specializzazione Specializzazione { get; set; }

		[ForeignKey("Cantiere")]
		public Guid? CantiereId { get; set; }
		public virtual Cantiere Cantiere { get; set; }


		[ForeignKey("ResourceRequest")]
		public Guid? ResourceRequestId { get; set; }
		public virtual ResourceRequest ResourceRequest { get; set; }


		[Display(Name = "Quantità")]
		public int qty { get; set; }

		public DateTime? WorkDate { get; set; }
	}
}
