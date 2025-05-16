namespace AlquileresApp.Core.Entidades;

public class Tarjeta
{
    public int Id { get; set; }
    public string NumeroTarjeta { get; set; } = string.Empty;
    public string Titular { get; set; } = string.Empty;
    public string FechaVencimiento { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    public int ReservaId { get; set; }
    public Reserva Reserva { get; set; } = null!;
} 