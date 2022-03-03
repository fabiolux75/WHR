using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoocERP.Models.Sdi
{

    public class TipiSdiDoc
    {

        public int Id { get; set; }

        public string Codice { get; set; }

        public string Descrizione { get; set; }


    }
}
