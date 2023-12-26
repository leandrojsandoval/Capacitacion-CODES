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
    public class DatosMateriales : DatosBase, IDatosMateriales {

        public DatosMateriales (ARQContext context) : base(context) { }

        public IList<Material> ObtenerMateriales (string nombre, string descripcion, double? multiplicador, bool? activo) {
            
            List<Material> materiales = new();
            
            try {
                using (SqlCommand command = new(Constantes.SP_MATERIALES_OBTENER, (SqlConnection)_context.Database.GetDbConnection())) {
                    
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("nombre", nombre);
                    command.Parameters.AddWithValue("descripcion", descripcion);
                    command.Parameters.AddWithValue("multiplicador", multiplicador.HasValue? multiplicador : null);
                    command.Parameters.AddWithValue("activo", activo.HasValue? activo : null);

                    using (SqlDataAdapter da = new(command)) {
                        
                        DataTable dt = new();
                        
                        da.Fill(dt);

                        if (dt != null) {

                            if (dt.Rows.Count > 0) {

                                materiales = new();

                                foreach (DataRow row in dt.Rows) {
                                    materiales.Add(FromDataRow(row));
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception) {
                throw;
            }

            return materiales;
        }

        private Material FromDataRow (DataRow row) {

            Material material = new();

            if (row.Table.Columns.Contains("ID_MATERIAL_RODILLO")) {
                material.Id = int.Parse(row["ID_MATERIAL_RODILLO"].ToString());
            }

            if (row.Table.Columns.Contains("NOMBRE")) {
                material.Nombre = row["NOMBRE"].ToString();
            }

            if (row.Table.Columns.Contains("DESCRIPCION")) {
                material.Descripcion = row["DESCRIPCION"].ToString();
            }

            if (row.Table.Columns.Contains("MULTIPLICADOR_TONELADAS")) {
                material.MultiplicadorToneladas = int.Parse(row["MULTIPLICADOR_TONELADAS"].ToString());
            }

            if (row.Table.Columns.Contains("ACTIVO")) {
                material.Activo = row["ACTIVO"].ToString() == "True";
            }

            if (row.Table.Columns.Contains("FECHA_ALTA")) {
                material.FechaAlta = DateTime.Parse(row["FECHA_ALTA"].ToString());
            }

            return material;

        }

    }

}
