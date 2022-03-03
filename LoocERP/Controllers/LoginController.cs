using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Models;
using LoocERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using LoocERP.Data;
using Newtonsoft.Json;
using LoocERP.Helpers;

namespace LoocERP.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly Data.ApplicationDBContext _context;

        // DBWorker db = new DBWorker();
        private readonly IStringLocalizer<LoginController> _localizer;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(
                                Data.ApplicationDBContext context
                                , IStringLocalizer<LoginController> localizer
                                , SignInManager<AppUser> signInManager
                                , UserManager<AppUser> userManager
                                ,IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _localizer = localizer;
            this.signInManager = signInManager;
            this.userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        public IActionResult IndexAsync(string error = null, string errorTitle = null,string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.IsSignedIn(User);

                if (result)
                {
                    return RedirectToAction("index", "home");
                }
            }
            ViewData["error"] = error;
            ViewData["errorTitle"] = errorTitle;
            ViewData["loginText"] = _localizer["loginText"].Value;
            ViewData["rememberMe"] = _localizer["rememberMe"].Value;
            ViewData["logInButton"] = _localizer["logInButton"].Value;
            ViewData["loginForgotText"] = _localizer["loginForgotText"].Value;
            var model = new LoginViewModel { ReturnUrl = ReturnUrl };

            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            ViewBag.banner = "/looc/images/banner/login_maincheck.jpg";
            ViewBag.logo = "/looc/images/logo/logo_kresearch.png";
            var r = _context.Set<MultiTenant>().Where(oh => host.Contains(oh.slughost)).FirstOrDefault();
            if ( r != null && r.LoocId != null){
                ViewBag.banner = "/looc/images/banner/login_"+r.LoocId+".jpg";
                ViewBag.logo = "/looc/images/logo/logo_" + r.LoocId + ".png";
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {

            try{
                AppUser res = await userManager.FindByEmailAsync(model.Email);

                if (ModelState.IsValid)
                {


                    var result = await signInManager.PasswordSignInAsync(res, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        LogHR.Instance.Log(_context, res, LogAuditHR.LogAuditHREventType.Login,"Accesso utente");
                        if(model.ReturnUrl != null)
                        {
                            return Redirect(model.ReturnUrl);
                        }                    
                        /* inserisco l'utente in session */
                        return RedirectToAction("index", "home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }catch(Exception e){
                Console.Write(e.Message);
            }

            ViewData["error"] = "Nome utente o password non validi";
            ViewData["errorTitle"] = "Dati inseriti non validi";
            ViewData["loginText"] = _localizer["loginText"].Value;
            ViewData["rememberMe"] = _localizer["rememberMe"].Value;
            ViewData["logInButton"] = _localizer["logInButton"].Value;
            ViewData["loginForgotText"] = _localizer["loginForgotText"].Value;
                        

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            LogHR.Instance.Log(_context, user, LogAuditHR.LogAuditHREventType.Logout,"Logout utente");

            await signInManager.SignOutAsync();
            return RedirectToAction("index");
        }

        [AllowAnonymous]
        public IActionResult language(string lang = null)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return Redirect(ViewBag.returnUrl);
        }

        [AllowAnonymous]
        public IActionResult forgot(string error = null)
        {
            var a = _localizer["test"].Value;
            ViewData["error"] = error;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {

            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to AppUser
                var user = new AppUser
                {
                    UserName = model.User.Email,
                    Email = model.User.Email,
                    FirstName = model.User.FirstName,
                    LastName = model.User.LastName
                };

                // Store user data in AspNetUsers database table
                //var result = await userManager.CreateAsync(user, model.Password);
                var result = await userManager.CreateAsync(user);

                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }   
}