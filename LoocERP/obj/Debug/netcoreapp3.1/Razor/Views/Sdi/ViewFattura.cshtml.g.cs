#pragma checksum "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec49a58eb41581d462066d016e07758e84aba705"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sdi_ViewFattura), @"mvc.1.0.view", @"/Views/Sdi/ViewFattura.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec49a58eb41581d462066d016e07758e84aba705", @"/Views/Sdi/ViewFattura.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0e64de071199ae55fa1bfd96ad5ab02ba5443b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Sdi_ViewFattura : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FatturaElettronica.Ordinaria.FatturaOrdinaria>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/theme/plugins/jsgrid/jsgrid.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/theme/plugins/jsgrid/jsgrid-theme.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/theme/plugins/tabulator/css/semantic-ui/tabulator_semantic-ui.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
    ViewData["Title"] = "Fatture ricevute";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ec49a58eb41581d462066d016e07758e84aba7055001", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ec49a58eb41581d462066d016e07758e84aba7056099", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ec49a58eb41581d462066d016e07758e84aba7057197", async() => {
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
<style xmlns:p=""http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2"" type=""text/css""></style>


<!--Export stuff-->
<script type=""text/javascript"" src=""https://oss.sheetjs.com/sheetjs/xlsx.full.min.js""></script>

<div>&nbsp;</div>

<section class=""content"">
<div class=""header header-fattura noprint"">
    <div class=""mt-4 mb-3"">
        <div class=""send-buttons"">
            <div class=""btn-group"" role=""group"" aria-label=""Azioni"">
                <a class=""btn btn-default p-2 historyBack"" href=""/Sdi/FattureExtreme"">
                    <i class=""fa fa-chevron-left""></i> TORNA ALL'ELENCO
                </a>

            </div>
");
            WriteLiteral(@"            <div class=""float-right mb-3"">
                <a class=""btn btn-outline-primary ripple-surface ripple-surface-dark p-2 text-dark printInvoice"" href=""#"" onclick=""window:print();return false;"">
                    <i class=""fas fa-print""></i>
                </a>
                <a download=""Fattura-123.pdf"" class=""btn btn-outline-primary ripple-surface ripple-surface-dark p-2""");
            BeginWriteAttribute("href", " href=\"", 1876, "\"", 1919, 2);
            WriteAttributeValue("", 1883, "/Sdi/getPdf?idPdf=", 1883, 18, true);
#nullable restore
#line 39 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
WriteAttributeValue("", 1901, ViewBag.idFattura, 1901, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    <i class=""fas fa-file-pdf text-red"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Scarica PDF"" data-original-title=""Scarica PDF""></i>
                </a>
                <a class=""btn btn-outline-primary ripple-surface ripple-surface-dark p-2 viewSdi""");
            BeginWriteAttribute("href", " href=\"", 2201, "\"", 2254, 2);
            WriteAttributeValue("", 2208, "/api/FattureApi/download?id=", 2208, 28, true);
#nullable restore
#line 42 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
WriteAttributeValue("", 2236, ViewBag.idFattura, 2236, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" target=""_blank"">
                    <i class=""far fa-file-code"" data-toggle=""tooltip"" data-placement=""bottom"" title=""Scarica XML"" data-original-title=""Scarica XML""></i>
                </a>
                
            </div>
        </div>
    </div>
</div>
");
#nullable restore
#line 50 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
 foreach (var doc in @Model.FatturaElettronicaBody)
{
    
    var datiDocumento = doc.DatiGenerali.DatiGeneraliDocumento;    
    var mittente = @Model.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.Anagrafica.Denominazione ?? @Model.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.Anagrafica.CognomeNome;
    var destinatario = @Model.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.Anagrafica.Denominazione ?? @Model.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.Anagrafica.CognomeNome;


#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container-fluid template-fattura my-5 mx-2 mx-md-auto"" id=""invoicePage"">
    <div xmlns:p=""http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2"" id=""container"">
        <div class=""testataFattura"">
            <div class=""intestazione"">
                    <div class=""titolo-documento"">Fattura ");
#nullable restore
#line 61 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                     Write(doc.DatiGenerali.DatiGeneraliDocumento.Numero);

#line default
#line hidden
#nullable disable
            WriteLiteral(" del  ");
#nullable restore
#line 61 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                                         Write(datiDocumento.Data.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        <hr>
                    </div>
            </div>
        </div>
    </div>
    
    <div class=""separa""><p></p></div>
    <div class=""row"">
            <div class=""box mittente col-12 col-md-6"">
                    <div class=""info"">");
#nullable restore
#line 71 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                 Write(mittente);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\n                            <p class=\"info\">");
#nullable restore
#line 72 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                       Write(Model.FatturaElettronicaHeader.CedentePrestatore.Sede.Indirizzo);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 72 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                                          Write(Model.FatturaElettronicaHeader.CedentePrestatore.Sede.CAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(" -  ");
#nullable restore
#line 72 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                                                                                                        Write(Model.FatturaElettronicaHeader.CedentePrestatore.Sede.Comune);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (");
#nullable restore
#line 72 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                                                                                                                                                                       Write(Model.FatturaElettronicaHeader.CedentePrestatore.Sede.Provincia);

#line default
#line hidden
#nullable disable
            WriteLiteral(")\n                            </p>\n                            <span>Partita IVA:<span>");
#nullable restore
#line 74 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                               Write(Model.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.IdFiscaleIVA.IdCodice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></span><br>\n                            ");
#nullable restore
#line 75 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                       Write(await Component.InvokeAsync("RegimeFiscale", new{codiceRegime = @Model.FatturaElettronicaHeader.CedentePrestatore.DatiAnagrafici.RegimeFiscale}));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                           
                    </div>
                </div>
            </div>
    <div class=""row"">
        <div class=""cessionario col-12 col-md-6 offset-md-6"">
            <div class=""info text-right mt-3 mt-md-0 pr-5"">
                <strong>Spett.
                    <br class=""noprint"">
                    </strong>");
#nullable restore
#line 85 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                        Write(destinatario);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\n                    ");
#nullable restore
#line 86 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
               Write(Model.FatturaElettronicaHeader.CessionarioCommittente.Sede.Indirizzo);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 86 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                       Write(Model.FatturaElettronicaHeader.CessionarioCommittente.Sede.CAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(" -  ");
#nullable restore
#line 86 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                                                                                          Write(Model.FatturaElettronicaHeader.CessionarioCommittente.Sede.Comune);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (");
#nullable restore
#line 86 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                                                                                                                                                                                              Write(Model.FatturaElettronicaHeader.CessionarioCommittente.Sede.Provincia);

#line default
#line hidden
#nullable disable
            WriteLiteral(")\n                    <div>Codice Fiscale:  ");
#nullable restore
#line 87 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                     Write(Model.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.CodiceFiscale);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\n                    <div>Codice Destinatario:    ");
#nullable restore
#line 88 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                            Write(Model.FatturaElettronicaHeader.CessionarioCommittente.DatiAnagrafici.IdFiscaleIVA.IdCodice);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
            </div>
        </div>
    </div>        

        <div class=""separa""><p></p></div>
     <div class=""elementoLotto"">
        <div class=""intestazioneElemLotto"">
            <div class=""intestazione"">
            <div class=""titolo-documento""></div>
            </div>
            <h2 class=""title-left smaller2"">Causale: ");
#nullable restore
#line 99 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                        
                                                        var causale = String.Join("-",@Model.FatturaElettronicaBody[0].DatiGenerali.DatiGeneraliDocumento.Causale);
                                                        

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 101 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                                     Write(causale);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </h2>
        </div>
        <div class=""dettagli"">
            <div class=""separa"">
                <p></p>
            </div>
            <table class=""tableDettagli"">
            <thead>
                <tr>
                <th style=""width: 25%;"">Descrizione del prodotto/servizio</th>
                <th>Importo unitario</th>
                <th style=""width: 75px"">Quantità</th>
                <th>Importo totale</th>
                <th>IVA%</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 118 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                 foreach (var linea in @Model.FatturaElettronicaBody[0].DatiBeniServizi.DettaglioLinee)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td class=\"text-left\">");
#nullable restore
#line 121 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                         Write(linea.Descrizione);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 122 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                       Write(linea.PrezzoUnitario);

#line default
#line hidden
#nullable disable
            WriteLiteral(" €</td>\n                        <td>");
#nullable restore
#line 123 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                       Write(linea.Quantita);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 124 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                       Write(linea.PrezzoTotale);

#line default
#line hidden
#nullable disable
            WriteLiteral(" €</td>\n                        <td>");
#nullable restore
#line 125 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                       Write(linea.AliquotaIVA);

#line default
#line hidden
#nullable disable
            WriteLiteral(" %</td>\n                    </tr>\n");
#nullable restore
#line 127 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
            <tfoot></tfoot>
            </table>
        </div>
        <div class=""separa"">
            <p></p>
        </div>
        <div class=""riassunto"">
            <div class=""datiGeneraliRaggrupati"">
            <div class=""separa"">
                <p></p>
            </div>
");
#nullable restore
#line 140 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
             if  ( (@Model.FatturaElettronicaBody[0].DatiGenerali.DatiOrdineAcquisto != null ) &&
                   (@Model.FatturaElettronicaBody[0].DatiGenerali.DatiOrdineAcquisto.Any() ) ) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                   <table class=""tableDettagli"">
                    <caption>Dati generali</caption>

                    <thead>
                    <tr>
                        <th>Tipologia</th>
                        <th>Nr. dettaglio doc.</th>
                        <th>Documento</th>
                        <th>Nr. linea riferita</th>
                        <th>CIG</th>
                    </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 155 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                     foreach (var item in @Model.FatturaElettronicaBody[0].DatiGenerali.DatiOrdineAcquisto)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td class=\"text-left\">\n                            <strong>Ordine di acquisto</strong>\n                            </td>\n                            <td class=\"text-left\">");
#nullable restore
#line 161 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(item.RiferimentoNumeroLinea);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td class=\"text-left\">");
#nullable restore
#line 162 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(item.IdDocumento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 163 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                           Write(item.NumItem);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td class=\"text-left\">");
#nullable restore
#line 164 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(item.CodiceCIG);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        </tr>\n");
#nullable restore
#line 166 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\n                    <tfoot></tfoot>\n                </table>\n");
#nullable restore
#line 170 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"separa\">\n                <p></p>\n            </div>\n");
#nullable restore
#line 174 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
             if (@Model.FatturaElettronicaBody[0].DatiGenerali.DatiDDT != null ) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""tableDettagli"">
                    <caption>Dati DDT</caption>

                    <thead>
                    <tr>
                        <th>Data</th>
                        <th>Nr. dettaglio doc.</th>
                        <th>Nr. linea riferita</th>
                    </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 186 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                     foreach (var ddt in @Model.FatturaElettronicaBody[0].DatiGenerali.DatiDDT)
                    {
                        var lineeRif = String.Join("-",ddt.RiferimentoNumeroLinea);

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td class=\"text-left\">");
#nullable restore
#line 190 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(ddt.DataDDT.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                            <td class=\"text-left\">");
#nullable restore
#line 191 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(ddt.NumeroDDT);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td class=\"text-left\">");
#nullable restore
#line 192 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(lineeRif);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        </tr>\n");
#nullable restore
#line 194 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\n                    <tfoot></tfoot>\n                </table>\n");
#nullable restore
#line 198 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
            }            

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
        <div class=""riepilogo-aliquote-nature"">
            <br>
            <table class=""tableDettagli"">
            <caption>Dati riepilogo IVA</caption>
            <thead>
                <tr>
                <th>IVA</th>
                <th>Imponibile/Importo </th>
                <th>Imposta </th>
                <th>Esigibilità</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 214 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                 foreach (var iva in @Model.FatturaElettronicaBody[0].DatiBeniServizi.DatiRiepilogo)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td class=\"text-right\">");
#nullable restore
#line 217 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                          Write(iva.AliquotaIVA);

#line default
#line hidden
#nullable disable
            WriteLiteral(" % </td>\n                        <td class=\"text-right\">");
#nullable restore
#line 218 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                          Write(iva.ImponibileImporto);

#line default
#line hidden
#nullable disable
            WriteLiteral(" €</td>\n                        <td class=\"text-right\">");
#nullable restore
#line 219 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                          Write(iva.Imposta);

#line default
#line hidden
#nullable disable
            WriteLiteral(" €</td>\n                        <td class=\"text-right\">");
#nullable restore
#line 220 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                          Write(await Component.InvokeAsync("Esigibilita", new{codiceEsigibilita = @iva.EsigibilitaIVA}));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                </tr>\n");
#nullable restore
#line 222 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
            <tfoot></tfoot>
            </table>
            <div class=""separa"">
            <p></p>
            </div>
        </div>
        <div class=""datiPagamentoCondizioni"">
            <div class=""separa"">
            <p></p>
            </div>
            <table class=""tableDettagli"">
            <caption>Modalità di pagamento</caption>
            <thead>
                <tr>
                <th>Modalità</th>
                <th>Importo</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 243 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                 foreach (var pag in @Model.FatturaElettronicaBody[0].DatiPagamento)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 245 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                     foreach (var dett in pag.DettaglioPagamento)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td class=\"text-left\">");
#nullable restore
#line 248 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                             Write(await Component.InvokeAsync("ModPagamento", new{codiceModPagamento = @dett.ModalitaPagamento}));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td class=\"text-right\">\n                                <span");
            BeginWriteAttribute("class", " class=\"", 11755, "\"", 11763, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 250 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                                          Write(dett.ImportoPagamento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\n                            </td>\n                        </tr>\n");
#nullable restore
#line 253 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 253 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
            <tfoot></tfoot>
            </table>
            <div class=""separa"">
            <p></p>
            </div>
            <div class=""separa"">
            <p></p>
            </div>
        </div>
        <table class=""tableDettagli"">
            <caption>Altri dati</caption>
            <thead>
            <tr>
                <th>Importo totale documento</th>
            </tr>
            </thead>
            <tbody>
            <tr>
                <td>");
#nullable restore
#line 274 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
               Write(Model.FatturaElettronicaBody[0].DatiGenerali.DatiGeneraliDocumento.ImportoTotaleDocumento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            </tr>\n            </tbody>\n            <tfoot></tfoot>\n        </table>\n        </div>\n\n</div> \n");
#nullable restore
#line 282 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/ViewFattura.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    <script>\n        $(document).ready(function () {\n\n            \n        });\n    </script>\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FatturaElettronica.Ordinaria.FatturaOrdinaria> Html { get; private set; }
    }
}
#pragma warning restore 1591
