using AlquileresApp.UI.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlquileresApp.Data;
using AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Core.CasosDeUso.Administrador;
using AlquileresApp.Core.CasosDeUso.Propiedad;

using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Validadores;
using AlquileresApp.Core.Servicios;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.CasosDeUso.Imagen;
using AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.CasosDeUso.Tarjeta;
using AlquileresApp.Core.CasosDeUso.Comentario;
using AlquileresApp.Core.CasosDeUso.Calificacion;
using AlquileresApp.Core;
using Microsoft.AspNetCore.Components.Authorization;
using AlquileresApp.Core.CasosDeUso.Promocion;

using AlquileresApp.Core.CasosDeUso.PreguntasFrecuentes;
using AlquileresApp.Core.CasosDeUso.ContactarAdmin;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Agregar soporte para páginas Razor
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
});

builder.Services.AddCascadingAuthenticationState();

// Configurar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ServicioSesion>();
builder.Services.AddScoped<ServicioAutenticacion>();
builder.Services.AddScoped<ServicioCookies>();
builder.Services.AddScoped<IServicioSesion, ServicioSesion>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ServicioAutenticacion>());
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<CasoDeUsoIniciarSesion>();
builder.Services.AddScoped<IPropiedadRepositorio, PropiedadesRepositorio>();
builder.Services.AddScoped<IImagenesRepositorio, ImagenesRepositorio>();
builder.Services.AddScoped<IPropiedadValidador, PropiedadValidador>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
builder.Services.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
builder.Services.AddScoped<CasoDeUsoListarPropiedades>();
builder.Services.AddScoped<CasoDeUsoListarPropiedadesDestacadas>();
builder.Services.AddScoped<CasoDeUsoAgregarPropiedad>();
builder.Services.AddScoped<CasoDeUsoCargarImagen>();
builder.Services.AddScoped<CasoDeUsoModificarPropiedad>();
builder.Services.AddScoped<CasoDeUsoCalcularPrecioConPromocion>();
builder.Services.AddScoped<CasoDeUsoEliminarPropiedad>();
builder.Services.AddScoped<CasoDeUsoObtenerPropiedades>();
builder.Services.AddScoped<CasoDeUsoMostrarImagenes>();
builder.Services.AddScoped<CasoDeUsoEliminarImagen>();
builder.Services.AddScoped<CasoDeUsoEliminarPropiedad>();
builder.Services.AddScoped<CasoDeUsoCrearReserva>();
builder.Services.AddScoped<CasoDeUsoListarPropiedadesFiltrado>();
builder.Services.AddScoped<ITarjetaRepositorio, TarjetaRepositorio>();
builder.Services.AddScoped<IFechaReservaValidador, FechaReservaValidador>();
builder.Services.AddScoped<ITarjetaValidador, TarjetaValidador>();
builder.Services.AddScoped<CasoDeUsoRegistrarTarjeta>();
builder.Services.AddScoped<CasoDeUsoObtenerPropiedad>();
builder.Services.AddScoped<CasoDeUsoListarMisReservas>();
builder.Services.AddScoped<CasoDeUsoCancelarReserva>();
builder.Services.AddScoped<CasoDeUsoModificarReserva>();
builder.Services.AddScoped<CasoDeUsoObtenerReserva>();
builder.Services.AddScoped<CasoDeUsoRegistrarEncargado>(); 
builder.Services.AddScoped<CasoDeUsoListarEncargados>();
builder.Services.AddScoped<CasoDeUsoEliminarEncargado>();
builder.Services.AddTransient<INotificadorEmail>(provider =>
    new NotificadorEmail(
        "reservaenalquilando@gmail.com",
        "fxsl hsck basy pamv"
    )
);

builder.Services.AddScoped<CasoDeUsoVisualizarReserva>();
builder.Services.AddScoped<CasoDeUsoVisualizarTarjeta>();
builder.Services.AddScoped<CasoDeUsoEliminarTarjeta>();
builder.Services.AddScoped<CasoDeUsoModificarTarjeta>();
builder.Services.AddScoped<ICasoDeUsoVerReserva, CasoDeUsoVerReserva>();
builder.Services.AddScoped<CasoDeUsoCerrarSesion>();
builder.Services.AddScoped<CasoDeUsoListarReservasAdm>();
builder.Services.AddScoped<IComentarioRepositorio, ComentarioRepositorio>();
builder.Services.AddScoped<CasoDeUsoAgregarComentario>();
builder.Services.AddScoped<CasoDeUsoListarComentarios>();
builder.Services.AddScoped<CasoDeUsoOcultarComentario>();
builder.Services.AddScoped<CasoDeUsoRegistrarCheckout>();
builder.Services.AddScoped<ICalificacionRepositorio, CalificacionRepositorio>();
builder.Services.AddScoped<CasoDeUsoAgregarCalificacion>();
builder.Services.AddScoped<CasoDeUsoMostrarCalificacion>();
builder.Services.AddScoped<CasoDeUsoMarcarPropiedadComoNoHabitable>();


builder.Services.AddAuthentication().AddScheme<CustomOptions, ServicioAutorizacion>("CustomAuth", options => { });
builder.Services.AddScoped<CasoDeUsoEliminarPromocion>();
builder.Services.AddScoped<IPromocionRepositorio, PromocionRepositorio>();
builder.Services.AddScoped<CasoDeUsoCrearPromocion>();
builder.Services.AddScoped<CasoDeUsoListarPromociones>();
builder.Services.AddScoped<CasoDeUsoModificarPromocion>();
builder.Services.AddScoped<CasoDeUsoObtenerPromocion>();
builder.Services.AddScoped<CasoDeUsoListarPromocionesActivas>(); 

builder.Services.AddScoped<IPreguntasFrecuentesRepositorio, PreguntaFrecuenteRepositorio>();
builder.Services.AddScoped<CasoDeUsoMostrarPreguntasFrecuentes>();
builder.Services.AddScoped<CasoDeUsoCrearPreguntaFrecuente>();
builder.Services.AddScoped<CasoDeUsoModificarPreguntaFrecuente>();
builder.Services.AddScoped<CasoDeUsoEliminarPreguntaFrecuente>();
builder.Services.AddScoped<CasoDeUsoContactarAdmin>();
builder.Services.AddResponseCompression();

var app = builder.Build();
app.UseResponseCompression();
// Initialize Database and Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
        Console.WriteLine("Asegurando que la base de datos existe...");
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Base de datos creada o verificada.");
         // Inicializar datos de prueba
        Console.WriteLine("Inicializando datos de prueba...");
        SeedData.Initialize(dbContext);
        Console.WriteLine("Datos de prueba inicializados correctamente.");
        // Add test user if no users exist
        if (!dbContext.Usuarios.Any())
        {

            Console.WriteLine("No hay usuarios en la base de datos. Creando usuario de prueba...");
            var hashService = services.GetRequiredService<IServicioHashPassword>();
            var hashedPassword = hashService.HashPassword("Password123!");
            Console.WriteLine($"Contraseña hasheada para usuario de prueba: {hashedPassword}");
            
            var testUser = new Administrador(
                "Admin",
                "User",
                "admin@gmail.com",
                hashedPassword
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
