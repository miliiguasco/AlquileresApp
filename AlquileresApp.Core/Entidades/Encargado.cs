using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;


public class Encargado : Usuario
{
    public Encargado(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
        : base(nombre, apellido, email, telefono, password, fechaNacimiento, RolUsuario.Encargado)
    {
    }

    public Encargado()
        : base("", "", "", "", "", DateTime.Now, RolUsuario.Encargado)
    {
    }

    // Funcionalidades específicas del Encargado
    public void GestionarPropiedad(Propiedad propiedad)
    {
        // Implementación pendiente
    }

    public void ResponderConsultas()
    {
        // Implementación pendiente
    }
} 