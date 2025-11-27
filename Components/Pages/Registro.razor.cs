using ElSazonCotizacionesApp.Services;

namespace ElSazonCotizacionesApp.Components.Pages
{
    public partial class Registro
    {       
        private RegistroModel model = new();
        private string mensaje = "";
        private bool exito = false;

        private async Task Registrar()
        {
            var (success, message) = await UsuarioLoginService.RegistrarAsync(
                model.NombreUsuario,
                model.Correo,
                model.Password
            );

            exito = success;
            mensaje = message;

            if (success)
            {
                await Task.Delay(1000);
                Navigation.NavigateTo("/Login");
            }
        }

        public class RegistroModel
        {
            public string NombreUsuario { get; set; }

            public string Correo { get; set; }

            public string Password { get; set; }
        }
    }
}

