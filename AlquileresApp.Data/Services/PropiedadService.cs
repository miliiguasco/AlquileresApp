using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlquileresApp.Data.Services
{
    public class PropiedadService : IPropiedadService
    {
        private readonly AppDbContext _context;
        private readonly IPropiedadRepositorio _propiedadRepositorio;

        public PropiedadService(AppDbContext context, IPropiedadRepositorio propiedadRepositorio)
        {
            _context = context;
            _propiedadRepositorio = propiedadRepositorio;
        }

        public async Task<List<Propiedad>> ObtenerTodasAsync()
        {
            return _propiedadRepositorio.ListarPropiedades();
        }

        public async Task<List<Propiedad>> BuscarDisponiblesAsync(SearchFilters filtros)
        {
            Console.WriteLine("📡 Llamado a BuscarDisponiblesAsync");
            if (string.IsNullOrWhiteSpace(filtros.Localidad) || 
                filtros.FechaInicio == default || 
                filtros.FechaFin == default)
            {
                Console.WriteLine("❌ Filtros inválidos");
                return await ObtenerTodasAsync();
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
