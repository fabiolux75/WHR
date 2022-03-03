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

namespace LoocERP.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TEST_CompanyController : Controller
    {
        private ApplicationDBContext _context;

        public TEST_CompanyController(ApplicationDBContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var c_ana_companies = _context.C_ANA_Companies.Select(i => new {
                i.CreatedAt,
                i.UpdatedAt,
                i.DeletedAt,
                i.CreatedBy,
                i.UpdatedBy,
                i.DeletedBy,
                i.ID,
                i.InternalCode,
                i.RagioneSociale,
                i.PIva,
                i.FiscalCode,
                i.EmailPec,
                i.CodiceSdi,
                i.Nazione,
                i.Regione,
                i.Provincia,
                i.Citta,
                i.Indirizzo,
                i.EmailAziendale,
                i.SitoWeb,
                i.Telefono,
                i.Fax,
                i.isSupplier,
                i.isCustomer,
                i.isOfficina,
                i.isExternal,
                i.ParentID,
                i.active,
                i.MultiTenantId,
                i.Banca,
                i.IBAN,
                i.isCopiedOnLooc,
                i.PagheCodAzienda,
                i.PagheCodFiliale
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(c_ana_companies, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new ANA_Company();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.C_ANA_Companies.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.C_ANA_Companies.FirstOrDefaultAsync(item => item.ID == key);
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
        public async Task Delete(Guid key) {
            var model = await _context.C_ANA_Companies.FirstOrDefaultAsync(item => item.ID == key);

            _context.C_ANA_Companies.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> C_ANA_CompaniesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.C_ANA_Companies
                         orderby i.CreatedBy
                         select new {
                             Value = i.ID,
                             Text = i.RagioneSociale
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> C_MultitenantLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.C_Multitenant
                         orderby i.Name
                         select new {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(ANA_Company model, IDictionary values) {
            string ID = nameof(ANA_Company.ID);
            string INTERNAL_CODE = nameof(ANA_Company.InternalCode);
            string RAGIONE_SOCIALE = nameof(ANA_Company.RagioneSociale);
            string PIVA = nameof(ANA_Company.PIva);
            string FISCAL_CODE = nameof(ANA_Company.FiscalCode);
            string EMAIL_PEC = nameof(ANA_Company.EmailPec);
            string CODICE_SDI = nameof(ANA_Company.CodiceSdi);
            string NAZIONE = nameof(ANA_Company.Nazione);
            string REGIONE = nameof(ANA_Company.Regione);
            string PROVINCIA = nameof(ANA_Company.Provincia);
            string CITTA = nameof(ANA_Company.Citta);
            string INDIRIZZO = nameof(ANA_Company.Indirizzo);
            string EMAIL_AZIENDALE = nameof(ANA_Company.EmailAziendale);
            string SITO_WEB = nameof(ANA_Company.SitoWeb);
            string TELEFONO = nameof(ANA_Company.Telefono);
            string FAX = nameof(ANA_Company.Fax);
            string IS_SUPPLIER = nameof(ANA_Company.isSupplier);
            string IS_CUSTOMER = nameof(ANA_Company.isCustomer);
            string IS_OFFICINA = nameof(ANA_Company.isOfficina);
            string IS_EXTERNAL = nameof(ANA_Company.isExternal);
            string PARENT_ID = nameof(ANA_Company.ParentID);
            string ACTIVE = nameof(ANA_Company.active);
            string MULTI_TENANT_ID = nameof(ANA_Company.MultiTenantId);
            string BANCA = nameof(ANA_Company.Banca);
            string IBAN = nameof(ANA_Company.IBAN);
            string IS_COPIED_ON_LOOC = nameof(ANA_Company.isCopiedOnLooc);
            string PAGHE_COD_AZIENDA = nameof(ANA_Company.PagheCodAzienda);
            string PAGHE_COD_FILIALE = nameof(ANA_Company.PagheCodFiliale);

            if(values.Contains(ID)) {
                model.ID = ConvertTo<System.Guid>(values[ID]);
            }

            if(values.Contains(INTERNAL_CODE)) {
                model.InternalCode = Convert.ToString(values[INTERNAL_CODE]);
            }

            if(values.Contains(RAGIONE_SOCIALE)) {
                model.RagioneSociale = Convert.ToString(values[RAGIONE_SOCIALE]);
            }

            if(values.Contains(PIVA)) {
                model.PIva = Convert.ToString(values[PIVA]);
            }

            if(values.Contains(FISCAL_CODE)) {
                model.FiscalCode = Convert.ToString(values[FISCAL_CODE]);
            }

            if(values.Contains(EMAIL_PEC)) {
                model.EmailPec = Convert.ToString(values[EMAIL_PEC]);
            }

            if(values.Contains(CODICE_SDI)) {
                model.CodiceSdi = Convert.ToString(values[CODICE_SDI]);
            }

            if(values.Contains(NAZIONE)) {
                model.Nazione = Convert.ToString(values[NAZIONE]);
            }

            if(values.Contains(REGIONE)) {
                model.Regione = Convert.ToString(values[REGIONE]);
            }

            if(values.Contains(PROVINCIA)) {
                model.Provincia = Convert.ToString(values[PROVINCIA]);
            }

            if(values.Contains(CITTA)) {
                model.Citta = Convert.ToString(values[CITTA]);
            }

            if(values.Contains(INDIRIZZO)) {
                model.Indirizzo = Convert.ToString(values[INDIRIZZO]);
            }

            if(values.Contains(EMAIL_AZIENDALE)) {
                model.EmailAziendale = Convert.ToString(values[EMAIL_AZIENDALE]);
            }

            if(values.Contains(SITO_WEB)) {
                model.SitoWeb = Convert.ToString(values[SITO_WEB]);
            }

            if(values.Contains(TELEFONO)) {
                model.Telefono = Convert.ToString(values[TELEFONO]);
            }

            if(values.Contains(FAX)) {
                model.Fax = Convert.ToString(values[FAX]);
            }

            if(values.Contains(IS_SUPPLIER)) {
                model.isSupplier = values[IS_SUPPLIER] != null ? Convert.ToBoolean(values[IS_SUPPLIER]) : (bool?)null;
            }

            if(values.Contains(IS_CUSTOMER)) {
                model.isCustomer = values[IS_CUSTOMER] != null ? Convert.ToBoolean(values[IS_CUSTOMER]) : (bool?)null;
            }

            if(values.Contains(IS_OFFICINA)) {
                model.isOfficina = values[IS_OFFICINA] != null ? Convert.ToBoolean(values[IS_OFFICINA]) : (bool?)null;
            }

            if(values.Contains(IS_EXTERNAL)) {
                model.isExternal = values[IS_EXTERNAL] != null ? Convert.ToBoolean(values[IS_EXTERNAL]) : (bool?)null;
            }

            if(values.Contains(PARENT_ID)) {
                model.ParentID = values[PARENT_ID] != null ? ConvertTo<System.Guid>(values[PARENT_ID]) : (Guid?)null;
            }

            if(values.Contains(ACTIVE)) {
                model.active = Convert.ToBoolean(values[ACTIVE]);
            }

            if(values.Contains(MULTI_TENANT_ID)) {
                model.MultiTenantId = values[MULTI_TENANT_ID] != null ? ConvertTo<System.Guid>(values[MULTI_TENANT_ID]) : (Guid?)null;
            }

            if(values.Contains(BANCA)) {
                model.Banca = Convert.ToString(values[BANCA]);
            }

            if(values.Contains(IBAN)) {
                model.IBAN = Convert.ToString(values[IBAN]);
            }

            if(values.Contains(IS_COPIED_ON_LOOC)) {
                model.isCopiedOnLooc = Convert.ToBoolean(values[IS_COPIED_ON_LOOC]);
            }

            if(values.Contains(PAGHE_COD_AZIENDA)) {
                model.PagheCodAzienda = Convert.ToString(values[PAGHE_COD_AZIENDA]);
            }

            if(values.Contains(PAGHE_COD_FILIALE)) {
                model.PagheCodFiliale = Convert.ToString(values[PAGHE_COD_FILIALE]);
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