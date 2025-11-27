using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ElSazonCotizacionesApp.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreProducto { get; set; }

        [StringLength(300)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }
        public string TipoImagen { get; set; }
        public bool Activo { get; set; } = true;
    }
}
