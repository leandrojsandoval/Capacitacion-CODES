using System.Data;

namespace API_Cinema.Entidades {
    public class Horario {

        public int Id { get; set; }
        public int IdPelicula { get; set; }
        public int IdSucursal { get; set; }
        public string Hora { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public Horario FromDataRow(DataRow dataRow) {

            Horario entidad = new Horario();

            if (dataRow.Table.Columns.Contains("Id"))
                entidad.Id = (int)dataRow["Id"];

            if (dataRow.Table.Columns.Contains("IdPelicula"))
                entidad.IdPelicula = (int)dataRow["IdPelicula"];

            if (dataRow.Table.Columns.Contains("IdSucursal"))
                entidad.IdSucursal = (int)dataRow["IdSucursal"];

            if (dataRow.Table.Columns.Contains("Hora"))
                entidad.Hora = dataRow["Hora"].ToString();

            if (dataRow.Table.Columns.Contains("FechaCreacion"))
                entidad.FechaCreacion = (DateTime)dataRow["FechaCreacion"];

            if (dataRow.Table.Columns.Contains("FechaActualizacion"))
                entidad.FechaActualizacion = (DateTime)dataRow["FechaActualizacion"];

            return entidad;
        }

    }

}
