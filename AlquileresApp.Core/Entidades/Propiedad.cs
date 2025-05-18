using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;

public class Propiedad
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public decimal PrecioPorNoche { get; set; }
    public int Capacidad { get; set; }
    public List<ServiciosPropiedad> ServiciosDisponibles { get; set; } = new();
    public List<Imagen> Imagenes { get; set; } = new();
} 