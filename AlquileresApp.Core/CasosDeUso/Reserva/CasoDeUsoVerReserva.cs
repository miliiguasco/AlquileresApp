namespace AlquileresApp.Core.CasosDeUso.Reserva;

using System.Threading.Tasks;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoVerReserva : ICasoDeUsoVerReserva
{
    private readonly IReservaRepositorio _reservaRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public CasoDeUsoVerReserva(
        IReservaRepositorio reservaRepositorio,
        IUsuarioRepositorio usuarioRepositorio)
    {
        _reservaRepositorio = reservaRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task<Reserva?> Ejecutar(int reservaId, int usuarioId)
    {
        var usuario = _usuarioRepositorio.ObtenerUsuarioPorId(usuarioId);
        if (usuario == null)
        {
            throw new Exception("El usuario no existe.");
        }

        var reserva = await _reservaRepositorio.ObtenerReservaPorId(reservaId);
        if (reserva == null)
        {
            return null;
        }

        // Verificar que la reserva pertenece al usuario
        if (reserva.ClienteId != usuarioId)
        {
            return null;
        }

        return reserva;
    }
} 