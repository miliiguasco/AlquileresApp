namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoPagar(ITarjetaRepositorio tarjetaRepositorio,  ITarjetaValidador tarjetaValidador)
{
    public void Ejecutar(Tarjeta tarjeta){
        tarjetaValidador.ValidarTarjeta(tarjeta);
        tarjetaRepositorio.Pagar(tarjeta);
    }
}