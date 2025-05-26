 using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class PagoTarjetaValidador : IPagoTarjetaValidador
{
    public void ValidarPagoTarjeta(Reserva reserva, Tarjeta tarjeta){
        if (reserva.TipoPago == TipoPagoReserva.Parcial)
        {
            if (!tarjetaRepositorio.Pagar(tarjeta, reserva.MontoPagoParcial)){
            throw new Exception("No se pudo cobrar el anticipo de la reserva");
        };
       } 
       else if (reserva.TipoPago == TipoPagoReserva.Total)
       {
         if (!tarjetaRepositorio.Pagar(tarjeta, reserva.PrecioTotal)){
            throw new Exception("No se pudo cobrar el total de la reserva");
         };
       }
    }
}