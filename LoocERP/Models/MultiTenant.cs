using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models
{
    public class MultiTenant
    {        
        public MultiTenant()
        {         
        }
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? LogoUrl { get; set; }

        [MaxLength(50)]
        public string? LoocId { get; set; }

        [MaxLength(200)]
        public string? slughost { get; set; }
    }
}