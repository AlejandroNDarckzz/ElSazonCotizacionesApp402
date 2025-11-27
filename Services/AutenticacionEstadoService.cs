using ElSazonCotizacionesApp.Contracts;

namespace ElSazonCotizacionesApp.Services
{
    public class AutenticacionEstadoService : IAutenticacionEstadoService
    {
        private int? _usuarioId;
        private string _correo;
        private string _rol;

        public void SetUsuarioActual(int usuarioId, string correo, string rol)
        {
            _usuarioId = usuarioId;
            _correo = correo;
            _rol = rol;
        }

        public int? GetUsuarioId()
        {
            return _usuarioId;
        }

        public string GetCorreo()
        {
            return _correo ?? "";
        }

        public string GetRol()
        {
            return _rol ?? "Usuario";
        }

        public bool EsAdministrador()
        {
            return _rol == "Administrador";
        }

        public bool EstaAutenticado()
        {
            return _usuarioId.HasValue;
        }

        public void CerrarSesion()
        {
            _usuarioId = null;
            _correo = null;
            _rol = null;
        }
    }
}