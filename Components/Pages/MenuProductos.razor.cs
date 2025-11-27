using ElSazonCotizacionesApp.Models;
using Microsoft.JSInterop;

namespace ElSazonCotizacionesApp.Components.Pages
{
    public partial class MenuProductos
    {
        
        private List<Producto> productos;
        private bool mostrarFormulario = false;
        private int? productoIdSeleccionado = null;

        protected override async Task OnInitializedAsync()
        {
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            productos = await ProductoMenuService.ObtenerTodosAsync();
        }

        private void AbrirFormularioNuevo()
        {
            productoIdSeleccionado = null;
            mostrarFormulario = true;
        }

        private void AbrirFormularioEditar(int id)
        {
            productoIdSeleccionado = id;
            mostrarFormulario = true;
        }

        private async Task CerrarFormulario()
        {
            mostrarFormulario = false;
            productoIdSeleccionado = null;
            await CargarProductos();
        }

        private async Task EliminarProducto(int id)
        {
            if (await JSRuntime.InvokeAsync<bool>("confirm", "¿Estás seguro de eliminar este producto?"))
            {
                var (success, message) = await ProductoMenuService.EliminarAsync(id);
                await CargarProductos();
            }
        }
    }
}

