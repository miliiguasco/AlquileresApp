namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoPagar(ITarjetaRepositorio tarjetaRepositorio) 
{
    public void Ejecutar(Tarjeta tarjeta, decimal montoAPagar)
    {
        tarjetaRepositorio.Pagar(tarjeta, montoAPagar);
    }
}