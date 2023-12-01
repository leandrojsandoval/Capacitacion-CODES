/* LS: Agrego esta clase Categoria para luego manejarlo en el controlador. (PUNTO 4) 
 *
 * MODIFICADO: Agrego el metodo FromDataRow para que lo reciba el metodo de la clase
 * Producto */

using System.Data;

namespace Entidades {
    public class Categoria {

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public Categoria FromDataRow (DataRow dataRow) {

            Categoria entidad = new Categoria();

            if (dataRow.Table.Columns.Contains("ID_CATEGORIA"))
                entidad.Id = (int)dataRow["ID_CATEGORIA"];

            if (dataRow.Table.Columns.Contains("CATEGORIA"))
                entidad.Descripcion = dataRow["CATEGORIA"].ToString();

            return entidad;
        }

    }
}
