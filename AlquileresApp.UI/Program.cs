using AlquileresApp.UI.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlquileresApp.Data;
using AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Validadores;
using AlquileresApp.Core.Servicios;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Core.Entidades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=../AlquileresApp.Data/Alquilando.db"));

// Configure HTTPS
builder.WebHost.UseUrls("https://localhost:7234", "http://localhost:5234");

// Configurar autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Registrar servicios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<CasoDeUsoRegistrarUsuario>();
builder.Services.AddScoped<CasoDeUsoIniciarSesion>();
builder.Services.AddScoped<IUsuarioValidador, UsuarioValidador>();
builder.Services.AddScoped<IServicioHashPassword, ServicioHashPassword>();
builder.Services.AddScoped<IServicioIniciarSesion, ServicioIniciarSesion>();

var app = builder.Build();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
        Console.WriteLine("Asegurando que la base de datos existe...");
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Base de datos creada o verificada.");
        
        // Add test user if no users exist
        if (!dbContext.Usuarios.Any())
        {
            Console.WriteLine("No hay usuarios en la base de datos. Creando usuario de prueba...");
            var hashService = services.GetRequiredService<IServicioHashPassword>();
            var hashedPassword = hashService.HashPassword("Password123!");
            Console.WriteLine($"Contraseña hasheada para usuario de prueba: {hashedPassword}");
            
            var testUser = new Cliente(
                "Test",
                "User",
                "test@test.com",
                "123456789",
                hashedPassword,
                DateTime.Now.AddYears(-20)
            );
            
            dbContext.Usuarios.Add(testUser);
            dbContext.SaveChanges();
            Console.WriteLine("Usuario de prueba creado exitosamente.");
        }
        else
        {
            Console.WriteLine("Ya existen usuarios en la base de datos.");
            var usuarios = dbContext.Usuarios.ToList();
            foreach (var u in usuarios)
            {
                Console.WriteLine($"Usuario existente: {u.Email}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error durante la inicialización de la base de datos: {ex.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapBlazorHub();

// Agregar endpoints para autenticación
app.MapGet("/Logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

app.Run();
