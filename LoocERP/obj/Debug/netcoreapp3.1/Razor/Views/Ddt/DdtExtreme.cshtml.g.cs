#pragma checksum "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Ddt/DdtExtreme.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07b572adf3d56741eb205d850ef8906210491a9b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ddt_DdtExtreme), @"mvc.1.0.view", @"/Views/Ddt/DdtExtreme.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07b572adf3d56741eb205d850ef8906210491a9b", @"/Views/Ddt/DdtExtreme.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0e64de071199ae55fa1bfd96ad5ab02ba5443b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Ddt_DdtExtreme : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 1 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Ddt/DdtExtreme.cshtml"
  
    ViewData["Title"] = "DDT emessi";
//     Layout = "_DevExtremeLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "07b572adf3d56741eb205d850ef8906210491a9b4991", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "07b572adf3d56741eb205d850ef8906210491a9b6089", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "07b572adf3d56741eb205d850ef8906210491a9b7187", async() => {
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



<div>&nbsp;</div>
<section class=""content"">
    <div class=""row"">
        <div class=""col-12"">
            <div class=""card card-primary"">
                <!-- /.card-header -->
                <div class=""card-body"">
                    <div class=""col-12"">

                        ");
#nullable restore
#line 25 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Ddt/DdtExtreme.cshtml"
                    Write( Html.DevExtreme().DataGrid<LoocERP.Models.Ddt>()
                            .Selection(s => s.Mode(SelectionMode.Multiple).ShowCheckBoxesMode(GridSelectionShowCheckBoxesMode.Always))
                            .ID("gridContainer")
                            .ShowBorders(true)
                            //.HeaderFilter(headerFilter => headerFilter.Visible(true))
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
                            .SearchPanel(searchPanel => searchPanel
                                .Visible(true)
                                .Width(240)
                                .Placeholder("Cerca...")
                            )
                            .DataSource(ds => ds.Mvc()
                                .Controller("Ddt")
                                .LoadAction("Get")
                                //.DeleteAction("Delete")
                                //.UpdateAction("Put") 
                                .Key("Id")
                            )
                            .RemoteOperations(true)
                            .Columns(columns => {

                                columns.AddFor(m => m.DataDDT).Caption("Data emissione").Format("d/M/yyyy");

                                columns.AddFor(m => m.NumeroProgressivoDocumento).Caption("# Doc.");

                                columns.Add()
                                        .Caption("Tipologia")
                                        .Width(230)
                                        .CalculateCellValue(
                                            item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(@"
                                                function(data){
                                                    if (data.Suffisso == null) return '-';
                                                    if (data.Suffisso == 'S') return "" DDT di scarico"";
                                                    if (data.Suffisso == 'T') return "" DDT di trasferimento"";
                                                }
                               ");
    PopWriter();
}
));
                                        

                                        
                                columns.AddFor(m => m.Mittente.RagioneSociale);

                                columns.AddFor(m => m.NomeDestinatario).HeaderFilter(hf => hf.AllowSearch(false));

                                columns.AddFor(m => m.DataExpDatev).Caption("Data ultima esportazione");

                            })

                        );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"



                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<style>
    
    #gridContainer .dx-datagrid-headers .dx-header-row {
        background-color: rgb(154 201 255 / 60%);
    }
       
    .dx-header-row {
        color: white;
    }

    #gridContainer .dx-datagrid-headers .dx-header-row {
        background-color: #507CD1;
    }

    #gridContainer .dx-datagrid-header-panel {
        padding: 0;
        background-color: rgb(154 201 255 / 60%);
        border-radius: 5px 5px 0px 0px;
    }

    #gridContainer .dx-datagrid-header-panel .dx-toolbar {
        margin: 0;
        padding-right: 20px;
        background-color: transparent;
    }

    #gridContainer .dx-datagrid-header-panel .dx-toolbar-items-container {
        height: 70px;
    }

    #gridContainer .dx-datagrid-header-panel .dx-toolbar-before .dx-toolbar-item:not(:first-child) {
        background-color: rgba(103, 171, 255, 0.6);
    }

    #gridContainer .dx-datagrid-header-panel .dx-t");
            WriteLiteral(@"oolbar-before .dx-toolbar-item:last-child {
        padding-right: 10px;
    }

    #gridContainer .dx-datagrid-header-panel .dx-selectbox {
        margin: 17px 10px;
    }

    #gridContainer .dx-datagrid-header-panel .dx-button {
        margin: 17px 0;
    }

    #gridContainer .informer {
        height: 70px;
        width: 130px;
        text-align: center;
        color: #fff;
    }

    #gridContainer .count {
        padding-top: 15px;
        line-height: 27px;
        font-size: 28px;
        margin: 0;
        font-weight: normal;
        font-family: ""Helvetica Neue"", ""Segoe UI"", Helvetica, Verdana, sans-serif;
    }

    .form-container {
        padding: 20px;
    }

    ​
    .address-form label {
        font-weight: bold;
    }
</style>
");
            DefineSection("Scripts", async() => {
                WriteLiteral(@" 
    <script>
        var prevGrid = null;
        function selection_changed(e) {
            /*
            if (prevGrid != null) {
                var key = prevGrid;
                var treeList = $(""#gridContainer"").dxDataGrid(""instance"");
                treeList.collapseRow(key);
            }
            */
            

            if (prevGrid != null ) {
                var key = prevGrid;
                var treeList = $(""#gridContainer"").dxDataGrid(""instance"");
                treeList.collapseRow(key);
            }
            
            var key = e.selectedRowKeys[0];
            var treeList = $(""#gridContainer"").dxDataGrid(""instance"");

           
            if (treeList.isRowExpanded(key)) {
                treeList.collapseRow(key);
            } else {
                treeList.expandRow(key);
            }
            prevGrid = key;
            //$('#mybutton').trigger('click');
        }

    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
