namespace AlquileresApp.Core.Entidades;

public class Promocion
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public decimal PorcentajeDescuento { get; set; }

    public bool Activa { get; set; } = true;
    
    public bool borrada { get; set; } = false;
}
