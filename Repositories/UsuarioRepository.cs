using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Data;
using ElSazonCotizacionesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ElSazonCotizacionesApp.Repositories 
{
    public class UsuarioRepository : IUsuarioRepository, IDisposable
    {
        private readonly ElSazonCotizacionesDbContext _db;

        public UsuarioRepository(IDbContextFactory<ElSazonCotizacionesDbContext> dbfactory)
        {
            _db = dbfactory.CreateDbContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<Usuario> ObtenerPorCorreoAsync(string email)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == email);
        }

        public async Task<bool> ExisteCorreoAsync(string email)
        {
            return await _db.Usuarios.AnyAsync(u => u.CorreoElectronico == email);
        }

        public async Task AgregarAsync(Usuario nombre)
        {
            await _db.Usuarios.AddAsync(nombre);
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            _db.Usuarios.Update(usuario);
            await _db.SaveChangesAsync();
        }

    }
}//Repositorio de Methods para ElSazonCotizacionesDbContext
