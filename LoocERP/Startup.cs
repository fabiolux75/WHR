// #define LIVE_RELOAD
#define LOCAL_DEV
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LoocERP.Models;
//using LoocERP.Seeders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using LoocERP.Data;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
#if DEBUG
using Westwind.AspNetCore.LiveReload;
#endif
namespace LoocERP.Controllers
{
    public class Startup
    {
        // public static string CONNECTION_TYPE = "CLOUD";
        public static string CONNECTION_TYPE = "TEST";
        //public static string CONNECTION_TYPE = "DEV";
        //public static string CONNECTION_TYPE = "LOCAL";
        //public static string CONNECTION_TYPE = "PRE";
        public static string UK_PATH_TYPE = "CLOUD";
        //public static string UK_PATH_TYPE = "TEST";
        //public static string UK_PATH_TYPE = "DEV";
        //public static string UK_PATH_TYPE = "LOCAL";
        //public static string UK_PATH_TYPE = "PRE";

        public static string CONNECTION_STRING = "";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceCollection services { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;

#if DEBUG && LIVE_RELOAD
            services.AddLiveReload();
#endif
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddLocalization(options => {

                options.ResourcesPath = "Resources";

            });

            CONNECTION_STRING = Configuration.GetConnectionString("CloudConnection");
            if (CONNECTION_TYPE.Equals("LOCAL"))
            {
                CONNECTION_STRING = Configuration.GetConnectionString("DefaultConnection");
            }
            else if (CONNECTION_TYPE.Equals("DEV"))
            {
                CONNECTION_STRING = Configuration.GetConnectionString("DevConnection");
            }
            else if (CONNECTION_TYPE.Equals("TEST"))
            {
                CONNECTION_STRING = Configuration.GetConnectionString("CloudConnectionTest");
            }else if (CONNECTION_TYPE.Equals("PRE"))
            {
                CONNECTION_STRING = Configuration.GetConnectionString("PreConnection");
            }

            services.AddDbContext<Data.ApplicationDBContext>(options => options.UseSqlServer(
                CONNECTION_STRING,
                sqlServerOptions => sqlServerOptions.CommandTimeout(400)
                )
            );
            services.AddDbContext<EnumDBContext>(options => options.UseSqlServer(CONNECTION_STRING));


#if LOCAL_DEV
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
#else
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
#endif

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.Cookie.Name = ".GHPlatform.Session";
                options.IdleTimeout = TimeSpan.FromHours(8);
                options.Cookie.HttpOnly = true;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };

            });


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(8);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(8);

                options.LoginPath = "/login";
                options.AccessDeniedPath = "/login";
                options.SlidingExpiration = true;
            });



            services.AddIdentity<LoocERP.Models.AppUser, IdentityRole>().AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDBContext>()
               .AddDefaultTokenProviders();

            /*
            services.ConfigureApplicationCookie(
                options => options.LoginPath = "/login");
            */

            services.AddMvc(
                options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }
                ).AddXmlSerializerFormatters().AddRazorRuntimeCompilation();




            services.ConfigureApplicationCookie(options => {
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = "/login";
            }); 






            services.AddAuthorization(options =>
            {
                options.AddPolicy("user.edit.email", policy => policy.RequireClaim("user.edit.email", "user.edit.email"));
                options.AddPolicy("Users.Show", policy => policy.RequireClaim("Users.Show", "Users.Show"));
                options.AddPolicy("Users.Create", policy => policy.RequireClaim("Users.Create", "Users.Create"));
                options.AddPolicy("Users.Edit", policy => policy.RequireClaim("Users.Edit", "Users.Edit"));

                options.AddPolicy("Cantiere.Show", policy => policy.RequireClaim("Cantiere.Show", "Cantiere.Show"));
                options.AddPolicy("Cantiere.Create", policy => policy.RequireClaim("Cantiere.Create", "Cantiere.Create"));
                options.AddPolicy("Cantiere.Edit", policy => policy.RequireClaim("Cantiere.Edit", "Cantiere.Edit"));

                options.AddPolicy("Companies.Show", policy => policy.RequireClaim("Companies.Show", "Companies.Show"));
                options.AddPolicy("Companies.Create", policy => policy.RequireClaim("Companies.Create", "Companies.Create"));
                options.AddPolicy("Companies.Edit", policy => policy.RequireClaim("Companies.Edit", "Companies.Edit"));

                options.AddPolicy("Project.Show", policy => policy.RequireClaim("Project.Show", "Project.Show"));
                options.AddPolicy("Project.Create", policy => policy.RequireClaim("Project.Create", "Project.Create"));
                options.AddPolicy("Project.Edit", policy => policy.RequireClaim("Project.Edit", "Project.Edit"));

                options.AddPolicy("ContractUser.Show", policy => policy.RequireClaim("ContractUser.Show", "ContractUser.Show"));
                options.AddPolicy("ContractUser.Create", policy => policy.RequireClaim("ContractUser.Create", "ContractUser.Create"));
                options.AddPolicy("ContractUser.Edit", policy => policy.RequireClaim("ContractUser.Edit", "ContractUser.Edit"));

                options.AddPolicy("Document.Show", policy => policy.RequireClaim("Document.Show", "Document.Show"));
                options.AddPolicy("Document.Create", policy => policy.RequireClaim("Document.Create", "Document.Create"));
                options.AddPolicy("Document.Edit", policy => policy.RequireClaim("Document.Edit", "Document.Edit"));

                options.AddPolicy("MalattiaUser.Show", policy => policy.RequireClaim("MalattiaUser.Show", "MalattiaUser.Show"));
                options.AddPolicy("MalattiaUser.Create", policy => policy.RequireClaim("MalattiaUser.Create", "MalattiaUser.Create"));
                options.AddPolicy("MalattiaUser.Edit", policy => policy.RequireClaim("MalattiaUser.Edit", "MalattiaUser.Edit"));

                options.AddPolicy("RelTurnoUser.Show", policy => policy.RequireClaim("RelTurnoUser.Show", "RelTurnoUser.Show"));
                options.AddPolicy("RelTurnoUser.Create", policy => policy.RequireClaim("RelTurnoUser.Create", "RelTurnoUser.Create"));
                //options.AddPolicy("RelTurnoUser.Edit", policy => policy.RequireClaim("RelTurnoUser.Edit", "RelTurnoUser.Edit"));

                options.AddPolicy("Roles.Show", policy => policy.RequireClaim("Roles.Show", "Roles.Show"));
                options.AddPolicy("Roles.Create", policy => policy.RequireClaim("Roles.Create", "Roles.Create"));
                options.AddPolicy("Roles.Edit", policy => policy.RequireClaim("Roles.Edit", "Roles.Edit"));

                options.AddPolicy("Timesheet.Show", policy => policy.RequireClaim("Timesheet.Show", "Timesheet.Show"));
                //options.AddPolicy("Timesheet.Create", policy => policy.RequireClaim("Timesheet.Create", "Timesheet.Create"));
                //options.AddPolicy("Timesheet.Edit", policy => policy.RequireClaim("Timesheet.Edit", "Timesheet.Edit"));

                options.AddPolicy("TimesheetDailyReport.Show", policy => policy.RequireClaim("TimesheetDailyReport.Show", "TimesheetDailyReport.Show"));
                //options.AddPolicy("TimesheetDailyReport.Create", policy => policy.RequireClaim("TimesheetDailyReport.Create", "TimesheetDailyReport.Create"));
                //options.AddPolicy("TimesheetDailyReport.Edit", policy => policy.RequireClaim("TimesheetDailyReport.Edit", "TimesheetDailyReport.Edit"));

                options.AddPolicy("Turni.Show", policy => policy.RequireClaim("Turni.Show", "Turni.Show"));
                options.AddPolicy("Turni.Create", policy => policy.RequireClaim("Turni.Create", "Turni.Create"));
                //options.AddPolicy("Turni.Edit", policy => policy.RequireClaim("Turni.Edit", "Turni.Edit"));

                options.AddPolicy("RelMansioneUser.Show", policy => policy.RequireClaim("RelMansioneUser.Show", "RelMansioneUser.Show"));
                options.AddPolicy("RelMansioneUser.Create", policy => policy.RequireClaim("RelMansioneUser.Create", "RelMansioneUser.Create"));
                options.AddPolicy("RelMansioneUser.Edit", policy => policy.RequireClaim("RelMansioneUser.Edit", "RelMansioneUser.Edit"));

                options.AddPolicy("RelSpecializzazioneUser.Show", policy => policy.RequireClaim("RelSpecializzazioneUser.Show", "RelSpecializzazioneUser.Show"));
                options.AddPolicy("RelSpecializzazioneUser.Create", policy => policy.RequireClaim("RelSpecializzazioneUser.Create", "RelSpecializzazioneUser.Create"));
                options.AddPolicy("RelSpecializzazioneUser.Edit", policy => policy.RequireClaim("RelSpecializzazioneUser.Edit", "RelSpecializzazioneUser.Edit"));

                options.AddPolicy("Corsi.Show", policy => policy.RequireClaim("Corsi.Show", "Corsi.Show"));
                options.AddPolicy("Corsi.Create", policy => policy.RequireClaim("Corsi.Create", "Corsi.Create"));
                options.AddPolicy("Corsi.Edit", policy => policy.RequireClaim("Corsi.Edit", "Corsi.Edit"));

                options.AddPolicy("VisitaMedica.Show", policy => policy.RequireClaim("VisitaMedica.Show", "VisitaMedica.Show"));
                options.AddPolicy("VisitaMedica.Create", policy => policy.RequireClaim("VisitaMedica.Create", "VisitaMedica.Create"));
                options.AddPolicy("VisitaMedica.Edit", policy => policy.RequireClaim("VisitaMedica.Edit", "VisitaMedica.Edit"));

                options.AddPolicy("RichiestaDipendente.Show", policy => policy.RequireClaim("RichiestaDipendente.Show", "RichiestaDipendente.Show"));
                options.AddPolicy("RichiestaDipendente.Create", policy => policy.RequireClaim("RichiestaDipendente.Create", "RichiestaDipendente.Create"));
                options.AddPolicy("RichiestaDipendente.Edit", policy => policy.RequireClaim("RichiestaDipendente.Edit", "RichiestaDipendente.Edit"));

                options.AddPolicy("Vettore.Show", policy => policy.RequireClaim("Vettore.Show", "Vettore.Show"));
                options.AddPolicy("Vettore.Create", policy => policy.RequireClaim("Vettore.Create", "Vettore.Create"));
                options.AddPolicy("Vettore.Edit", policy => policy.RequireClaim("Vettore.Edit", "Vettore.Edit"));
                options.AddPolicy("VettoreUser.Show", policy => policy.RequireClaim("VettoreUser.Show", "VettoreUser.Show"));
                options.AddPolicy("VettoreUser.Create", policy => policy.RequireClaim("VettoreUser.Create", "VettoreUser.Create"));
                options.AddPolicy("VettoreUser.Edit", policy => policy.RequireClaim("VettoreUser.Edit", "VettoreUser.Edit"));
                options.AddPolicy("VettoreCantiere.Show", policy => policy.RequireClaim("VettoreCantiere.Show", "VettoreCantiere.Show"));
                options.AddPolicy("VettoreCantiere.Create", policy => policy.RequireClaim("VettoreCantiere.Create", "VettoreCantiere.Create"));
                options.AddPolicy("VettoreCantiere.Edit", policy => policy.RequireClaim("VettoreCantiere.Edit", "VettoreCantiere.Edit"));
                options.AddPolicy("VettoreParcheggio.Show", policy => policy.RequireClaim("VettoreParcheggio.Show", "VettoreParcheggio.Show"));
                options.AddPolicy("VettoreParcheggio.Create", policy => policy.RequireClaim("VettoreParcheggio.Create", "VettoreParcheggio.Create"));
                options.AddPolicy("VettoreParcheggio.Edit", policy => policy.RequireClaim("VettoreParcheggio.Edit", "VettoreParcheggio.Edit"));

                //LOOC LINK
                options.AddPolicy("Magazzino.Show", policy => policy.RequireClaim("Magazzino.Show", "Magazzino.Show"));
                options.AddPolicy("Interventi.Show", policy => policy.RequireClaim("Interventi.Show", "Interventi.Show"));
                options.AddPolicy("PianiDiManutenzione.Show", policy => policy.RequireClaim("PianiDiManutenzione.Show", "PianiDiManutenzione.Show"));
                options.AddPolicy("TracciamentoFlotta.Show", policy => policy.RequireClaim("TracciamentoFlotta.Show", "TracciamentoFlotta.Show"));
                options.AddPolicy("GestioneMovimento.Show", policy => policy.RequireClaim("GestioneMovimento.Show", "GestioneMovimento.Show"));





                options.AddPolicy("Hr.Show", policy => policy.RequireClaim("Hr.Show", "Hr.Show"));
                options.AddPolicy("Commesse.Show", policy => policy.RequireClaim("Commesse.Show", "Commesse.Show"));
                options.AddPolicy("Logistica.Show", policy => policy.RequireClaim("Logistica.Show", "Logistica.Show"));
                options.AddPolicy("Settings.Show", policy => policy.RequireClaim("Settings.Show", "Settings.Show"));
                options.AddPolicy("Home.Show", policy => policy.RequireClaim("Home.Show", "Home.Show"));


                options.AddPolicy("LateralHr.Show", policy => policy.RequireClaim("LateralHr.Show", "LateralHr.Show"));
                options.AddPolicy("LateralCommesse.Show", policy => policy.RequireClaim("LateralCommesse.Show", "LateralCommesse.Show"));
                options.AddPolicy("LateralGestioneMovimento.Show", policy => policy.RequireClaim("LateralGestioneMovimento.Show", "LateralGestioneMovimento.Show"));


                options.AddPolicy("Parking.Edit", policy => policy.RequireClaim("Parking.Edit", "Parking.Edit"));
                options.AddPolicy("Parking.Create", policy => policy.RequireClaim("Parking.Create", "Parking.Create"));


                options.AddPolicy("Leasing.Edit", policy => policy.RequireClaim("Leasing.Edit", "Leasing.Edit"));
                options.AddPolicy("Leasing.Create", policy => policy.RequireClaim("Leasing.Create", "Leasing.Create"));
                options.AddPolicy("Leasing.Show", policy => policy.RequireClaim("Leasing.Show", "Leasing.Show"));

                options.AddPolicy("Noleggi.Edit", policy => policy.RequireClaim("Noleggi.Edit", "Noleggi.Edit"));
                options.AddPolicy("Noleggi.Create", policy => policy.RequireClaim("Noleggi.Create", "Noleggi.Create"));
                options.AddPolicy("Noleggi.Show", policy => policy.RequireClaim("Noleggi.Show", "Noleggi.Show"));



                options.AddPolicy("mansione.add.macchina", policy => policy.RequireClaim("mansione.add.macchina", "mansione.add.macchina"));
                options.AddPolicy("mansione.delete", policy => policy.RequireClaim("mansione.delete", "mansione.delete"));
                options.AddPolicy("mansione.delete.macchina", policy => policy.RequireClaim("mansione.delete.macchina", "mansione.delete.macchina"));
                options.AddPolicy("mansione.Edit", policy => policy.RequireClaim("mansione.Edit", "mansione.Edit"));
                options.AddPolicy("mansione.View", policy => policy.RequireClaim("mansione.View", "mansione.View"));
                options.AddPolicy("mansione.move.macchina", policy => policy.RequireClaim("mansione.move.macchina", "mansione.move.macchina"));
                options.AddPolicy("mansione.create", policy => policy.RequireClaim("mansione.create", "mansione.create"));



                options.AddPolicy("specializzazioni.view", policy => policy.RequireClaim("specializzazioni.view", "specializzazioni.view"));
                options.AddPolicy("specializzazioni.delete", policy => policy.RequireClaim("specializzazioni.delete", "specializzazioni.delete"));
                options.AddPolicy("specializzazioni.edit", policy => policy.RequireClaim("specializzazioni.edit", "specializzazioni.edit"));
                options.AddPolicy("specializzazioni.create", policy => policy.RequireClaim("specializzazioni.create", "specializzazioni.create"));


                options.AddPolicy("resourcerequest.view", policy => policy.RequireClaim("resourcerequest.view", "resourcerequest.view"));
                options.AddPolicy("resourcerequest.delete", policy => policy.RequireClaim("resourcerequest.delete", "resourcerequest.delete"));
                options.AddPolicy("resourcerequest.edit", policy => policy.RequireClaim("resourcerequest.edit", "resourcerequest.edit"));
                options.AddPolicy("resourcerequest.create", policy => policy.RequireClaim("resourcerequest.create", "resourcerequest.create"));



                options.AddPolicy("Vettori.View.Stradali", policy => policy.RequireClaim("Vettori.View.Stradali", "Vettori.View.Stradali"));
                options.AddPolicy("Vettori.View.Industriali", policy => policy.RequireClaim("Vettori.View.Industriali", "Vettori.View.Industriali"));
                options.AddPolicy("Vettori.View.Attrezzature", policy => policy.RequireClaim("Vettori.View.Attrezzature", "Vettori.View.Attrezzature"));
                options.AddPolicy("Vettori.View", policy => policy.RequireClaim("Vettori.View", "Vettori.View"));
                options.AddPolicy("VettoreUser.Show", policy => policy.RequireClaim("VettoreUser.Show", "VettoreUser.Show"));
                options.AddPolicy("ResourceRequest.Assignment", policy => policy.RequireClaim("ResourceRequest.Assignment", "ResourceRequest.Assignment"));
                options.AddPolicy("anagrafica.vettore", policy => policy.RequireClaim("anagrafica.vettore", "anagrafica.vettore"));

                options.AddPolicy("DDT.Manage", policy => policy.RequireClaim("DDT.Manage", "DDT.Manage"));

                options.AddPolicy("TimesheetDailyReport.Approvazione", policy => policy.RequireClaim("TimesheetDailyReport.Approvazione", "TimesheetDailyReport.Approvazione"));

                options.AddPolicy("Comunicazioni.View", policy => policy.RequireClaim("Comunicazioni.View", "Comunicazioni.View"));
                options.AddPolicy("Comunicazioni.Create", policy => policy.RequireClaim("Comunicazioni.Create", "Comunicazioni.Create"));
                options.AddPolicy("Scadenziario.Show", policy => policy.RequireClaim("Scadenziario.Show", "Scadenziario.Show"));
                
                
                options.AddPolicy("report.giornaliero", policy => policy.RequireClaim("report.giornaliero", "report.giornaliero"));
                options.AddPolicy("report.mensile", policy => policy.RequireClaim("report.mensile", "report.mensile"));
                options.AddPolicy("report.timesheet", policy => policy.RequireClaim("report.timesheet", "report.timesheet"));
            });
            //services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext _context)
        {


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "NONE");
                await next();
            });


            app.UseSession();


#if DEBUG && LIVE_RELOAD
            app.UseLiveReload();
#endif

            if (true || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                    // Localization: Notice that neutral cultures (like 'es') are
                    //               listed after specific cultures. This best practice
                    //               ensures that if a particular culture request could
                    //               be satisifed by either a supported specific culture
                    //               or a supported neutral culture, the specific culture
                    //               will be preferred.
                    new CultureInfo("en-US"),
                    new CultureInfo("it"),
              };
            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("it-IT"),

                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,

                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(requestLocalizationOptions);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}");
            });

            var ukpath = Configuration.GetSection("loocPath:cloud").Value.ToString();
            if (UK_PATH_TYPE.Equals("LOCAL"))
            {
                ukpath = Configuration.GetSection("loocPath:local").Value.ToString(); ;
            }
            else if (UK_PATH_TYPE.Equals("DEV"))
            {
                ukpath = "";
            }
            else if (UK_PATH_TYPE.Equals("TEST"))
            {
                ukpath = Configuration.GetSection("loocPath:test").Value.ToString();
            }
            else if (UK_PATH_TYPE.Equals("PRE"))
            {
                ukpath = Configuration.GetSection("loocPath:pre").Value.ToString();
            }


            try
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                    ukpath),
                    RequestPath = ukpath
                });
            }
            catch (Exception e)
            {

            }





        }
    }
}