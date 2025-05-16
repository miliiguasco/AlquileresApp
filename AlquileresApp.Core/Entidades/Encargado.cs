namespace AlquileresApp.Core.Entidades;

public class Encargado : Trabajador
{
    public List<Propiedad> Propiedades { get; set; } = new();
    public string Zona { get; set; } = string.Empty;
} 