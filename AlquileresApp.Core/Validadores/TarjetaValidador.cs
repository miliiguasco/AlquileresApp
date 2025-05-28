using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class TarjetaValidador : ITarjetaValidador

{
    public void ValidarTarjeta(Tarjeta tarjeta){
        if (tarjeta == null)
        {
            throw new Exception("La tarjeta no existe");
        }
    }
}