namespace AlquileresApp.Core;

public class Encargado : Usuario
{
    public List<Propiedad> Propiedades { get; set; } = new();
    public string Zona { get; set; } = string.Empty;
} 