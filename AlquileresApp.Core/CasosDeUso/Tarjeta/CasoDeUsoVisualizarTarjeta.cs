namespace AlquileresApp.Core.CasosDeUso.Tarjeta;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoVisualizarTarjeta(ITarjetaRepositorio tarjetaRepositorio)
{
    public Tarjeta Ejecutar(int id)
    {
        var tarjeta = tarjetaRepositorio.ObtenerPorClienteId(id);
        return tarjeta;
    }
}
