using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
	public enum StatoRequest
	{
		bozza,
		[Display(Name = "Inviata")]
		inviata,
		[Display(Name = "Presa In Carico")]
		inCarico,
		[Display(Name = "Assegnazione Completata")]
		completata,

	}



	public class ResourceRequest
	{
		public Guid? Id { get; set; }
		public string Descr { get; set; }

		public StatoRequest? Stato { get; set; }

		[ForeignKey("Turno")]
		public Guid? TurnoId { get; set; }

		public Turno Turno { get; set; }

		public DateTime? WorkDate { get; set; }

		[ForeignKey("MultiTenant")]
		public Guid? MultiTenantId { get; set; }
		public virtual MultiTenant MultiTenant { get; set; }


		public List<ResourceRequestDetails> dettagli { get; set; }
		
		public ResourceRequest(){ 
			
		}
	}
}
