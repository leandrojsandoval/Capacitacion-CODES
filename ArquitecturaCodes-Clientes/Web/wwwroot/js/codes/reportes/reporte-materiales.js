vueAppParams.data.gridData = [];

vueAppParams.data.breadcrums = [
	{ text: 'Reportes', disabled: false, href: '/Material/Listado' },
	{ text: 'Reporte de Materiales', disabled: true, href: '' }
];

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

// Mounted
vueAppParams.mounted = function () {
	this.loadGrid();
};

vueAppParams.methods.loadGrid = function () {

	$.ajax({
		url: "/Reportes/ObtenerReporteMateriales",
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

/*
//CONSTANTES
const TODOS = "todos";
ESTADO_TODOS = 0;

// Modelo

vueAppParams.data.dialog = null;
vueAppParams.data.itemDelete = "";
vueAppParams.data.loadingMateriales = true;
vueAppParams.data.nombre = '';
vueAppParams.data.descripcion = '';
vueAppParams.data.multiplicadorToneladas = '';
vueAppParams.data.activo = 'true';
vueAppParams.data.filtros = { nombre: vueAppParams.data.nombre, descripcion: vueAppParams.data.descripcion, multiplicadorToneladas: vueAppParams.data.multiplicadorToneladas, activo: vueAppParams.data.activo };

// Metodos
vueAppParams.methods.onClickNuevo = function (event) {
	window.location = "Detalle/";
};

vueAppParams.methods.onClickLimpiarFiltros = function (filterName) {

	if (filterName == TODOS) {

		vueAppParams.data.filtros.nombre = '';
		vueAppParams.data.filtros.descripcion = '';
		vueAppParams.data.filtros.multiplicadorToneladas = '';
		vueAppParams.data.filtros.activo = 'true';
	}
	else {

		if (filterName) {
			if (filterName == 'activo') {
				vueApp.filtros[filterName] = '0';
			} else {
				vueApp.filtros[filterName] = '';
			}
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
*/