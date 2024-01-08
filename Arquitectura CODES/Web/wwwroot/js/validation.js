//Las rules deben evaluar en true o el mensaje de error
vueAppParams.data.vrules = {
	required: v => (!!v && v != "") || "El campo es requerido",
	maxLength: (v, l, txt) => (!!v && (v + "").length <= l) || "El campo " + txt + " no debe superar los " + l + " caracteres",
	number: v => parseInt(v) || "Debe ingresar un número",
	numerico: v => (v && new RegExp('^[0-9]*$').test(v)) || jsglobals.SoloNumeros,
	mayorQueCero: v => (v && v != 0) || "El campo debe ser mayor a 0",
	mayorOIgualCero: v => (v>= 0) || "El campo debe ser mayor o igual a 0",
	decimalesConPunto: v => (new RegExp('^[0-9]+([.][0-9]+)?$').test(v)) || jsglobals.FormatoDecimal,
	mayorQueCeroONulo: v => (!v || v > 0) || jsglobals.CampoMayorQueCero,
	mayorOIgualQueCeroONulo: v => (!v || v >= 0) || jsglobals.CampoMayorQueCero,
	decimalesConPuntoONulo: v => (!v || new RegExp('^(-)?[0-9]+([.][0-9]+)?$').test(v)) || jsglobals.FormatoDecimal,
	aMayorQueBONulo: (v, b) => (!v || parseFloat(v) <= parseFloat(b)) || jsglobals.NoSuperarMaximo,
	aMenorQueBONulo: (v, b) => (!v || parseFloat(v) >= parseFloat(b)) || jsglobals.NoInferiorMinimo,
	aMayorQueB: (v, b) => (parseFloat(v) <= parseFloat(b)) || jsglobals.NoSuperarMaximo,
	aMenorQueB: (v, b) => (parseFloat(v) >= parseFloat(b)) || jsglobals.NoInferiorMinimo,
	alertaMayorLimite: (v, b) => ( (!v && !b) || parseFloat(v) < parseFloat(b)) || "La Alerta es mayor al Límite",
	limiteMenorAlerta: (v, b) => ((!v && !b) || parseFloat(v) > parseFloat(b)) || "El Límite es menor a la Alerta",
};