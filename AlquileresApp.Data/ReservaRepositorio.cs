using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

public class ReservasRepositorio(AppDbContext dbContext) : IReservaRepositorio
{
    public void CrearReserva(Cliente  cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin){
       Reserva reserva = new Reserva(cliente, propiedad, fechaInicio, fechaFin);
       dbContext.Reservas.Add(reserva);
       dbContext.SaveChanges();
       Console.WriteLine("Reserva creada correctamente");
    }

    public void ModificarReserva(Reserva reserva){ 
        var reservaExistente = dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .FirstOrDefault(r => r.Id == reserva.Id);
        if (reservaExistente == null)
            throw new Exception("Reserva no encontrada");
            
        dbContext.Entry(reservaExistente).CurrentValues.SetValues(reserva);
        dbContext.SaveChanges();
    }

    public Reserva? ObtenerReservaPorId(int id)
    {
        return dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .FirstOrDefault(r => r.Id == id);
    }   
    /*
    public void CancelarReserva(Reserva reserva){

    }   

    public void RegistrarCheckout(Reserva reserva){

    }
    */
    public List<Reserva> ListarReservas(){
        var reservas = dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .ToList();
        if (reservas.Count == 0)
            throw new Exception("No se encontraron reservas.");
        return reservas;
    }                           

    public List<Reserva> ListarMisReservas(Usuario usuario){
        var reservas = dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .Where(r => r.Cliente.Id == usuario.Id)
            .ToList();  
        if (reservas.Count == 0)
            throw new Exception("No se encontraron reservas.");
        return reservas;    
    }



























}
