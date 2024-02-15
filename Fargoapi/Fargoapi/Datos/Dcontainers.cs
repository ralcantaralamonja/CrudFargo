using Fargoapi.Conexion;
using Fargoapi.Modelo;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

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
                                mcontenedores.idTipo = contenedores.GetInt32(contenedores.GetOrdinal("idTipo"));
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

        public async Task<Mcontenedor?> ListarContenedor(int id)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadena()))
                {
                    await sql.OpenAsync();

                    using (var cmd = new SqlCommand("usp_ListarContenedor", sql))  
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);  

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var contenedor = new Mcontenedor();
                                contenedor.idContenedor = reader.GetInt32(reader.GetOrdinal("idContenedor"));
                                contenedor.numContenedor = reader.GetInt32(reader.GetOrdinal("numContenedor"));
                                contenedor.TipContenedor = reader.GetString(reader.GetOrdinal("Tipo"));
                                contenedor.idTipo = reader.GetInt32(reader.GetOrdinal("idTipo"));
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

        public async Task<bool> InsertarContenedor(Mcontenedor nuevoContenedor)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadena()))
                {
                    await sql.OpenAsync();

                    using (var cmd = new SqlCommand("usp_InsertarContenedor", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@numCon", nuevoContenedor.numContenedor);
                        cmd.Parameters.AddWithValue("@idTipo", nuevoContenedor.idTipo);
                        cmd.Parameters.AddWithValue("@tamCon", nuevoContenedor.tamContenedor);
                        cmd.Parameters.AddWithValue("@pesCon", nuevoContenedor.pesoContenedor);
                        cmd.Parameters.AddWithValue("@tarCon", nuevoContenedor.taraContenedor);

                        await cmd.ExecuteNonQueryAsync();
                        return true;
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
                throw new Exception("Error desconocido al insertar el contenedor", ex);
            }
        }


        public async Task<bool> ActualizarContenedor(Mcontenedor contenedorActualizado)
        {
            try
            {
                using (var sql = new SqlConnection(cn.cadena()))
                {
                    await sql.OpenAsync();

                    using (var cmd = new SqlCommand("usp_ActualizarContenedor", sql))
                    { 
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", contenedorActualizado.idContenedor);
                        cmd.Parameters.AddWithValue("@numCont", contenedorActualizado.numContenedor);
                        cmd.Parameters.AddWithValue("@IdTipo", contenedorActualizado.idTipo);
                        cmd.Parameters.AddWithValue("@tamCont", contenedorActualizado.tamContenedor);
                        cmd.Parameters.AddWithValue("@pesCont", contenedorActualizado.pesoContenedor);
                        cmd.Parameters.AddWithValue("@taraCont", contenedorActualizado.taraContenedor);

                        await cmd.ExecuteNonQueryAsync();
                        return true;
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
                throw new Exception("Error desconocido al actualizar el contenedor", ex);
            }
        }



    }


}
