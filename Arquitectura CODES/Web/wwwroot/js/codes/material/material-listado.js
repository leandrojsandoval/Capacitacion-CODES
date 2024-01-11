//CONSTANTES
const TODOS = "todos";
ESTADO_TODOS = 0;

// Modelo
vueAppParams.data.breadcrums = [
    { text: jsglobals.Materiales, disabled: false, href: '/Material/Listado' },
    { text: jsglobals.Listado, disabled: true, href: '' }
];
vueAppParams.data.dialog = null;
vueAppParams.data.filtrosVacios = { nombre: '', descripcion: '', multiplicadorToneladas: '', activo: 'true' };
vueAppParams.data.filtros = { ...vueAppParams.data.filtrosVacios };
vueAppParams.data.gridData = [];
vueAppParams.data.headers = [
    { value: 'idMaterial', align: ' d-none' },
    {
        text: jsglobals.Nombre, value: 'nombre', width: '150px', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: jsglobals.Descripcion, value: 'descripcion', width: '300px', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: jsglobals.MultiplicadorToneladas, value: 'multiplicadorToneladas', align: 'center',
        class: 'acerbrag-headers'
    },
    {
        text: 'Estado', value: 'activo', align: 'center', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: 'Acciones', value: 'actions', align: 'start', sortable: false,
        class: 'acerbrag-headers'
    }
];
vueAppParams.data.itemDelete = "";
vueAppParams.data.loadingMateriales = true;

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid();
};

// Metodos
vueAppParams.methods.loadGrid = function () {

    $.ajax({
        url: "/Material/ObtenerMateriales",
        data: {
            nombre: vueAppParams.data.filtros.nombre,
            descripcion: vueAppParams.data.filtros.descripcion,
            multiplicador: vueAppParams.data.filtros.multiplicadorToneladas,
            activo: vueAppParams.data.filtros.activo
        },
        method: "POST",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.loadingMateriales = false;

        },
        error: defaultErrorHandler
    })
};

vueAppParams.methods.onClickNuevo = function () {
    window.location = "Detalle/";
};

vueAppParams.methods.onClickLimpiarFiltros = function (filterName) {

    if (filterName == TODOS) {
        vueApp.filtros = { ...vueApp.filtrosVacios };
    }
    else {

        if (filterName) {
            vueApp.filtros[filterName] = vueApp.filtrosVacios[filterName];
        }
    }

    this.loadGrid();
};

vueAppParams.methods.onClickInactivar = function (item) {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemDelete = item;
};

vueAppParams.methods.onClickConfirmaBorrar = function (idMaterial) {

    vueApp.clearErrors();
    vueApp.submiting = true;
    vueAppParams.data.dialog = false;

    $.ajax({
        url: "/Material/Inactivar?materialId=" + idMaterial,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosEliminadosOk);
            setTimeout(function () { window.location = '/Material/Listado' });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.onClickEditar = function (idMaterial) {
    window.location = "/Material/Detalle/" + idMaterial;
};

vueAppParams.methods.onClickExportar = function () {

    var filtros = "?nombre=" + vueApp.filtros.nombre + "&descripcion=" + vueApp.filtros.descripcion
        + "&multiplicadorToneladas=" + vueApp.filtros.multiplicadorToneladas + "&activo=" + vueApp.filtros.activo;


    return new Promise(resolve => {

        var urlToSend = "/Material/Exportar" + filtros;
        var req = new XMLHttpRequest();
        req.open("GET", urlToSend, true);
        req.responseType = "blob";
        req.onload = function (event) {
            var blob = req.response;
            if (req.status == HTTP_ERROR) {
                vueApp.notification.showError(jsglobals.ErrorGenerico);
                return;
            }
            var fecha = new Date();
            var fechaLocal = fecha.toLocaleDateString();
            var fechaLocalSlash = fechaLocal.replaceAll("/", "-")
            var fileName = "Materiales_" + fechaLocalSlash;
            var link = document.createElement("a");
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();

        };

        req.send()

        resolve(req.status);
    });
};

vueAppParams.methods.onClickVistaPrueba = function () {
    window.location = "Prueba/";
};