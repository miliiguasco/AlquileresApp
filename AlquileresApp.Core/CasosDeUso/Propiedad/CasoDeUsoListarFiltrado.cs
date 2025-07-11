namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoListarPropiedadesFiltrado(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar(SearchFilters filtros)
    {
        Console.WriteLine("📡 Ejecutando CasoDeUsoListarPropiedadesFiltrado");

        var propiedades = propiedadesRepositorio.ListarPropiedadesFiltrado(filtros);

        Console.WriteLine($"✅ Se encontraron {propiedades.Count} propiedades con filtros aplicados");
        return propiedades;
    }
}