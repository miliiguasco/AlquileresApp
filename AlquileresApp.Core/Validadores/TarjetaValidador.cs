using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;

public class TarjetaValidador : ITarjetaValidador
{
    public void ValidarDatos(Tarjeta tarjeta)
    {
        if (tarjeta == null)
            throw new ArgumentNullException(nameof(tarjeta), "La tarjeta no puede ser nula");

        ValidarNumeroTarjeta(tarjeta.NumeroTarjeta);
        ValidarTitular(tarjeta.Titular);
        ValidarFechaVencimiento(tarjeta.FechaVencimiento);
        ValidarCVV(tarjeta.CVV);
        ValidarSaldo(tarjeta.Saldo);
    }

    private void ValidarNumeroTarjeta(string numeroTarjeta)
    {
        if (string.IsNullOrWhiteSpace(numeroTarjeta))
            throw new ArgumentException("El número de tarjeta es requerido");

        // Remover espacios y caracteres no numéricos
        var numeroLimpio = new string(numeroTarjeta.Where(char.IsDigit).ToArray());

        if (numeroLimpio.Length != 16)
            throw new ArgumentException("El número de tarjeta es inválido");
    }

    private void ValidarTitular(string titular)
    {
        if (string.IsNullOrWhiteSpace(titular))
            throw new ArgumentException("El titular de la tarjeta es requerido");
    }

    private void ValidarFechaVencimiento(string fechaVencimiento)
    {
        if (string.IsNullOrWhiteSpace(fechaVencimiento))
            throw new ArgumentException("La fecha de vencimiento es requerida");

        if (fechaVencimiento.Length != 5 || !fechaVencimiento.Contains('/'))
            throw new ArgumentException("La fecha de vencimiento debe tener el formato MM/AA");

        var partes = fechaVencimiento.Split('/');
        if (partes.Length != 2)
            throw new ArgumentException("La fecha de vencimiento debe tener el formato MM/AA");

        if (!int.TryParse(partes[0], out int mes) || !int.TryParse(partes[1], out int anio))
            throw new ArgumentException("La fecha de vencimiento contiene caracteres inválidos");

        if (mes < 1 || mes > 12)
            throw new ArgumentException("El mes debe estar entre 01 y 12");

        var fechaActual = DateTime.Now;
        var anioActual = fechaActual.Year % 100; // Obtener los últimos dos dígitos del año

        if (anio < anioActual || (anio == anioActual && mes < fechaActual.Month))
            throw new ArgumentException("La tarjeta está vencida");
    }

    private void ValidarCVV(string cvv)
    {
        if (string.IsNullOrWhiteSpace(cvv))
            throw new ArgumentException("El código de seguridad (CVV) es requerido");

        if (cvv.Length != 3)
            throw new ArgumentException("El código de seguridad (CVV) es inválido");

        if (!cvv.All(char.IsDigit))
            throw new ArgumentException("El código de seguridad (CVV) debe contener solo números");
    }

    private void ValidarSaldo(decimal saldo)
    {
        if (saldo < 0)
            throw new ArgumentException("El saldo no puede ser negativo");
    }
    
}
