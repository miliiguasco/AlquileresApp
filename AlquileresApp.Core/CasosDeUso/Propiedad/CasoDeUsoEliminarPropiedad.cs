namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoEliminarPropiedad(IPropiedadRepositorio propiedadesRepositorio)
{
    public void Ejecutar(Propiedad propiedad)
    {
        if (propiedadesRepositorio.TieneReservaActiva(propiedad.Id))
        {
            throw new Exception("No se puede eliminar la propiedad porque tiene reservas activas.");
        }
        propiedadesRepositorio.EliminarPropiedad(propiedad);
    }
}