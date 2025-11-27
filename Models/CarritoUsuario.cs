using System.ComponentModel.DataAnnotations;

namespace ElSazonCotizacionesApp.Models
{
    public class CarritoUsuario
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public DateTime FechaAgregado { get; set; } = DateTime.Now;

        // Relaciones
        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
    }
}
