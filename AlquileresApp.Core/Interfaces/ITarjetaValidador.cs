namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
public interface ITarjetaValidador
{
    public void ValidarTarjeta(Tarjeta tarjeta);
    public Tarjeta ObtenerTarjetaPorId(int id);
}
