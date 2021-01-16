﻿let Global = {
    UrlBack: '/SIA_MOVIL/'
};

let Route = {
    Login: "/SIA_MOVIL/Login/",
    Home: "/SIA_MOVIL/Login/"
};

var SessionName = 'CMPC_PortalProveedores_SessionUserData';
var SessionMenu = 'CMPC_PortalProveedores_SessionMenu';

function SetValidation() {

    $("input").bind("paste", function (e) {

        return false;

    });

    $('input').on('drop', function (event) {
        event.preventDefault();
    });

    $('.Number').keypress(function (event) {
        var keycode = event.which;
        console.log(keycode);
        if (!(event.shiftKey == false && (keycode == 44 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {
            event.preventDefault();
        }
    });

    $('.Alphanumeric').keypress(function (event) {
        var keycode = event.which;
        console.log(keycode);
        if (!(event.shiftKey == false && (keycode == 44 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {

            if ((keycode >= 97 && keycode <= 122) || (keycode >= 65 && keycode <= 90)) {

            }
            else {

                event.preventDefault();
            }

        }
    });

    $('.AlphanumericW').keypress(function (event) {
        var keycode = event.which;
        console.log(keycode);
        if (!(event.shiftKey == false && (keycode == 44 || keycode == 8 || keycode == 32 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))) {

            if ((keycode >= 97 && keycode <= 122) || (keycode >= 65 && keycode <= 90)) {

            }
            else {

                event.preventDefault();
            }

        }
    });

    $('.EmailFormat').blur(function (event) {
        if (ValidateEmail($(this).val())) {


        }
        else {
            toastr.warning("Formato de correo no valido");
            $(this).val('');
        }
    });
}


function SetSessionUserData(data) {
    sessionStorage.setItem(SessionName, JSON.stringify(data));
}
function GettSessionUserData() {
    return JSON.parse(sessionStorage.getItem(SessionName))
}

function SetSessionMenu(data) {
    sessionStorage.setItem(SessionMenu, JSON.stringify(data));
}
function GettSessionMenu() {
    return JSON.parse(sessionStorage.getItem(SessionMenu))
}

$(document).ajaxStop(function () {
    $(".modal_load").hide();
});
$(document).ajaxStart(function () {
    $(".modal_load").show();
});

function openNav() {
    document.getElementById("mySidenav").style.width = "280px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

function HandleError(ErrorCod, MsjError) {

    if (ErrorCod == 401) {
        swal({
            title: "Su sesión ha expirado",
            text: "Sera redireccionado a la pagina de inicio de sesión",
            icon: "info"
        })
            .then(function () {
                location.href = Route.Login;
            });

    }
    else if (ErrorCod == 403) {

        swal({
            title: "Su sesión ha expirado",
            text: MsjError,
            icon: "info"
        })
            .then(function () {
                location.href = Route.Login;
            });

    }
    else if (ErrorCod == 404) {

        toastr.error("Error de comunicación");

    }
    else if (ErrorCod == 500) {

        toastr.error(MsjError);

    }
    else {

        toastr.error(MsjError);

    }
}