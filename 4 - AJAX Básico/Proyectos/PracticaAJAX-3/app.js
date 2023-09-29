var datos = [];
$("<div>").attr("id", "resultado").appendTo("body");

function getAll() {
    return datos;
}

function addNewItem(elem) {
    datos.push(elem);
}

$("#formulario").on("submit", function (event) {
    event.preventDefault();

    var nombreAux = $("#nombre").val();
    var apellidoAux = $("#apellido").val();
    var edadAux = $("#edad").val();
    var dniAux = $("#dni").val();
    var emailAux = $("#email").val();

    if (emailAux.indexOf("@") === -1) {
        alert("El campo de correo electronico es invalido");
        return;
    }

    var formObj = {
        nombre: $.trim(nombreAux),
        apellido: $.trim(apellidoAux),
        edad: $.trim(edadAux),
        dni: $.trim(dniAux),
        email: $.trim(emailAux)
    };

    var formJSON = JSON.stringify(formObj);

    $.post("handler.ashx", formJSON).done(function (data, status) {
        var responseObj = JSON.parse(data);
        $("#resultado").css("display", "block");
        if (responseObj.result === -1) {
            alert(responseObj.message);
        } else {
            addNewItem(responseObj);
            $("#resultado").empty().text("{ \"result\": 0, \"message\": \"\" }");
        }
    }).fail(function (jqXHR, textStatus, errorMessage) {
        var responseObj = JSON.parse(jqXHR.responseText);
        $("#resultado").empty().text(responseObj.message);
    });
});