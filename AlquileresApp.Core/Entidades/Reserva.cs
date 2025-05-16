namespace AlquileresApp.Core;

public class Reserva
{
    public int Id { get; set; }
    public int UsuarioRegistradoId { get; set; }
    public UsuarioRegistrado Usuario { get; set; } = null!;
    public int PropiedadId { get; set; }
    public Propiedad Propiedad { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public decimal PrecioTotal { get; set; }
    public Tarjeta? Tarjeta { get; set; }
} 