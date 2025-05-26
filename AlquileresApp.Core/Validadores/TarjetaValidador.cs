using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class TarjetaValidador : ITarjetaValidador

{

    public void validarTarjeta(Tarjeta tarjeta){
        if (String.IsNullOrWhiteSpace(tarjeta.NumeroTarjeta))
        {
            throw new Exception("El número de tarjeta es requerido");
        }
    }
}