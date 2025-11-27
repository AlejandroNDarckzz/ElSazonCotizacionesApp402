namespace ElSazonCotizacionesApp.Contracts
{
    public interface IAutenticacionEstadoService
    {
        void SetUsuarioActual(int usuarioId, string correo, string rol);  // ← Agregar usuarioId
        int? GetUsuarioId();  
        string GetCorreo();
        string GetRol();
        bool EsAdministrador();
        bool EstaAutenticado();  
        void CerrarSesion();
    }
}