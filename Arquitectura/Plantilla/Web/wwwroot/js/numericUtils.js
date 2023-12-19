vueAppParams.methods.formatoDecimal = function (numero, minimumFractionDigits = 2) {
	return numero ? numero.toLocaleString('es-AR', { minimumFractionDigits: minimumFractionDigits }) : ''
};