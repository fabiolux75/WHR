namespace LoocERP.Models
{

    public class CompanyCsv
    {
        public string RagioneSociale { get; set; }
        public string PIva { get; set; }
        public string FiscalCode { get; set; }
        public string? EmailPec { get; set; }
        public string? CodiceSdi { get; set; }
        public string Nazione { get; set; }
        public string Regione { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }  // Non presente sul tracciato
        public string EmailAziendale { get; set; }
        public string? SitoWeb { get; set; }
        public string? Telefono { get; set; }
        public string? Fax { get; set; }
        // public bool isSupplier { get; set; }
        // public bool isCustomer { get; set; }
        // public bool isOfficina { get; set; }
        // public string? ParentID { get; set; }
        // public string? Img { get; set; }
        // public string? active { get; set; }
        // public string? isExternal { get; set; }
        public string? Pagamento { get; set; }
        /*public string TipoPagamentoID { get; set; }
        public string ModPagamentoID { get; set; }*/
        public string? Banca { get; set; }
        public string? IBAN { get; set; }
        public string DatevCode { get; set; }
        public TipoPagamento? TipoPagamentoID { get; set; }
        public ModPagamento? ModalitaPagamentoID { get; set; }
    }
}