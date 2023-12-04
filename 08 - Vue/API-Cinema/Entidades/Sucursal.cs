using System.Data;

namespace API_Cinema.Entidades {
    public class Sucursal {
        public int Id {  get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public Sucursal FromDataRow(DataRow dataRow) {

            Sucursal entidad = new Sucursal();

            if (dataRow.Table.Columns.Contains("Id"))
                entidad.Id = (int)dataRow["Id"];

            if (dataRow.Table.Columns.Contains("Nombre"))
                entidad.Nombre = dataRow["Nombre"].ToString();

            if (dataRow.Table.Columns.Contains("Precio"))
                entidad.Precio = (decimal)dataRow["Precio"];

            if (dataRow.Table.Columns.Contains("FechaCreacion"))
                entidad.FechaCreacion = (DateTime)dataRow["FechaCreacion"];

            if (dataRow.Table.Columns.Contains("FechaActualizacion"))
                entidad.FechaActualizacion = (DateTime)dataRow["FechaActualizacion"];

            return entidad;
        }

    }
}
