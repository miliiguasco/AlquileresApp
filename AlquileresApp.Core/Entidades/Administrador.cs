namespace AlquileresApp.Core.Entidades;

public class Administrador : Usuario
{
    public Administrador(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
        : base(nombre, apellido, email, telefono, password, fechaNacimiento)
    {
    }

    public Administrador()
        : base("", "", "", "", "", DateTime.Now)
    {
    }
} 