using System.ComponentModel.DataAnnotations;

namespace ElSazonCotizacionesApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = "Usuario";  // "Usuario" o "Administrador"
    }
}
    