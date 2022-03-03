using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models
{
    public partial class MedModelloVettoriInfo
    {
        public MedModelloVettoriInfo()
        {

        }



        [Key]
        public long Codice { get; set; }
        public long CodModVettore { get; set; }
        public string Descr { get; set; }
        public byte? Cancellato { get; set; }
        public DateTime? DataCreazione { get; set; }
        public DateTime? DataModifica { get; set; }
        public string OperatoreCreazione { get; set; }
        public string OperatoreModifica { get; set; }
        public string Img { get; set; }

        [ForeignKey("CodModVettore")]
        public virtual ModelloVettore ModelloVettore { get; set; }

    }
}
