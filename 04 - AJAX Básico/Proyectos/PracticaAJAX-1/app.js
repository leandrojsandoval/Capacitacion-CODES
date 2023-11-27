$(document).ready(function () {
    $("#categoria").change(function () {
        var categoriaSeleccionada = $(this).val();
        $.post("myHandler.ashx", { categoria: categoriaSeleccionada }, function (data) {
            var $seleccionadorCorrespondiente = $("<select>").html(data);
            $("#resultado").empty().append($seleccionadorCorrespondiente);
        }).fail(function () {
            $("#resultado").empty().text("Error en la solicitud AJAX");
        });
    });
});