namespace AlquileresApp.Core.CasosDeUso.Reserva;

using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using System.Threading.Tasks;

public class CasoDeUsoRegistrarCheckout(IReservaRepositorio reservaRepositorio)
{
    public async Task Ejecutar(int reservaId)
    {
        // Obtener la reserva por ID
        var reserva = await reservaRepositorio.ObtenerReservaPorId(reservaId);
        if (reserva == null)
        {
            throw new Exception("Reserva no encontrada.");
        }

        // Verificar que la reserva esté en estado "Activa"
        if (reserva.Estado != EstadoReserva.Activa)
        {
            throw new Exception("La reserva no está activa.");
        }

        // Registrar el checkout
        reservaRepositorio.RegistrarCheckout(reserva);
    }
}