namespace AlquileresApp.Core.Entidades;

public class Imagen
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int PropiedadId { get; set; }
    public Propiedad Propiedad { get; set; } = null!;
} 