#pragma checksum "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_edit_noleggioleasing.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "413e8016d2d888f93a404eeef62e877fe1549f01"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vettore__edit_noleggioleasing), @"mvc.1.0.view", @"/Views/Vettore/_edit_noleggioleasing.cshtml")]
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
#line 2 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/_ViewImports.cshtml"
using LoocERP.Models;

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
#line 1 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_edit_noleggioleasing.cshtml"
using LoocERP.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"413e8016d2d888f93a404eeef62e877fe1549f01", @"/Views/Vettore/_edit_noleggioleasing.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0e64de071199ae55fa1bfd96ad5ab02ba5443b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Vettore__edit_noleggioleasing : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("assignNoleggioLeasingForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""modalproperyedit"" role=""dialog"">
    <div class=""modal-dialog"">
        <!-- Modal content-->
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Associazione noleggio/Leasing</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "413e8016d2d888f93a404eeef62e877fe1549f015168", async() => {
                WriteLiteral(@"
                    <input class=""CodiceVettore"" type=""hidden"" name=""IdVettore"" />
                    <div class=""form-group"">
                        <label>Tipologia</label>
                        <select class=""form-control"" onchange=""changeProperty(this)"" id=""propertyType"">
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "413e8016d2d888f93a404eeef62e877fe1549f015745", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "413e8016d2d888f93a404eeef62e877fe1549f016727", async() => {
                    WriteLiteral("Noleggio");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "413e8016d2d888f93a404eeef62e877fe1549f017962", async() => {
                    WriteLiteral("Leasing");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                        </select>
                    </div>
                    <div class=""form-group"" id=""noleggioSelect"">
                        <label>Numero protocollo</label>
                        <div class=""form-group"">
                            <select style=""width:100%"" class=""select2js form-control"" select2-url=""/Noleggi/ajaxNoleggiList"" select2-placeholder=""Noleggio"" name=""IdNoleggio"" id=""IdNoleggio""></select>
                        </div>
                    </div>
                    <div class=""form-group"" id=""leasingSelect"">
                        <label>Numero protocollo</label>
                        <div class=""form-group"">
                            <select style=""width:100%"" class=""select2js form-control"" select2-url=""/Leasing/ajaxLeasingList"" select2-placeholder=""Leasing"" name=""IdLeasing"" id=""IdLeasing""></select>
                        </div>
                    </div>

                    <button type=""submit"" class=""btn btn-primary"">Aggiorna</button>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n        </div>\n    </div>\n</div>\n\n");
#nullable restore
#line 41 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_edit_noleggioleasing.cshtml"
 using (Html.BeginScripts())
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <script>
        function changeProperty(value) {
            if ($(value).val() == ""1"") {
                $(""#noleggioSelect"").hide();
                $(""#leasingSelect"").show();
            }
            if ($(value).val() == ""0"") {
                $(""#leasingSelect"").hide();
                $(""#noleggioSelect"").show();
            }
        }
        function openModalProperty(CodiceVettore) {
            var empModal = $('#modalproperyedit');

            $(""#noleggioSelect"").hide();
            $(""#leasingSelect"").hide();
            $(""#propertyType"").val("""");

            $(""#IdLeasing"").attr(""title"", """");
            $(""#IdLeasing"").val("""");
            $(""#IdNoleggio"").attr(""title"","""");
            $(""#IdNoleggio"").val("""").trigger(""change""); 

            empModal.find("".CodiceVettore"").val(CodiceVettore);
            empModal.modal('show');
        }

        function removeFromLeasing(id) {
            console.log(id);
            $.ajax({
                url: '/Leasing/ajaxRemoveVettore?idAsse");
            WriteLiteral(@"gnazione='+id,
                type: 'get',
                success: function (data) {
                    reloadTable();
                }
            });
        }
        
        function removeFromNoleggio(id) {

            console.log(id);
            $.ajax({
                url: '/Noleggi/ajaxRemoveVettore?idAssegnazione=' + id,
                type: 'get',
                success: function (data) {
                    reloadTable();
                }
            });
        }


        $(document).ready(function () {

            $('#assignNoleggioLeasingForm').on(""submit"", function (e) {
                e.preventDefault(); // cancel the actual submit
                var elements = $(this).serializeArray();
                var data = [];



                console.log(elements);




                jQuery.each(elements, function (i, elem) {
                    data[elem.name] = elem.value;
                });
                if (data[""propertyType""] = ""1"") {
                    $.ajax({
            ");
            WriteLiteral(@"            url: '/Leasing/ajaxAddVettore',
                        type: 'post',
                        data: { IdVettore: data[""IdVettore""], IdLeasing: data[""IdLeasing""] },
                        success: function (data) {
                            console.log(data.data);

                            // Add response in Modal body
                            //Svuoto modale
                            var empModal = $('#modalproperyedit');
                            $('#modalproperyedit').modal('hide');


                            reloadTable();
                        }
                    });
                }
                if (data[""propertyType""] = ""2"") {
                    $.ajax({
                        url: '/Noleggi/ajaxAddVettore',
                        type: 'post',
                        data: { IdVettore: data[""IdVettore""], IdNoleggio: data[""IdNoleggio""] },
                        success: function (data) {
                            console.log(data.data);

                       ");
            WriteLiteral(@"     var empModal = $('#modalproperyedit');
                            $('#modalproperyedit').modal('hide');

                            reloadTable();
                        }
                    });
                }

            });


        });
       
       


    </script>
");
#nullable restore
#line 154 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_edit_noleggioleasing.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
