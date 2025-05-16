namespace AlquileresApp.Core.Entidades;

public abstract class Trabajador : Usuario
{
    public DateTime FechaContratacion { get; set; }
    public decimal Salario { get; set; }
} 