using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Contracts
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
        Task EliminarAsync(int id);
        Task GuardarAsync();
    }
}
