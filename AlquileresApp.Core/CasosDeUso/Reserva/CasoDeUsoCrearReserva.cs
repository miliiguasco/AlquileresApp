namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio,  IReservaValidador reservaValidador)
{
    public void Ejecutar(Reserva reserva){
        reservaValidador.FechaValidador(reserva);
        reservasRepositorio.ReservarPropiedad(reserva);
    }
}