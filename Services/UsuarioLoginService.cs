using Microsoft.EntityFrameworkCore;
using System;
using ElSazonCotizacionesApp.Data;
using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Models;
using BCrypt.Net;

namespace ElSazonCotizacionesApp.Services
{
    public class UsuarioLoginService : IUsuarioLoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioLoginService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(bool Success, Usuario Usuario, string Message)> LoginAsync(string email, string pass)
        {
            var usuario = await _usuarioRepository.ObtenerPorCorreoAsync(email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(pass, usuario.Contraseña))
            {
                return (false, null, "Correo o contraseña incorrectos");
            }

            return (true, usuario, "Login exitoso");
        }

        public async Task<(bool Success, string Message)> RegistrarAsync(string nombre, string email, string pass)
        {
            if (await _usuarioRepository.ExisteCorreoAsync(email))
            {
                return (false, "El correo ya está registrado");
            }

            var usuario = new Usuario
            {
                NombreUsuario = nombre,
                CorreoElectronico = email,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(pass),
                Rol = "Usuario"  // Todos los nuevos registros son usuarios normales
            };

            await _usuarioRepository.AgregarAsync(usuario);
            await _usuarioRepository.GuardarAsync();

            return (true, "Registro exitoso");
        }

        public async Task<(bool Success, string Message)> CambiarContraseñaAsync(string email, string nuevaContraseña)
        {
            var usuario = await _usuarioRepository.ObtenerPorCorreoAsync(email);

            if (usuario == null)
            {
                return (false, "El correo no está registrado");
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);
            await _usuarioRepository.ActualizarAsync(usuario);
            await _usuarioRepository.GuardarAsync();  

            return (true, "Contraseña cambiada exitosamente");
        }
    }
}