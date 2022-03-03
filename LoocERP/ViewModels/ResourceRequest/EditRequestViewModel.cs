using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels.ResourceRequest
{
    public class EditRequestViewModel
    {
		public Guid? Id { get; set; }
		public string Descr { get; set; }
		public StatoRequest? Stato { get; set; }
		public Turno Turno { get; set; }

		public DateTime? WorkDate { get; set; }

		public List<ResourceRequestDetails> dettagli { get; set; }


	}
}
