using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoocERP.Data;
using LoocERP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoocERP.Controllers
{
    public class ApiController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly Data.ApplicationDBContext _context;

        public ApiController(ApplicationDBContext context
                                , UserManager<AppUser> userManager
                                , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> RedirectTo( string urlRed,string udid)
        {

            var user = _context.Users.Where(c => c.Id == udid).FirstOrDefault();
            //HttpContext.Session.SetString("Layout", "_LayoutEmpty");

            DateTimeOffset theTime = DateTimeOffset.Now;


            var cookieOptions = new CookieOptions
            {
                // Set the secure flag, which Chrome's changes will require for SameSite none.
                // Note this will also require you to be running on HTTPS.
                Secure = true,

                // Set the cookie to HTTP only which is good practice unless you really do need
                // to access it client side in scripts.
                HttpOnly = true,

                // Add the SameSite attribute, this will emit the attribute with a value of none.
                // To not emit the attribute at all set
                // SameSite = (SameSiteMode)(-1)
                SameSite = SameSiteMode.None
            };


            //HttpContext.Response.Cookies.Append("Layout", "_LayoutEmpty", cookieOptions);


            if (user != null)
            {


                //await signInManager.SignInAsync(user, false);
                /*
                await signInManager.PasswordSignInAsync(
                        "test@looc.com", "Produzione.15", false, false);
                */
                
                return Redirect(urlRed);
            }
            return Redirect("");
            
        }

    }
}