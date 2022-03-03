using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Data;
using LoocERP.Models;
using System.Text.Json;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LoocERP.Controllers
{
    public class RemoteSetupsController : Controller
    {
        private ApplicationDBContext _context;

        private readonly UserManager<AppUser> userManager;

        public RemoteSetupsController(ApplicationDBContext context,
                                    UserManager<AppUser> userManager) {

            this.userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {

            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            var remotesetup = _context.RemoteSetup
                .Include(c => c.Device)
                .Where(c => c.MultiTenantId == user.MultiTenantId)
                .Select(i => new {
                    i.Id,
                    i.IdDevice,
                    i.ImeiDevice,
                    i.SetupType,
                    i.CodiceAutista,
                    i.NomeAutista,
                    i.CodiceVettore,
                    i.Targa,
                    i.ModelloVettore,
                    i.CodiceAziendaHr,
                    i.ConnectionType,
                    i.UriTest,
                    i.UriProd,
                    i.Stato,
                    i.RequestStatus,
                    i.DeviceModel,
                    i.CodiceCliente,
                    i.DataRichiesta,
                    i.DataLastConnection,
                    i.RequestLatitude,
                    i.RequestLongitude,
                    i.Telefono,
                    i.StatoAccount,
                    i.TimeoutAccount,
                    UserId = i.UserId.Trim(),
                    i.Provenienza,
                    i.SimSerialNumber,
                    i.TelephoneNumber,
                    i.AlertMessage,
                    i.PrivacyFlag,
                    i.LogoAz,
                    DeviceDescription = (i.Device != null) ? i.Device.Description : "",
                    CodiceVettoreDevice = (i.Device != null) ? i.Device.CodiceVettore : "",
                    CodiceClienteDevice = (i.Device != null) ? i.Device.CodiceCliente : "",
                    CodiceAutistaDevice = (i.Device != null) ? i.Device.CodiceAutista : "",
                    StatoDeviceDevice = (i.Device != null) ? i.Device.StatoDevice : 'N',
                    DescStatoDeviceDevice = (i.Device != null) ? i.Device.DescStatoDevice : "",
                    i.DeviceType,
                    i.DeviceCode,
                    ODBRead = i.ODBRead.Trim(),
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(remotesetup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }


        
        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new RemoteSetupModel();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.RemoteSetup.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id }, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.RemoteSetup.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.RemoteSetup.FirstOrDefaultAsync(item => item.Id == key);

            _context.RemoteSetup.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> C_ANA_CompaniesLookup(DataSourceLoadOptions loadOptions) {
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            var lookup = from i in _context.C_ANA_Companies
                         where i.MultiTenantId == user.MultiTenantId
                         orderby i.CreatedBy
                         select new {
                             Value = i.ID,
                             Text = i.RagioneSociale
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        [HttpGet]
        public async Task<IActionResult> UsersLookup(DataSourceLoadOptions loadOptions) {
            AppUser user = _context.Users.Include(c => c.MultiTenant).Where(c => c.Id == userManager.GetUserId(HttpContext.User)).FirstOrDefault();

            var lookup = from i in _context.Users
                         where i.MultiTenantId == user.MultiTenantId
                         orderby i.DataCreazione
                         select new {
                             Value = i.Id,
                             Text = i.FirstName + " " + i.LastName,
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        private void PopulateModel(RemoteSetupModel model, IDictionary values) {
            string ID = nameof(RemoteSetupModel.Id);
            string ID_DEVICE = nameof(RemoteSetupModel.IdDevice);
            string IMEI_DEVICE = nameof(RemoteSetupModel.ImeiDevice);
            string SETUP_TYPE = nameof(RemoteSetupModel.SetupType);
            string CODICE_AUTISTA = nameof(RemoteSetupModel.CodiceAutista);
            string NOME_AUTISTA = nameof(RemoteSetupModel.NomeAutista);
            string CODICE_VETTORE = nameof(RemoteSetupModel.CodiceVettore);
            string TARGA = nameof(RemoteSetupModel.Targa);
            string MODELLO_VETTORE = nameof(RemoteSetupModel.ModelloVettore);
            string CODICE_AZIENDA_HR = nameof(RemoteSetupModel.CodiceAziendaHr);
            string CONNECTION_TYPE = nameof(RemoteSetupModel.ConnectionType);
            string URI_TEST = nameof(RemoteSetupModel.UriTest);
            string URI_PROD = nameof(RemoteSetupModel.UriProd);
            string STATO = nameof(RemoteSetupModel.Stato);
            string REQUEST_STATUS = nameof(RemoteSetupModel.RequestStatus);
            string DEVICE_MODEL = nameof(RemoteSetupModel.DeviceModel);
            string CODICE_CLIENTE = nameof(RemoteSetupModel.CodiceCliente);
            string DATA_RICHIESTA = nameof(RemoteSetupModel.DataRichiesta);
            string DATA_LAST_CONNECTION = nameof(RemoteSetupModel.DataLastConnection);
            string REQUEST_LATITUDE = nameof(RemoteSetupModel.RequestLatitude);
            string REQUEST_LONGITUDE = nameof(RemoteSetupModel.RequestLongitude);
            string TELEFONO = nameof(RemoteSetupModel.Telefono);
            string STATO_ACCOUNT = nameof(RemoteSetupModel.StatoAccount);
            string TIMEOUT_ACCOUNT = nameof(RemoteSetupModel.TimeoutAccount);
            string USER_ID = nameof(RemoteSetupModel.UserId);
            string PROVENIENZA = nameof(RemoteSetupModel.Provenienza);
            string SIM_SERIAL_NUMBER = nameof(RemoteSetupModel.SimSerialNumber);
            string TELEPHONE_NUMBER = nameof(RemoteSetupModel.TelephoneNumber);
            string ALERT_MESSAGE = nameof(RemoteSetupModel.AlertMessage);
            string PRIVACY_FLAG = nameof(RemoteSetupModel.PrivacyFlag);
            string LOGO_AZ = nameof(RemoteSetupModel.LogoAz);
            string DEVICE_TYPE = nameof(RemoteSetupModel.DeviceType);
            string DEVICE_CODE = nameof(RemoteSetupModel.DeviceCode);
            string ODB_READ = nameof(RemoteSetupModel.ODBRead);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(ID_DEVICE)) {
                model.IdDevice = values[ID_DEVICE] != null ? Convert.ToInt32(values[ID_DEVICE]) : (int?)null;
            }

            if(values.Contains(IMEI_DEVICE)) {
                model.ImeiDevice = Convert.ToString(values[IMEI_DEVICE]);
            }

            if(values.Contains(SETUP_TYPE)) {
                model.SetupType = values[SETUP_TYPE] != null ? Convert.ToInt32(values[SETUP_TYPE]) : (int?)null;
            }

            if(values.Contains(CODICE_AUTISTA)) {
                model.CodiceAutista = Convert.ToString(values[CODICE_AUTISTA]);
            }

            if(values.Contains(NOME_AUTISTA)) {
                model.NomeAutista = Convert.ToString(values[NOME_AUTISTA]);
            }

            if(values.Contains(CODICE_VETTORE)) {
                model.CodiceVettore = Convert.ToString(values[CODICE_VETTORE]);
            }

            if(values.Contains(TARGA)) {
                model.Targa = Convert.ToString(values[TARGA]);
            }

            if(values.Contains(MODELLO_VETTORE)) {
                model.ModelloVettore = Convert.ToString(values[MODELLO_VETTORE]);
            }

            if(values.Contains(CODICE_AZIENDA_HR)) {
                model.CodiceAziendaHr = ConvertTo<System.Guid>(values[CODICE_AZIENDA_HR]);
            }

            if(values.Contains(CONNECTION_TYPE)) {
                model.ConnectionType = Convert.ToString(values[CONNECTION_TYPE]);
            }

            if(values.Contains(URI_TEST)) {
                model.UriTest = Convert.ToString(values[URI_TEST]);
            }

            if(values.Contains(URI_PROD)) {
                model.UriProd = Convert.ToString(values[URI_PROD]);
            }

            if(values.Contains(STATO)) {
                model.Stato = values[STATO] != null ? Convert.ToBoolean(values[STATO]) : (bool?)null;
            }

            if(values.Contains(REQUEST_STATUS)) {
                model.RequestStatus = Convert.ToString(values[REQUEST_STATUS]);
            }

            if(values.Contains(DEVICE_MODEL)) {
                model.DeviceModel = Convert.ToString(values[DEVICE_MODEL]);
            }

            if(values.Contains(CODICE_CLIENTE)) {
                model.CodiceCliente = Convert.ToString(values[CODICE_CLIENTE]);
            }

            if(values.Contains(DATA_RICHIESTA)) {
                model.DataRichiesta = values[DATA_RICHIESTA] != null ? Convert.ToDateTime(values[DATA_RICHIESTA]) : (DateTime?)null;
            }

            if(values.Contains(DATA_LAST_CONNECTION)) {
                model.DataLastConnection = values[DATA_LAST_CONNECTION] != null ? Convert.ToDateTime(values[DATA_LAST_CONNECTION]) : (DateTime?)null;
            }

            if(values.Contains(REQUEST_LATITUDE)) {
                model.RequestLatitude = Convert.ToString(values[REQUEST_LATITUDE]);
            }

            if(values.Contains(REQUEST_LONGITUDE)) {
                model.RequestLongitude = Convert.ToString(values[REQUEST_LONGITUDE]);
            }

            if(values.Contains(TELEFONO)) {
                model.Telefono = Convert.ToString(values[TELEFONO]);
            }

            if(values.Contains(STATO_ACCOUNT)) {
                model.StatoAccount = Convert.ToString(values[STATO_ACCOUNT]);
            }

            if(values.Contains(TIMEOUT_ACCOUNT)) {
                model.TimeoutAccount = Convert.ToString(values[TIMEOUT_ACCOUNT]);
            }

            if(values.Contains(USER_ID)) {
                model.UserId = Convert.ToString(values[USER_ID]);
                var user = _context.Users.Where(c => c.Id == model.UserId).FirstOrDefault();
                if(user != null)
                {
                    model.NomeAutista = user.FirstName + " " + user.LastName;
                }
            }

            if(values.Contains(PROVENIENZA)) {
                model.Provenienza = Convert.ToString(values[PROVENIENZA]);
            }

            if(values.Contains(SIM_SERIAL_NUMBER)) {
                model.SimSerialNumber = Convert.ToString(values[SIM_SERIAL_NUMBER]);
            }

            if(values.Contains(TELEPHONE_NUMBER)) {
                model.TelephoneNumber = Convert.ToString(values[TELEPHONE_NUMBER]);
            }

            if(values.Contains(ALERT_MESSAGE)) {
                model.AlertMessage = Convert.ToString(values[ALERT_MESSAGE]);
            }

            if(values.Contains(PRIVACY_FLAG)) {
                model.PrivacyFlag = values[PRIVACY_FLAG] != null ? Convert.ToBoolean(values[PRIVACY_FLAG]) : (bool?)null;
            }

            if(values.Contains(LOGO_AZ)) {
                model.LogoAz = Convert.ToString(values[LOGO_AZ]);
            }
        }

        private T ConvertTo<T>(object value) {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if(converter != null) {
                return (T)converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            } else {
                // If necessary, implement a type conversion here
                throw new NotImplementedException();
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}