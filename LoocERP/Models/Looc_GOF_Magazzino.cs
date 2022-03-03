using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace LoocERP.Models
{
    
    public class Looc_GOF_Magazzino
    {

        public string CodCliente { get; set; }


        public string Codice { get; set; }


        public string codSettore { get; set; }


        public string codSottosettore { get; set; }
        

        public int CodMarca { get; set; }
        

        public int codTipoArticolo { get; set; }

        public string? Descr { get; set; }
        public Single? PrezzoListino1 { get; set; }
        public Single? PrezzoListino2 { get; set; }

        public bool isExported { get; set; }
        public DateTime? dataExp { get; set; }

         public int flagBeneServizio { get;  set; }

        [ForeignKey ("um")]
        public int codUM { get; set; }
        public virtual Looc_GOF_um um  { get; set; }


        [ForeignKey("cat")]
        public Int64 Categoria { get; set; }

        public virtual Looc_GOF_Categoria cat { get; set; }


        [ForeignKey("aliquota")]
        public string? codAliquota { get; set; }

        public virtual Looc_GOF_Aliquota aliquota { get; set; }
        public string? codContropartita { get; set; }

 
    }


    public class Looc_GOF_Categoria {

        [Key]
        public Int64 Codice { get; set; }
        public string  Nome { get; set; }

    }

    public class Looc_GOF_um {

        [Key]
        public int codice { get; set; }
        public string  descr { get; set; }

    }


    public class Looc_GOF_Aliquota {

        [Key]
        public string Codice { get; set; }

        public string Descr { get; set; }
        public string codDatev { get; set; }
        public double  aliquota { get; set; }

    }

}
