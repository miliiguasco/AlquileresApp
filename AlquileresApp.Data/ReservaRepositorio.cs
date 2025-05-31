using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Core.Enumerativos;

public class ReservaRepositorio(AppDbContext dbContext) : IReservaRepositorio
{
    public void CrearReserva(Reserva reserva)
    {
        try
        {         
            dbContext.Reservas.Add(reserva);      
            dbContext.SaveChanges();            
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al crear la reserva: {ex.Message}");
        }
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
        return reservas;    
    }

    public void RegistrarCheckout(Reserva reserva)
    {
        reserva.Estado = EstadoReserva.Finalizada;
        reserva.FechaCheckOut = DateTime.Now;
        dbContext.SaveChanges();
    }

    


    public void CancelarReserva(Reserva reserva)
    {
        // TODO: Implementa la lógica para cancelar una reserva
        throw new NotImplementedException();
    }




























}
