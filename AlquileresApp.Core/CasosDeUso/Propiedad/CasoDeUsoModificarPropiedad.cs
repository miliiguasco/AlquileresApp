namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoModificarPropiedad(IPropiedadRepositorio propiedadesRepositorio, IPropiedadValidador propiedadValidador)
{
    public void Ejecutar(Propiedad propiedad)
    {
        propiedadValidador.ValidarPropiedad(propiedad);
        propiedadesRepositorio.ModificarPropiedad(propiedad);
    }
}
