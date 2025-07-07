namespace AlquileresApp.Core.CasosDeUso.InformarCheckOut;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoInformarCheckOut(IReservaRepositorio reservasRepositorio)
{
    public async Task Ejecutar(int  reservaId, int empleadoId)
    {
        var reserva = await reservasRepositorio.ObtenerReservaPorId(reservaId);
        if (reserva == null)
        {
            throw new Exception("La reserva no existe.");
        }
        if (reserva.Estado != EstadoReserva.Activa)
            throw new Exception("No se puede hacer check-out. La reserva no está activa.");

        if (DateTime.Now < reserva.FechaInicio)
            throw new Exception("No se puede hacer check-out antes del check-in.");
        
       // reservasRepositorio.RegistrarCheckout(reserva);
        reservasRepositorio.ModificarReserva(reserva);
    }
}