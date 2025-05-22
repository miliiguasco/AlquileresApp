using AlquileresApp.UI.Components;
using AlquileresApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Data.Services;
using AlquileresApp.Core.CasosDeUso.Propiedad;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar AppDbContext con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios de aplicación
builder.Services.AddScoped<IPropiedadRepositorio, PropiedadesRepositorio>();
builder.Services.AddScoped<IPropiedadService, PropiedadService>();
builder.Services.AddScoped<CasoDeUsoListarPropiedades>();

var app = builder.Build();

// Crear base de datos y sembrar datos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Asegura que la base de datos y las tablas se creen antes de usarlas
    context.EnsureDatabaseCreated();
    
    // Inicializar datos
    SeedData.Initialize(context);
}

// Configurar el pipeline de HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
