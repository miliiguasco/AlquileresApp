using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;


public class Encargado : Usuario
{
    public Encargado(string nombre, string apellido, string email, string password)
        : base(nombre, apellido, email, null, password, null, RolUsuario.Encargado)
    {
    }

    public Encargado()
        : base("", "", "", "", "", DateTime.Now, RolUsuario.Encargado)
    {
    }
} 