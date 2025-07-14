namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoListarPropiedadesFiltrado(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar(SearchFilters filtros)
    {
        Console.WriteLine("📡 Ejecutando CasoDeUsoListarPropiedadesFiltrado");

        var propiedades = propiedadesRepositorio.ListarPropiedadesFiltrado(filtros);

        var propiedadesHabilitadas = propiedades.Where(p => p.NoHabitable != true).ToList();

        Console.WriteLine($"✅ Se encontraron {propiedadesHabilitadas} propiedades con filtros aplicados");
        return propiedadesHabilitadas;
    }
}