namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IReservaRepositorio{
    void CrearReserva(Reserva reserva);
    void ModificarReserva(Reserva reserva);
    //cancelar reserva
    Task<Reserva?> ObtenerReservaPorId(int id);
   


    public Task EliminarAsync(int id);
    public Task<bool> SeSuperponeAsync(int propiedadId, DateTime inicio, DateTime fin);
   // void CancelarReserva(Reserva reserva);
   // void RegistrarCheckout(Reserva reserva);
    public List<Reserva> ListarReservas();
    public List<Reserva> ListarMisReservas(int usuario);
    void Actualizar(Reserva reserva);
    public void RegistrarCheckout(Reserva reserva);

    IEnumerable<Reserva> ObtenerReservasPorUsuarioYPropiedad(int? usuarioId, int propiedadId);
    public void ModificarReserva2(Reserva reserva);
}


