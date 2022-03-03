using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{

	[Table("GOF_scheda")]
	public class GOF_scheda
    {
		public GOF_scheda()
		{
		}


		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Key]
		public long Codice { get; set; }
		public string CodCliente { get; set; }
		public string codSettore { get; set; }
		public string codSottosettore { get; set; }
		public DateTime DataIngresso { get; set; }
		public DateTime DataUscita { get; set; }
		public long clienteScheda { get; set; }


		[ForeignKey("Vettore")]
		public int codVettore { get; set; }
		public virtual Vettore Vettore { get; set; }


		public long kmIngresso { get; set; }
		public long oreIngresso { get; set; }
		public string difettoDichiarato { get; set; }
		public int stato { get; set; }
		public double costo_trasferta { get; set; }
		public int sconto { get; set; }
		public double abbuono { get; set; }
		public double acconto { get; set; }
		public double saldo { get; set; }
		public string aliquota { get; set; }
		public string note { get; set; }
		public string nscheda { get; set; }
		public string richiedente { get; set; }
		public string orafermo { get; set; }
		public string oraservizio { get; set; }
		public byte fuoriservizio { get; set; }
		public byte inservizio { get; set; }
		public string motivazioneModifica { get; set; }
		public long codOrdine { get; set; }
		public int codRichiedente { get; set; }
		public string FLTipoScheda { get; set; }
		public bool FL_FB { get; set; }
	}
}
