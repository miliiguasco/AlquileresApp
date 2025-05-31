namespace AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
public class CasoDeUsoObtenerReserva(IReservaRepositorio _repo)
{

    public Reserva Ejecutar(int id)
    {
        return _repo.ObtenerReservaPorId(id);
    }
}