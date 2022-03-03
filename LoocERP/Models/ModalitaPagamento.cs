using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class ModalitaPagamento
    {
 
        [Key]

        public int id { get; set; }

        public string codiceModalitaPagamento { get; set; }
        public string descrizione { get; set; }

    }
}
