using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{

    public enum Typology
    {
        [Display(Name = "Assegnazione Cantiere")]
        assignationCantiere,
        [Display(Name = "Generica")]
        generica,
    }

    public class UserNotification
    {

        public UserNotification()
        {
            DataCreazione = DateTime.Now;
            DataModifica = DateTime.Now;
            SeenDate = null;
            SoundedDate = null;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }

        public String MessageText { get; set; }
        public Typology Tipologia { get; set; }



        [ForeignKey("AppUser")]
        public String? UserId { get; set; }
        public virtual AppUser User { get; set; }



        public int Seen { get; set; }
        public int Sounded { get; set; }

        [Display(Name = "Multitenant")]
        [ForeignKey("MultiTenant")]
        public Guid? MultiTenantId { get; set; }
        public virtual MultiTenant MultiTenant { get; set; }



        public DateTime? SeenDate { get; set; }
        public DateTime? SoundedDate { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataModifica { get; set; }
    }
}
