let GRILLA = null;
let DETALLE = null;

$("#BtnVolverGrilla").click(function () {

    //location.href a donde quiera que tenga que volver
});

$("#BtnVolverDetalle").click(function () {

    ObtieneGrilla();
});

$(function () {

    setTimeout(function () {

        SetValidation();
        ObtieneGrilla();

        $("#CurrentYear").html(new Date().getFullYear());
        $("#CurrentMonth").html(months[new Date().getMonth()]);


        $("#btnIndicadores").attr("href", `/${Global.UrlBack}/Home/Index`);
        $("#CodigoPantalla").html(Global.CodigoPantalla.Indicadores);

    }, 500);
    
});

function ObtieneGrilla() {

    $("#DivGrilla").removeClass("d-none");
    $("#DivDetalle").addClass("d-none");

    $("#BtnVolverGrilla").removeClass("d-none");
    $("#BtnVolverDetalle").addClass("d-none");

    GRILLA = {

        VN_FILIAL: 0,
        VV_PLANTA: 'TOT',
        VV_FECHAINICIO: null,
        VV_FECHAFIN: null,
        VV_USUARIO: DataSesion.USER
    };

    GetGrilla_Service(GRILLA, function (res) {
        GRILLA = res;
        InitTableGrilla(GRILLA.GRILLA);
    });

}


function InitTableGrilla(data) {

    $("#BodyGrilla").empty();

    data.forEach(function (item, i) {

        var row = "";

        if (item.PLANTA_ID == "TOT" && item.FILIAL_ID != "0") {

            row = `
                <tr>
                    <td class="bg-planta"><strong><span id="plantaPulp">${item.PLANTA_DSC}</span></strong></td>
                    <td class="text-center bg-planta">
                        <span id="reclamPulpmes">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.RECLAMO_TIPO}','${item.PLANTA_DSC + ' RECLAMOS'}' )">${item.RECLAMOS_MES}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-planta">
                        <span id="reclamPulpaño">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.RECLAMO_TIPO}','${item.PLANTA_DSC + ' RECLAMOS'}' )">${item.RECLAMOS_AGNO}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-planta">
                        <span id="incidPulpmes">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.INCIDENTE_TIPO}','${item.PLANTA_DSC + ' INCIDENTES'}' )">${item.INCIDENTE_MES}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-planta">
                        <span id="incidPulpaño">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.INCIDENTE_TIPO}' ,'${item.PLANTA_DSC + ' INCIDENTES'}')">${item.INCIDENTE_AGNO}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-planta">
                        <span id="fiscPulpmes">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.FISCALIZACION_TIPO}','${item.PLANTA_DSC + ' FISCALIZACIONES'}' )">${item.FISCALIZACION_MES}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-planta">
                        <span id="fiscPulpaño">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.FISCALIZACION_TIPO}' ,'${item.PLANTA_DSC + ' FISCALIZACIONES'}')">${item.FISCALIZACION_AGNO}</span>
                            </strong>
                        </span>
                    </td>

                </tr>

            `;

        }

        if (item.PLANTA_ID != "TOT" && item.FILIAL_ID != "0") {
            row = `
                <tr>
                    <td class=""><span id="plantaForestal">${item.PLANTA_DSC}</span></td>
                    <td class="text-center">
                        <span id="reclamForestalchilemes">
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.RECLAMO_TIPO}','${item.PLANTA_DSC + ' RECLAMOS'}' )">${item.RECLAMOS_MES}</span>
                        </span>
                    </td>
                    <td class="text-center bg-año">
                        <span id="reclamForestalchileaño">
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.RECLAMO_TIPO}','${item.PLANTA_DSC + ' RECLAMOS'}' )">${item.RECLAMOS_AGNO}</span>
                        </span>
                    </td>
                    <td class="text-center">
                        <span id="incidForestalchilemes">
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.INCIDENTE_TIPO}','${item.PLANTA_DSC + ' INCIDENTES'}')">${item.INCIDENTE_MES}</span>
                        </span>
                    </td>
                    <td class="text-center bg-año">
                        <span id="incidForestalchileaño">
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.INCIDENTE_TIPO}' ,'${item.PLANTA_DSC + ' INCIDENTES'}')">${item.INCIDENTE_AGNO}</span>
                        </span>
                    </td>
                    <td class="text-center">
                        <span id="fiscForestalchilemes">
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.FISCALIZACION_TIPO}','${item.PLANTA_DSC + ' FISCALIZACIONES'}' )">${item.FISCALIZACION_MES}</span>
                        </span>
                    </td>
                    <td class="text-center bg-año">
                        <span id="fiscForestalchileaño">
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.FISCALIZACION_TIPO}','${item.PLANTA_DSC + ' FISCALIZACIONES'}' )">${item.FISCALIZACION_AGNO}</span>
                        </span>
                    </td>

                </tr>

            `;

        }



        if (item.PLANTA_ID == "TOT" && item.FILIAL_ID == "0") {
            row = `
                <tr>
                    <td class="bg-total"><strong><span id="total">${item.PLANTA_DSC}</span></strong></td>
                    <td class="text-center bg-total">
                        <span id="reclamTotalmes">
                        <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.RECLAMO_TIPO}','${item.PLANTA_DSC + ' RECLAMOS'}' )">${item.RECLAMOS_MES}</span>
                        </strong> 
                        </span>
                    </td>
                    <td class="text-center bg-total">
                        <span id="reclamTotalaño">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.RECLAMO_TIPO}','${item.PLANTA_DSC + ' RECLAMOS'}' )">${item.RECLAMOS_AGNO}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-total">
                        <span id="incidTotalmes">
                        <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID},'${item.PLANTA_ID}','M','${item.INCIDENTE_TIPO}' ,'${item.PLANTA_DSC + ' INCIDENTES'}')">${item.INCIDENTE_MES}</span>
                        </strong>
                        </span>
                    </td>
                    <td class="text-center bg-total">
                        <span id="incidTotalaño">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.INCIDENTE_TIPO}','${item.PLANTA_DSC + ' INCIDENTES'}' )">${item.INCIDENTE_AGNO}</span>
                            </strong>
                        </span>
                    </td>
                    <td class="text-center bg-total">
                        <span id="fiscTotalmes">
                        <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','M','${item.FISCALIZACION_TIPO}' ,'${item.PLANTA_DSC + ' FISCALIZACIONES'}')">${item.FISCALIZACION_MES}</span>
                        </strong>                       
                        </span>
                    </td>
                    <td class="text-center bg-total">
                        <span id="fiscTotalaño">
                            <strong>
                            <span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle('${item.FILIAL_ID}','${item.PLANTA_ID}','A','${item.FISCALIZACION_TIPO}','${item.PLANTA_DSC + ' FISCALIZACIONES'}' )">${item.FISCALIZACION_AGNO}</span>
                            </strong>
                        </span>
                    </td>

                </tr>

            `;

        }



        $("#BodyGrilla").append(row);

    })




}



function ObtieneDetalle(FILIAL, PLANTA, TIPO_FECHA, TIPO_INDICADOR, TITULO) {

    DETALLE = {

        VN_FILIAL: FILIAL,
        VV_PLANTA: PLANTA,
        VV_FECHAINICIO: '',
        VV_TIPO_FECHA: TIPO_FECHA,
        VN_TIPO_INDICADOR: TIPO_INDICADOR,
        VV_USUARIO: DataSesion.USER
    };

    //console.log(DETALLE);

    GetDetalle_Service(DETALLE, function (res) {

        DETALLE = res;
        InitTableDetalle(DETALLE.DETALLE, TITULO);
    });

}

function InitTableDetalle(data, titulo) {

    if (data.length <= 0) {
        toastr.warning("No hay registros", "Advertencia");
        return;
    }


    $("#DivDetalle").removeClass("d-none");
    $("#DivGrilla").addClass("d-none");

    $("#BtnVolverGrilla").addClass("d-none");
    $("#BtnVolverDetalle").removeClass("d-none");

    $("#BodyDetalle").empty();

    $("#TituloDetalle").html(titulo);
    
    data.forEach(function (item, i) {

        var row = "";


        row = `
        <div class="card border-0 shadow-sm mb-3">
            <div class="card-body">
                <div id="fecha">
                    <p><strong>${item.FECHA}</strong></p>
                </div>
                <div class="row">
                    <div class="col-3">
                        <p>
                            <strong>Planta: </strong><span id="planta">${item.PLANTA_DSC}</span>
                        </p>
                    </div>
                    <div class="col-3">
                        <p>
                            <strong>Organismo: </strong><span id="organismo">${item.ORGANISMO}</span>
                        </p>
                    </div>
                        <div class="col-3">
                        <p>
                            <strong>Tipo: </strong><span id="organismo">${item.TIPO}</span>
                        </p>
                    </div>
                    <div class="col-12">
                        <h6><strong>Descripción:</strong></h6>
                        <p>${item.DESCRIPCION}</p>
                    </div>
                </div>

            </div>
        </div>

            `;



        $("#BodyDetalle").append(row);

    })

    $("#btnIndicadores").attr("href", `/${Global.UrlBack}/Indicadores/Index`);

}