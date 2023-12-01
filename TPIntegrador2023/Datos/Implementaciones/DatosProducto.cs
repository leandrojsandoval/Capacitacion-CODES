using Entidades;
using Entidades.Filtros;
using Framework.Common;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/* LS: Agrego los parametros de Precio y Categoria tanto para los 
 * metodos de Insertar y Actualizar. 
 * 
 * En ObtenerPorFiltro agrego los controles de null para el Precio y Categoria.
 * Con el operador ? hace que un entero pueda ser nulo utilizando HasValue
 * y Value.
 *
 * Al metodo ObtenerPaginado se agregaron los parametros de precio y el de idCategoria
 * pasados luego como parametros al stored procedure. (PUNTO 4) 
 * MODIFICADO: Agrego a este mismo metodo la creacion de un objeto Categoria dentro
 * de producto. (Ver el metodo FromDataRow de la clase Producto) */

namespace Datos.Implementaciones {
    public class DatosProducto {
        
        readonly SqlConnection connection;
        readonly string connstr;

        public DatosProducto () {
            connstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connstr);
        }

        public void Insertar (Producto p) {

            using (SqlCommand command = new SqlCommand(Constantes.SP_PRODUCTO_INSERT, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("descripcion", p.Descripcion);
                command.Parameters.AddWithValue("precio", p.Precio);
                command.Parameters.AddWithValue("idCategoria", p.Categoria.Id);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Actualizar (Producto p) {

            using (SqlCommand command = new SqlCommand(Constantes.SP_PRODUCTO_ACTUALIZAR, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("id", p.Id);
                command.Parameters.AddWithValue("descripcion", p.Descripcion);
                command.Parameters.AddWithValue("precio", p.Precio);
                command.Parameters.AddWithValue("idCategoria", p.Categoria.Id);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Borrar (int id) {
            using (SqlCommand command = new SqlCommand(Constantes.SP_PRODUCTO_DELETE, connection)) {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Producto> ObtenerPorFiltro (ProductoFiltro pFiltro) {

            List<Producto> registrosEncontrados = new List<Producto>();

            using (SqlCommand command = new SqlCommand(Constantes.SP_PRODUCTO_OBTENER_POR_FILTRO, connection)) {

                command.CommandType = CommandType.StoredProcedure;

                if (pFiltro.Id.HasValue)
                    command.Parameters.AddWithValue("id", pFiltro.Id.Value);

                if (!string.IsNullOrEmpty(pFiltro.Descripcion))
                    command.Parameters.AddWithValue("descripcion", pFiltro.Descripcion);

                if (pFiltro.Precio.HasValue) {
                    command.Parameters.AddWithValue("precio", pFiltro.Precio.Value);
                }

                if (pFiltro.IdCategoria.HasValue) {
                    command.Parameters.AddWithValue("idCategoria", pFiltro.IdCategoria.Value);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(command)) {
                    
                    DataTable dt = new DataTable();
                    
                    da.Fill(dt);
                    
                    if (dt != null) {
                        
                        Producto producto = null;
                        
                        foreach (DataRow dataRow in dt.Rows) {

                            producto = new Producto {
                                Categoria = new Categoria()
                            };

                            registrosEncontrados.Add(producto.FromDataRow(dataRow));
                        
                        }
                    
                    }
                
                }
            
            }
            
            return registrosEncontrados;
        
        }

        public List<Producto> ObtenerPaginado (string descripcion, int? precio, int? idCategoria, string sidx, string sord, int page, int rows, out int total) {

            List<Producto> registrosEncontrados = new List<Producto>();

            using (SqlCommand command = new SqlCommand(Constantes.SP_PRODUCTO_OBTENER_PAGINADO, connection)) {

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("intPagNumero", page);
                command.Parameters.AddWithValue("intPagTamano", rows);

                if (!string.IsNullOrEmpty(descripcion))
                    command.Parameters.AddWithValue("descripcion", descripcion);

                if (precio.HasValue) {
                    command.Parameters.AddWithValue("precio", precio.Value);
                }

                if (idCategoria.HasValue) {
                    command.Parameters.AddWithValue("idCategoria", idCategoria.Value);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(command)) {

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    if (dt != null) {
                        Producto producto = null;
                        foreach (DataRow dataRow in dt.Rows) {
                            producto = new Producto {
                                Categoria = new Categoria()
                            };
                            registrosEncontrados.Add(producto.FromDataRow(dataRow));
                        }
                    }

                }

                total = registrosEncontrados.Count;

            }

            return registrosEncontrados;

        }
    
    }

}
