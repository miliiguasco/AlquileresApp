namespace AlquileresApp.Core.Entidades;

public class Tarjeta
{
    public int Id { get; set; }
    public string NumeroTarjeta { get; set; }
    public Usuario Titular { get; set; }    
    public string FechaVencimiento { get; set; }    
    public string CVV { get; set; }
    public decimal Saldo { get; set; }  .
    //public int ReservaId { get; set; }
    //public Reserva Reserva { get; set; } = null!;
} 