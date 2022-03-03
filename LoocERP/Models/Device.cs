using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace LoocERP.Models
{
    public class Device
    {
        public int Id { get; set; }
        public int idDevice { get; set; }
        public string Description { get; set; }
        public string CodiceVettore { get; set; }
        public string CodiceCliente { get; set; }
        public string CodiceAutista { get; set; }
        public char StatoDevice { get; set; }
        public string DescStatoDevice { get; set; }

    }
}
