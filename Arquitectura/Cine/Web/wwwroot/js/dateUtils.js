vueAppParams.methods.formatoFecha = function (fecha) {
	return fecha ? moment(fecha).format('DD/MM/YYYY') : ''
};

vueAppParams.methods.primerDiaMes = function () {
	return new Date(new Date().getFullYear(), new Date().getMonth(), 1).toISOString().substr(0, 10);
};

vueAppParams.methods.ultimoDiaMes = function () {
	return new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).toISOString().substr(0, 10);
};

vueAppParams.methods.hoy = function () {
	return new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()).toISOString().substr(0, 10);
};



