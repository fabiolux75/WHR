#pragma checksum "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ff55cfc8eaf1e21dcfeeaaf68a9779bbddba737"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vettore__storicoVettore), @"mvc.1.0.view", @"/Views/Vettore/_storicoVettore.cshtml")]
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
#line 1 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
using LoocERP.Controllers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ff55cfc8eaf1e21dcfeeaaf68a9779bbddba737", @"/Views/Vettore/_storicoVettore.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0e64de071199ae55fa1bfd96ad5ab02ba5443b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Vettore__storicoVettore : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
  
    var idModal = Guid.NewGuid();
    var idTable = Guid.NewGuid();

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n<div class=\"modal fade\"");
            BeginWriteAttribute("id", " id=\"", 125, "\"", 138, 1);
#nullable restore
#line 8 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
WriteAttributeValue("", 130, idModal, 130, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" role=""dialog"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title""></h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
            </div>
            <div class=""modal-body"">
                <div id=""tableZone""></div>
            </div>
        </div>
    </div>
</div>

");
#nullable restore
#line 22 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
 using (Html.BeginScripts())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<script>\n\n    function openVectorHistory(codiceVettore,type) {\n        var empModal = $(\'#");
#nullable restore
#line 27 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
                      Write(idModal);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        
        if (type == 0) {
            reloadTableVettoriUser(codiceVettore);
            empModal.modal('show');
        }
        if (type == 1) {
            reloadTableVettoriParking(codiceVettore);
            empModal.modal('show');
        }
        if (type == 2) {
            reloadTableVettoriCantieri(codiceVettore);
            empModal.modal('show');
        }

    }

    var dataTable,
        domTable,
        htmlTable = '<table id=""");
#nullable restore
#line 46 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
                           Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" class=""table table-bordered table-hover""><tbody></tbody></table>';


    function reloadTableVettoriUser(codice) {
        const myNode = document.getElementById(""tableZone"");
        myNode.textContent = '';

        if ($.fn.DataTable.fnIsDataTable(domTable)) {
            dataTable.fnDestroy(true);
            $('#tableZone').append(htmlTable);
        }
        $('#tableZone').append(htmlTable);


         dataTable = $('#");
#nullable restore
#line 60 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
                    Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"').DataTable({
                ajax: ""/Vettore/ajaxVettoriUserHistory?codiceVettore="" + codice,

                dom: 'Bifrtip',//dom: 'Bftp',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ],
             columns: [
                 {
                     title : ""Nome"",
                     ""data"": ""nome""
                 },
                 {
                     title: ""Associato dal"",
                     ""data"": ""inizio""
                 },
                 {
                     title: ""al"",
                     ""data"": ""fine""
                 },
                 ],
             columnDefs: [
                 { ""name"": ""engine"", ""targets"": 0 },
                ],
                ""language"": {
                    ""sEmptyTable"": ""Nessun dato presente nella tabella"",
                    ""sInfo"": ""Vista da _START_ a _END_ di _TOTAL_ elementi"",
                    ""sInfoEmpty"": ""Vist");
            WriteLiteral(@"a da 0 a 0 di 0 elementi"",
                    ""sInfoFiltered"": ""(filtrati da _MAX_ elementi totali)"",
                    ""sInfoPostFix"": """",
                    ""sInfoThousands"": ""."",
                    ""sLengthMenu"": ""Visualizza _MENU_ elementi"",
                    ""sLoadingRecords"": ""Caricamento..."",
                    ""sProcessing"": ""Elaborazione..."",
                    ""sSearch"": ""Cerca:"",
                    ""sZeroRecords"": ""La ricerca non ha portato alcun risultato."",
                    ""oPaginate"": {
                        ""sFirst"": ""Inizio"",
                        ""sPrevious"": ""Precedente"",
                        ""sNext"": ""Successivo"",
                        ""sLast"": ""Fine""
                    },
                    ""oAria"": {
                        ""sSortAscending"": "": attiva per ordinare la colonna in ordine crescente"",
                        ""sSortDescending"": "": attiva per ordinare la colonna in ordine decrescente""
                    }
                },
                initComplete:");
            WriteLiteral(@" function () {
                    // Apply the search
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.header()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that
                                    .search(this.value)
                                    .draw();
                            }
                        });
                    });
                },
                ""autoWidth"": false,
                ""responsive"": true,
                orderCellsTop: true,
                fixedHeader: true
            });
        $('#tableZone thead tr.rowclone:eq(1) th').each(function (i) {
                var title = $(this).text();
                if (!$(this).hasClass(""hide-search"")) {
                    $(this).html('<input type=""text"" placeholder=""Cerca ' + title + '"" />');
                    $('input', this).on('keyup chang");
            WriteLiteral(@"e', function () {
                        if (table.column(i).search() !== this.value) {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                }
         });


        //$('#");
#nullable restore
#line 144 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
         Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral("\').DataTable();\n        //$(\'#");
#nullable restore
#line 145 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
         Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral("\').DataTable().clear();\n        //$(\'#");
#nullable restore
#line 146 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
         Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"').DataTable().destroy();


    }

    function reloadTableVettoriParking(codice) {
        const myNode = document.getElementById(""tableZone"");
        myNode.textContent = '';

        if ($.fn.DataTable.fnIsDataTable(domTable)) {
            dataTable.fnDestroy(true);
            $('#tableZone').append(htmlTable);
        }
        $('#tableZone').append(htmlTable);


         dataTable = $('#");
#nullable restore
#line 162 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
                    Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"').DataTable({
                ajax: ""/Vettore/ajaxVettoriParkingHistory?codiceVettore="" + codice,

                dom: 'Bifrtip',//dom: 'Bftp',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ],
             columns: [
                 {
                     title : ""Nome"",
                     ""data"": ""name""
                 },
                 {
                     title: ""Nel parcheggio dal"",
                     ""data"": ""startDate""
                 }
                 
                 ],
             columnDefs: [
                 { ""name"": ""engine"", ""targets"": 0 },
                ],
                ""language"": {
                    ""sEmptyTable"": ""Nessun dato presente nella tabella"",
                    ""sInfo"": ""Vista da _START_ a _END_ di _TOTAL_ elementi"",
                    ""sInfoEmpty"": ""Vista da 0 a 0 di 0 elementi"",
                    ""sInfoFiltered"": ""(filtrati da _MA");
            WriteLiteral(@"X_ elementi totali)"",
                    ""sInfoPostFix"": """",
                    ""sInfoThousands"": ""."",
                    ""sLengthMenu"": ""Visualizza _MENU_ elementi"",
                    ""sLoadingRecords"": ""Caricamento..."",
                    ""sProcessing"": ""Elaborazione..."",
                    ""sSearch"": ""Cerca:"",
                    ""sZeroRecords"": ""La ricerca non ha portato alcun risultato."",
                    ""oPaginate"": {
                        ""sFirst"": ""Inizio"",
                        ""sPrevious"": ""Precedente"",
                        ""sNext"": ""Successivo"",
                        ""sLast"": ""Fine""
                    },
                    ""oAria"": {
                        ""sSortAscending"": "": attiva per ordinare la colonna in ordine crescente"",
                        ""sSortDescending"": "": attiva per ordinare la colonna in ordine decrescente""
                    }
                },
                initComplete: function () {
                    // Apply the search
                    this.a");
            WriteLiteral(@"pi().columns().every(function () {
                        var that = this;
                        $('input', this.header()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that
                                    .search(this.value)
                                    .draw();
                            }
                        });
                    });
                },
                ""autoWidth"": false,
                ""responsive"": true,
                orderCellsTop: true,
                fixedHeader: true
            });
        $('#tableZone thead tr.rowclone:eq(1) th').each(function (i) {
                var title = $(this).text();
                if (!$(this).hasClass(""hide-search"")) {
                    $(this).html('<input type=""text"" placeholder=""Cerca ' + title + '"" />');
                    $('input', this).on('keyup change', function () {
                        if (table.column(i).search() !== this.v");
            WriteLiteral(@"alue) {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                }
        });

    }
    
    function reloadTableVettoriCantieri(codice) {
        const myNode = document.getElementById(""tableZone"");
        myNode.textContent = '';

        if ($.fn.DataTable.fnIsDataTable(domTable)) {
            dataTable.fnDestroy(true);
            $('#tableZone').append(htmlTable);
        }
        $('#tableZone').append(htmlTable);


         dataTable = $('#");
#nullable restore
#line 255 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
                    Write(idTable);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"').DataTable({
                ajax: ""/Vettore/ajaxVettoriCantiereHistory?codiceVettore="" + codice,

                dom: 'Bifrtip',//dom: 'Bftp',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ],
             columns: [
                 {
                     title : ""Nome"",
                     ""data"": ""name""
                 },
                 {
                     title: ""Nel cantiere dal"",
                     ""data"": ""from""
                 },
                 
                 {
                     title: ""al"",
                     ""data"": ""to""
                 }
                 
                 ],
             columnDefs: [
                 { ""name"": ""engine"", ""targets"": 0 },
                ],
                ""language"": {
                    ""sEmptyTable"": ""Nessun dato presente nella tabella"",
                    ""sInfo"": ""Vista da _START_ a _END_ di _TOTAL_ elementi"",
 ");
            WriteLiteral(@"                   ""sInfoEmpty"": ""Vista da 0 a 0 di 0 elementi"",
                    ""sInfoFiltered"": ""(filtrati da _MAX_ elementi totali)"",
                    ""sInfoPostFix"": """",
                    ""sInfoThousands"": ""."",
                    ""sLengthMenu"": ""Visualizza _MENU_ elementi"",
                    ""sLoadingRecords"": ""Caricamento..."",
                    ""sProcessing"": ""Elaborazione..."",
                    ""sSearch"": ""Cerca:"",
                    ""sZeroRecords"": ""La ricerca non ha portato alcun risultato."",
                    ""oPaginate"": {
                        ""sFirst"": ""Inizio"",
                        ""sPrevious"": ""Precedente"",
                        ""sNext"": ""Successivo"",
                        ""sLast"": ""Fine""
                    },
                    ""oAria"": {
                        ""sSortAscending"": "": attiva per ordinare la colonna in ordine crescente"",
                        ""sSortDescending"": "": attiva per ordinare la colonna in ordine decrescente""
                    }
          ");
            WriteLiteral(@"      },
                initComplete: function () {
                    // Apply the search
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.header()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that
                                    .search(this.value)
                                    .draw();
                            }
                        });
                    });
                },
                ""autoWidth"": false,
                ""responsive"": true,
                orderCellsTop: true,
                fixedHeader: true
            });
        $('#tableZone thead tr.rowclone:eq(1) th').each(function (i) {
                var title = $(this).text();
                if (!$(this).hasClass(""hide-search"")) {
                    $(this).html('<input type=""text"" placeholder=""Cerca ' + title + '"" />');
              ");
            WriteLiteral(@"      $('input', this).on('keyup change', function () {
                        if (table.column(i).search() !== this.value) {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                }
        });

    }

</script>
");
#nullable restore
#line 343 "/home/fabio/Documenti/Emadema/dotnNet_prj/loocDev/LoocERP/Views/Vettore/_storicoVettore.cshtml"
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
