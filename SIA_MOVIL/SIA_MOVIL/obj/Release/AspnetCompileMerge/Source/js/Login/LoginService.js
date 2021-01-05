function Login_Service(body) {

    $.ajax({
        type: "POST",
        url: Global.UrlBack + 'Login/IniciarSesion',
        data: JSON.stringify({ Data: JSON.stringify(body) }),
        contentType: "application/json",
        crossDomain: true,
        success: function (data) {

            SetSessionUserData(data);
            location.href = Route.Home;
        },
        error: function (err) {
            try {
                toastr.error(err.responseJSON.ERROR_DSC);
            } catch (e) {
                toastr.error("Usuario no existe, validar datos");
            }

        },
        complete: function (data) {
            wait = false;
        }
    });
};
