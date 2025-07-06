namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoCalcularPrecioConPromocion(IPropiedadRepositorio propiedadesRepositorio,  IPropiedadValidador propiedadValidador)
{
    public decimal Ejecutar(Propiedad propiedad, DateTime fecha, DateTime fechaInicio, DateTime fechaFin){
        return propiedadesRepositorio.CalcularPrecioConPromocion(propiedad, fecha, fechaInicio, fechaFin);
    }
}