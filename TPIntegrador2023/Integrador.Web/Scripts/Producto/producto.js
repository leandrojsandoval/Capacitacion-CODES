$(document).ready(function () {
    $("#divSearch").show();
    $("#divNewUpdate").hide();
    $('#btnExportar').hide();

    cargarHandlers();
    inicializarGrilla();
    btnBuscarClick();
});

function cargarHandlers() {
    $('#btnBuscar').click(btnBuscarClick);
    $('#btnLimpiarFiltros').click(btnLimpiarFiltrosClick);
    $('#btnNuevo').click(btnNuevoClick);
    $('#btnCancelar').click(btnCancelarClick);
    $('#btnAceptar').click(btnAceptarClick);
}

/* AGREGADO DESPUES DE LA ENTREGA
 * LS: Agrego una funcion para limpiar campos ya que se limpian en varias funciones */

function limpiarCampos() {
    $("#hdnTipo").val('');

    $("#txtDescripcion").val('');
    $("#txtDescripcion_NU").val('');

    $("#txtPrecio").val('');
    $("#txtPrecio_NU").val('');

    $("#selectCategoria").val('');
    $("#selectCategoria_NU").val('');

    $("#hiddenId").val('');

}

/* LS: Agrego el campo Precio y Categoria para limpiarlo (PUNTO 4) */

function btnNuevoClick() {
    //Se limpian los campos
    limpiarCampos();

    $("#divSearch").hide();
    $("#divNewUpdate").show();
    $("#hdnTipo").val("N");

    $("#lblNuevo").show();
    $("#lblModificar").hide();
}

/* LS: Idem la funcion anterior. Se limpian los campos de Precio y categoria. (PUNTO 4) */

function btnCancelarClick() {
    //Se limpian los campos
    limpiarCampos();

    $("#divNewUpdate").hide();
    $("#divSearch").show();
}

/* LS: Agrego el Precio y Categoria que fue ingresado/selecionado por el usuario 
 * Ademas de pasarlo a la data de la solicitud AJAX. (PUNTO 4) */

function btnAceptarClick() {

    var tipo = $("#hdnTipo").val();
    var id = $("#hiddenId").val();
    var descripcion = $("#txtDescripcion_NU").val();
    var precio = $("#txtPrecio_NU").val();
    var idCategoria = $("#selectCategoria_NU").val();

    $.ajax({
        type: "POST",
        url: '/Producto/GestionProducto',
        data: { tipo, id, descripcion, precio, idCategoria },
        success: function (data) {
            if (data.result == 0) {
                NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-success", data.content, true);

                //Se limpian los campos
                limpiarCampos();

                $("#divNewUpdate").hide();
                $("#divSearch").show();

                btnBuscarClick();
            }
            else {
                NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-danger", data.content, true);
            }
        },
        error: function (data) {
            NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-danger", "Error de conexión", true);
        }
    });
}

/* LS: Limpio ademas los campos agregados (PUNTO 4) */

function btnLimpiarFiltrosClick() {
    $('#txtDescripcion').val('');
    $("#txtPrecio").val("");
    $("#selectCategoria").val("");
    btnBuscarClick();
}

/* LS: Agrego los parametros de Precio y IDCategoria en la URL (PUNTO 4) 
 * NOTA: LOS PARAMETROS DEBEN RESPETAR TAL CUAL ESTAN DECLARADOS EN ProductoViewModel */

function btnBuscarClick() {

    var searchUrl = "/Producto/BuscarProducto?Descripcion=" + $('#txtDescripcion').val() + "&Precio=" + $("#txtPrecio").val() + "&Categoria.Id=" + $("#selectCategoria").val();

    $('#grilla').jqGrid('setGridParam',
        {
            'url': searchUrl,
            'datatype': 'json',
            'page': 1
        })
        .trigger('reloadGrid');
}

/* LS: Agrego la columna Categoria, tanto el ID como su Descripcion (PUNTO 4)
 * Tambien modifico los valores anteriores que tenia el atributo width para que se encuentre bien formateado. */

function inicializarGrilla() {

    var columns = ['ID', 'Descripción', 'Precio', 'IDCategoria', 'Categoría', 'Acciones'];

    var columnModel = [
        { name: 'ID', index: 'ID', hidden: true },
        { name: 'Descripcion', index: 'Descripcion', resizable: false, fixed: true, align: 'center', width: '315%' },
        { name: 'Precio', index: 'Precio', resizable: false, fixed: true, align: 'center', width: '240%' },
        { name: 'IDCategoria', index: 'IDCategoria', hidden: true },
        { name: 'Categoria', index: 'Categoria', resizable: false, fixed: true, align: 'center', width: '240%' },
        { align: "center", editable: false, sortable: false, resizable: false, fixed: true, align: 'center', width: '179%', formatter: botonFormatter }
    ];

    jqgridDefault("grilla",
        "grillaPaginador",
        '',
        columns,
        columnModel,
        function (jqXHR, textStatus, errorThrown) {
            NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-danger", "Error de conexión", true);
        });
}

/* LS: Agrego el parametro de precio y el de categoria en handlerUpdateCallString. (PUNTO 4) */

function botonFormatter(cellvalue, options, rowObject) {

    var row = "<div>";

    var handlerUpdateCallString = "javascript:handlerUpdate('" +
        rowObject.ID + "," +
        encodeURIComponent(rowObject.Descripcion) + "," +
        rowObject.Precio + "," +
        rowObject.IDCategoria +
        "')";

    botonUpdateString = "<a title='Modificar' href= " + handlerUpdateCallString + "><span class='glyphicon glyphicon-edit'></span></a>";

    row += botonUpdateString;

    var handlerDeleteCallString = "javascript:handlerDelete('" + rowObject.ID + "')";
    botonDeleteString = "<a title='Eliminar' href= " + handlerDeleteCallString + "><span class='glyphicon glyphicon-remove-sign'></span></a>";

    row += botonDeleteString;

    row += "</div>";

    return row;
}

/* LS: Agrego el precio que modifique en botonFormatter (PUNTO 4)
 * Agrego el precio y la categoria para completar el campos. */

function handlerUpdate(valor) {

    var valores = valor.split(",");
    var id = valores[0];
    var descripcion = valores[1];
    var precio = valores[2];
    var idCategoria = valores[3];

    $("#hdnTipo").val('U');

    $("#txtDescripcion_NU").val(descripcion);
    $("#txtPrecio_NU").val(precio);
    $("#selectCategoria_NU").val(idCategoria);

    $("#hiddenId").val(id);
    $("#divSearch").hide();
    $("#divNewUpdate").show();

    $("#lblNuevo").hide();
    $("#lblModificar").show();
}

/* LS : Se corrigio el mensaje de exito ya que anteriormente se encontraba como data.content.mensaje
 * cambiado a solo data.content (PUNTO 1) */

function handlerDelete(valor) {
    $.ajax({
        type: "POST",
        url: '/Producto/GestionProducto',
        data: { tipo: "D", id: valor, descripcion: "", precio: 0, categoria: 0 },
        success: function (data) {
            if (data.result == 0) {
                btnBuscarClick();
                NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-success", data.content, true);
            }
            else {
                NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-danger", data.content, true);
            }
        },
        error: function (data) {
            NotificationUi.CrearNotificationBoxFor($('#notificationBox'), "alert-danger", "Error de conexión", true);
        }
    });
}