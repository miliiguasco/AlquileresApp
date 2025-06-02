namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoListarPropiedades(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar()
    {
        var propiedades = propiedadesRepositorio.ListarPropiedades();
        return propiedades;
    }
}