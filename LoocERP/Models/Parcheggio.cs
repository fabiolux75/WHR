using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
	public class Parcheggio
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Coordinates { get; set; }
		public virtual IEnumerable<VettoreParcheggio> VettoriParcheggio { get; set; }


		public Parcheggio()
		{
			
		}
	}
}
