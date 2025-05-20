using AlquileresApp.Core.Entidades;
public class PropiedadService : IPropiedadService
{
    private readonly AppDbContext _context;

    public PropiedadService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Propiedad>> BuscarDisponiblesAsync(SearchFilters filtros)
{
    var propiedades = await _context.Propiedades
        .Include(p => p.Reservas)
        .Where(p =>
            p.Localidad == filtros.Localidad &&
            p.Capacidad >= filtros.CantidadHuespedes &&
            !p.Reservas.Any(r =>
                (filtros.FechaInicio < r.FechaFin) &&
                (filtros.FechaFin > r.FechaInicio)
            )
        ).ToListAsync();

    return propiedades;
}

}
