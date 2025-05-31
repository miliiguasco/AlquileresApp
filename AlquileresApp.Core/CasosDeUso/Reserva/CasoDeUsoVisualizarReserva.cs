namespace AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoVisualizarReserva(
    IReservaRepositorio reservaRepositorio,
    IUsuarioRepositorio usuarioRepositorio)
{
    public List<Reserva> Ejecutar(int usuarioId)
    {
        var usuario = usuarioRepositorio.ObtenerUsuarioPorId(usuarioId);
        if (usuario == null)
        {
            throw new Exception("El usuario no existe.");
        }

        var reservas = reservaRepositorio.ListarMisReservas(usuario);
        if (reservas == null)
        {
            throw new Exception("No se pudieron obtener las reservas.");
        }
        return reservas;
    }
}
