using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class ModelloVettoreInfo
    {
        public ModelloVettoreInfo()
        {

        }

        [Key]
        [Display(Name = "Codice")]
		public Int64 Codice { get; set; }


		[ForeignKey("CodModVettore")]
		public virtual ModelloVettore ModelloVettore { get; set; }

		public Int64 CodModVettore { get; set; }
		public string Descr { get; set; }
		public byte Cancellato { get; set; }
		public DateTime DataCreazione { get; set; }
		public DateTime DataModifica { get; set; }
		public string OperatoreCreazione { get; set; }
		public string OperatoreModifica { get; set; }
		public string Img { get; set; }

	}


    
}
