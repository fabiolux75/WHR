using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoocERP.Models
{
    public class TipiDocumentoFE
    {



        [Key]
        public int Id { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }

    }
}
