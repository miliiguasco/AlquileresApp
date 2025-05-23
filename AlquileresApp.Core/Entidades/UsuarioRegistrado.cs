namespace AlquileresApp.Core.Entidades;

public class UsuarioRegistrado : Usuario
{
    public List<Reserva> Reservas { get; set; } = new();

    public UsuarioRegistrado(string nombre, string apellido, string email, string telefono, string password, DateTime fechaNacimiento)
        : base(nombre, apellido, email, telefono, password, fechaNacimiento)
    {}

    public UsuarioRegistrado()
        : base("", "", "", "", "", DateTime.Now)
    {
        Reservas = new List<Reserva>();
    }
} 