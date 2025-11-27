using ElSazonCotizacionesApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ElSazonCotizacionesApp.Components
{
    public partial class FormularioProductos
    {
        [Parameter] public int? ProductoId { get; set; }  
        [Parameter] public EventCallback OnGuardar { get; set; }
        [Parameter] public EventCallback OnCancelar { get; set; }

        private Producto producto = new();
        private byte[] imagenPreview;
        private string tipoImagenPreview;
        private string mensaje = "";
        private bool exito = false;

        protected override async Task OnParametersSetAsync()
        {
            if (ProductoId.HasValue)
            {
                producto = await ProductoMenuService.ObtenerPorIdAsync(ProductoId.Value);
                if (producto.Imagen != null)
                {
                    imagenPreview = producto.Imagen;
                    tipoImagenPreview = producto.TipoImagen;
                }
            }
            else
            {
                producto = new Producto();
            }
        }

        private async Task CargarImagen(InputFileChangeEventArgs e)
        {
            var archivo = e.File;
            if (archivo != null)
            {
                if (!archivo.ContentType.StartsWith("image/"))
                {
                    mensaje = "Solo se permiten imágenes";
                    exito = false;
                    return;
                }

                if (archivo.Size > 2 * 1024 * 1024)
                {
                    mensaje = "La imagen debe ser menor a 2MB";
                    exito = false;
                    return;
                }

                using var stream = archivo.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                producto.Imagen = memoryStream.ToArray();
                producto.TipoImagen = archivo.ContentType;

                imagenPreview = producto.Imagen;
                tipoImagenPreview = producto.TipoImagen;

                mensaje = "";
            }
        }

        private async Task Guardar()
        {
            var (success, message) = ProductoId.HasValue
                ? await ProductoMenuService.ActualizarAsync(producto)
                : await ProductoMenuService.AgregarAsync(producto);

            exito = success;
            mensaje = message;

            if (success)
            {
                await Task.Delay(1000);
                await OnGuardar.InvokeAsync();
            }
        }

        private async Task Cancelar()
        {
            await OnCancelar.InvokeAsync();
        }
    }
}

