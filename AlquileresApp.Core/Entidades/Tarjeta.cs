namespace AlquileresApp.Core.Entidades;

public class Tarjeta
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public required string NumeroTarjeta { get; set; }
    public required string Titular { get; set; }
    public required string FechaVencimiento { get; set; }
    public required string CVV { get; set; }
    public decimal Saldo { get; set; }

    public Tarjeta() { }

    public Tarjeta(string numeroTarjeta, string titular, string fechaVencimiento, string cvv, decimal saldo, int clienteId)
    {
        NumeroTarjeta = numeroTarjeta;
        Titular = titular;
        FechaVencimiento = fechaVencimiento;
        CVV = cvv;
        Saldo = saldo;
        ClienteId = clienteId;
    }
} 