using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Data;
using ElSazonCotizacionesApp.Models;
using ElSazonCotizacionesApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElSazonCotizacionesApp.Services
{
    public class ProductoMenuService : IProductoMenuService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoMenuService(IDbContextFactory<ElSazonCotizacionesDbContext> dbfactory)
        {
            _productoRepository = new ProductoRepository(dbfactory);
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _productoRepository.ObtenerTodosAsync();
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            return await _productoRepository.ObtenerPorIdAsync(id);
        }

        public async Task<(bool Success, string Message)> AgregarAsync(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                return (false, "El nombre es requerido");
            }

            if (producto.Precio <= 0)
            {
                return (false, "El precio debe ser mayor a 0");
            }

            await _productoRepository.AgregarAsync(producto);
            await _productoRepository.GuardarAsync();

            return (true, "Producto agregado exitosamente");
        }

        public async Task<(bool Success, string Message)> ActualizarAsync(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                return (false, "El nombre es requerido");
            }

            if (producto.Precio <= 0)
            {
                return (false, "El precio debe ser mayor a 0");
            }

            await _productoRepository.ActualizarAsync(producto);
            await _productoRepository.GuardarAsync();

            return (true, "Producto actualizado exitosamente");
        }

        public async Task<(bool Success, string Message)> EliminarAsync(int id)
        {
            await _productoRepository.EliminarAsync(id);
            await _productoRepository.GuardarAsync();

            return (true, "Producto eliminado exitosamente");
        }


    }
}
