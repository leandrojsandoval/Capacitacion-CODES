$("<div>").attr("id", "resultadoValidacion").appendTo("body");

$("<ul>").attr("id", "listaPersonas").appendTo("body");

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

    $.ajax({
        type: "POST",
        url: "HandlerPost.ashx",
        dataType: 'json',
        data: formJSON,
        success: function (respuesta) {
            if (respuesta.result === 0) {
                $("#listaPersonas").hide();
                $("#resultadoValidacion").show();

                $("#resultadoValidacion").text("result: " + respuesta.result + '; message: ' + respuesta.message);
            } else {
                alert("result: " + respuesta.result + '; message: ' + respuesta.message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
           alert(errorMessage);
        }
    });
});

$("#listarPersonas").on("click", function () {
    $.ajax({
        type: "GET",
        url: "HandlerGet.ashx",
        success: function (data) {

            $("#listaPersonas").show();
            $("#resultadoValidacion").hide();

            var personas = JSON.parse(data);
            var lista = $("#listaPersonas");
            lista.empty();
            personas.forEach(function (persona) {
                var campo = $("<li>").text(persona.Nombre + " " + persona.Apellido + " " + persona.Edad + " " + persona.Dni + " " + persona.Email);
                lista.append(campo);
            });
        },
        error: function (jqXhr, textStatus, errorMessage) {
            alert(errorMessage);
        }
    });
});

$("#borrarDatos").on("click", function () {
    $.ajax({
        method: "POST",
        url: "HandlerDeleteTable.ashx",
        success: function (respuesta) {

            $("#listaPersonas").hide();
            $("#resultadoValidacion").show();

            $("#resultadoValidacion").text(respuesta);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $("#resultadoValidacion").text(errorMessage);
        }
    })
});