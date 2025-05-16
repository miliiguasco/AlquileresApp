namespace AlquileresApp.Core.Entidades;

public abstract class Usuario 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
}