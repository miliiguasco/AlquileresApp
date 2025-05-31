namespace AlquileresApp.Core.CasosDeUso.Tarjeta;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoModificarTarjeta(ITarjetaRepositorio tarjetaRepositorio)
{
    public async Task Ejecutar(int  tarjetaId, string numeroTarjeta, string titular, string fechaVencimiento, string cvv)
    {
        var tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(tarjetaId);
        if (tarjeta == null)
        {
            throw new Exception("La tarjeta no existe.");
        }
        tarjeta.NumeroTarjeta = numeroTarjeta;
        tarjeta.Titular = titular;
        tarjeta.FechaVencimiento = fechaVencimiento;
        tarjeta.CVV = cvv;
        tarjetaRepositorio.ModificarTarjeta(tarjeta);
    }
}