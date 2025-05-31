namespace AlquileresApp.Core.CasosDeUso.Tarjeta;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoEliminarTarjeta(ITarjetaRepositorio tarjetaRepositorio)
{
    public async Task Ejecutar(int  tarjetaId)
    {
        var tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(tarjetaId);
        if (tarjeta == null)
        {
            throw new Exception("La tarjeta no existe.");
        }
        tarjetaRepositorio.EliminarTarjeta(tarjeta);
    }
}