using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using FatturaElettronica.Defaults;
using FatturaElettronica.Extensions;
using FatturaElettronica.Ordinaria;

namespace LoocERP.Models.Sdi
{

    public class SdiDoc
    {
        public SdiDoc()
        {
            Id = Guid.NewGuid();
        }        

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [Required]
        public string XmlBase64 { get; set; }
        public string filename { get; set; }
        public string numDoc { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime DataCreazione { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]      
        public DateTime DataDoc { get; set; }

        public string InOut { get; set; }
        public string CompanyName { get; set; }  // Nome del destinatario/mittente (in base se in fattura in uscita/entrata)
        public decimal Importo { get; set; }
        public decimal Imponibile { get; set; }
        // public string StatoSdi { get; set; }


        public string TipoDoc { get; set; }


        [ForeignKey("ANA_Company")]
        public Guid? IDCompany { get; set; }   // ID dell'azienda che riceve o invia la fattura
        public virtual ANA_Company ANA_Company { get; set; }   // Mittente


        [ForeignKey("Company")]
        public Guid? IdAnaCompany { get; set; }

        public virtual ANA_Company  Company{ get; set; }  // Destinatario


        /*public Guid? DdtId { get; set; }
        public string NumDDT { get; set; }*/

        public virtual ICollection<SdiDocDdt> SdiDocDdts { get; set; }

        [ForeignKey("AppUser")]
        public String UserId { get; set; }        
        public virtual AppUser User { get; set; }
        public string annoDoc  { get; set; }



        public FatturaOrdinaria getDecodeFatturaXml(){
            
            var data            = Convert.FromBase64String(this.XmlBase64);
            var contents        = new MemoryStream(data);
            var fatturaDecode   = new FatturaOrdinaria();
            string outputHtml   = "fat.html";
            string outputXsl    = string.Empty;

            try
            {
                fatturaDecode = FatturaOrdinaria.CreateInstance(Instance.Privati);
                // fatturaDecode.ReadXmlSigned(contents);
                fatturaDecode.ReadXml(contents);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Errore nella lettura fattura XML: [{ex.Message}]");
            }

            return fatturaDecode;
        }

    }



}
