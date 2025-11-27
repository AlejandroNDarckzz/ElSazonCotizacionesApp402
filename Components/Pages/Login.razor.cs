using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using ElSazonCotizacionesApp.Contracts;

namespace ElSazonCotizacionesApp.Components.Pages
{
    public partial class Login
    {
        [Inject]
        private IAutenticacionEstadoService AuthState { get; set; }

        [SupplyParameterFromForm]
        private LoginModel model { get; set; } = new();

        private string mensaje = "";

        private async Task IniciarSesion()
        {
            var (success, usuario, message) = await UsuarioLoginService.LoginAsync(
                model.Correo,
                model.Password
            );

            mensaje = message;

            if (success)
            {
                // Guardar el ID del usuario además del correo y rol
                AuthState.SetUsuarioActual(usuario.Id, usuario.CorreoElectronico, usuario.Rol);

                // Redirigir según el rol
                if (usuario.Rol == "Administrador")
                {
                    Navigation.NavigateTo("/menuproductos");
                }
                else
                {
                    Navigation.NavigateTo("/catalogo");
                }
            }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "El correo es requerido")]
            [EmailAddress(ErrorMessage = "Correo no válido")]
            public string Correo { get; set; } = "";

            [Required(ErrorMessage = "La contraseña es requerida")]
            public string Password { get; set; } = "";
        }
    }
}