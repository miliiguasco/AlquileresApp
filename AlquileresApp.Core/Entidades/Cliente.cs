using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.Entidades;

public class Cliente : Usuario
{
    public List<Reserva> Reservas { get; set; } = new();

    public Cliente(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
        : base(nombre, apellido, email, telefono, password, fechaNacimiento, RolUsuario.Cliente)
    {
    }

    public Cliente() : base()
    {
        Rol = RolUsuario.Cliente;
        Reservas = new List<Reserva>();
    }
} 