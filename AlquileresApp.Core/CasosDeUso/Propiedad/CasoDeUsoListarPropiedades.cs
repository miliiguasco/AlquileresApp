namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoListarPropiedades(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar()
    {
        Console.WriteLine("📡 Ejecutando CasoDeUsoListarPropiedades");
        var propiedades = propiedadesRepositorio.ListarPropiedades();
        Console.WriteLine($"✅ Se encontraron {propiedades.Count} propiedades");
        return propiedades;
    }
}