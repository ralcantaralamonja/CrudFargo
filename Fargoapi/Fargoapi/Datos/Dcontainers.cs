using Fargoapi.Conexion;
using Fargoapi.Modelo;
using System.Data.SqlClient;
using System.Data;

namespace Fargoapi.Datos
{
    public class Dcontainers
    {
        ConexionDB cn = new ConexionDB();

 

        public async Task<List<Mcontenedor>> ListarContenedores()
        {
            var lista = new List<Mcontenedor>();

            try
            {
                using (var sql = new SqlConnection(cn.cadena()))
                {
                    await sql.OpenAsync();

                    using (var cmd = new SqlCommand("usp_ListarContenedores", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var contenedores = await cmd.ExecuteReaderAsync())
                        {
                            while (await contenedores.ReadAsync())
                            {
                                var mcontenedores = new Mcontenedor();
                                mcontenedores.idContenedor = contenedores.GetInt32(contenedores.GetOrdinal("idContenedor"));
                                mcontenedores.numContenedor = contenedores.GetInt32(contenedores.GetOrdinal("numContenedor"));
                                mcontenedores.TipContenedor = contenedores.GetString(contenedores.GetOrdinal("Tipo"));
                                mcontenedores.tamContenedor = contenedores.GetInt32(contenedores.GetOrdinal("tamContenedor"));

                                if (!contenedores.IsDBNull(contenedores.GetOrdinal("pesoContenedor")))
                                {
                                    mcontenedores.pesoContenedor = contenedores.GetDouble(contenedores.GetOrdinal("pesoContenedor"));
                                }

                                if (!contenedores.IsDBNull(contenedores.GetOrdinal("taraContenedor")))
                                {
                                    mcontenedores.taraContenedor = contenedores.GetDouble(contenedores.GetOrdinal("taraContenedor"));
                                }

                                lista.Add(mcontenedores);
                            }
                        }
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
               
                throw new Exception("Error al ejecutar la consulta SQL", ex);
            }
            catch (InvalidOperationException ex)
            {
                 
                throw new Exception("Error de operación no válida", ex);
            }
            catch (Exception ex)
            {
             
                throw new Exception("Error desconocido al listar contenedores", ex);
            }
        }

        public async Task<Mcontenedor> ObtenerContenedorPorId(int id)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadena()))
                {
                    await sql.OpenAsync();

                    using (var cmd = new SqlCommand("usp_ListarContenedor", sql))  
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id); // Suponiendo que el nombre del parámetro en el procedimiento almacenado es @id

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var contenedor = new Mcontenedor();
                                contenedor.idContenedor = reader.GetInt32(reader.GetOrdinal("idContenedor"));
                                contenedor.numContenedor = reader.GetInt32(reader.GetOrdinal("numContenedor"));
                                contenedor.TipContenedor = reader.GetString(reader.GetOrdinal("Tipo"));
                                contenedor.tamContenedor = reader.GetInt32(reader.GetOrdinal("tamContenedor"));

                                if (!reader.IsDBNull(reader.GetOrdinal("pesoContenedor")))
                                {
                                    contenedor.pesoContenedor = reader.GetDouble(reader.GetOrdinal("pesoContenedor"));
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("taraContenedor")))
                                {
                                    contenedor.taraContenedor = reader.GetDouble(reader.GetOrdinal("taraContenedor"));
                                }

                                return contenedor;
                            }
                            else
                            {
                                return null; // El contenedor no se encontró
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al ejecutar la consulta SQL", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error de operación no válida", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error desconocido al obtener el contenedor", ex);
            }
        }




    }


}
