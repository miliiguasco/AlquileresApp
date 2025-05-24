using AlquileresApp.UI.Components;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar el contexto de la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var dbPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "AlquileresApp.Data", "Alquilando.db"));
    options.UseSqlite($"Data Source={dbPath}");
});

// Registrar el repositorio
builder.Services.AddScoped<IPropiedadRepositorio, PropiedadesRepositorio>();

var app = builder.Build();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
