using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Data;
using ElSazonCotizacionesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ElSazonCotizacionesApp.Repositories
{
    public class ProductoRepository : IProductoRepository, IDisposable
    {
        private readonly ElSazonCotizacionesDbContext _db;

        public ProductoRepository(IDbContextFactory<ElSazonCotizacionesDbContext> dbfactory)
        {
            _db = dbfactory.CreateDbContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _db.Productos
                .Where(p => p.Activo)
                .OrderBy(p => p.NombreProducto)
                .ToListAsync();
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            return await _db.Productos.FindAsync(id);
        }

        public async Task AgregarAsync(Producto producto)
        {
            await _db.Productos.AddAsync(producto);
        }

        public async Task ActualizarAsync(Producto producto)
        {
            _db.Productos.Update(producto);
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await ObtenerPorIdAsync(id);
            if (producto != null)
            {
                producto.Activo = false;  // Eliminación lógica
                await ActualizarAsync(producto);
            }
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
