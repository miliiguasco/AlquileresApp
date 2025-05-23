namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoFiltrarBusqueda(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar(SearchFilters filtros)
    {
        Console.WriteLine("📡 Ejecutando CasoDeUsoFiltrarBusqueda");
        var propiedades = propiedadesRepositorio.BuscarDisponiblesAsync(filtros);
        //Console.WriteLine($"✅ Se encontraron {propiedades.Count} propiedades");
        return propiedades;
    }
}