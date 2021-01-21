var tableHeaderModal = [
    { Nombre: "FECHA", MinWidth: "", MaxWidth: "", ClassName: "" },
    { Nombre: "VALOR", MinWidth: "", MaxWidth: "", ClassName: "" },
    { Nombre: "ESTADO", MinWidth: "", MaxWidth: "", ClassName: "" }
];

var ListaEstaciones = [];
var EstacionIndex = null;
var RangoDefault = 4;
var objEstacion = null;

var slideIndex = 1;

$(document).ready(function () {

    handlePagesStates.init();

});

var handlePagesStates = function () {

    function init() {

        $("#CodigoPantalla").html(Global.CodigoPantalla.Estaciones);
        handleButtonsEvents();
        handleGenericEvents();

        var fecha_actual = moment().format('DD-MM-YYYY');
        $('#txtFecha').val(fecha_actual);
        $("#txtRango").val(RangoDefault);

        $(".datepicker").datepicker();

        setTimeout(function () {
            handleEstaciones.ConsultaEstaciones();
        }, 500);

    }

    function handleButtonsEvents() {

        $("#btnMeteorologia").on("click", function (e) {

            if ($("#dvMeteorologia").css("display") === "none") {
                $("#dvMeteorologia").show();
                $("#dvGases").hide();
            } else {
               // $("#dvMeteorologia").hide();
            }
            if ($("#dvGases").css("display") === "block") {
                setTimeout(function () {
                    $("#dvGases").hide();
                }, 1000);
            }
            
        });

        $("#btnGases").on("click", function (e) {

            if ($("#dvGases").css("display") === "none") {
                $("#dvGases").show();
                $("#dvMeteorologia").hide();
            } else {
                //$("#dvGases").hide();
            }
            if ($("#dvMeteorologia").css("display") === "block") {
                setTimeout(function () {
                    $("#dvMeteorologia").hide();
                }, 1000);
            }

        });

    }

    function handleGenericEvents() {

        $("#txtFecha").on("change", function (e) {
            handleEstaciones.CargaDetalleEstacion();
        });

        $("#txtRango").on("blur", function (e) {
            handleEstaciones.CargaDetalleEstacion();
        });
    }

    return {
        init: init
    }

}();

var handleEstaciones = function () {

    function ConsultaEstaciones() {

        var params = {
            USUARIO: DataSesion.USER
        }

        EjecutaConsulta.Post(Global.EstacionesController.Name, Global.EstacionesController.ConsultaEstaciones, params, true)
            .then(response => {
                if (response.Resultado) {
                    ListaEstaciones = response.Elemento;
                    handleDataHtml.LlenarData();
                }
            });

    }

    function ConsultaDetalleEstacion(planta, estacion, callback = null) {

        var fecha = null, rango = RangoDefault;
        var variable = 0;
        if (IsNull($("#txtFecha").val()) == null) {
            fecha = moment().format('DD-MM-YYYY');
            $("#txtFecha").val(fecha);
        }
        else
            fecha = $("#txtFecha").val();

        if (IsNull($("#txtRango").val()) == null) {
            rango = RangoDefault;
            $("#txtRango").val(RangoDefault);
        }
        else
            rango = $("#txtRango").val();

        var params = {
            PLANTA: planta,
            ESTACION: estacion,
            VARIABLE: variable,
            FECHA: fecha,
            RANGO: rango,
            USUARIO: DataSesion.USER
        }

        EjecutaConsulta.Post(Global.EstacionesController.Name, Global.EstacionesController.ConsultaDetalleEstacion, params, true)
            .then(response => {
                if (callback != null)
                    callback(response);
            });

    }

    function CargaDetalleEstacion() {

        ListaEstaciones.map((element, i) => {
            if (i == EstacionIndex) {
                objEstacion = element;

                handleEstaciones.ConsultaDetalleEstacion(element.PLANTA_ID, element.ESTACION_ID, function (response) {
                    if (response.Resultado) {

                        $("#CodigoPantalla").html(Global.CodigoPantalla.ComportamientoVariables);

                        $('#lbNombreEstacion').text('Estación ' + element.ESTACION_DSC);
                        $('#lbTRS').text(element.TRS + ' ppb');
                        $("#dvDetalleEstacion").show("medium");
                        $("#dvListaEstaciones").hide("medium");

                        var data = response.Elemento;
                        handleDataDetalleEstacion.CargaGrafico(data.GRAFICO);
                        handleDataDetalleEstacion.CargaTablasVariables(data.TABLA);
                        $("#dvGases").show();

                        switch (objEstacion.PLANTA_ID)
                        {
                            case 'LAJ':
                                $("#dvLaja").show();
                                break;
                            case 'SFE':
                                $("#dvSantaFe").show();
                                break;
                            case 'PAC':
                                $("#dvPacifico").show();
                                break;
                        }

                    }
                });

            }
        })

    }

    function ConsultaDetalleVariable(planta, variable, callback = null) {

        var fecha = null, rango = RangoDefault;
        if (IsNull($("#txtFecha").val()) == null) {
            fecha = moment().format('DD-MM-YYYY');
        }
        else
            fecha = $("#txtFecha").val();

        if (IsNull($("#txtRango").val()) == null) {
            rango = RangoDefault;
        }
        else
            rango = $("#txtRango").val();

        var params = {
            PLANTA: planta,
            VARIABLE: variable,
            ESTACION: objEstacion.ESTACION_ID,
            FECHA: fecha,
            RANGO: rango,
            USUARIO: DataSesion.USER
        }

        EjecutaConsulta.Post(Global.EstacionesController.Name, Global.EstacionesController.ConsultaDetalleVariable, params, true)
            .then(response => {
                if (callback != null)
                    callback(response);
            });

    }

    return {
        ConsultaEstaciones: ConsultaEstaciones,
        ConsultaDetalleEstacion: ConsultaDetalleEstacion,
        CargaDetalleEstacion: CargaDetalleEstacion,
        ConsultaDetalleVariable: ConsultaDetalleVariable
    }

}();

var handleDataHtml = function () {

    function handleEvents(index) {

        $(`#VerEstacion${index}`).on("click", function (e) {
            EstacionIndex = index;
            handleEstaciones.CargaDetalleEstacion();
        });

    }

    function LlenarData() {

        $.each(ListaEstaciones, function (i, element) {

            var estado = '';
            switch (element.ESTACION_ESTADO) {
                case '1':
                    estado = 'circle-good';
                    break;
                case '2':
                    estado = 'circle-warning';
                    break;
                case '3':
                    estado = 'circle-bad';
                    break;
            }

            let row = '';
            row += `<a href="javascript:void(0);" id="VerEstacion${i}" style="text-decoration: inherit; color: inherit;">
                <div class="card border-0 shadow-sm mb-3">
                    <div class="card-header">
                        <div class="row">
                            <h5 class="ml-3 col-10">Estación ${element.ESTACION_DSC}</h5>
                            <div class="${estado}"></div>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-6">
                                <p>${element.ESTACION_FECHA}</p>
                            </div>
                            <div class="col-6">
                                <p>${IsNull(element.COMUNIDAD) == null ? '' : '(' + element.COMUNIDAD + ')'}</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <p>
                                    <strong>TRS: </strong><span id="valorTrs">${element.TRS} ppb</span>
                                </p>
                            </div>
                            <div class="col-6">
                                <p>
                                    <strong>Dir. Viento: </strong><span id="valorViento" style="color: #016E3F; font-weight: 600;">${element.DIRV}°</span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </a>`;
            $('#dvEstaciones').append(row);
            handleEvents(i);
        });

    }

    function LlenaDataMetereologia(data) {

        $('#dvDetalleMeteorologia').html('');

        var fecha = (IsNull(data[0]) == null) ? '' : data[0].ESTACION_FECHA;
        $("#lbFechaMeteorologia").text(fecha);

        $.each(data, function (i, element) {

            let row = '';
            row += `<a href="javascript:void(0);" class="VerVariableMeteorologia" data-id="${element.VARIABLE_ID}" data-planta="${element.PLANTA_ID}" style="text-decoration: inherit; color: inherit;">
                        <div class="row border-bottom mb-2">
                            <div class="col-6">
                                <h6><strong>${element.VARIABLE_DSC}</strong></h6>
                                <p>Unidad: ${element.UNIDAD}</p>
                            </div>
                            <div class="col-6">
                                <h4 class="text-right mr-5"><span>${element.VALOR}</span></h4>
                            </div>
                        </div>
                    </a>`;
            $('#dvDetalleMeteorologia').append(row);

        });

        $(".VerVariableMeteorologia").on("click", function (e) {

            var planta = $(this).attr("data-planta");
            var variable = $(this).attr("data-id");
            handleEstaciones.ConsultaDetalleVariable(planta, variable, function (response) {

                SetColumnsTable(tableHeaderModal)
                    .then(res => {
                        $('#table-list-head').html(`<tr>${res}</tr>`);
                        LlenaTablaModal(response.Elemento);

                        setTimeout(function () {
                            $('#modalTrs').modal("show");
                        }, 250);
                    });            
                
            });

        });

    }

    function LlenaDataGases(data) {

        $('#dvDetalleGases').html('');

        var fecha = (IsNull(data[0]) == null) ? '' : data[0].ESTACION_FECHA;
        $("#lbFechaGases").text(fecha);

        $.each(data, function (i, element) {

            let row = '';
            row += `<a href="javascript:void(0);" class="VerVariableGases" data-id="${element.VARIABLE_ID}" data-planta="${element.PLANTA_ID}" style="text-decoration: inherit; color: inherit;">
                        <div class="row border-bottom mb-2">
                            <div class="col-6">
                                <h6><strong>${element.VARIABLE_DSC}</strong></h6>
                                <p>Unidad: ${element.UNIDAD}</p>
                            </div>
                            <div class="col-6">
                                <h4 class="text-right mr-5"><span>${element.VALOR}</span></h4>
                            </div>
                        </div>
                    </a>`;
            $('#dvDetalleGases').append(row);

        });

        $(".VerVariableGases").on("click", function (e) {

            var planta = $(this).attr("data-planta");
            var variable = $(this).attr("data-id");
            handleEstaciones.ConsultaDetalleVariable(planta, variable, function (response) {

                SetColumnsTable(tableHeaderModal)
                    .then(res => {
                        $('#table-list-head').html(`<tr>${res}</tr>`);
                        LlenaTablaModal(response.Elemento);

                        setTimeout(function () {
                            $('#modalTrs').modal("show");
                        }, 250);
                    });

            });

        });
    }

    function LlenaTablaModal(data) {

        $('#table-list-body').html('');

        $.each(data, function (i, element) {
            let row = '';
            row += `<tr>
                        <td>${element.FECHA}</td>
                        <td>${element.VALOR}</td>
                        <td>${element.ESTADO}</td>
                    </tr>`;

            $('#table-list-body').append(row);
        });

    }

    return {
        LlenarData: LlenarData,
        LlenaDataMetereologia: LlenaDataMetereologia,
        LlenaDataGases: LlenaDataGases
    }

}();

var handleDataDetalleEstacion = function () {

    function CargaGrafico(datos) {

        var arrayX = [], arrayY = [];
        $.each(datos, function (i, element) {
            arrayX.push(element.FECHA);
            arrayY.push(parseFloat(element.VALOR.replace(',', '.')));
        });

        feather.replace();
        var ctx = $('#myChart');
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: arrayX,
                datasets: [{
                    data: arrayY,
                    lineTension: 0,
                    backgroundColor: 'transparent',
                    borderColor: '#003E7B',
                    borderWidth: 4,
                    pointBackgroundColor: '#003E7B'
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: false
                        }
                    }]
                },
                legend: {
                    display: false
                }
            }
        });

    }

    function CargaTablasVariables(datos) {

        var array_metereologia = [], array_gases = [];
        $.each(datos, function (i, element) {
            if (element.CLASE == 'METEOROLOGIA') {
                array_metereologia.push({
                    ESTACION_DSC: element.ESTACION_DSC,
                    ESTACION_FECHA: element.ESTACION_FECHA,
                    PLANTA_ID: element.PLANTA_ID,
                    UNIDAD: element.UNIDAD,
                    VALOR: element.VALOR,
                    VARIABLE_DSC: element.VARIABLE_DSC,
                    VARIABLE_ID: element.VARIABLE_ID
                });
            }
            if (element.CLASE == 'GASES') {
                array_gases.push({
                    ESTACION_DSC: element.ESTACION_DSC,
                    ESTACION_FECHA: element.ESTACION_FECHA,
                    PLANTA_ID: element.PLANTA_ID,
                    UNIDAD: element.UNIDAD,
                    VALOR: element.VALOR,
                    VARIABLE_DSC: element.VARIABLE_DSC,
                    VARIABLE_ID: element.VARIABLE_ID
                });
            }
        });

        handleDataHtml.LlenaDataMetereologia(array_metereologia);
        handleDataHtml.LlenaDataGases(array_gases);

    }

    return {
        CargaGrafico: CargaGrafico,
        CargaTablasVariables: CargaTablasVariables
    }

}();