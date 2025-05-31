namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IReservaRepositorio{
    void CrearReserva(Cliente cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin, int cantidadHuespedes);
    void ModificarReserva(Reserva reserva);
    public Reserva? ObtenerReservaPorId(int id);
    public Task EliminarAsync(int id);
    public Task<bool> SeSuperponeAsync(int propiedadId, DateTime inicio, DateTime fin);
   // void CancelarReserva(Reserva reserva);
   // void RegistrarCheckout(Reserva reserva);
    public List<Reserva> ListarReservas();
    public List<Reserva> ListarMisReservas(int usuario);
    void Actualizar(Reserva reserva);

}


