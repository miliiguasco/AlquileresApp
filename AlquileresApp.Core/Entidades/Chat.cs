namespace AlquileresApp.Core.Entidades;

public class Chat
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public int AdministradorId { get; set; }
    public int ReservaId { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public List<Mensaje> Mensajes { get; set; } = new();
}
