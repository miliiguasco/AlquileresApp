using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;

public abstract class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public RolUsuario Rol { get; protected set; }
    
    public ICollection<Comentario> ComentariosRealizados { get; set; } = new List<Comentario>();

    public ICollection<Calificacion> CalificacionesRealizadas { get; set; } = new List<Calificacion>();

    protected Usuario(string nombre, string apellido, string email, string? telefono, string password, DateTime? fechaNacimiento, RolUsuario rol)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        Telefono = telefono;
        Contraseña = password;
        FechaNacimiento = fechaNacimiento;
        Rol = rol;
    }

    protected Usuario() 
    {
        Nombre = "";
        Apellido = "";
        Email = "";
        Telefono = "";
        Contraseña = "";
        FechaNacimiento = null;
    }
}