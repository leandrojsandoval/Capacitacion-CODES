using System.Data;

namespace API_Cinema.Entidades {
    public class Usuario {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public bool EsAdministrador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public Usuario FromDataRow(DataRow dataRow) {

            Usuario entidad = new();

            if (dataRow.Table.Columns.Contains("Id"))
                entidad.Id = (int)dataRow["Id"];

            if (dataRow.Table.Columns.Contains("Nombre"))
                entidad.Nombre = dataRow["Nombre"].ToString();

            if (dataRow.Table.Columns.Contains("Email"))
                entidad.Email = dataRow["Email"].ToString();

            if (dataRow.Table.Columns.Contains("Contrasenia"))
                entidad.Contrasenia = dataRow["Contrasenia"].ToString();

            if (dataRow.Table.Columns.Contains("EsAdministrador"))
                entidad.EsAdministrador = (bool)dataRow["EsAdministrador"];

            if (dataRow.Table.Columns.Contains("FechaCreacion"))
                entidad.FechaCreacion = (DateTime)dataRow["FechaCreacion"];

            if (dataRow.Table.Columns.Contains("FechaActualizacion"))
                entidad.FechaActualizacion = (DateTime)dataRow["FechaActualizacion"];

            return entidad;
        }

    }
}
