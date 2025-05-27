namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoPagar(ITarjetaRepositorio tarjetaRepositorio,  IPagoTarjetaValidador tarjetaValidador)
{
    public void Ejecutar(Tarjeta tarjeta, decimal montoAPagar){
        tarjetaRepositorio.Pagar(tarjeta,montoAPagar);
    }
}