using Entidades;
using Framework.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/* LS: Misma clase identica que DatosProducto, solo que la utilizo para traerme 
 * todos los elementos de la tabla Categoria. (PUNTO 4) 
 *
 * MODIFICADO: Cambio consulta que se encontraba en un string a un SP dentro del 
 * metodo ObtenerCategorias() */

namespace Datos.Implementaciones {
    public class DatosCategoria {
        
        readonly SqlConnection connection;
        readonly string connstr;

        public DatosCategoria () {
            connstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connstr);
        }

        public List<Categoria> ObtenerCategorias () {
            
            List<Categoria> registrosEncontrados = new List<Categoria>();
                        
            using (SqlCommand command = new SqlCommand(Constantes.SP_CATEGORIA_OBTENER, connection)) {

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read()) {

                    registrosEncontrados.Add(new Categoria {
                        Id = Convert.ToInt32(reader["ID_CATEGORIA"]),
                        Descripcion = reader["DESCRIPCION"].ToString(),
                    });
                
                }

            }

            return registrosEncontrados;

        }

    }

}
