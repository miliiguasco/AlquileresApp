namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IReservaRepositorio{
    void CrearReserva(Reserva reserva);
    void ModificarReserva(Reserva reserva);
    //cancelar reserva
    public Reserva? ObtenerReservaPorId(int id);
    // void CancelarReserva(Reserva reserva);
    void RegistrarCheckout(Reserva reserva);
   
    public void ModificarReserva2(Reserva reserva);

    public List<Reserva> ListarReservas();
    public List<Reserva> ListarMisReservas(int usuario);
    void Actualizar(Reserva reserva);
    //  public List<Reserva> ListarMisReservas(Usuario usuario);
    //  public void RegistrarCheckout(Reserva reserva);

}


