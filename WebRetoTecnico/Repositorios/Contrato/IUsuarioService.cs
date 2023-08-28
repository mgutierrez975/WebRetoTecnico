using WebRetoTecnico.Models;

namespace WebRetoTecnico.Repositorios.Contrato
{
    public interface IUsuarioService
    {
        Task<bool> GetUsuario(string correo, string clave);
    }
}
