//CONSTANTES
const TODOS = "todos";
ESTADO_TODOS = 0;

/*************************************** Modelo ***************************************/

// Inicializacion de propiedades del objeto vueAppParams.data
vueAppParams.data.gridData = [];
vueAppParams.data.dialog = null;
vueAppParams.data.itemDelete = "";
vueAppParams.data.loadingVentas = true;
vueAppParams.data.filtrosVacios = { fecha: '', descripcion: '', cantidad: '', total: "", idCliente: "", nombreCliente: "" , activo: 'true' };
vueAppParams.data.filtros = { ...vueAppParams.data.filtrosVacios };
vueAppParams.data.breadcrums = [
    { text: jsglobals.Ventas, disabled: false, href: '/Venta/Listado' },
    { text: jsglobals.Listado, disabled: true, href: '' }
];

// Array de encabezados de tabla
vueAppParams.data.headers = [
    { value: 'idVenta', align: ' d-none' },
    {
        text: jsglobals.Fecha, value: 'fecha', width: '150px', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: jsglobals.Descripcion, value: 'descripcion', width: '200px', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: jsglobals.Cantidad, value: 'cantidad', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: jsglobals.Total, value: 'total', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: jsglobals.Cliente, value: 'nombreCliente', align: 'start', sortable: true,
        class: 'acerbrag-headers',
    },
    {
        text: 'Estado', value: 'activo', align: 'start', sortable: true,
        class: 'acerbrag-headers'
    },
    {
        text: 'Acciones', value: 'actions', align: 'start', sortable: false,
        class: 'acerbrag-headers'
    }
];

/*************************************** Mounted ***************************************/

// Carga la grilla cuando se monta el componente Vue
vueAppParams.mounted = function () {
    this.loadGrid();
};

/*************************************** Metodos ***************************************/

// onClickNuevo - Redirige a una nueva ubicación.
vueAppParams.methods.onClickNuevo = function (event) {
    window.location = "Detalle/";
};

// onClickLimpiarFiltros - Borra los filtros y vuelve a cargar la grilla.
vueAppParams.methods.onClickLimpiarFiltros = function (filterName) {
    if (filterName == TODOS) {
        vueApp.filtros = { ...vueApp.filtrosVacios };
    }
    else if (filterName) {
        vueApp.filtros[filterName] = vueApp.filtrosVacios[filterName];
    }
    this.loadGrid();
};

// loadGrid - Envía una solicitud AJAX para recuperar datos de clientes y setea gridData.
vueAppParams.methods.loadGrid = function () {
    $.ajax({
        url: "/Venta/ObtenerVentas",
        data: {
            descripcion: vueAppParams.data.filtros.descripcion,
            fecha: vueAppParams.data.filtros.fecha,
            cantidad: vueAppParams.data.filtros.cantidad,
            total: vueAppParams.data.filtros.total,
            idCliente: vueAppParams.data.filtros.idCliente,
            activo: vueAppParams.data.filtros.activo
        },
        method: "POST",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.loadingVentas = false;
        },
        error: defaultErrorHandler
    })
};

// onClickInactivar - Establece una bandera de diálogo y actualiza un elemento para ser eliminado.
vueAppParams.methods.onClickInactivar = function (item) {
    vueAppParams.data.dialog = true;
    vueAppParams.data.itemDelete = item;
};

// onClickConfirmaBorrar - Envía una solicitud AJAX para marcar a un cliente como inactivo.
vueAppParams.methods.onClickConfirmaBorrar = function (idVenta) {
    vueApp.clearErrors();
    vueApp.submiting = true;
    vueAppParams.data.dialog = false;
    $.ajax({
        url: "/Venta/Inactivar?ventaId=" + idVenta,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosEliminadosOk);
            setTimeout(function () { window.location = '/Venta/Listado' });
        },
        error: defaultErrorHandler
    });
};

// onClickEditar - Redirige a la página de detalles del cliente.
vueAppParams.methods.onClickEditar = function (idVenta) {
    window.location = "/Venta/Detalle/" + idVenta;
};

// onClickExportar - Envía una solicitud AJAX para exportar datos de clientes como un blob y activa una descarga.
vueAppParams.methods.onClickExportar = function () {
    var filtros =
        "?descripcion=" + vueApp.filtros.descripcion +
        "&fecha=" + vueApp.filtros.fecha +
        "&cantidad=" + vueApp.filtros.cantidad +
        "&total=" + vueApp.filtros.total +
        "&idCliente=" + vueApp.filtros.idCliente +
        "&activo=" + vueApp.filtros.activo;
    return new Promise(resolve => {
        var urlToSend = "/Venta/Exportar" + filtros;
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
            var fileName = "Venta_" + fechaLocalSlash;
            var link = document.createElement("a");
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();
        };
        req.send();
        resolve(req.status);
    });
};
