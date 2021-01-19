let GRILLA = null;
let DETALLE = null;

$("#BtnVolverGrilla").click(function () {

    //location.href a donde quiera que tenga que volver
});

$("#BtnVolverDetalle").click(function () {

    ObtieneGrilla();
});

$(function () {
    SetValidation();
    ObtieneGrilla();

    $("#btnIndicadores").attr("href", `/${Global.UrlBack}/Home/Index`);
    $("#CodigoPantalla").html(Global.CodigoPantalla.Indicadores);
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
        VV_USUARIO: "SIA"
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
                    <td class="text-center bg-planta"><span id="reclamPulpmes">${item.RECLAMOS_MES}</span></td>
                    <td class="text-center bg-planta"><span id="reclamPulpaño"><strong><a href="#" style="color: inherit; text-decoration: inherit;">${item.RECLAMOS_AGNO}</a></strong></span></td>
                    <td class="text-center bg-planta"><span id="incidPulpmes">${item.INCIDENTE_MES}</span></td>
                    <td class="text-center bg-planta"><span id="incidPulpaño"><strong><a href="#" style="color: inherit; text-decoration: inherit;">${item.INCIDENTE_AGNO}</a></strong></span></td>
                    <td class="text-center bg-planta"><span id="fiscPulpmes">${item.FISCALIZACION_MES}</span></td>
                    <td class="text-center bg-planta"><span id="fiscPulpaño"><strong><span style="color: inherit; text-decoration: inherit; cursor: pointer" onclick="ObtieneDetalle(${i})">${item.FISCALIZACION_AGNO}</span></strong></span></td>

                </tr>

            `;

        }

        if (item.PLANTA_ID == "TOT" && item.FILIAL_ID == "0") {
            row = `
                <tr>
                    <td class="bg-total"><strong><span id="total">${item.PLANTA_DSC}</span></strong></td>
                    <td class="text-center bg-total"><span id="reclamTotalmes">${item.RECLAMOS_MES}</span></td>
                    <td class="text-center bg-total"><span id="reclamTotalaño"><strong><a href="#" style="color: inherit; text-decoration: inherit;">${item.RECLAMOS_AGNO}</a></strong></span></td>
                    <td class="text-center bg-total"><span id="incidTotalmes">${item.INCIDENTE_MES}</span></td>
                    <td class="text-center bg-total"><span id="incidTotalaño"><strong><a href="#" style="color: inherit; text-decoration: inherit;">${item.INCIDENTE_AGNO}</a></strong></span></td>
                    <td class="text-center bg-total"><span id="fiscTotalmes">${item.FISCALIZACION_MES}</span></td>
                    <td class="text-center bg-total"><span id="fiscTotalaño"><strong><a href="#" style="color: inherit; text-decoration: inherit;">${item.FISCALIZACION_AGNO}</a></strong></span></td>
                </tr>

            `;

        }

        if (item.PLANTA_ID != "TOT" && item.FILIAL_ID != "0") {
            row = `
                <tr>
                    <td class=""><span id="plantaForestal">${item.PLANTA_DSC}</span></td>
                    <td class="text-center"><span id="reclamForestalchilemes">${item.RECLAMOS_MES}</span></td>
                    <td class="text-center bg-año"><span id="reclamForestalchileaño">${item.RECLAMOS_AGNO}</span></td>
                    <td class="text-center"><span id="incidForestalchilemes">${item.INCIDENTE_MES}</span></td>
                    <td class="text-center bg-año"><span id="incidForestalchileaño">${item.INCIDENTE_AGNO}</span></td>
                    <td class="text-center"><span id="fiscForestalchilemes">${item.FISCALIZACION_MES}</span></td>
                    <td class="text-center bg-año"><span id="fiscForestalchileaño">${item.FISCALIZACION_AGNO}</span></td>
                </tr>

            `;

        }

        $("#BodyGrilla").append(row);

    })




}



function ObtieneDetalle(i) {

    $("#DivDetalle").removeClass("d-none");
    $("#DivGrilla").addClass("d-none");

    $("#BtnVolverGrilla").addClass("d-none");
    $("#BtnVolverDetalle").removeClass("d-none");

    DETALLE = {

        VN_FILIAL: 0,
        VV_PLANTA: 'TOT',
        VV_TIPO_FECHA: 'M',
        VN_TIPO_INDICADOR: 1,
        VV_USUARIO: "SIA"
    };

    GetDetalle_Service(DETALLE, function (res) {
        DETALLE = res;
        InitTableDetalle(DETALLE.DETALLE);
    });

}

function InitTableDetalle(data) {

    $("#BodyDetalle").empty();

    data.forEach(function (item, i) {

        var row = "";


        row = `
        <div class="card border-0 shadow-sm mb-3">
            <div class="card-body">
                <div id="fecha">
                    <p><strong>${item.FECHA}</strong></p>
                </div>
                <div class="row">
                    <div class="col-6">
                        <p>
                            <strong>Planta: </strong><span id="planta">${item.PLANTA_DSC}</span>
                        </p>
                    </div>
                    <div class="col-6">
                        <p>
                            <strong>Organismo: </strong><span id="organismo">${item.ORGANISMO}</span>
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