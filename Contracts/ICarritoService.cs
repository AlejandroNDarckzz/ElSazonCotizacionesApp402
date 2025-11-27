using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Contracts
{
    public interface ICarritoService
    {
        Task<List<ItemCarrito>> ObtenerItemsAsync();
        Task AgregarProductoAsync(Producto producto);
        Task AumentarCantidadAsync(int productoId);
        Task DisminuirCantidadAsync(int productoId);
        Task EliminarItemAsync(int productoId);
        Task LimpiarCarritoAsync();
        Task<int> ObtenerCantidadTotalAsync();
        Task<decimal> ObtenerSubtotalAsync();
        Task<decimal> ObtenerIVAAsync();
        Task<decimal> ObtenerTotalAsync();
        event Action OnCambio;
    }
}
