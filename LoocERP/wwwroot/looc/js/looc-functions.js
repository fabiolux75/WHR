/************************************************************************/
/**             Generiche Function                                     **/
/************************************************************************/
/* Crea Guid da JS */
function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

/* Fa il parsing della data a partire dall'input */
function parseDate(input, format,separator="-") {
    format = (format || 'yyyy'+separator+'mm'+separator+'dd').toLowerCase(); // default format
    var parts = input.match(/(\d+)/g), 
    i = 0, fmt = {};
    // extract date-part indexes from the format
    format.replace(/(yyyy|dd|mm)/g, function(part) { fmt[part] = i++; });
    return new Date([parts[fmt['yyyy']], parts[fmt['mm']], parts[fmt['dd']]].filter(x => x !== undefined).join(separator));
}

/************************************************************************/
/**             GESTIONE NAZIONE/REGIONE/PROVINCIA/CITTA               **/
/************************************************************************/
$(document).ready(function () {    
    var tabs = $(".table-js-gen");
    if (tabs != null && tabs.length > 0) {
       
        var url = tabs.attr("tab-url");

        var columns = [];

        tabs.each(function () {
            $.each(this.attributes, function () {
                if (this.specified) {
                    if (this.name.startsWith('column-')) {
                        var sp = parseInt(this.name.split("column-")[1]);
                        columns[sp] = { "data": this.value };
                    }
                }
            });
            
            $(this).find("thead tr").each(function (i) {
                $(this).clone(true).appendTo($(this));

            });
        });
        tabs.DataTable({
            ajax: url,
            dom: 'Bfrtip',//dom: 'Bftp',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'csvHtml5',
                'pdfHtml5'
            ],
            "columns": columns,
            columnDefs: [
            ],
            "language": {
                "sEmptyTable": "Nessun dato presente nella tabella",
                "sInfo": "Vista da _START_ a _END_ di _TOTAL_ elementi",
                "sInfoEmpty": "Vista da 0 a 0 di 0 elementi",
                "sInfoFiltered": "(filtrati da _MAX_ elementi totali)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "Visualizza _MENU_ elementi",
                "sLoadingRecords": "Caricamento...",
                "sProcessing": "Elaborazione...",
                "sSearch": "Cerca:",
                "sZeroRecords": "La ricerca non ha portato alcun risultato.",
                "oPaginate": {
                    "sFirst": "Inizio",
                    "sPrevious": "Precedente",
                    "sNext": "Successivo",
                    "sLast": "Fine"
                },
                "oAria": {
                    "sSortAscending": ": attiva per ordinare la colonna in ordine crescente",
                    "sSortDescending": ": attiva per ordinare la colonna in ordine decrescente"
                }
            },
            "autoWidth": false,
            "responsive": true,
            orderCellsTop: true,
            fixedHeader: true
        });
    }

    var select2 = $(".select2js");
    if (select2 != null && select2.length > 0) {
        var url = tabs.attr("select2-url");

        select2.each(function () {
            var url = $(this).attr("select2-url");
            var placeholder = $(this).attr("select2-placeholder");

            $(this).select2({
                placeholder: placeholder,
                minimumInputLength: 1,
                allowClear: true,
                ajax: {
                    url: url,
                    dataType: 'json',

                },
            });

        });
    }


            if (
                $(".group-location").length > 0
            ) {
                //VALORE SETTATO
                var val = $(".nazione").attr("data-value");
                $.ajax({
                    url: '/MiniOffice/getnazioni',
                    type: 'get',

                    success: function (data) {
                        var total = $(".nazione").length;

                        for (var k = 0; k < total; k++) {
                            $.each(data.data, function (key, modelName) {
                                var selected = "";
                                if (val != null && val != "" && val != undefined && val == modelName.codice_Alpha3) selected = "selected";
                                var option = "<option value='" + modelName.codice_Alpha3 + "' " + selected + ">" + modelName.nome + "</option>";
                                $($(".nazione")[k]).append(option);
                            });
                            if (val != null && val != "" && val != undefined) {
                                $($(".nazione")[k]).value = val;
                                $($(".nazione")[k]).attr("data-value", "");
                                setRegioni($($(".nazione")[k]), val);
                            }
                        }
                        //SETTO IL VALORE PRESELEZIONATO
                       
                    }
                });
            }

        });
        //isvalue indica se nazione è un array

function setRegioni(father, nazione) {


    var a = $(father.closest(".group-location")[0]);

    var regioni = $(a).find(".region");
    var provincie = $(a).find(".provincia");
    var cities = $(a).find(".city");
    var caps = $(a).find(".caps");


    var input = nazione;
    if (nazione.value != undefined) input = nazione.value;

    $(a).find(".region option").remove();
    regioni.attr("disabled", true);

    $(a).find(".provincia option").remove();
    provincie.attr("disabled", true);

    $(a).find(".city option").remove();
    cities.attr("disabled", true);

    $(a).find(".caps option").remove();
    caps.attr("disabled", true);


    if (input == "") return;


    var val = regioni.attr("data-value");
    $.ajax({
        url: '/MiniOffice/getregioni',
        type: 'get',
        success: function (data) {
            var option = '<option></option>';
            regioni.append(option);
            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName) selected = "selected";
                option = "<option value=\"" + modelName + "\" " + selected + ">" + modelName + "</option>";
                regioni.removeAttr("disabled");
                regioni.append(option);
            });
            //SETTO IL VALORE PRESELEZIONATO           
            if (val != null && val != "" && val != undefined) {
                regioni.value = val;
                regioni.attr("data-value", "");
                setProvince(regioni,val);
            }
        }
    });
    /*
    var input = nazione;
    if (nazione.value != undefined) input = nazione.value;

    $("#regione option").remove();
    $("#regione").attr("disabled", true);
    $("#provincia option").remove();
    $("#provincia").attr("disabled", true);
    $("#comune option").remove();
    $("#comune").attr("disabled", true);
    if (input == "") return;

    var val = $("#regione").attr("data-value");
    $.ajax({
        url: '/MiniOffice/getregioni',
        type: 'get',
        success: function (data) {
            var option = '<option></option>';
            $("#regione").append(option);
            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName) selected = "selected";
                option = "<option value=\"" + modelName + "\" " + selected + ">" + modelName + "</option>";
                $("#regione").removeAttr("disabled");
                $("#regione").append(option);
            });
            //SETTO IL VALORE PRESELEZIONATO           
            if (val != null && val != "" && val != undefined) {
                $("#regione").value = val;
                $("#regione").attr("data-value", "");
                setProvince(val);
            }
        }
    });
    */
}



function setProvince(father) {


    var a = $($(father).closest(".group-location")[0]);

    
    var provincie = $(a).find(".provincia");
    var cities = $(a).find(".city");
    var caps = $(a).find(".caps");


    var input = father;
    if (father.value != undefined) input = father.value;



    $(a).find(".provincia option").remove();
    provincie.attr("disabled", true);

    $(a).find(".caps option").remove();
    caps.attr("disabled", true);

    var val = provincie.attr("data-value");
    $.ajax({
        url: '/MiniOffice/getprovince',
        data: { regione: input },
        type: 'get',
        success: function (data) {
            var option = '<option></option>';
            provincie.append(option);
            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName.sigla) { selected = "selected"; };
                option = "<option value='" + modelName.sigla + "' " + selected + ">" + modelName.estesa + "</option>";
                provincie.removeAttr("disabled");
                provincie.append(option);
            });
            //SETTO IL VALORE PRESELEZIONATO                          
            if (val != null && val != "" && val != undefined) {
                provincie.value = val;
                provincie.attr("data-value", "");
                setComuni(provincie);
            }
        }
    });

}




function setComuni(father) {    
    var a = $($(father).closest(".group-location")[0]);

    var cities = $(a).find(".city");

    var input = father;
    if (father.value != undefined) input = father.value;

    $(a).find(".city option").remove();
    
    cities.attr("disabled", true);    

    var val = cities.attr("data-value");

    $.ajax({
        url: '/MiniOffice/getcomuni',
        data: { provincia: input },
        type: 'get',
        success: function (data) {
            
            var option = '<option></option>';
            cities.append(option);
            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName.nome) selected = "selected";
                option = "<option value=\"" + modelName.nome + "\" " + selected + ">" + modelName.nome + "</option>";
                cities.removeAttr("disabled");
                cities.append(option);
            });
            //SETTO IL VALORE PRESELEZIONATO    
            /*
            if (val != null && val != "" && val != undefined) {
                cities.value = val;
                cities.attr("data-value", "");
                setCaps(cities);
            }
            */
        }
    });
    
}


function setCaps(father) {
    

    var a = $($(father).closest(".group-location")[0]);

    var caps = $(a).find(".caps");

    var input = father;
    if (father.value != undefined) input = father.value;

    $(a).find(".caps option").remove();
    caps.attr("disabled", true);

    var val = caps.attr("data-value");
    
    $.ajax({
        url: '/MiniOffice/getCaps?comune=' + input,
        type: 'get',
        success: function (data) {
            var option = '<option></option>';
            caps.append(option);

            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName) selected = "selected";
                option = "<option value='" + modelName + "' " + selected + ">" + modelName + "</option>";
                caps.removeAttr("disabled");
                caps.append(option);

            });
            
            
        }
    });
    
}


/*
//isvalue indica se regione è un array
function setComuni(provincia) {
    var input = provincia;
    if (provincia.value != undefined) input = provincia.value;

    $("#comune option").remove();
    $("#comune").attr("disabled", true);
    var val = $("#comune").attr("data-value");
    $.ajax({
        url: '/MiniOffice/getcomuni',
        data: { provincia: input },
        type: 'get',
        success: function (data) {
            var option = '<option></option>';
            $("#comune").append(option);
            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName.nome) selected = "selected";
                option = "<option value='" + modelName.nome + "' " + selected + ">" + modelName.nome + "</option>";
                $("#comune").removeAttr("disabled");
                $("#comune").append(option);
            });
        }
    });
    //SETTO IL VALORE PRESELEZIONATO                          
    if (val != null && val != "" && val != undefined) {
        $("#comune").value = val;
        $("#comune").attr("data-value", "");
    }
}
*/
/************************************************************************/
/**         END GESTIONE NAZIONE/REGIONE/PROVINCIA/CITTA               **/
/************************************************************************/

/************************************************************************/
/**             Dominio multilivello                                   **/
/************************************************************************/
function setDomainParent(id, tipo){
        var orig = $("#"+id);
        var val = orig.attr("data-value");
        var secondId = orig.attr('data-childid');
        $("#"+secondId+ " option").remove();

        $.ajax({
            url: '/Domain/getList',
            type: 'get',
            data: {
                tipo: tipo
            },
            success: function (data) {
                orig.append("<option value=''>-</option>");
                $.each(data.data, function (key, modelName) {
                    var selected = "";
                    if (val != null && val != "" && val != undefined && val == modelName.id) selected = "selected";
                    var option = "<option value='" + modelName.id + "' " + selected + ">" + modelName.name + "</option>";
                    orig.append(option);
                });
            }
        });

        //SETTO IL VALORE PRESELEZIONATO
        if (val != null && val != "" && val != undefined) {
            setDomainChild(null, val,secondId);
        }
}

function setDomainChild(e, parentId, childid){
    
    var orig = $("#"+childid);
    
    var val = "";
    if (typeof orig.getAttribute !== "undefined") { 
        val = orig.getAttribute('data-value');
    }else{
        val = orig.attr('data-value');
    }
    if(e != null) parentId = e.value;
    
    $("#"+childid+ " option").remove();
    orig.removeClass("d-none");
    
    $.ajax({
        url: '/Domain/getChild',
        type: 'get',
        data: {
            Id: parentId
        },
        success: function (data) {
            orig.append("<option value=''>-</option>");
            $.each(data.data, function (key, modelName) {
                var selected = "";
                if (val != null && val != "" && val != undefined && val == modelName.id) selected = "selected";
                var option = "<option value='" + modelName.id + "' " + selected + ">" + modelName.name + "</option>";
                orig.append(option);
            });
        }
    });
    
}
/************************************************************************/
/**             END Dominio multilivello                               **/
/************************************************************************/