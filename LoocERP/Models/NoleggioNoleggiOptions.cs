using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class NoleggioNoleggiOptions
    {

        
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }


        [ForeignKey("Noleggio")]
        public Guid? NoleggioId { get; set; }
        public virtual Noleggio Noleggio { get; set; }

        [ForeignKey("NoleggiOption")]
        public Guid? NoleggioOptionId { get; set; }
        public virtual NoleggiOption NoleggiOption { get; set; }



    }
}
