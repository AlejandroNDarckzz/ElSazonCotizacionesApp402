using Microsoft.EntityFrameworkCore;
using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Data
{
    public class  ElSazonCotizacionesDbContext : DbContext
    {
        public ElSazonCotizacionesDbContext(DbContextOptions<ElSazonCotizacionesDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CarritoUsuario> CarritoUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.CorreoElectronico)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            // Configuración de Producto 
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18,2);  // 18 dígitos totales, 2 decimales

            // Configuración de CarritoUsuario
            modelBuilder.Entity<CarritoUsuario>()  
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarritoUsuario>()  
                .HasOne(c => c.Producto)
                .WithMany()
                .HasForeignKey(c => c.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }//de ElSazonCotizacionesDbContext

}
