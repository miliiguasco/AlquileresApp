using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;

public class Administrador : Usuario
{
    public Administrador(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
        : base(nombre, apellido, email, telefono, password, fechaNacimiento, RolUsuario.Administrador)
    {
    }

    public Administrador()
        : base("", "", "", "", "", DateTime.Now, RolUsuario.Administrador)
    {
    }

    // Funcionalidades específicas del Administrador
    public void GestionarUsuarios() 
    {
        // Implementación pendiente
    }

    public void GestionarPropiedades()
    {
        // Implementación pendiente
    }
} 