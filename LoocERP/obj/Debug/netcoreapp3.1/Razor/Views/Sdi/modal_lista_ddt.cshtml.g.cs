#pragma checksum "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/modal_lista_ddt.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8941b834a9ca0d7cf9b8bcc29d3d6df333c9cf95"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sdi_modal_lista_ddt), @"mvc.1.0.view", @"/Views/Sdi/modal_lista_ddt.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8941b834a9ca0d7cf9b8bcc29d3d6df333c9cf95", @"/Views/Sdi/modal_lista_ddt.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0e64de071199ae55fa1bfd96ad5ab02ba5443b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Sdi_modal_lista_ddt : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LoocERP.ViewModels.Sdi.FatturaViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<div class=""modal fade"" id=""modal_lista_ddt"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalCenterTitle"" aria-hidden=""true"">
  <div class=""modal-dialog modal-dialog-centered modal-xl  modal-dialog-scrollable"" role=""document"">
    <div class=""modal-content"">
      <div class=""modal-header"">
        <h5 class=""modal-title"" id=""exampleModalLongTitle"">DDT emessi </h5>
        <p>");
#nullable restore
#line 9 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/modal_lista_ddt.cshtml"
      Write(Model.fattura.IDCompany);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n        <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">\n          <span aria-hidden=\"true\">&times;</span>\n        </button>\n      </div>\n      <div class=\"modal-body\">\n                        ");
#nullable restore
#line 15 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/modal_lista_ddt.cshtml"
                    Write( Html.DevExtreme().DataGrid<LoocERP.Models.Ddt>()
                            .ID("DdtContainer")
                            .ShowBorders(true)
                            .Selection(s => s.Mode(SelectionMode.Multiple)
                                              .ShowCheckBoxesMode(GridSelectionShowCheckBoxesMode.Always))
                            //.OnSelectionChanged("selection_changed")                                              
                            .AllowColumnReordering(true)
                            .AllowColumnResizing(true)
                            .ColumnAutoWidth(true)
                            .HoverStateEnabled(true)
                            .Paging(paging => paging.PageSize(10))
                            .Pager(pager => {
                                pager.Visible(true);
                                pager.DisplayMode(GridPagerDisplayMode.Full);
                                pager.ShowPageSizeSelector(true);
                                pager.AllowedPageSizes(new JS("[5, 10, 'all']"));
                                pager.ShowInfo(true);
                                pager.ShowNavigationButtons(true);
                            })
                            .FilterRow(filterRow => filterRow
                                .Visible(true)
                                .ApplyFilter(GridApplyFilterMode.Auto)
                            )
                            .DataSource(ds => ds.Mvc()
                                .Controller("sdi")
                                .LoadAction("getDdtByFornitore")
                                // .LoadParams(new { idFornitore = @Model.fattura.IDCompany }) *@
                                .LoadParams(new { idFornitore = "FAB53D66-34D0-4661-C4EB-08D8A1D577F8" })
                                .Key("Id")
                            )
                            .RemoteOperations(true)
                            .Columns( columns => {
                                    columns.Add().DataField("NumeroProgressivoDocumento").Caption("Num. DDT").Width("10%");
                                    columns.Add().DataField("DataDDT")
                                            .Caption("Data emissione")
                                            .Format("dd/M/yyyy")
                                            .Alignment(HorizontalAlignment.Left)
                                            .DataType(GridColumnDataType.Date)
                                            ;
                                }
                            )
                            .MasterDetail(md => md
                                            .Enabled(true)
                                            .Template(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("\n                                                <div class=\"master-detail-caption bg-blue rounded\"><span class=\"ml-2 font-weight-bolder\">Dettaglio DDT:</span></div>\n                                                ");
#nullable restore
#line 60 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Sdi/modal_lista_ddt.cshtml"
                                            Write(Html.DevExtreme().DataGrid<LoocERP.Models.DdtDettaglio>()
                                                    .DataSource(d => d.Mvc()
                                                        .Controller("sdi")
                                                        .LoadAction("DdtDetails")
                                                        .LoadParams(new { DdtId = new JS("data.Id") })
                                                    )
                                                    .ShowBorders(true)
                                                    .Columns( columns => {
                                                                columns.AddFor(m => m.Codice).Caption("Cod. articolo");
                                                                columns.AddFor(m => m.Descrizione).Caption("Descrizione");
                                                            }

                                                    )
                                                );

#line default
#line hidden
#nullable disable
    WriteLiteral("\n\n                                            ");
    PopWriter();
}
))
                            )

                        );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            
      </div>
      <div class=""modal-footer"">
        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Chiudi</button>
        <button type=""button"" class=""btn btn-primary"" id=""btn-associaddt"">Associa</button>
      </div>
    </div>
  </div>
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoocERP.ViewModels.Sdi.FatturaViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
