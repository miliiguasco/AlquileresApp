namespace AlquileresApp.Core.Entidades;

public class UsuarioRegistrado : Usuario
{
    public List<Reserva> Reservas { get; set; } = new();
    public DateTime FechaRegistro { get; set; }
} 