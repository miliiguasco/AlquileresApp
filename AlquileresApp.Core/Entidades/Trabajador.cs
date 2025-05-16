namespace AlquileresApp.Core;

public abstract class Trabajador : Usuario
{
    public DateTime FechaContratacion { get; set; }
    public decimal Salario { get; set; }
} 