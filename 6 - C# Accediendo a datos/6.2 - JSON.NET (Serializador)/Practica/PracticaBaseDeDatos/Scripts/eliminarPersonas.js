$("#eliminarPersonas").on("click", function () {
    $.ajax({
        method: "GET",
        dataType: "text",
        url: "HandlerDeleteTable.ashx",
        success: function (respuesta) {
            $("#tablaContainer").hide();
            $("#mensajeRespuesta").show().text(respuesta);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $("#mensajeRespuesta").text(errorMessage);
        }
    })
});