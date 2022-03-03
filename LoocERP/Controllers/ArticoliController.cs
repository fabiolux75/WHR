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

namespace LoocERP.Controllers
{
    public class ArticoliController : Controller
    {
        private ApplicationDBContext _context;

        public ArticoliController(ApplicationDBContext context) {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? idArticolo)
        {

            if(idArticolo != null)
            {
                var articolo = _context.C_MAG_ARTICOLI.Where(c => c.ID == idArticolo).FirstOrDefault();
                return View(articolo);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var c_mag_articoli = _context.C_MAG_ARTICOLI.Include(c => c.marca).Select(i => new {
                i.ID,
                i.CodiceMag,
                i.Descrizione,
                i.idUm,
                i.ScortaMinima,
                i.Qty,
                i.IdMarca,
                i.IdCategoria,
                i.MultiTenantId,
                i.PrezzoVendita,
                i.IdFamiglia,
                i.IdSottoFamiglia,
                i.IdGruppo,
                i.IdSottoGruppo,
                nomeMarca = i.marca.Descr,
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(c_mag_articoli, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Articolo();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.C_MAG_ARTICOLI.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ID }, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.C_MAG_ARTICOLI.FirstOrDefaultAsync(item => item.ID == key);
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
            var model = await _context.C_MAG_ARTICOLI.FirstOrDefaultAsync(item => item.ID == key);

            _context.C_MAG_ARTICOLI.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> C_UMLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.C_UM
                         orderby i.Descrizione
                         select new {
                             Value = i.Id,
                             Text = i.Descrizione
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        [HttpGet]
        public async Task<IActionResult> MARCHELookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MARCHE
                         orderby i.Descr
                         select new {
                             Value = i.Codice,
                             Text = i.Descr
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions), new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
            });
        }

        private void PopulateModel(Articolo model, IDictionary values) {
            string ID = nameof(Articolo.ID);
            string CODICE_MAG = nameof(Articolo.CodiceMag);
            string DESCRIZIONE = nameof(Articolo.Descrizione);
            string ID_UM = nameof(Articolo.idUm);
            string SCORTA_MINIMA = nameof(Articolo.ScortaMinima);
            string QTY = nameof(Articolo.Qty);
            string ID_MARCA = nameof(Articolo.IdMarca);
            string ID_CATEGORIA = nameof(Articolo.IdCategoria);
            string MULTI_TENANT_ID = nameof(Articolo.MultiTenantId);
            string PREZZO_VENDITA = nameof(Articolo.PrezzoVendita);
            string ID_FAMIGLIA = nameof(Articolo.IdFamiglia);
            string ID_SOTTO_FAMIGLIA = nameof(Articolo.IdSottoFamiglia);
            string ID_GRUPPO = nameof(Articolo.IdGruppo);
            string ID_SOTTO_GRUPPO = nameof(Articolo.IdSottoGruppo);

            if(values.Contains(ID)) {
                model.ID = ConvertTo<System.Guid>(values[ID]);
            }

            if(values.Contains(CODICE_MAG)) {
                model.CodiceMag = Convert.ToString(values[CODICE_MAG]);
            }

            if(values.Contains(DESCRIZIONE)) {
                model.Descrizione = Convert.ToString(values[DESCRIZIONE]);
            }

            if(values.Contains(ID_UM)) {
                model.idUm = Convert.ToInt32(values[ID_UM]);
            }

            if(values.Contains(SCORTA_MINIMA)) {
                model.ScortaMinima = Convert.ToDouble(values[SCORTA_MINIMA], CultureInfo.InvariantCulture);
            }

            if(values.Contains(QTY)) {
                model.Qty = Convert.ToDouble(values[QTY], CultureInfo.InvariantCulture);
            }

            if(values.Contains(ID_MARCA)) {
                model.IdMarca = values[ID_MARCA] != null ? Convert.ToInt32(values[ID_MARCA]) : (int?)null;
            }

            if(values.Contains(ID_CATEGORIA)) {
                model.IdCategoria = values[ID_CATEGORIA] != null ? Convert.ToInt32(values[ID_CATEGORIA]) : (int?)null;
            }

            if(values.Contains(MULTI_TENANT_ID)) {
                model.MultiTenantId = ConvertTo<System.Guid>(values[MULTI_TENANT_ID]);
            }

            if(values.Contains(PREZZO_VENDITA)) {
                model.PrezzoVendita = Convert.ToDouble(values[PREZZO_VENDITA], CultureInfo.InvariantCulture);
            }

            if(values.Contains(ID_FAMIGLIA)) {
                model.IdFamiglia = Convert.ToInt32(values[ID_FAMIGLIA]);
            }

            if(values.Contains(ID_SOTTO_FAMIGLIA)) {
                model.IdSottoFamiglia = Convert.ToInt32(values[ID_SOTTO_FAMIGLIA]);
            }

            if(values.Contains(ID_GRUPPO)) {
                model.IdGruppo = Convert.ToInt32(values[ID_GRUPPO]);
            }

            if(values.Contains(ID_SOTTO_GRUPPO)) {
                model.IdSottoGruppo = Convert.ToInt32(values[ID_SOTTO_GRUPPO]);
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