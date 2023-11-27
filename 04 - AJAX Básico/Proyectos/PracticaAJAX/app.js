function cargarDatosManejador() {

    var nombre = document.getElementById("nombre").value;
    var apellido = document.getElementById("apellido").value;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var textResponse = document.createTextNode(this.responseText);

            var spanElement = document.createElement("span");
            spanElement.appendChild(textResponse);

            var container = document.getElementById("container");
            container.innerHTML = null;
            container.appendChild(spanElement);
        }
    };

    xhttp.open("POST", "saludoHandler.ashx", true);
    xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    var dataToSend = "nombre=" + nombre + "&apellido=" + apellido;
    xhttp.send(dataToSend);
} 
