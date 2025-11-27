using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Data;
using ElSazonCotizacionesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ElSazonCotizacionesApp.Repositories
{
    public class CarritoRepository : ICarritoRepository, IDisposable
    {
        private readonly ElSazonCotizacionesDbContext _db;

        public CarritoRepository(IDbContextFactory<ElSazonCotizacionesDbContext> dbfactory)
        {
            _db = dbfactory.CreateDbContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<CarritoUsuario>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            return await _db.CarritoUsuarios
                .Include(c => c.Producto)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<CarritoUsuario> ObtenerItemAsync(int usuarioId, int productoId)
        {
            return await _db.CarritoUsuarios.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.ProductoId == productoId);
        }

        public async Task AgregarAsync(CarritoUsuario item)
        {
            await _db.CarritoUsuarios.AddAsync(item);
        }

        public async Task ActualizarAsync(CarritoUsuario item)
        {
            _db.CarritoUsuarios.Update(item);
        }

        public async Task EliminarAsync(int id)
        {
            var item = await _db.CarritoUsuarios.FindAsync(id);
            if (item != null)
            {
                _db.CarritoUsuarios.Remove(item);
            }
        }

        public async Task LimpiarPorUsuarioAsync(int usuarioId)
        {
            var items = await _db.CarritoUsuarios
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            _db.CarritoUsuarios.RemoveRange(items);
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}