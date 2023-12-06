using System.Data;

namespace API_Cinema.Entidades {
    public class Venta {

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdHorario { get; set; }
        public int Cantidad { get; set;}
        public decimal Total { get; set;}
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public Venta FromDataRow(DataRow dataRow) {

            Venta entidad = new Venta();

            if (dataRow.Table.Columns.Contains("Id"))
                entidad.Id = (int)dataRow["Id"];

            if (dataRow.Table.Columns.Contains("IdUsuario"))
                entidad.IdUsuario = (int)dataRow["IdUsuario"];

            if (dataRow.Table.Columns.Contains("IdHorario"))
                entidad.IdHorario = (int)dataRow["IdHorario"];

            if (dataRow.Table.Columns.Contains("Cantidad"))
                entidad.Cantidad = (int)dataRow["Cantidad"];

            if (dataRow.Table.Columns.Contains("Total"))
                entidad.Total = (decimal)dataRow["Total"];

            if (dataRow.Table.Columns.Contains("FechaCreacion"))
                entidad.FechaCreacion = (DateTime)dataRow["FechaCreacion"];

            if (dataRow.Table.Columns.Contains("FechaActualizacion"))
                entidad.FechaActualizacion = (DateTime)dataRow["FechaActualizacion"];

            return entidad;
        }

    }
}
