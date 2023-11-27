function cargarDatos() {
    console.log('Cargado datos...');
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            //document.getElementById("texto1").innerHTML =  this.responseText;
            document.getElementById("texto1").value = this.responseText;
        }
    };
    xhttp.open("GET", "data.txt", true);
    xhttp.send();
}