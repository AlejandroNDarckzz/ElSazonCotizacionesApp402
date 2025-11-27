using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Contracts
{
    public interface IUsuarioRepository 
    {
        Task<Usuario> ObtenerPorCorreoAsync(string email);
        Task<bool> ExisteCorreoAsync(string email);
        Task AgregarAsync(Usuario nombre);
        Task GuardarAsync();
        Task ActualizarAsync(Usuario usuario); 
    }
}
