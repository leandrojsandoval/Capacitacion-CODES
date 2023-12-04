using System.Data;
using System.Data.SqlClient;

namespace API_Cinema.Entidades {

    public class Pelicula {

        public int Id { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public Pelicula FromDataRow(DataRow dataRow) {

            Pelicula entidad = new Pelicula();

            if (dataRow.Table.Columns.Contains("Id"))
                entidad.Id = (int)dataRow["Id"];

            if (dataRow.Table.Columns.Contains("Imagen"))
                entidad.Imagen = dataRow["Imagen"].ToString();

            if (dataRow.Table.Columns.Contains("Nombre"))
                entidad.Nombre = dataRow["Nombre"].ToString();

            if (dataRow.Table.Columns.Contains("Descripcion"))
                entidad.Descripcion = dataRow["Descripcion"].ToString();

            if (dataRow.Table.Columns.Contains("FechaCreacion"))
                entidad.FechaCreacion = (DateTime)dataRow["FechaCreacion"];

            if (dataRow.Table.Columns.Contains("FechaActualizacion"))
                entidad.FechaActualizacion = (DateTime)dataRow["FechaActualizacion"];

            return entidad;
        }

        public Pelicula FromDataReader(SqlDataReader reader) {
            Pelicula entidad = new Pelicula();

            if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                entidad.Id = reader.GetInt32(reader.GetOrdinal("Id"));

            if (!reader.IsDBNull(reader.GetOrdinal("Imagen")))
                entidad.Imagen = reader.GetString(reader.GetOrdinal("Imagen"));

            if (!reader.IsDBNull(reader.GetOrdinal("Nombre")))
                entidad.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));

            if (!reader.IsDBNull(reader.GetOrdinal("Descripcion")))
                entidad.Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));

            if (!reader.IsDBNull(reader.GetOrdinal("FechaCreacion")))
                entidad.FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"));

            if (!reader.IsDBNull(reader.GetOrdinal("FechaActualizacion")))
                entidad.FechaActualizacion = reader.GetDateTime(reader.GetOrdinal("FechaActualizacion"));

            return entidad;
        }

    }

}
