using System.Data.SqlClient;
using System.Data;
using WebRetoTecnico.Models;
using WebRetoTecnico.Repositorios.Contrato;
using System.Reflection;

namespace WebRetoTecnico.Repositorios.Implementacion
{
    public class ComplejosDeportivosRepository : IGenericRepository<ComplejosDeportivos>
    {
        private readonly string? _conexionSql = "";

        public ComplejosDeportivosRepository(IConfiguration configuration)
        {
            _conexionSql = configuration.GetConnectionString("conexionSql");
        }

        public async Task<List<ComplejosDeportivos>> Lista()
        {
            try
            {
                List<ComplejosDeportivos> lista = new List<ComplejosDeportivos>();
                using (var conexion = new SqlConnection(_conexionSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ListarComplejosDeportivos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            lista.Add(new ComplejosDeportivos
                            {
                                i_ComplejoId = Convert.ToInt32(dr["i_ComplejoId"]),
                                refSedeOlimpica = new SedesOlimpicas()
                                {
                                    i_SedeId = Convert.ToInt32(dr["i_SedeId"]),
                                    v_Nombre = dr["v_Nombre"].ToString(),
                                },
                                v_TipoComplejo = dr["v_TipoComplejo"].ToString(),
                                v_Localizacion = dr["v_Localizacion"].ToString(),
                                v_JefeOrganizacion = dr["v_JefeOrganizacion"].ToString(),
                                v_AreaTotalOcupada = dr["v_AreaTotalOcupada"].ToString()

                            });
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<bool> Guardar(ComplejosDeportivos modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexionSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarComplejosDeportivos", conexion);
                    cmd.Parameters.AddWithValue("i_sedeid", modelo.refSedeOlimpica?.i_SedeId);
                    cmd.Parameters.AddWithValue("v_tipocomplejo", modelo.v_TipoComplejo);
                    cmd.Parameters.AddWithValue("v_localizacion", modelo.v_Localizacion);
                    cmd.Parameters.AddWithValue("v_jefeorganizacion", modelo.v_JefeOrganizacion);
                    cmd.Parameters.AddWithValue("v_areatotalocupada", modelo.v_AreaTotalOcupada);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int filas = await cmd.ExecuteNonQueryAsync();
                    if (filas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception ex) { throw ex; }

        }

        public async Task<bool> Editar(ComplejosDeportivos modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexionSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarComplejosDeportivos", conexion);
                    cmd.Parameters.AddWithValue("i_complejoid", modelo.i_ComplejoId);
                    cmd.Parameters.AddWithValue("i_sedeid", modelo.refSedeOlimpica?.i_SedeId);
                    cmd.Parameters.AddWithValue("v_tipocomplejo", modelo.v_TipoComplejo);
                    cmd.Parameters.AddWithValue("v_localizacion", modelo.v_Localizacion);
                    cmd.Parameters.AddWithValue("v_jefeorganizacion", modelo.v_JefeOrganizacion);
                    cmd.Parameters.AddWithValue("v_areatotalocupada", modelo.v_AreaTotalOcupada);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int filas = await cmd.ExecuteNonQueryAsync();
                    if (filas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexionSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarComplejosDeportivos", conexion);
                    cmd.Parameters.AddWithValue("i_complejoid", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int filas = await cmd.ExecuteNonQueryAsync();
                    if (filas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            
        }


    }
}
