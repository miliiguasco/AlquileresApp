using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class TarjetaValidador : ITarjetaValidador

{
    public Tarjeta ObtenerTarjetaPorId(int id){
        return dbContext.Tarjetas.Find(id);
    }
    public void ValidarTarjeta(Tarjeta tarjeta){
        if (String.IsNullOrWhiteSpace(tarjeta.NumeroTarjeta))
        {
            throw new Exception("El número de tarjeta es requerido");
        }
    }
}