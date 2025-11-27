using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Contracts
{
    public interface ICarritoRepository
    {
        Task<List<CarritoUsuario>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<CarritoUsuario> ObtenerItemAsync(int usuarioId, int productoId);
        Task AgregarAsync(CarritoUsuario item);
        Task ActualizarAsync(CarritoUsuario item);
        Task EliminarAsync(int id);
        Task LimpiarPorUsuarioAsync(int usuarioId);
        Task GuardarAsync();
    }
}
