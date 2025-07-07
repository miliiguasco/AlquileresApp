namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoObtenerPropiedades(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar()
    {
        return propiedadesRepositorio.obtenerPropiedades();
    }
}
