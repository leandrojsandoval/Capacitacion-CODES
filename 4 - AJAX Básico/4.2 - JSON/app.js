// JavaScript source code

function test01() {
    // Este es un objeto JSON simple
    var miObjeto = {
        id : Math.random(),
        nombre : "CODES S.A",
        direccion : "Av. Callao 5°A"
    };
    console.log(miObjeto);
}

function cargarArchivo() {
    var httpRequest = new XMLHttpRequest();
    var url = "/data.json";     // Esta URL deberia devolver datos JSON
    // Descarga los datos JSON del servidor.
    httpRequest.onreadystatechange = handleJSON;
    httpRequest.open("GET", url, true);
    httpRequest.send(null);
    function handleJSON() {
        if(httpRequest.readyState == 4) {
            if(httpRequest.status == 200) {
                var jsonData = httpRequest.responseText;
                var response = JSON.parse(jsonData);
                // Response debe ser un objeto JSON valido
                // Y su propiedad "content" debe ser un ARRAY
                var container = document.getElementById('container');
                container.innerHTML = null;
                response.content.map(function (item, index) {
                    var h = document.createElement('div');
                    h.setAttribute('class', 'row-item');
                    var t = document.createTextNode(item.nombre + " ... " + item.apellido);
                    h.appendChild(t);
                    container.appendChild(h);
                });
            }
            else {
                alert("Ocurrio una problema con la URL.");
            }
            httpRequest = null;
        }
    }
 
}
