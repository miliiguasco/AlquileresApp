namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoListarPropiedad(IPropiedadRepositorio propiedadesRepositorio)
{
    public List<Propiedad> Ejecutar()
    {
        return propiedadesRepositorio.ListarPropiedades();
    }
}
