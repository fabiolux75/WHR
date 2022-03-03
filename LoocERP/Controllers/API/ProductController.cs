using LoocERP.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Models;



namespace LoocERP.Controllers.API
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProductController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            List<Vettore> Items = new List<Vettore>();
            int Count = 0;
            return Ok(new { Items, Count});
        }



        [HttpPost("[action]")]
        public IActionResult Insert()
        {

            return Ok(new { });
        }

        [HttpPost("[action]")]
        public IActionResult Update()
        {
            return Ok(new { });
        }

        [HttpPost("[action]")]
        public IActionResult Remove()
        {
            return Ok(new { });

        }
    }
}
