var datos = [];
$("<div>").attr("id", "datosConfirmados").appendTo("body");

function getAll() {
    return datos;
}

function addNewItem(elem) {
    datos.push(elem);
}

function mostrarDatos() {
    var datos = getAll();
    var ultimoIngreso = datos[datos.length - 1];

    $("#datosConfirmados").empty();

    $("#datosConfirmados").html("Nombre: " + ultimoIngreso.nombre + "<br>" +
        "Apellido: " + ultimoIngreso.apellido + "<br>" +
        "Edad: " + ultimoIngreso.edad + " años" + "<br>" +
        "DNI: " + ultimoIngreso.dni + "<br>" +
        "Email: " + ultimoIngreso.email);

    $("#datosConfirmados").css("display", "block");
}

$("#formulario").on("submit", function (event) {
    event.preventDefault();

    var nombreAux = $("#nombre").val();
    var apellidoAux = $("#apellido").val();
    var edadAux = $("#edad").val();
    var dniAux = $("#dni").val();
    var emailAux = $("#email").val();

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
        if (responseObj.result === -1) {
            alert(responseObj.message);
        } else {
            addNewItem(responseObj);
            mostrarDatos();
            console.log("{ \"result\": 0, \"message\": \"\" }");
        }
    }).fail(function (jqXHR, textStatus, errorMessage) {
        var responseObj = JSON.parse(jqXHR.responseText); // Parsea el JSON en la respuesta
        alert(responseObj.message);
    });
});