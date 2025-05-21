using System.Collections.Generic;
using System.Threading.Tasks;
using AlquileresApp.Core.Entidades;


public interface IPropiedadService
{
    Task<List<Propiedad>> BuscarDisponiblesAsync(SearchFilters filtros);
}
