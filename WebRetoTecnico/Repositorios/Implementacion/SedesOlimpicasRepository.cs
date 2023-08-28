using System.Data;
using System.Data.SqlClient;
using WebRetoTecnico.Models;
using WebRetoTecnico.Repositorios.Contrato;

namespace WebRetoTecnico.Repositorios.Implementacion
{
    public class SedesOlimpicasRepository: IGenericRepository<SedesOlimpicas>
    {
        private readonly string? _conexionSql = "";

        public SedesOlimpicasRepository(IConfiguration configuration)
        {
            _conexionSql = configuration.GetConnectionString("conexionSql");
        }

        public async Task<List<SedesOlimpicas>> Lista()
        {
            List<SedesOlimpicas> lista = new List<SedesOlimpicas>();
            using (var conexion = new SqlConnection(_conexionSql))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarSedesOlimpicas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new SedesOlimpicas
                        {
                            i_SedeId = Convert.ToInt32(dr["i_SedeId"]),
                            v_Nombre = dr["v_Nombre"].ToString(),
                            i_NumeroComplejo = Convert.ToInt32(dr["i_NumeroComplejo"]),
                            d_PresupuestoAproximado = Convert.ToDecimal(dr["d_PresupuestoAproximado"]),
                        });
                    }
                }
            }
            return lista;  
        }

        public async Task<bool> Guardar(SedesOlimpicas modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexionSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarSedesOlimpicas", conexion);
                    cmd.Parameters.AddWithValue("v_nombre", modelo.v_Nombre);
                    cmd.Parameters.AddWithValue("i_numerocomplejo", modelo.i_NumeroComplejo);
                    cmd.Parameters.AddWithValue("d_presupuestoaproximado", modelo.d_PresupuestoAproximado);
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

        public async Task<bool> Editar(SedesOlimpicas modelo)
        {
            try
            {
                using (var conexion = new SqlConnection(_conexionSql))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarSedesOlimpicas", conexion);
                    cmd.Parameters.AddWithValue("i_sedeid", modelo.i_SedeId);
                    cmd.Parameters.AddWithValue("v_nombre", modelo.v_Nombre);
                    cmd.Parameters.AddWithValue("i_numerocomplejo", modelo.i_NumeroComplejo);
                    cmd.Parameters.AddWithValue("d_presupuestoaproximado", modelo.d_PresupuestoAproximado);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarSedesOlimpicas", conexion);
                    cmd.Parameters.AddWithValue("i_sedeid", id);
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
