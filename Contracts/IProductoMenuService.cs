using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Contracts
{
    public interface IProductoMenuService
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorIdAsync(int id);
        Task<(bool Success, string Message)> AgregarAsync(Producto producto);
        Task<(bool Success, string Message)> ActualizarAsync(Producto producto);
        Task<(bool Success, string Message)> EliminarAsync(int id);
    }
}
