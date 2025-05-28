namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IPagoTarjetaValidador{
    void ValidarPagoTarjeta(Reserva reserva, Tarjeta tarjeta);
}
