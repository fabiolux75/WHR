using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class TipoPagamentoDatev
    {
 
        [Key]

        public int id { get; set; }

        public string codiceTipoPagamento { get; set; }
        public string descrizione { get; set; }

    }
}
