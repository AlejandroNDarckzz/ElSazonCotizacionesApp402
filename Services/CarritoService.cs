using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Models;

namespace ElSazonCotizacionesApp.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly IAutenticacionEstadoService _authState;
        public event Action OnCambio;

        public CarritoService(ICarritoRepository carritoRepository, IAutenticacionEstadoService authState)
        {
            _carritoRepository = carritoRepository;
            _authState = authState;
        }

        private int? ObtenerUsuarioId()
        {
            return _authState.GetUsuarioId();
        }

        public async Task<List<ItemCarrito>> ObtenerItemsAsync()
        {
            var usuarioId = ObtenerUsuarioId();
            if (!usuarioId.HasValue)
                return new List<ItemCarrito>();

            var items = await _carritoRepository.ObtenerPorUsuarioAsync(usuarioId.Value);

            return items.Select(c => new ItemCarrito
            {
                ProductoId = c.ProductoId,
                NombreProducto = c.Producto.NombreProducto,
                Precio = c.Producto.Precio,
                Cantidad = c.Cantidad,
                Imagen = c.Producto.Imagen,
                TipoImagen = c.Producto.TipoImagen
            }).ToList();
        }

        public async Task AgregarProductoAsync(Producto producto)
        {
            var usuarioId = ObtenerUsuarioId();
            if (!usuarioId.HasValue) return;

            var itemExistente = await _carritoRepository.ObtenerItemAsync(usuarioId.Value, producto.Id);

            if (itemExistente != null)
            {
                itemExistente.Cantidad++;
                await _carritoRepository.ActualizarAsync(itemExistente);
            }
            else
            {
                var nuevoItem = new CarritoUsuario
                {
                    UsuarioId = usuarioId.Value,
                    ProductoId = producto.Id,
                    Cantidad = 1,
                    FechaAgregado = DateTime.Now
                };
                await _carritoRepository.AgregarAsync(nuevoItem);
            }

            await _carritoRepository.GuardarAsync();
            NotificarCambio();
        }

        public async Task AumentarCantidadAsync(int productoId)
        {
            var usuarioId = ObtenerUsuarioId();
            if (!usuarioId.HasValue) return;

            var item = await _carritoRepository.ObtenerItemAsync(usuarioId.Value, productoId);
            if (item != null)
            {
                item.Cantidad++;
                await _carritoRepository.ActualizarAsync(item);
                await _carritoRepository.GuardarAsync();
                NotificarCambio();
            }
        }

        public async Task DisminuirCantidadAsync(int productoId)
        {
            var usuarioId = ObtenerUsuarioId();
            if (!usuarioId.HasValue) return;

            var item = await _carritoRepository.ObtenerItemAsync(usuarioId.Value, productoId);
            if (item != null)
            {
                if (item.Cantidad > 1)
                {
                    item.Cantidad--;
                    await _carritoRepository.ActualizarAsync(item);
                }
                else
                {
                    await _carritoRepository.EliminarAsync(item.Id);
                }
                await _carritoRepository.GuardarAsync();
                NotificarCambio();
            }
        }

        public async Task EliminarItemAsync(int productoId)
        {
            var usuarioId = ObtenerUsuarioId();
            if (!usuarioId.HasValue) return;

            var item = await _carritoRepository.ObtenerItemAsync(usuarioId.Value, productoId);
            if (item != null)
            {
                await _carritoRepository.EliminarAsync(item.Id);
                await _carritoRepository.GuardarAsync();
                NotificarCambio();
            }
        }

        public async Task LimpiarCarritoAsync()
        {
            var usuarioId = ObtenerUsuarioId();
            if (!usuarioId.HasValue) return;

            await _carritoRepository.LimpiarPorUsuarioAsync(usuarioId.Value);
            await _carritoRepository.GuardarAsync();
            NotificarCambio();
        }

        public async Task<int> ObtenerCantidadTotalAsync()
        {
            var items = await ObtenerItemsAsync();
            return items.Sum(i => i.Cantidad);
        }

        public async Task<decimal> ObtenerSubtotalAsync()
        {
            var items = await ObtenerItemsAsync();
            return items.Sum(i => i.Subtotal);
        }

        public async Task<decimal> ObtenerIVAAsync()
        {
            var subtotal = await ObtenerSubtotalAsync();
            return subtotal * 0.16m;
        }

        public async Task<decimal> ObtenerTotalAsync()
        {
            var subtotal = await ObtenerSubtotalAsync();
            var iva = await ObtenerIVAAsync();
            return subtotal + iva;
        }

        private void NotificarCambio()
        {
            OnCambio?.Invoke();
        }
    }
}