namespace AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class Encargado : Usuario
{
    public Encargado(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
        : base(nombre, apellido, email, telefono, password, fechaNacimiento)
    {
    }

    public Encargado()
        : base("", "", "", "", "", DateTime.Now)
    {
    }
} 