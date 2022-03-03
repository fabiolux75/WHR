using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.ViewModels.RemoteSetup
{
    public class RemoteSetupViewModel
    {
        public int Id { get; set; }
        public int? IdDevice { get; set; }

        public string? ImeiDevice { get; set; }

        public int? SetupType { get; set; }

        public string? CodiceAutista { get; set; }
        public string? NomeAutista { get; set; }
        public string? CodiceVettore { get; set; }
        public string? Targa { get; set; }
        public string? ModelloVettore { get; set; }
        public Guid? CodiceAziendaHr { get; set; }
        public string? ConnectionType { get; set; }
        public string? UriTest { get; set; }
        public string? UriProd { get; set; }
        public bool? Stato { get; set; }
        public string? RequestStatus { get; set; }
        public string? DeviceModel { get; set; }
        public string? CodiceCliente { get; set; }
        public DateTime? DataRichiesta { get; set; }
        public DateTime? DataLastConnection { get; set; }
        public string? RequestLatitude { get; set; }
        public string? RequestLongitude { get; set; }
        public string? Telefono { get; set; }
        public string? StatoAccount { get; set; }
        public string? TimeoutAccount { get; set; }
        public string? UserId { get; set; }
        public string? Provenienza { get; set; }
        public string? SimSerialNumber { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? AlertMessage { get; set; }
        public bool? PrivacyFlag { get; set; }
        public string? LogoAz { get; set; }
        public Guid? MultiTenantId { get; set; }
        public string? DeviceType { get; set; }
        public string? DeviceCode { get; set; }
        public string? ODBRead { get; set; }

        public string? DeviceDescription { get; set; }
        public string? CodiceVettoreDevice { get; set; }
        public string? CodiceClienteDevice { get; set; }
        public string? CodiceAutistaDevice { get; set; }
        public string? StatoDeviceDevice { get; set; }
        public string? DescStatoDeviceDevice { get; set; }
        /*
         [idDevice]
      ,[Description]
      ,[CodiceVettore]
      ,[CodiceCliente]
      ,[CodiceAutista]
      ,[StatoDevice]
      ,[DescStatoDevice]
        */


    }
}
