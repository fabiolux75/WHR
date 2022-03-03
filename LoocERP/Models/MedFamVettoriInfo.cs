using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoocERP.Models
{
    public partial class MedFamVettoriInfo
    {
        public MedFamVettoriInfo()
        {

        }



        [Key]
        public int Codice { get; set; }
        public int CodFamVettore { get; set; }
        public string Descr { get; set; }
        public byte? Cancellato { get; set; }
        public DateTime? DataCreazione { get; set; }
        public DateTime? DataModifica { get; set; }
        public string OperatoreCreazione { get; set; }
        public string OperatoreModifica { get; set; }
        public string Img { get; set; }
    }
}
