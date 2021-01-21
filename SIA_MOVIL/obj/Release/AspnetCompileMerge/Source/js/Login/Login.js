
let DATA = null;

$("#BtnLogin").click(function () {

    DATA = {
        USER_DATA: {
            USER: $("#usuario").val(),
            PASS: $("#contraseña").val()
        }
    };

    Login_Service(DATA);

});

$(function () {
    console.log('x');
    SetSessionMenu(null);
    SetSessionUserData(null);
    SetValidation();
});

