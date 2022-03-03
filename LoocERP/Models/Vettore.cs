using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class Vettore
    {
        public Vettore()
        {
        }

        [Key]
        [Display(Name = "Codice")]
        public int Codice { get; set; }

        [Display(Name = "Codice Modello")]
        public long CodModello { get; set; }
        //[ForeignKey("CodModello")]
        //public virtual VettoreModello CodModelloLink { get; set; }

        [Display(Name = "Targa")]
        public String Targa { get; set; }

        [MaxLength(6)]
        [Display(Name = "Codice Cliente")]  //EMADEMA - FOREIGN KEY
        public string CodCliente { get; set; }





        [MaxLength(100)]
        [Display(Name = "Descrizione")]
        public string Descr { get; set; }

        [MaxLength(150)]
        [Display(Name = "Numero di Serie")]
        public string N_Serie { get; set; }

        [MaxLength(150)]
        [Display(Name = "Numero aziendale")]
        public string N_Aziendale { get; set; }

        [MaxLength(4)]
        [Display(Name = "Anno Matricola")]
        public string Matricola_ANNO { get; set; }

        [MaxLength(5)]
        [Display(Name = "GGMM Matricola")]
        public string Matricola_GGMM { get; set; }

        [MaxLength(6)]
        [Display(Name = "Codice Settore")]  //EMADEMA - FOREIGN KEY
        public string CodSettore { get; set; }
        [ForeignKey("CodSettore")]
        public virtual VettoreSettore VettoreSettore { get; set; }

        [MaxLength(6)]
        [Display(Name = "Codice Sotto Settore")]  //EMADEMA - FOREIGN KEY
        public string CodSottoSett { get; set; }

        [ForeignKey("CodSottoSett")]
        public virtual VettoreSottoSettore CodiceSottoSettore { get; set; }

        public virtual IEnumerable<VettoreUser> VettoreUsers { get; set; }

        public virtual IEnumerable<VettoreCantiere> VettoreCantieri { get; set; }
        public virtual IEnumerable<VettoreParcheggio> VettoreParcheggi { get; set; }

        [ForeignKey("CodModello")]
        public virtual ModelloVettore ModelloVettore { get; set; }


        public virtual IEnumerable<GOF_scheda>  GOF_Scheda { get; set; }

        //[ForeignKey("CodCliente")]
        //public virtual MultiTenant MultiTenant { get; set; }
        public MedAnaVettoriInfo MedAnaVettoriInfo { get; set; }

        public virtual Noleggio Noleggio { get; set; }

        public virtual IEnumerable<VettoreAssegnazione> VettoreAssegnazione { get; set; }







        [Display(Name = "Company")]
        [ForeignKey("company")]
        public Guid? IdCompany { get; set; }
        public virtual ANA_Company company { get; set; }


        public int? Nposti { get; set; }

        /*

            [Display(Name = "Codice Utilizzo")]
            public int CodUtilizzo { get; set; }

            [Display(Name = "Codice PM_CodBUM")]  
            public int  PM_CodBUM { get; set; }

            [Display(Name = "PM_Valore")]
            public float PM_Valore { get; set; }

            [Display(Name = "PM_Ore")]
            public float PM_Ore { get; set; }

            [Display(Name = "Multitenant")]
            [ForeignKey("MultiTenant")]
            public Guid? MultiTenantId { get; set; }
            public virtual MultiTenant MultiTenant { get; set; }

        */
    }
}
