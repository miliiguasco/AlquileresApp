namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoModificarPropiedad(IPropiedadRepositorio propiedadesRepositorio)
{
    public void Ejecutar(Propiedad propiedad)
    {
        propiedadesRepositorio.ModificarPropiedad(propiedad);
    }
}
