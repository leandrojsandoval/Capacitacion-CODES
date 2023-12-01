namespace Framework.Common
{
    public class Constantes
    {
        public const string NAME_VIEW_ERROR = "Error";
        public const string TODOS = "Todos";
        public const string SORT_ORDER_ASC = "asc";
        public const string SORT_ORDER_DESC = "desc";

        /* LS: Agrego constantes como buenas practicas para el manejo de la gestion del producto (PUNTO 1) */

        public const string NEW = "N";
        public const string UPDATE = "U";
        public const string DELETE = "D";

        /* MODIFICADO: Agrego distintos codigo de operacion que son usados en el controller. */

        public const int OPERACION_FALLIDA = -1;
        public const int OPERACION_EXITOSA = 0;
        public const int INSERCION_REALIZADA = 1;
        public const int MODIFACION_REALIZADA = 2;
        public const int ELIMINACION_REALIZADA = 3;
        public const int PRODUCTO_NO_ENCONTRADO = 4;
        public const int FORMATO_INVALIDO = 5;
        public const int PRODUCTO_ENCONTRADO = 6;

        #region PRODUCTO - STORED PROCEDURE

        /* LS: Cambio los nombres de los procedures con mis iniciales */

        public const string SP_PRODUCTO_INSERT = "sp_insert_productos_ls";
        public const string SP_PRODUCTO_ACTUALIZAR = "sp_update_productos_ls";
        public const string SP_PRODUCTO_DELETE = "sp_delete_productos_ls";
        public const string SP_PRODUCTO_OBTENER_POR_FILTRO = "sp_obtener_por_filtro_productos_ls";
        public const string SP_PRODUCTO_OBTENER_PAGINADO = "sp_obtener_productos_ls";
        
        /* MODIFICADO: Agrego el SP para obtener categorias. 
         * Este es llamado en la clase DatosCategoria*/
        public const string SP_CATEGORIA_OBTENER = "sp_obtener_categorias_ls";

        #endregion
    }
}
