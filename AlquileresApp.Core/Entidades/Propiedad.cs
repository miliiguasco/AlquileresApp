namespace AlquileresApp.Core;

public class Propiedad
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public decimal PrecioPorNoche { get; set; }
    public int Capacidad { get; set; }
    public List<ServiciosPropiedad> ServiciosDisponibles { get; set; } = new();
    public List<Imagen> Imagenes { get; set; } = new();
    public int EncargadoId { get; set; }
    public Encargado Encargado { get; set; } = null!;
} 