namespace AlquileresApp.Core.CasosDeUso.Tarjeta;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoVisualizarTarjeta(ITarjetaRepositorio tarjetaRepositorio)
{
    public List<Tarjeta> Ejecutar(int usuarioId)
    {
        var tarjetas = tarjetaRepositorio.ObtenerTarjetasPorUsuario(usuarioId);
        return tarjetas ?? new List<Tarjeta>();
    }
}
