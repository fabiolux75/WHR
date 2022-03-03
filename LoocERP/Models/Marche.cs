using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoocERP.Models
{
    public partial class Marche
    {
        public Marche()
        { }

        [Key]
        [Display(Name = "Codice")]
        public int Codice { get; set; }
        public string Descr { get; set; }
        public bool? FlgVettore { get; set; }
        public bool? FlgSistema { get; set; }
        public bool? FlgSottoSist { get; set; }
        public bool? FlgComponente { get; set; }
        public DateTime? DataModifica { get; set; }
        public DateTime? DataCreazione { get; set; }
        public string OperatoreCreazione { get; set; }
        public string OperatoreModifica { get; set; }
        public string Note { get; set; }


        /*
        public virtual ICollection<GofMagazzino> GofMagazzino { get; set; }
        public virtual ICollection<GofMagazzinoBackup> GofMagazzinoBackup { get; set; }
        public virtual ICollection<GofMagazzinoEsistentiSocbriDaModificare> GofMagazzinoEsistentiSocbriDaModificare { get; set; }
        public virtual ICollection<ModelloComponenti> ModelloComponenti { get; set; }
        public virtual ICollection<ModelloSistemi> ModelloSistemi { get; set; }
        public virtual ICollection<ModelloSottosistemi> ModelloSottosistemi { get; set; }
        public virtual ICollection<ModelloVettori> ModelloVettori { get; set; }
        */
    }
}
