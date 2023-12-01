using System;
using System.Data;

/* MODIFICADO: Agrego el atributo Categoria (PUNTO 4) ya que anteriormente tenia solo
 * el ID de Categoria como entero. De esta forma estoy realizando composicion. 
 *
 * LS: En el metodo se encontro en la condicion del if
 * dataRow.Table.Columns.Contains("PRCIO")
 * Siendo correcto el string "PRECIO" se realiza la modificacion. (PUNTO 2) 
 * Esta era la causa por la cual, en el listado, la columna de precio estaba en '0'
 *
 * MODIFICADO: Se añade tambien los atributos de la Categoria del Producto llamando
 * a su propio metodo de FromDataRow. (PUNTO 4) */

namespace Entidades {
    public class Producto {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }

        public Categoria Categoria { get; set; }

        public Producto FromDataRow (DataRow dataRow) {

            Producto entidad = new Producto();

            if (dataRow.Table.Columns.Contains("ID_PRODUCTO"))
                entidad.Id = (int)dataRow["ID_PRODUCTO"];

            if (dataRow.Table.Columns.Contains("DESCRIPCION"))
                entidad.Descripcion = dataRow["DESCRIPCION"].ToString();

            if (dataRow.Table.Columns.Contains("PRECIO"))
                entidad.Precio = (int)dataRow["PRECIO"];

            entidad.Categoria = Categoria.FromDataRow(dataRow);

            return entidad;
        }
    
    }
}
