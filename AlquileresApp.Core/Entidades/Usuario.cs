namespace AlquileresApp.Core.Entidades;

public abstract class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }

    public Usuario(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        Telefono = telefono;       
        Password = password;
        FechaNacimiento = fechaNacimiento;  
    }
    
}