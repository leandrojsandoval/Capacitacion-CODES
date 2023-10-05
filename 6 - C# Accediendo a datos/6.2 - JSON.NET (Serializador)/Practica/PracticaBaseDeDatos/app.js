$("<div>").attr("id", "resultadoValidacion").appendTo("body");

$("<ul>").attr("id", "listaPersonas").appendTo("body");

// Enviar Datos - Los datos que son validos se guardan en la BD

$("#formulario").on("submit", function (event) {
    event.preventDefault();

    // Se obtienen los valores de los campos del formulario HTML
    var nombreAux = $("#nombre").val();
    var apellidoAux = $("#apellido").val();
    var edadAux = $("#edad").val();
    var dniAux = $("#dni").val();
    var emailAux = $("#email").val();

    // Verifico que el email tenga el caracter de @, caso contrario finalizo
    if (emailAux.indexOf("@") === -1) {
        alert("El campo de correo electronico es invalido");
        return;
    }

    // Creo un objeto con los datos del formulario
    var formObj = {
        nombre: $.trim(nombreAux),
        apellido: $.trim(apellidoAux),
        edad: $.trim(edadAux),
        dni: $.trim(dniAux),
        email: $.trim(emailAux)
    };

    // Convierto el objeto en una cadena JSON
    var formJSON = JSON.stringify(formObj);

    $.ajax({
        method: "POST",
        url: "HandlerPost.ashx",
        data: formJSON,
        success: function (responseData) {
            if (responseData.resul === -1) {
                // Entra por aca si algunas validaciones del handler son invalidas
                alert(responseData.message);
            } else {
                // Caso contrario muestro el mensaje de que los campos fueron ingresado a la BD
                $("#resultadoValidacion").empty().text(responseData.message);
            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            // Entra por aca si ocurre algun error al realizar la solicitud
            $("#resultadoValidacion").empty().text(errorMessage);
        }
    });
});

// Listar Peronas - Trae los registros de la tabla Persona de la BD

$("#listarPersonas").on("click", function () {
    $.ajax({
        type: "GET",
        url: "HandlerGet.ashx",
        success: function (data) {
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

// Borrar Datos - Eliminar todos los registros de la tabla Persona en la BD

$("#borrarDatos").on("click", function () {
    $.ajax({
        method: "POST",
        url: "HandlerDeleteTable.ashx",
        success: function (dataRequest) {
            $("#resultadoValidacion").text(dataRequest);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $("#resultadoValidacion").text(errorMessage);
        }
    })
});