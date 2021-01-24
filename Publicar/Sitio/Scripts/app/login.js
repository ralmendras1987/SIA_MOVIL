$(document).ready(function () {

    handlePagesStates.init();

    EjecutaConsulta.GetSite(Global.LoginController.Name, Global.LoginController.GetClaveEncript, false)
        .then(response => {
            
            Global.ClaveEncriptacion = response.Elemento;
            var objRemember = localStorage.getItem(Global.ObjRemember);
            if (IsNull(objRemember) != null) {
                var decrypted = CryptoJS.AES.decrypt(objRemember, Global.ClaveEncriptacion);
                var obj = $.parseJSON(decrypted.toString(CryptoJS.enc.Utf8));

                if (obj.REMEMBER) {
                    SetInputValue('text', 'txtUsuario', obj.USER);
                    SetInputValue('text', 'txtClave', obj.PASS);
                    SetInputValue('check', 'chkRecordarUser', obj.REMEMBER);
                }
            }

        });

});

var handlePagesStates = function () {

    function init() {
        $("#CodigoPantalla").html(Global.CodigoPantalla.Login);
        handleButtonsEvents();
    }

    function handleButtonsEvents() {

        $("#btnIngresar").on("click", function (e) {

            var usuario = IsNull(GetInputValue('txtUsuario'));
            var clave = IsNull(GetInputValue('txtClave'));

            if (usuario != null && clave != null)
            {
                var params = {
                    USER_DATA: {
                        USER: GetInputValue('txtUsuario'),
                        PASS: GetInputValue('txtClave')
                    }
                }
                EjecutaConsulta.PostAuth(Global.LoginController.Name, Global.LoginController.ValidarLogin, params, true)
                    .then(response => {
                        if (response.Resultado) {

                            localStorage.setItem(Global.Token, response.Elemento.TOKEN);

                            delete response.Elemento.USER_DATA.ERROR_DSC;
                            delete response.Elemento.USER_DATA.ERROR_ID;
                            delete response.Elemento.USER_DATA.PASS;

                            var encrypted1 = CryptoJS.AES.encrypt(JSON.stringify(response.Elemento.USER_DATA), Global.ClaveEncriptacion);
                            handleSesion.Set(encrypted1);

                            var chkRecordarUser = GetCheckValue('chkRecordarUser');
                            var objRemember = {
                                USER: usuario,
                                PASS: clave,
                                REMEMBER: chkRecordarUser
                            }
                            var encrypted2 = CryptoJS.AES.encrypt(JSON.stringify(objRemember), Global.ClaveEncriptacion);
                            localStorage.setItem(Global.ObjRemember, encrypted2);

                            var params = {
                                TOKEN: response.Elemento.TOKEN
                            }
                            EjecutaConsulta.PostSite('Login', 'SeteaTokenSession', params, false)
                                .then(result => {
                                    null;
                                    if (result.Resultado)
                                        location.href = `/${Global.UrlBack}/Home/Index`;
                                });
                        }
                        else {
                            AlertToast('warning', response.Mensaje);
                        }
                    });
            }
            else {
                AlertToast('warning', 'Ingrese usuario / contraseña válidas');
            }

        });

    }

    return {
        init: init
    }

}();