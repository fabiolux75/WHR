using LoocERP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoocERP.Models
{
    public class RemoteSetupModel
    {                
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [Display(Name = "ID Device")]
        [ForeignKey("Device")]
        public int? IdDevice { get; set; }
        public Device Device { get; set; }

        public string? ImeiDevice { get; set; }

        public int? SetupType { get; set; }

        public string? CodiceAutista { get; set; }
        public string? NomeAutista { get; set; }
        public string? CodiceVettore { get; set; }
        public string? Targa { get; set; }
        public string? ModelloVettore { get; set; }

        [ForeignKey("Companies")]
        public Guid? CodiceAziendaHr { get; set; }
        public ANA_Company Companies { get; set; }
        
        public string? ConnectionType { get; set; }
        public string? UriTest { get; set; }
        public string? UriProd { get; set; }
        public bool? Stato { get; set; }
        public string? RequestStatus { get; set; }
        public string? DeviceModel { get; set; }
        public string? CodiceCliente { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DataRichiesta { get; set; }
        public DateTime? DataLastConnection { get; set; }
        public string? RequestLatitude { get; set; }
        public string? RequestLongitude { get; set; }
        public string? Telefono { get; set; }
        public string? StatoAccount { get; set; }
        public string? TimeoutAccount { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public AppUser User { get; set; }

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

    }
}
