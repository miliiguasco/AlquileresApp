namespace AlquileresApp.Core.CasosDeUso.Reserva;

using System.Threading.Tasks;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
public class CasoDeUsoObtenerReserva(IReservaRepositorio _repo)
{
    public async Task<Reserva> Ejecutar(int id)
    {
        return await _repo.ObtenerReservaPorId(id);
    }
}