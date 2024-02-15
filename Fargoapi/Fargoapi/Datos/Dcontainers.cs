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
            catch (Exception ex)
            {
               
                throw new Exception("Error al listar contenedores", ex);
            }
        }





    }


}
