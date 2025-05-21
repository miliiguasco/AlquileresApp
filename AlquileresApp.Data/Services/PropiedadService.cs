using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AlquileresApp.Data.Services{
public class PropiedadService : IPropiedadService
{
    private readonly AppDbContext _context;

    public PropiedadService(AppDbContext context)
    {
        _context = context;
    }




    public async Task<List<Propiedad>> BuscarDisponiblesAsync(SearchFilters filtros)
{
    Console.WriteLine("📡 Llamado a BuscarDisponiblesAsync");
    if (string.IsNullOrWhiteSpace(filtros.Localidad) || 
        filtros.FechaInicio == default || 
        filtros.FechaFin == default)
    {
         Console.WriteLine("❌ Filtros inválidos");
        return new List<Propiedad>();
    }

    var propiedades = await _context.Propiedades
        .Include(p => p.Reservas)
        .Where(p =>
            string.Equals(p.Localidad, filtros.Localidad, StringComparison.OrdinalIgnoreCase) &&
            p.Capacidad >= filtros.CantidadHuespedes &&
            !p.Reservas.Any(r =>
                filtros.FechaInicio < r.FechaFin &&
                filtros.FechaFin > r.FechaInicio
            )
        )
        .ToListAsync();

    Console.WriteLine($"✅ Se encontraron {propiedades.Count} propiedades disponibles");
    return propiedades;
}



}
 
}
