using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace LoocERP.Models
{
    public class Looc_GOF_Cliente_Fornitore_esterno
    {
        [Key]
        public long Codice { get; set; }
        public String? CodFiscale { get; set; }
        public String RagSoc { get; set; }
        public String? PIVA { get; set; }
        public int codRegione { get; set; }
        public int codProvincia { get; set; }
        public int codComune { get; set; }
        public String Indirizzo { get; set; }
        public string? cap { get; set; }
        public string? Località { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Cellulare { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Referente { get; set; }
        public string? RefTelefono { get; set; }
        public string? RefCellulare { get; set; }
        public string? RefEmail { get; set; }
        public DateTime? DataCreazione { get; set; }
        public string? OperatoreCreazione { get; set; }
        public DateTime? DataModifica { get; set; }
        public string? OperatoreModifica { get; set; }
        public bool? SMOC { get; set; }
        public Guid? IdAziendaHR { get; set; }
        public Guid? MultiTenantId { get; set; }
    }
    public class Looc_GOF_Cliente_Fornitore
    {
        [Key]
        public long Codice { get; set; }
        public String CodSettore { get; set; }
        public String? CodSottoSettore { get; set; }
        public String CodCliente { get; set; } //FK
        public String? CodClienteIdentita { get; set; } //FK
        public long? CodClienteEsternoIdentita { get; set; } //FK
        public int? Tipo { get; set; }
        public String FL_Tipo { get; set; }    // Vincolo ([FL_Tipo] = 'C' or [FL_Tipo] = 'F')  
        public int? Sconto { get; set; }
        public int? Pagamento { get; set; } //FK
        public String? Banca { get; set; }
        public String? IBAN { get; set; }
        public int? codEsecutore { get; set; }
    }

    [Keyless]
    public class Looc_Comuni
    {
        public int CodPro { get; set; }
        [Key]
        public int Codice { get; set; }
        public String? Descr { get; set; }
        public String? CAP { get; set; }
        public DateTime? DataModifica { get; set; }
        public DateTime? DataCreazione { get; set; }
        public String? OperatoreCreazione { get; set; }
        public String? OperatoreModifica { get; set; }
    }
    public class Looc_Province
    {
        public int CodReg { get; set; }
        [Key]
        public int Codice { get; set; }
        public String Descr { get; set; }
        public String Sigla { get; set; }
        public DateTime? DataModifica { get; set; }
        public DateTime? DataCreazione { get; set; }
        public String? OperatoreCreazione { get; set; }
        public String? OperatoreModifica { get; set; }
    }
    public class Looc_Regioni
    {
        public int CodPae { get; set; }
        [Key]
        public int Codice { get; set; }
        public String Descr { get; set; }
        public DateTime? DataModifica { get; set; }
        public DateTime? DataCreazione { get; set; }
        public String? OperatoreCreazione { get; set; }
        public String? OperatoreModifica { get; set; }
    }

}
