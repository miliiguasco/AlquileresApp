namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IReservaRepositorio{
    void CrearReserva(Cliente cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin, int cantidadHuespedes);
    void ModificarReserva(Reserva reserva);
    public Reserva? ObtenerReservaPorId(int id);
    // void CancelarReserva(Reserva reserva);
    // void RegistrarCheckout(Reserva reserva);
   
    public void ModificarReserva2(Reserva reserva);

    public List<Reserva> ListarReservas();
    public List<Reserva> ListarMisReservas(Usuario usuario);
}


