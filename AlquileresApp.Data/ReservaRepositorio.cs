using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

public class ReservaRepositorio(AppDbContext dbContext) : IReservaRepositorio
{
    public void ReservarPropiedad(Propiedad propiedad, Usuario usuario){
       /*Reserva reserva = new Reserva();
       reserva.Propiedad = propiedad;
       reserva.Usuario = usuario;
       dbContext.Reservas.Add(reserva);
       dbContext.SaveChanges();*/
    }

    public void ModificarReserva(Reserva reserva){ 
        /*var reservaExistente = dbContext.Reservas.Find(reserva.Id);
        if (reservaExistente == null)
            throw new Exception("Reserva no encontrada");
            
        dbContext.Entry(reservaExistente).CurrentValues.SetValues(reserva);
        dbContext.SaveChanges();*/
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

    /*public List<Reserva> ListarMisReservas(Usuario usuario){
        var reservas = dbContext.Reservas.Where(r => r.UsuarioId == usuario.Id).ToList();
        if (reservas.Count == 0)
            throw new Exception("No se encontraron reservas.");
        return reservas;    
    }*/
     
    public List<Reserva> ListarMisReservas(Usuario usuario){
        throw new NotImplementedException();
    }


    public void CancelarReserva(Reserva reserva)
    {
        // TODO: Implementa la lógica para cancelar una reserva
        throw new NotImplementedException();
    }

    public void RegistrarCheckout(Reserva reserva)
    {
        // TODO: Implementa la lógica para registrar el checkout
        throw new NotImplementedException();
    }




























}
