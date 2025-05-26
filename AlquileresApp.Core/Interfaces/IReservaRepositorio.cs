namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IReservaRepositorio{
    void ReservarPropiedad(Propiedad propiedad, Usuario usuario);
    void ModificarReserva(Reserva reserva);
    public Reserva? ObtenerReservaPorId(int id);
    void CancelarReserva(Reserva reserva);
    void RegistrarCheckout(Reserva reserva);
    public List<Reserva> ListarReservas();
    public List<Reserva> ListarMisReservas(Usuario usuario);
}


