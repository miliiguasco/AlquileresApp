using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;

public class Propiedad
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Localidad { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public decimal PrecioPorNoche { get; set; }
    public int Capacidad { get; set; }
    public TipoPagoReserva TipoPago { get; set; }
    public List<ServiciosPropiedad> ServiciosDisponibles { get; set; } = new();
    public List<Imagen> Imagenes { get; set; } = new();
    public List<Reserva> Reservas { get; set; } = new();
    public decimal PorcentajeAnticipo { get; set; }
    public decimal MontoAnticipo { get; set; }


} 