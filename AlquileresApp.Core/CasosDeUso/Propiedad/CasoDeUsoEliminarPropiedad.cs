namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoEliminarPropiedad(IPropiedadRepositorio propiedadesRepositorio)
{
    public bool Ejecutar(Propiedad propiedad)
    {
        return propiedadesRepositorio.EliminarPropiedad(propiedad);
    }
}