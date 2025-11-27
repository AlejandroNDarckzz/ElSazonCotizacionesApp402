using ElSazonCotizacionesApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ElSazonCotizacionesApp.Components.Pages
{
    public partial class Carrito : IDisposable
    {
        private List<ItemCarrito> items = new();
        private decimal subtotal = 0;
        private decimal iva = 0;
        private decimal total = 0;

        protected override async Task OnInitializedAsync()  // ← Cambiar a async
        {
            await CargarCarrito();  // ← Agregar await
            CarritoService.OnCambio += OnCarritoCambio;  // ← Cambiar nombre del handler
        }

        private async Task CargarCarrito()  // ← Cambiar a async Task
        {
            items = await CarritoService.ObtenerItemsAsync();  // ← Agregar await y Async
            subtotal = await CarritoService.ObtenerSubtotalAsync();  // ← Agregar await y Async
            iva = await CarritoService.ObtenerIVAAsync();  // ← Agregar await y Async
            total = await CarritoService.ObtenerTotalAsync();  // ← Agregar await y Async
            StateHasChanged();
        }

        private async void OnCarritoCambio()  // ← Nuevo handler async void
        {
            await CargarCarrito();
        }

        private async Task AumentarCantidad(int productoId)  // ← Cambiar a async Task
        {
            await CarritoService.AumentarCantidadAsync(productoId);  // ← Agregar await y Async
        }

        private async Task DisminuirCantidad(int productoId)  // ← Cambiar a async Task
        {
            await CarritoService.DisminuirCantidadAsync(productoId);  // ← Agregar await y Async
        }

        private async Task EliminarItem(int productoId)  // ← Cambiar a async Task
        {
            await CarritoService.EliminarItemAsync(productoId);  // ← Agregar await y Async
        }

        private async Task LimpiarCarrito()
        {
            var confirmado = await JSRuntime.InvokeAsync<bool>("confirm", "¿Estás seguro de limpiar todo el carrito?");
            if (confirmado)
            {
                await CarritoService.LimpiarCarritoAsync();  // ← Agregar await y Async
            }
        }

        public void Dispose()
        {
            CarritoService.OnCambio -= OnCarritoCambio;  // ← Cambiar al nuevo handler
        }
    }
}