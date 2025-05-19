namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoAgregarPropiedad(IPropiedadRepositorio propiedadesRepositorio,  IPropiedadValidador propiedadValidador)
{
    public void Ejecutar(Propiedad propiedad){
        propiedadValidador.validarPropiedad(propiedad);
        propiedadesRepositorio.CargarPropiedad(propiedad);
    }
}