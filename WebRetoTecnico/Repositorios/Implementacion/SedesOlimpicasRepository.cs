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

        public Task<bool> Editar(SedesOlimpicas modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(SedesOlimpicas modelo)
        {
            throw new NotImplementedException();
        }

    }
}
