// Codigo de los ejemplos
var myGlobalArray = [];

// Cada ejemplo estara contenido dentro de una funcion

function test01() {
    console.log("Ejecutando Test01...", new Date());
    var valor1 = parseInt(document.getElementById("txt1").value);
    var valor2 = parseInt(document.getElementById("txt2").value);
    var valor3 = valor1 + valor2;
    document.getElementById("txt3").value = valor3;
}

function updateSummary() {
    var nuevoMensaje = myGlobalArray.length + ' items en la lista';
    document.getElementById('messageSummary').innerHTML = nuevoMensaje;
}

// Armado de la lista en base a elementos de HTML
function test02() {
    // Tomo la referencia de la lista
    var lista = document.getElementById('lstItems');
    var newItem = document.createElement('li');     // li: list item
    // El valor del texto
    var newValue = document.getElementById('txtComentario').value;
    // Un ID (Unico) cualquiera ...
    newItem.setAttribute('id', Math.random());
    var isStrangeItem = newValue.indexOf('strange');
    if (isStrangeItem > -1) {
        // Si se da la condicion (Cualquiera, en este caso, que contenga la palabra strange) aplico la clase de estilo
        newItem.setAttribute('class', 'strange-item');
    }
    newItem.appendChild(document.createTextNode(newValue));
    lista.appendChild(newItem);
    myGlobalArray.push(newItem);
    updateSummary();
}

// Actualizo la lista en pantalla
function updateListOnScreen() {
    var lista = document.getElementById('lstItems');
    lista.innerHTML = null;     // Elimino cualquier item anterior, si lo hubiera

    // Por cada item del array, armo la lista en pantalla
    myGlobalArray.map(function (item, index) {
        var newItem = document.createElement('li'); // li: list item
        var newValue = item.comment;                // Valor del objeto global (El array)
        newItem.setAttribute('id', item.id);
        // Atributo HTML customizado (No es parte del estandar, pero esta permitido)
        newItem.setAttribute('created-date-time', item.createdDateTime);
        var isStrangeItem = newValue.indexOf('strange');
        if (isStrangeItem > -1) {
            // Si se da la condicion (Cualquiera, en este caso, que contenga la palabra strange) aplico la clase de estilo
            newItem.setAttribute('class', 'strange-item');
        }
        newItem.appendChild(document.createTextNode(newValue));
        lista.appendChild(newItem);
    });
}

function validateItem(item) {
    // El item debe tener un ID
    if (item.id == 0)
        return false;
    // El item debe tener una descripcion
    if (item.comment == '')
        return false;
    return true;    // Por defecto ...
}

function clearValidation() {
    document.getElementById('messageValidation').innerHTML = null;
}

function updateValidation() {
    document.getElementById('messageValidation').innerHTML = 'El item no es valido';
}

// Armado de la lista en base a un array global
// Acumulamos los objetos en el array y en base a su contenido mostramos los datos

function test03() {
    // Objeto JSON
    var newItem = {
        id: Math.random(),
        comment: document.getElementById('txtComentario').value,
        createdDateTime: new Date()
    };
    var isValid = validateItem(newItem);
    if (isValid) {
        clearValidation();
        // El item es valido, puedo proseguir
        myGlobalArray.push(newItem);
        updateListOnScreen();
        updateSummary();
    }
    else {
        // El item NO es valido, muestro mensaje de validacion
        updateValidation();
    }
}