using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

public class ReservasRepositorio(AppDbContext dbContext) : IReservaRepositorio
{
    public void ReservarPropiedad(Propiedad propiedad, Usuario usuario, DateTime fechaInicio, DateTime fechaFin){
       Reserva reserva = new Reserva(usuario, propiedad, fechaInicio, fechaFin);
       
       propiedadRepositorio.ComprobarDisponibilidad(reserva.Propiedad, reserva.FechaInicio, reserva.FechaFin);//verificar este metodo
       
       //Tarjeta y pago
       var tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(usuario.TarjetaId);
       tarjetaValidador.ValidarTarjetaExistente(tarjeta);
       pagoTarjetaValidador.ValidarPagoTarjeta(reserva, tarjeta);
     
        //confimar y guardar reserva
       reserva.Estado = EstadoReserva.Confirmada;
       dbContext.Reservas.Add(reserva);
       dbContext.SaveChanges();
       Console.WriteLine("Reserva creada correctamente");
    }

    public void ModificarReserva(Reserva reserva){ 
        var reservaExistente = dbContext.Reservas.Find(reserva.Id);
        if (reservaExistente == null)
            throw new Exception("Reserva no encontrada");
            
        dbContext.Entry(reservaExistente).CurrentValues.SetValues(reserva);
        dbContext.SaveChanges();
    }

    public Reserva? ObtenerReservaPorId(int id)
    {
        return dbContext.Reservas.Find(id);
    }   
    /*
    public void CancelarReserva(Reserva reserva){

    }   

    public void RegistrarCheckout(Reserva reserva){

    }
    */
    public List<Reserva> ListarReservas(){
        var reservas = dbContext.Reservas.ToList();
        if (reservas.Count == 0)
            throw new Exception("No se encontraron reservas.");
        return reservas;
    }                           

    public List<Reserva> ListarMisReservas(Usuario usuario){
        var reservas = dbContext.Reservas.Where(r => r.Cliente.Id == usuario.Id).ToList();  
        if (reservas.Count == 0)
            throw new Exception("No se encontraron reservas.");
        return reservas;    
    }



























}
