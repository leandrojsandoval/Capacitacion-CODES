using Entidades;
using Entidades.Filtros;
using Framework.Common;
using Integrador.Web.ViewModels;
using Integrador.Web.ViewModels.Categoria;
using Integrador.Web.ViewModels.Producto;
using Resources;
using Servicios.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Integrador.Controllers {
    public class ProductoController : Controller {

        private ServicioProducto servicioProducto;

        /* LS: Agrego este atributo de servicio ya que lo voy a utilizar tanto para los formularios
         * como para cargar las categorias en la grilla de productos. (PUNTO 4) */

        private ServicioCategoria servicioCategoria;

        public ProductoController () {

            servicioProducto = new ServicioProducto();
            servicioCategoria = new ServicioCategoria();

        }

        /* MODIFICADO: Utilizo el mismo model para cargar las categorias, ya no utilizo mas ViewBag
         * Para eso anteriomente las guardo en el atributo Categorias del model. 
         *
         * Por otro lado, ProductoViewModel tiene su atributo CategoriaViewModel, que es el ID y 
         * su descripcion para mostrarlos en la grilla. */

        public ActionResult Index () {
            try {
                List<Categoria> categorias = servicioCategoria.ObtenerCategorias();
                ProductoViewModel model = new ProductoViewModel {
                    Categoria = new CategoriaViewModel(),
                    Categorias = categorias.Select(categoria => new CategoriaViewModel {
                        Id = categoria.Id,
                        Descripcion = categoria.Descripcion
                    }).ToList()
                };
                return View(model);
            }
            catch (Exception) {
                ErrorViewModel errorModel = new ErrorViewModel(Global.ErrorPantalla);
                return View(Constantes.NAME_VIEW_ERROR, errorModel);
            }
        }

        /* LS: Agrego verificacion de Precio y Categoria. Ademas de agregar estos elementos al objeto de la grilla. (PUNTO 4) */

        public JsonResult BuscarProducto (ProductoViewModel listaVM) {

            JsonGridData jsonGridData = new JsonGridData();

            try {

                /* LS: La region de validaciones paso a un metodo para que se controlen las necesarias en la misma. (PUNTO 1)*/

                ValidacionDeAtributosEnLista(listaVM);

                /* LS : Separa la obtencion de los productos en la grilla en otro metodo para que sea mas legible el codigo (PUNTO 1)
                 * El metodo, recibe la lista view model y el total como variable out.
                 * https://www.educative.io/answers/what-is-the-out-parameter-in-c-sharp */

                GrillaProductoViewModel grillaProductoViewModel = ObtenerElementosEnLaGrilla(listaVM, out int total);

                /* LS: Tambien la carga de los datos del objeto JSON pasaron a un metodo, el cual
                 * los siguientes parametros son necesarios para su construccion. */

                ConstruirJsonGridData(jsonGridData, grillaProductoViewModel, listaVM, total);

            }
            catch (Exception e) {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = e.Message });
            }
            return Json(jsonGridData, JsonRequestBehavior.AllowGet);
        }

        /* LS: Arreglado el PUNTO 3, faltaba especificar que el metodo sea [HttpPost]
         * para que justamente pueda aceptar solicitudes POST. Ahora un producto puede
         * eliminarse correctamente de la base de datos. 
         *
         * En el metodo se encontraba bastante codigo duplicado, por lo tanto, realice
         * un switch en donde se puedan separar la menor cantidad de sentencias posibles.
         * 
         * 
         * Ademas, agrego el string en el archivo de recursos (PUNTO 1)
         * SePudoRealizarLaInsercion
         * SePudoRealizarLaActualizacion
         * SePudoRealizarLaEliminacion
         * NoSePudoEncontrarElProducto
         * SePudoRealizarLaOperacion
         *
         * Cambio SePudoRealizarLaOperacion a SePudoRealizarLaInsercion ya que era muy generico */

        [HttpPost]
        public JsonResult GestionProducto (string tipo, string id, string descripcion, string precio, string idCategoria) {

            JsonData jsonData = new JsonData();

            try {

                int resultadoOperacion = Constantes.OPERACION_FALLIDA;

                switch (tipo) {

                    /* MODIFICADO: Para cualquiera de las operaciones que se realicen, separe cada una en metodos
                     * para que se encuentre mejor organizado. Estos metodos devuelven un resultado de operacion */

                    case Constantes.NEW:
                        resultadoOperacion = GestionarNuevoProducto(descripcion, precio, idCategoria);
                        break;

                    case Constantes.UPDATE:
                        resultadoOperacion = GestionarActualizacionProducto(id, descripcion, precio, idCategoria);
                        break;

                    case Constantes.DELETE:
                        resultadoOperacion = GestionarEliminacionProducto(id);
                        break;

                }

                /* MODIFICADO: Segun que resultadoOperacion tenga, sera lo que voy a cargar en el jsonData */

                ConstruirJsonData(jsonData, resultadoOperacion);

            }
            catch (Exception) {
                jsonData.content = Global.ErrorGenerico;
                jsonData.result = JsonData.Result.Error;
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        /*********************************** Metodos Privados ***********************************/

        private void ValidacionDeAtributosEnLista (ProductoViewModel listaVM) {

            if (string.IsNullOrEmpty(listaVM.Descripcion) || listaVM.Descripcion.Equals(Global.Null) || listaVM.Descripcion.Equals("Todos"))
                listaVM.Descripcion = string.Empty;

            if (!listaVM.Precio.HasValue)
                listaVM.Precio = null;

            if (listaVM.Categoria == null) {
                listaVM.Categoria = new CategoriaViewModel();
            }
            else if (!listaVM.Categoria.Id.HasValue) {
                listaVM.Categoria.Id = null;
            }

        }

        /* MODIFICADO: La descripcion del a categoria ahora viene desde el mismo SP y ya no necesito ningun otro metodo
         * para conseguir el nombre, como tenia anteriormente. */

        private GrillaProductoViewModel ObtenerElementosEnLaGrilla (ProductoViewModel listaVM, out int total) {

            GrillaProductoViewModel grillaProductoViewModel = new GrillaProductoViewModel {
                Items = servicioProducto.ObtenerPaginado(
                    listaVM.Descripcion, listaVM.Precio, listaVM.Categoria.Id,
                    listaVM.sidx, listaVM.sord, listaVM.page, listaVM.rows, out total).
                    Select(producto => new GrillaProductoViewModel.ItemViewModel() {
                        ID = producto.Id.ToString(),
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio,
                        IDCategoria = producto.Categoria.Id.ToString(),
                        Categoria = producto.Categoria.Descripcion
                    }).ToList()
            };

            return grillaProductoViewModel;
        }

        private void ConstruirJsonGridData (JsonGridData jsonGridData, GrillaProductoViewModel grillaProductoViewModel, ProductoViewModel listaVM, int total) {

            jsonGridData.rows = grillaProductoViewModel.Items;
            jsonGridData.total = (total / listaVM.rows) + (total % listaVM.rows > 0 ? 1 : 0);
            jsonGridData.records = total;
            jsonGridData.page = listaVM.page;
            jsonGridData.rowNum = listaVM.rows;

        }

        /* AGREGADO: Metodo que carga todo el contenido necesario en el jsonData segun el resultado de la operacion.
         * Se agregaron algunas cadenas mas en el paquete de idiomas. */

        private void ConstruirJsonData (JsonData jsonData, int resultadoOperacion) {

            switch (resultadoOperacion) {

                case Constantes.INSERCION_REALIZADA:
                    jsonData.content = Global.SePudoRealizarLaInsercion;
                    break;

                case Constantes.MODIFACION_REALIZADA:
                    jsonData.content = Global.SePudoRealizarLaActualizacion;
                    break;

                case Constantes.ELIMINACION_REALIZADA:
                    jsonData.content = Global.SePudoRealizarLaEliminacion;
                    break;

                case Constantes.PRODUCTO_NO_ENCONTRADO:
                    jsonData.content = Global.NoSePudoEncontrarElProducto;
                    break;

                case Constantes.PRODUCTO_ENCONTRADO:
                    jsonData.content = Global.SeEncontroElProducto;
                    break;

                case Constantes.FORMATO_INVALIDO:
                    jsonData.content = Global.NoSePusoRealizarElFormateo;
                    break;

                default:
                    jsonData.content = Global.NoSePudoRealizarLaOperacion;
                    break;

            }

            jsonData.result = resultadoOperacion == Constantes.INSERCION_REALIZADA ||
                resultadoOperacion == Constantes.MODIFACION_REALIZADA ||
                resultadoOperacion == Constantes.ELIMINACION_REALIZADA ?
                JsonData.Result.Ok : JsonData.Result.Error;

        }

        /* AGREGADO: Metodo para verificar si el producto se encontraba en la base de datos (En caso de insercion) 
         * O que trajera todos los datos por si la operacion era una modificacion o eliminacion (El producto existe)
         * Si devuelve null es porque no se encontro. */

        private Producto BuscarProductoExistente (string id, string descripcion) {

            IList<Producto> productos = servicioProducto.ObtenerPorFiltro(
                !string.IsNullOrEmpty(id) && int.TryParse(id, out int idInt) ?
                    new ProductoFiltro() { Id = idInt } :
                    new ProductoFiltro() { Descripcion = descripcion }
            );

            return productos.FirstOrDefault();

        }

        /* AGREGADO: Es la misma seccion que antes cuando se encontraba en el metodo principal con algunas
         * modificaciones: Realizo un control para el precio y el idCategoria para pasarlos a enteros y
         * Agrego la creacion del objeto Categoria pasandole el ID correspondiente. */

        private int GestionarNuevoProducto (string descripcion, string precio, string idCategoria) {

            Producto productoEncontrado = BuscarProductoExistente(null, descripcion);

            if (productoEncontrado != null)
                return Constantes.PRODUCTO_ENCONTRADO;

            if (!int.TryParse(precio, out int precioInt) || !int.TryParse(idCategoria, out int idCategoriaInt))
                return Constantes.FORMATO_INVALIDO;

            Producto producto = new Producto {
                Descripcion = descripcion,
                Precio = precioInt,
                Categoria = new Categoria {
                    Id = idCategoriaInt
                }
            };

            servicioProducto.Insertar(producto);

            return Constantes.INSERCION_REALIZADA;

        }

        private int GestionarActualizacionProducto (string id, string descripcion, string precio, string idCategoria) {

            Producto productoEncontrado = BuscarProductoExistente(id, null);

            if (productoEncontrado == null)
                return Constantes.PRODUCTO_NO_ENCONTRADO;

            if (!int.TryParse(precio, out int precioInt) || !int.TryParse(idCategoria, out int idCategoriaInt))
                return Constantes.FORMATO_INVALIDO;

            productoEncontrado.Id = int.Parse(id);
            productoEncontrado.Descripcion = descripcion;
            productoEncontrado.Precio = precioInt;
            productoEncontrado.Categoria.Id = idCategoriaInt;

            servicioProducto.Actualizar(productoEncontrado);

            return Constantes.MODIFACION_REALIZADA;

        }

        private int GestionarEliminacionProducto (string id) {
            
            Producto productoEncontrado = BuscarProductoExistente(id, null);

            if (productoEncontrado == null)
                return Constantes.PRODUCTO_NO_ENCONTRADO;

            
            servicioProducto.Borrar(productoEncontrado.Id);
            
            return Constantes.ELIMINACION_REALIZADA;
        }

    }

}