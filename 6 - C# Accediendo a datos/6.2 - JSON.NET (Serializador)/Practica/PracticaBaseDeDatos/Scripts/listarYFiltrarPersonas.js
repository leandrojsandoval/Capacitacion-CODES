/* Limpia por el filtro que se haya buscado y lista las personas por completo
 * tambien limpia los campos por los que se habia filtrado y llama al evento
 * listarPersonas. 
 * https://api.jquery.com/trigger/ */

$("#eliminarFiltro").on("click", function () {
    $("#nombre-filtro").val("");
    $("#apellido-filtro").val("");
    $("#dni-filtro").val("");
    $("#email-filtro").val("");
    $("#edad-filtro").val("");
    $("#listarPersonas").trigger("click");
});

$(document).ready(cargarListaDespegableDeEdad);

function cargarListaDespegableDeEdad() {
    var edadSelect = $("#edad-filtro");
    for (var i = 18; i <= 65; i++) {
        edadSelect.append($("<option></option>").attr("value", i).text(i + " años"));
    }
}


/* Funciones para manejar el response */

function agregarEncabezados(tabla, persona) {
    var cabezaTabla = tabla.find("thead");
    var encabezado = cabezaTabla.find("tr");
    for (var datoEncabezado in persona) {
        encabezado.append($("<th>").text(datoEncabezado.toUpperCase()));
    }
}

function agregarFilaDeDatos(tabla, persona) {
    var cuerpoTabla = tabla.find("tbody");
    var fila = $("<tr>").appendTo(cuerpoTabla);
    for (var datoCampo in persona) {
        fila.append($("<td>").text(persona[datoCampo]));
    }
}

function limpiarDatos(tabla) {
    tabla.find("thead tr").empty();
    tabla.find("tbody").empty();
}

function manejarRespuestaPersonas(data) {
    var tablaPersonas = $("#tablaPersonas");
    limpiarDatos(tablaPersonas);
    $("#tablaContainer").show();
    $("#mensajeRespuesta").hide();
    var personas = JSON.parse(data);
    if (personas.length > 0) {
        agregarEncabezados(tablaPersonas, personas[0]);
        personas.forEach(function (persona) {
            agregarFilaDeDatos(tablaPersonas, persona);
        });
    }
}

/* Solicitudes AJAX */

$("#listarPersonas").on("click", function () {
    $.ajax({
        type: "GET",
        url: "HandlerGet.ashx",
        success: manejarRespuestaPersonas,
        error: function (jqXhr, textStatus, errorMessage) {
            alert(errorMessage);
        }
    });
});

$("#filtrarDatos").on("click", function () {

    var nombre = $("#nombre-filtro").val();
    var apellido = $("#apellido-filtro").val();
    var dni = $("#dni-filtro").val();
    var email = $("#email-filtro").val();
    var edad = $("#edad-filtro").val();

    $.ajax({
        type: "POST",
        url: "HandlerFiltro.ashx",
        data: { nombre: nombre, apellido: apellido, dni: dni, email: email, edad: edad },
        success: manejarRespuestaPersonas,
        error: function (jqXhr, textStatus, errorMessage) {
            alert(errorMessage);
        }
    });
});
