namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IReservaRepositorio{
    void CrearReserva(Reserva reserva);
    void ModificarReserva(Reserva reserva);
    //cancelar reserva
    public Reserva? ObtenerReservaPorId(int id);
    public List<Reserva> ListarReservas();
    public List<Reserva> ListarMisReservas(Usuario usuario);
    public void RegistrarCheckout(Reserva reserva, int empleadoId);
}


