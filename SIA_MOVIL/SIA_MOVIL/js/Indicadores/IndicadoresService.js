function GetGrilla_Service(body, handleData) {
    $.ajax({
        type: "POST",
        url: Global.UrlBack + 'Indicadores/SP_LISTA_INDICADORES',
        data: JSON.stringify({ Data: JSON.stringify(body) }),
        contentType: "application/json",
        crossDomain: true,
        success: function (data) {
            handleData(data);
        },
        error: function (err) {


            try {
                HandleError(err.status, err.responseJSON.ERROR_DSC);
            }
            catch {
                HandleError(err.status, "Error critico");
            }

        },
        complete: function (data) {
            wait = false;
        }
    });
}

function GetDetalle_Service(body, handleData) {
    $.ajax({
        type: "POST",
        url: Global.UrlBack + 'Indicadores/SP_LISTA_INDICADORES_DET',
        data: JSON.stringify({ Data: JSON.stringify(body) }),
        contentType: "application/json",
        crossDomain: true,
        success: function (data) {
            handleData(data);
        },
        error: function (err) {


            try {
                HandleError(err.status, err.responseJSON.ERROR_DSC);
            }
            catch {
                HandleError(err.status, "Error critico");
            }

        },
        complete: function (data) {
            wait = false;
        }
    });
}