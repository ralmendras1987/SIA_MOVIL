var Global = {
    UrlBack: 'SIA_MOVIL',
    //UrlApi: 'http://desarrollocmpc.genesys.cl:112/SIA_MOVIL_WEBAPI/api',
    //UrlApi: 'http://localhost:9770/SIA_MOVIL_WEBAPI/api',
    UrlApi: '/SIA_MOVIL_WEBAPI/api',
    ObjSesion: 'obj1',
    ObjRemember: 'obj2',
    Token: 'obj3',
    ClaveEncriptacion: null,
    LoginController: {
        Name: 'Login',
        ValidarLogin: 'ValidarLogin',
        CerrarSesion: 'CerrarSesion',
        GetClaveEncript: 'GetClaveEncript'
    },
    EstacionesController: {
        Name: 'Estaciones',
        ConsultaEstaciones: 'ConsultaEstaciones',
        ConsultaDetalleEstacion: 'ConsultaDetalleEstacion',
        ConsultaDetalleVariable: 'ConsultaDetalleVariable'
    },
    CodigoPantalla: {
        Login: 'SIA-F0001 v(1.1.1)',
        Estaciones: 'SIA-F0002 v(1.1.1)',
        ComportamientoVariables: 'SIA-F0003 v(1.1.1)',
        Indicadores: 'SIA-F0004 v(1.1.1)'
    }
}

let DataSesion = null;

//Función que abre el modal de carga de transacciones
$(function () {
    $('.loading').hide();

    window.ajax_loading = false;
    $.hasAjaxRunning = function () {
        ShowLoading();
    };
    $(document).ajaxStart(function () {
        ShowLoading();
    });
    $(document).ajaxStop(function () {
        CloseLoading();
    });

    $(document).on('focus', ':input', function () {
        $(this).attr('autocomplete', 'off');
    }).on('paste drop', ':input', function () {
        return false;
    });

    InitDatePickerOptions();

    var sesion = handleSesion.Get();

    if (sesion != null) {

        EjecutaConsulta.GetSite(Global.LoginController.Name, Global.LoginController.GetClaveEncript, false)
            .then(response => {

                Global.ClaveEncriptacion = response.Elemento;

                var decrypted = CryptoJS.AES.decrypt(sesion, Global.ClaveEncriptacion);
                var obj = $.parseJSON(decrypted.toString(CryptoJS.enc.Utf8));
                DataSesion = obj;
                console.log(DataSesion);
                $("#lbNombreUsuario").html(obj.NOMBRE);
            });
    }

    $(".cerrarSesion").on("click", function () {

        EjecutaConsulta.GetSite(Global.LoginController.Name, Global.LoginController.CerrarSesion, false)
            .then(response => {
                var objRemember = localStorage.getItem(Global.ObjRemember);
                localStorage.clear();
                localStorage.setItem(Global.ObjRemember, objRemember);
                location.href = location.href = `/${Global.UrlBack}/Login/Login`;
            });

    });

});

//Función que cierra el modal de carga de transacciones
function ShowLoading() {
    $('.loading').show();
}

//Función que cierre el modal de carga de transacciones
function CloseLoading() {
    $('.loading').hide();
}

function ReplaceAll(text, busca, reemplaza) {
    while (text.toString().indexOf(busca) != -1)
        text = text.toString().replace(busca, reemplaza);
    return text;
}

//Objeto que provee de metodos para realizar peticiones get y post a los metodos en los controladores
var EjecutaConsulta = function () {
    return {
        Get: function (controller, action, showMessage = true) {
            ShowLoading();
            return new Promise(resolve => {
                $.ajax({
                    type: 'GET',
                    url: `${Global.UrlApi}/${controller}/${action}`,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    headers: { "Authorization": localStorage.getItem(Global.Token) },
                    success: res => {
                        if (res) {
                            if (!res.EsError) {
                                resolve(res);
                            }
                            else {
                                AlertToast('error', res.Mensaje);
                            }
                        }
                    },
                    error: (xhr) => {
                        let tipoAlerta = null;
                        if (xhr.status == 404) tipoAlerta = 'warning';
                        if (xhr.status == 500 || xhr.status == 400 || xhr.status == 401) tipoAlerta = 'error';
                        if (showMessage || xhr.status == 400 || xhr.status == 500) {
                            AlertToast(tipoAlerta ? tipoAlerta : 'error', 'error', xhr.responseText);
                        }
                        CloseLoading();
                        resolve(false);
                    }
                });

            });
        },
        PostAuth: function (controller, action, params = null, showMessage = true) {
            ShowLoading();
            return new Promise(resolve => {

                $.ajax({
                    type: 'POST',
                    url: `${Global.UrlApi}/${controller}/${action}`,
                    data: JSON.stringify(params),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: res => {
                        if (res) {
                            if (!res.EsError) {
                                resolve(res);
                            }
                            else {
                                AlertToast('error', res.Mensaje);
                            }
                        }
                    },
                    error: (xhr, ajaxOptions, thrownError) => {
                        let tipoAlerta = null;
                        if (xhr.status == 404) tipoAlerta = 'warning';
                        if (xhr.status == 500 || xhr.status == 400 || xhr.status == 401) tipoAlerta = 'error';
                        if (showMessage || xhr.status == 400 || xhr.status == 500) {
                            AlertToast(tipoAlerta ? tipoAlerta : 'error', 'error', thrownError);
                        }
                        CloseLoading();
                        resolve(false);
                    }
                });

            });
        },
        Post: function (controller, action, params = null, showMessage = true) {
            ShowLoading();
            return new Promise(resolve => {

                $.ajax({
                    type: 'POST',
                    url: `${Global.UrlApi}/${controller}/${action}`,
                    data: JSON.stringify(params),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    headers: { "Authorization": 'Bearer ' + localStorage.getItem(Global.Token) },
                    success: res => {
                        if (res) {
                            if (!res.EsError) {
                                resolve(res);
                            }
                            else {
                                AlertToast('error', res.Mensaje);
                            }
                        }
                    },
                    error: (xhr, ajaxOptions, thrownError) => {
                        let tipoAlerta = null;
                        if (xhr.status == 404) tipoAlerta = 'warning';
                        if (xhr.status == 500 || xhr.status == 400 || xhr.status == 401) tipoAlerta = 'error';
                        if (showMessage || xhr.status == 400 || xhr.status == 500) {
                            AlertToast(tipoAlerta ? tipoAlerta : 'error', 'error', xhr.responseText);
                        }
                        CloseLoading();
                        resolve(false);
                    }
                });

            });
        },
        PostSite: function (controller, action, params = null, showMessage = true) {
            ShowLoading();
            return new Promise(resolve => {

                $.ajax({
                    type: 'POST',
                    url: `/${Global.UrlBack}/${controller}/${action}`,
                    data: JSON.stringify({ data: params }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    //headers: { "Authorization": 'Bearer ' + localStorage.getItem('Token') },
                    success: res => {
                        if (res) {
                            if (!res.EsError) {
                                resolve(res);
                            }
                            else {
                                AlertToast('error', res.Mensaje);
                            }
                        }
                    },
                    error: (xhr, ajaxOptions, thrownError) => {
                        let tipoAlerta = null;
                        if (xhr.status == 404) tipoAlerta = 'warning';
                        if (xhr.status == 500 || xhr.status == 400 || xhr.status == 401) tipoAlerta = 'error';
                        if (showMessage || xhr.status == 400 || xhr.status == 500) {
                            AlertToast(tipoAlerta ? tipoAlerta : 'error', 'error', xhr.responseText);
                        }
                        CloseLoading();
                        resolve(false);
                    }
                });

            });
        },
        GetSite: function (controller, action, showMessage = true) {
            ShowLoading();
            return new Promise(resolve => {

                $.ajax({
                    type: 'GET',
                    url: `/${Global.UrlBack}/${controller}/${action}`,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: res => {
                        if (res) {
                            if (!res.EsError) {
                                resolve(res);
                            }
                            else {
                                AlertToast('error', res.Mensaje);
                            }
                        }
                    },
                    error: (xhr, ajaxOptions, thrownError) => {
                        let tipoAlerta = null;
                        if (xhr.status == 404) tipoAlerta = 'warning';
                        if (xhr.status == 500 || xhr.status == 400 || xhr.status == 401) tipoAlerta = 'error';
                        if (showMessage || xhr.status == 400 || xhr.status == 500) {
                            AlertToast(tipoAlerta ? tipoAlerta : 'error', 'error', xhr.responseText);
                        }
                        CloseLoading();
                        resolve(false);
                    }
                });

            });
        }
    }
}();

//Objeto de funciones que se utlizan para el control de los datos de sesion del usuario una vez esté logueado en el sistema
var handleSesion = function () {

    return {
        Set: function (value) {
            localStorage.setItem(Global.ObjSesion, value);
        },
        Get: function () {
            var value = localStorage.getItem(Global.ObjSesion);
            return (IsNull(value) == null) ? null : value;
        }
    }

}();

//Función que retorna nulo en caso de que el objeto no tenga valor o sea indefinido
function IsNull(value) {
    if (value == '' || value == null || value == undefined || value == NaN || value == 'null')
        return null;
    else
        return value;
}

//Obtiene el valor de un input
function GetInputValue(id) {
    return $('#' + id).val();
}

function AlertToast(type, message) {
    switch (type) {
        case 'error':
            toastr.error(message);
            break;
        case 'info':
            toastr.info(message);
            break;
        case 'success':
            toastr.success(message);
            break;
        case 'warning':
            toastr.warning(message);
            break;
    }
}

function InitDatePickerOptions() {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi\xE9rcoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi\xE9', 'Juv', 'Vie', 'S\xE1b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S\xE1'],
        weekHeader: 'Sm',
        dateFormat: 'dd-mm-yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
}

//Función que renderiza los nombres de las columnas de encabezado de un datatable pasado por parametro
function SetColumnsTable(headerTable = []) {
    return new Promise(resolve => {
        var columns = "";
        headerTable.forEach((element, index) => {
            element.Id = element.Id ? element.Id : '';
            columns += `<th style="min-width: ${element.MinWidth}px !important; max-width: ${element.MaxWidth}px !important;" class="${element.Id}${element.ClassName}">${element.Nombre}</th>`;
            if (index == headerTable.length - 1)
                resolve(`<tr>${columns}</tr>`);
        });
    });
}

//Obtiene el valor de un input check
function GetCheckValue(id) {
    return ($('#' + id).is(":checked")) ? true : false;
}

//Asigna un valor aun input o combo box
function SetInputValue(inputType, inputId, inputValue) {
    if (inputType.toLowerCase() == 'text') {
        $(`#${inputId}`).val(inputValue);
    }
    if (inputType.toLowerCase() == 'select') {
        $(`#${inputId}`).val(inputValue).trigger('chosen:updated');
    }
    if (inputType.toLowerCase() == 'file') {
        $(`#${inputId}`).val(inputValue);
    }
    if (inputType.toLowerCase() == 'check') {
        $(`#${inputId}`).prop('checked', inputValue == 1 ? true : false)
    }
}

/*****************************************************************************************************/

let Route = {
    Login: "/",
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


var months = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];

