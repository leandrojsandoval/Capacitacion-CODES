function cargarDatos() {
    console.log('Cargando datos...');
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            //document.getElementById("texto1").innerHTML = this.responseText;
            document.getElementById("texto1").value = this.responseText;
        }
    };
    xhttp.open("GET", "data.txt", true);
    xhttp.send();
}

function cargarDatosManejador01() {
    console.log('Cargando datos desde manejador de servidor...');
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var t = document.createTextNode(this.responseText + " " + Math.random());
            var h = document.createElement("span");
            h.appendChild(t);
            var container = document.getElementById("container");
            container.innerHTML = null;
            container.appendChild(h);
        }
    };
    // Para enviar parametros, utilizo POST, no GET
    xhttp.open("POST", "test.ashx", true);
    xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    var dataToSend = "parametro1=P1&parametro2=P2";
    xhttp.send(dataToSend);
}