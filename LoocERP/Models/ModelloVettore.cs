using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class ModelloVettore
    {
        public ModelloVettore()
        {

        }



        [Key]
        [Display(Name = "Codice")]
        public Int64 Codice { get; set; }

        public int CodFamiglia { get; set; }
        public int CodMarca { get; set; }
        public string Descr { get; set; }
        public string DescrEn { get; set; }
        public DateTime DataModifica{ get; set; }
        public DateTime DataCreazione { get; set; }
        public string OperatoreCreazione { get; set; }
        public string OperatoreModifica { get; set; }
        public string Note { get; set; }
        public ModelloVettoreInfo modelloInfo { get; set; }
        [ForeignKey("CodMarca")]
        public Marche Marca { get; set; }



    }


    
}
