using ARQ.Datos.EFScafolding;
using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace ARQ.Datos.Implementacion {
    public class DatosClientes : DatosBase, IDatosClientes {

        public DatosClientes (ARQContext context) : base(context) { }

        public IList<Cliente> ObtenerClientes (string nombre, string apellido, DateTime? fechaNacimiento, bool? activo) {

            List<Cliente> clientes = new();

            try {
                using (SqlCommand command = new(Constantes.SP_CLIENTES_OBTENER, (SqlConnection)_context.Database.GetDbConnection())) {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("nombre", nombre);
                    command.Parameters.AddWithValue("apellido", apellido);
                    command.Parameters.AddWithValue("fechaNacimiento", fechaNacimiento.HasValue ? fechaNacimiento : null);
                    command.Parameters.AddWithValue("activo", activo.HasValue ? activo : null);

                    using (SqlDataAdapter da = new(command)) {

                        DataTable dt = new();

                        da.Fill(dt);

                        if (dt != null) {

                            if (dt.Rows.Count > 0) {

                                clientes = new();

                                foreach (DataRow row in dt.Rows) {
                                    clientes.Add(FromDataRow(row));
                                }

                            }

                        }

                    }

                }

            }
            catch (Exception) {
                throw;
            }
            return clientes;
        }

        private Cliente FromDataRow (DataRow row) {

            Cliente cliente = new();

            if (row.Table.Columns.Contains("ID_CLIENTE")) {
                cliente.Id = int.Parse(row["ID_CLIENTE"].ToString());
            }

            if (row.Table.Columns.Contains("NOMBRE")) {
                cliente.Nombre = row["NOMBRE"].ToString();
            }

            if (row.Table.Columns.Contains("APELLIDO")) {
                cliente.Apellido = row["APELLIDO"].ToString();
            }

            if (row.Table.Columns.Contains("FECHA_NACIMIENTO")) {
                cliente.FechaNacimiento = DateTime.Parse(row["FECHA_NACIMIENTO"].ToString());
            }

            if (row.Table.Columns.Contains("ACTIVO")) {
                cliente.Activo = row["ACTIVO"].ToString() == "True";
            }

            if (row.Table.Columns.Contains("FECHA_ALTA")) {
                cliente.FechaAlta = DateTime.Parse(row["FECHA_ALTA"].ToString());
            }

            return cliente;

        }

    }
}
