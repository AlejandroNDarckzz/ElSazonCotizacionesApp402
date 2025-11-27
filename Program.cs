using ElSazonCotizacionesApp.Components;
using ElSazonCotizacionesApp.Contracts;
using ElSazonCotizacionesApp.Data;
using ElSazonCotizacionesApp.Models;
using ElSazonCotizacionesApp.Repositories;
using ElSazonCotizacionesApp.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


//Configurar DbContext with SQL Server
builder.Services.AddDbContextFactory<ElSazonCotizacionesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionE")));
//builder.Services.AddDbContext<ElSazonCotizacionesDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionE")));

//Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();

//Services
builder.Services.AddScoped<IUsuarioLoginService, UsuarioLoginService>();
builder.Services.AddScoped<IProductoMenuService, ProductoMenuService>();
builder.Services.AddScoped<IAutenticacionEstadoService, AutenticacionEstadoService>();
builder.Services.AddScoped<ICarritoService, CarritoService>();

var app = builder.Build();

// ========== CREAR USUARIO ADMINISTRADOR POR DEFECTO ==========
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ElSazonCotizacionesDbContext>();

    // Asegurar que la base de datos está creada
    context.Database.EnsureCreated();

    // Verificar si ya existe el admin
    var adminExiste = await context.Usuarios.AnyAsync(u => u.CorreoElectronico == "admin@elsazon.com");

    if (!adminExiste)
    {
        var admin = new Usuario
        {
            NombreUsuario = "Administrador",
            CorreoElectronico = "admin@elsazon.com",
            Contraseña = BCrypt.Net.BCrypt.HashPassword("Admin123"),
            Rol = "Administrador",            
        };

        context.Usuarios.Add(admin);
        await context.SaveChangesAsync();

        Console.WriteLine("   Usuario administrador creado:");
        Console.WriteLine("   Correo: admin@elsazon.com");
        Console.WriteLine("   Contraseña: Admin123");
    }
}
// ============================================================

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
