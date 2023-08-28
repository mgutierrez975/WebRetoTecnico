using System.Data;
using System.Data.SqlClient;
using WebRetoTecnico.Models;
using WebRetoTecnico.Repositorios.Contrato;

namespace WebRetoTecnico.Repositorios.Implementacion
{
    public class UsuarioRepository : IUsuarioService
    {
        private readonly string? _conexionSql = "";
        public UsuarioRepository(IConfiguration configuration)
        {
            _conexionSql = configuration.GetConnectionString("conexionSql");
        }
       

        public async Task<bool> GetUsuario(string correo, string clave)
        {
            using (SqlConnection conn = new SqlConnection(_conexionSql))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@v_Correo", correo);
                    cmd.Parameters.AddWithValue("@v_Clave", clave); // Considera usar hashing aquí.

                    //Usuario result = await cmd.ExecuteScalarAsync();

                    int count = Convert.ToInt32(cmd.ExecuteScalarAsync());

                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
