using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace ElSazonCotizacionesApp.Components.Pages
{
    public partial class RecuperarContrasena
    {
        [SupplyParameterFromForm]
        private RecuperarModel model { get; set; } = new();

        private string mensaje = "";
        private bool exito = false;

        private async Task CambiarContraseña()
        {
            var (success, message) = await UsuarioLoginService.CambiarContraseñaAsync(
                model.Correo,
                model.NuevaContraseña
            );

            exito = success;
            mensaje = message;

            if (success)
            {
                await Task.Delay(2000);
                Navigation.NavigateTo("/login");
            }
        }

        public class RecuperarModel
        {
            [Required(ErrorMessage = "El correo es requerido")]
            [EmailAddress(ErrorMessage = "Correo no válido")]
            public string Correo { get; set; } = "";

            [Required(ErrorMessage = "La nueva contraseña es requerida")]
            [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
            public string NuevaContraseña { get; set; } = "";

            [Required(ErrorMessage = "Confirma tu nueva contraseña")]
            [Compare("NuevaContraseña", ErrorMessage = "Las contraseñas no coinciden")]
            public string ConfirmarContraseña { get; set; } = "";
        }
    }
}
