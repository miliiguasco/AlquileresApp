using AlquileresApp.UI.Components;
<<<<<<< HEAD
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
=======
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlquileresApp.Data;
using AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Validadores;
using AlquileresApp.Core.Servicios;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Core.Entidades;
>>>>>>> 7a02049defab08c041791872f3d075bf7648b509

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

<<<<<<< HEAD
// Registrar el contexto de la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var dbPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "AlquileresApp.Data", "Alquilando.db"));
    options.UseSqlite($"Data Source={dbPath}");
});

// Registrar el repositorio
builder.Services.AddScoped<IPropiedadRepositorio, PropiedadesRepositorio>();
=======
// Agregar soporte para páginas Razor
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
});

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
builder.Services.AddScoped<IUsuarioValidador, UsuarioValidador>();
builder.Services.AddScoped<IServicioHashPassword, ServicioHashPassword>();
>>>>>>> 7a02049defab08c041791872f3d075bf7648b509

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
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Configuración de archivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// Mapear endpoints
app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

// Agregar endpoints para autenticación
app.MapGet("/Logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

// Importante: Este debe ser el último mapeo
app.MapFallbackToPage("/_Host");

app.Run();
