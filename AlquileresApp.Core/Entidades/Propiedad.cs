using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;

public class Propiedad
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public decimal PrecioPorNoche { get; set; }
    public int Capacidad { get; set; }
    public TipoPago TipoPago { get; set; }
    public double Latitud { get; set; }
    public string Localidad { get; set; } = string.Empty;
    public double Longitud { get; set; }

    public bool NoHabitable { get; set; } = false;
    public int EncargadoId { get; set; }
    public List<ServiciosPropiedad> ServiciosDisponibles { get; set; } = new();
    public List<Imagen> Imagenes { get; set; } = new();
    public List<Reserva> Reservas { get; set; } = new();
    public PoliticasDeCancelacion PoliticaCancelacion { get; set; }
    public decimal MontoAPagar { get; set; }
    public decimal MontoPagoAnticipado { get; set; }
    public List<Promocion> Promociones { get; set; } = new();
    public bool Destacada { get; set; } = false;
    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    public double CalificacionPromedio { get; set; } = 0; 
    public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
}

