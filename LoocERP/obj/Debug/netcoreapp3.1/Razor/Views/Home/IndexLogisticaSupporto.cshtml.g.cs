#pragma checksum "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "615ea4fb86bdf39ce4b7147672502fe747538a57"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_IndexLogisticaSupporto), @"mvc.1.0.view", @"/Views/Home/IndexLogisticaSupporto.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/_ViewImports.cshtml"
using LoocERP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
using LoocERP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"615ea4fb86bdf39ce4b7147672502fe747538a57", @"/Views/Home/IndexLogisticaSupporto.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0e64de071199ae55fa1bfd96ad5ab02ba5443b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_IndexLogisticaSupporto : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LoocERP.ViewModels.RoleClaimsViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/theme/plugins/jsgrid/jsgrid.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/theme/plugins/jsgrid/jsgrid-theme.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/theme/plugins/tabulator/css/semantic-ui/tabulator_semantic-ui.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "IndexGestioneMovimento", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color: inherit;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "IndexManutenzione", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 11 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
  
    AppUser layout_user = new AppUser();
    try
    {
        layout_user = @UserManager.GetUserAsync(User).Result;
    }
    catch (Exception)
    {
        layout_user = new AppUser();
    }
    //.MultiTenantId

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
  
    ViewData["Title"] = "";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "615ea4fb86bdf39ce4b7147672502fe747538a577752", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "615ea4fb86bdf39ce4b7147672502fe747538a578850", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "615ea4fb86bdf39ce4b7147672502fe747538a579948", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"


<!--Export stuff-->
<script type=""text/javascript"" src=""https://oss.sheetjs.com/sheetjs/xlsx.full.min.js""></script>


<section class=""content-header"">
    <div class=""container-fluid"">
        <div class=""row mb-2"">
            <div class=""col-sm-6 c-white"">
                <h1>");
#nullable restore
#line 41 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
            </div>
            <!--div class=""col-sm-6"">
                <ol class=""breadcrumb float-sm-right"">
                    <li class=""breadcrumb-item""><a asp-controller=""Home"" asp-action=""Index"">Home</a></li>
                    <li class=""breadcrumb-item active"">HR</li>
                </ol>
            </!-div-->
        </div>
    </div><!-- /.container-fluid -->
</section>
<section class=""content"">
    <div class=""row"">
");
#nullable restore
#line 54 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
         if ((await AuthorizationService.AuthorizeAsync(User, "GestioneMovimento.Show")).Succeeded)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-3 col-sm-6 col-12\">\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "615ea4fb86bdf39ce4b7147672502fe747538a5712428", async() => {
                WriteLiteral(@"
                    <div class=""info-box"">
                        <span class=""info-box-icon bg-info"">GM</span>
                        <div class=""info-box-content"">
                            <span class=""info-box-text"">Gestione movimento</span>
                            <small class=""info-box-number"">Area gestionale dedicata alla movimentazione della flotta</small>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                <!-- /.info-box -->\n            </div>\n");
#nullable restore
#line 69 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
         if ((await AuthorizationService.AuthorizeAsync(User, "TracciamentoFlotta.Show")).Succeeded)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-md-3 col-sm-6 col-12"">
                <a href=""https://gsh27.net/id18/"" target=""_blank"" style=""color: inherit;"">
                    <div class=""info-box"">
                        <span class=""info-box-icon bg-info"">TF</span>
                        <div class=""info-box-content"">
                            <span class=""info-box-text"">Tracciamento flotta</span>
                            <small class=""info-box-number"">Tracciamento Flotta</small>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                </a>
                <!-- /.info-box -->
            </div>
");
#nullable restore
#line 85 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 86 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
         if ((await AuthorizationService.AuthorizeAsync(User, "Magazzino.Show")).Succeeded)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-3 col-sm-6 col-12\">\n");
#nullable restore
#line 89 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
                 if (LoocERP.Controllers.Startup.CONNECTION_TYPE == "TEST")
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a target=\"_blank\"");
            BeginWriteAttribute("href", " href=\"", 3585, "\"", 3804, 6);
            WriteAttributeValue("", 3592, "http://test.looc.kresearch.it/PackageGovernance/ServiziAutenticazione/login.aspx?user=", 3592, 86, true);
#nullable restore
#line 91 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
WriteAttributeValue("", 3678, layout_user.LoocUsername, 3678, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3703, "&passwd=", 3703, 8, true);
#nullable restore
#line 91 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
WriteAttributeValue("", 3711, layout_user.LoocPassword, 3711, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3736, "&urlhr=/packageadvanced/GestioneMagazzinoHR.aspx&uid=", 3736, 53, true);
#nullable restore
#line 91 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
WriteAttributeValue("", 3789, layout_user.Id, 3789, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""color: inherit;"">
                        <div class=""info-box"">
                            <span class=""info-box-icon bg-info"">MAG</span>
                            <div class=""info-box-content"">
                                <span class=""info-box-text"">Gestione Magazzino</span>
                                <small class=""info-box-number"">Area dedicata alla gestione del magazzino</small>
                            </div>
                            <!-- /.info-box-content -->
                        </div>
                    </a>
");
#nullable restore
#line 101 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <a target=\"_blank\"");
            BeginWriteAttribute("href", " href=\"", 4453, "\"", 4667, 6);
            WriteAttributeValue("", 4460, "http://looc.kresearch.it/PackageGovernance/ServiziAutenticazione/login.aspx?user=", 4460, 81, true);
#nullable restore
#line 104 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
WriteAttributeValue("", 4541, layout_user.LoocUsername, 4541, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4566, "&passwd=", 4566, 8, true);
#nullable restore
#line 104 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
WriteAttributeValue("", 4574, layout_user.LoocPassword, 4574, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4599, "&urlhr=/packageadvanced/GestioneMagazzinoHR.aspx&uid=", 4599, 53, true);
#nullable restore
#line 104 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
WriteAttributeValue("", 4652, layout_user.Id, 4652, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""color: inherit;"">
                        <div class=""info-box"">
                            <span class=""info-box-icon bg-info"">MAG</span>
                            <div class=""info-box-content"">
                                <span class=""info-box-text"">Gestione Magazzino</span>
                                <small class=""info-box-number"">Area dedicata alla gestione del magazzino</small>
                            </div>
                            <!-- /.info-box-content -->
                        </div>
                    </a>
");
#nullable restore
#line 114 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <!-- /.info-box -->\n            </div>\n");
#nullable restore
#line 117 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 119 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
         if ((await AuthorizationService.AuthorizeAsync(User, "PianiDiManutenzione.Show")).Succeeded)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-3 col-sm-6 col-12\">\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "615ea4fb86bdf39ce4b7147672502fe747538a5721210", async() => {
                WriteLiteral(@"
                    <div class=""info-box"">
                        <span class=""info-box-icon bg-info"">MT</span>
                        <div class=""info-box-content"">
                            <span class=""info-box-text"">Manutenzione</span>
                            <small class=""info-box-number"">Area dedicata alla manutenzione</small>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                <!-- /.info-box -->\n            </div>\n");
#nullable restore
#line 134 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Home/IndexLogisticaSupporto.cshtml"
        }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n\n</section>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IAuthorizationService AuthorizationService { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Http.IHttpContextAccessor IHttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AppUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<AppUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LoocERP.ViewModels.RoleClaimsViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
