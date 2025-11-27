using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Contracts
{
    public interface IUsuarioLoginService
    {
        Task<(bool Success, Usuario Usuario, string Message)> LoginAsync(string email, string pass);
        Task<(bool Success, string Message)> RegistrarAsync(string nombre, string email, string pass);
        Task<(bool Success, string Message)> CambiarContraseñaAsync(string email, string nuevaContraseña);
    }
}
