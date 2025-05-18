using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.CasosDeUso.Propiedad;

public class CasoDeUsoAgregarPropiedad(IPropiedadesRepositorio propiedadesRepositorio, IPropiedadValidador propiedadValidador)
{
    public void Ejecutar(Propiedad propiedad){
        propiedadValidador.ValidarPropiedad(propiedad);
        propiedadesRepositorio.CargarPropiedad(propiedad);
    }
}