namespace AlquileresApp.Core.Entidades;

public class Mensaje
{
    public int Id { get; set; }

    public int ChatId { get; set; }
    public Chat? Chat { get; set; }

    public int EmisorId { get; set; }   // Puede ser Cliente o Administrador
    public string Contenido { get; set; } = string.Empty;
    public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
}
