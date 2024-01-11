// Modelo

vueAppParams.data.activarMaterial = true;
vueAppParams.data.breadcrums = [];
vueAppParams.data.dialog = null;
vueAppParams.data.isValid = false;
vueAppParams.data.itemCambio = "";
vueAppParams.data.submiting = false;

//Mounted

vueAppParams.mounted = function () {
    //Breadcrums
    this.breadcrums = [
        { text: jsglobals.Materiales, disabled: false, href: '/Material/Listado' },
        { text: this.model.idMaterial > 0 ? jsglobals.Editar : jsglobals.Nuevo, href: '/Material/Detalle', disabled: true }
    ];
};

// Handlers

vueAppParams.methods.isDisabled = function () {
    if (this.model.idMaterial == null)
        return true;
};

vueAppParams.methods.onChangeCambioEstado = function () {
    vueAppParams.data.dialog = true;
    vueAppParams.data.itemCambio = this.model;
};

//Metodos

vueAppParams.methods.onClickNoConfirma = function (itemCambio) {
    itemCambio.activo = !itemCambio.activo;
    vueAppParams.data.dialog = false;
}

vueAppParams.methods.onClickGuardar = function () {
    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid)
        return false;

    vueApp.clearErrors();
    vueApp.submiting = true;

    $.ajax({
        url: "/Material/Guardar",
        method: "POST",
        data: vueApp.model,
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
            setTimeout(function () { window.location = '/Material/Listado' }, TIEMPO_CIERRE_FORMULARIO);
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.onClickVolver = function () {
    window.location = "/Material/Listado";
};
