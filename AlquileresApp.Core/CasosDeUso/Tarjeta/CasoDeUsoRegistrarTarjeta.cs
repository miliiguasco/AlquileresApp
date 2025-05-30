namespace AlquileresApp.Core.CasosDeUso.Tarjeta;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public class CasoDeUsoRegistrarTarjeta (ITarjetaRepositorio tarjetaRepositorio, ITarjetaValidador tarjetaValidador)
{
    public void Ejecutar(int clienteId, string numeroTarjeta, string titular, string fechaVencimiento, string cvv, decimal saldo)
    {
        var tarjeta = new Tarjeta
        {
            NumeroTarjeta = numeroTarjeta,
            Titular = titular,
            FechaVencimiento = fechaVencimiento,
            CVV = cvv,
            Saldo = saldo,
            ClienteId = clienteId
        };

        tarjetaValidador.ValidarDatos(tarjeta);
        tarjetaRepositorio.RegistrarTarjeta(tarjeta);
    }
}
      