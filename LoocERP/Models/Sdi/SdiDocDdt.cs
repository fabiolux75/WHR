using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models.Sdi
{
    public class SdiDocDdt
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [ForeignKey("ddt")]
        public Guid DdtId { get; set; }
        public Ddt ddt { get; set; }
        


        [ForeignKey("sdiDoc")]
        public Guid SdiDocId { get; set; }
        public SdiDoc sdiDoc { get; set; }
    }
}