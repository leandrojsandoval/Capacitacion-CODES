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

    $.ajax({
        type: "POST",
        url: "HandlerPost.ashx",
        dataType: "text",
        data: formJSON,
        success: function (respuesta) {
            $("#tablaContainer").hide();
            $("#mensajeRespuesta").show().text(respuesta);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorMessage);
        }
    });
});