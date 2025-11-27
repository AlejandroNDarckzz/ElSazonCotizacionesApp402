using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Components.Pages
{
    public partial class Catalogo : IDisposable
    {
        private List<Producto> productos = new();
        private bool cargando = true;
        private bool mostrarMensaje = false;
        private int cantidadCarrito = 0;

        protected override async Task OnInitializedAsync()
        {
            await CargarProductos();
            await ActualizarCantidadCarrito();
            CarritoService.OnCambio += OnCarritoCambio;
        }

        private async Task CargarProductos()
        {
            cargando = true;

            var tareaProductos = ProductoMenuService.ObtenerTodosAsync();
            var tareaDelay = Task.Delay(800);

            await Task.WhenAll(tareaProductos, tareaDelay);

            productos = await tareaProductos;
            cargando = false;
        }

        private async Task AgregarAlCarrito(Producto producto)
        {
            await CarritoService.AgregarProductoAsync(producto);  // ← Cambiar a AgregarProductoAsync
            mostrarMensaje = true;
            await Task.Delay(2000);
            mostrarMensaje = false;
            StateHasChanged();
        }

        private async void OnCarritoCambio()
        {
            await ActualizarCantidadCarrito();
            StateHasChanged();
        }

        private async Task ActualizarCantidadCarrito()
        {
            cantidadCarrito = await CarritoService.ObtenerCantidadTotalAsync();
        }

        private void IrAlCarrito()
        {
            Navigation.NavigateTo("/carrito");
        }

        private void OcultarMensaje()
        {
            mostrarMensaje = false;
        }

        public void Dispose()
        {
            CarritoService.OnCambio -= OnCarritoCambio;
        }
    }
}