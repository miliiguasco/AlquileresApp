using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

public class ReservaRepositorio(AppDbContext dbContext) : IReservaRepositorio
{
    public void CrearReserva(Cliente cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin, int cantidadHuespedes)
    {
        try
        {
            Console.WriteLine($"Creando reserva: ClienteId={cliente.Id}, PropiedadId={propiedad.Id}");
            Console.WriteLine($"Conexión a la base de datos: {dbContext.Database.GetConnectionString()}");
            
            // Verificar si la base de datos existe
            Console.WriteLine($"Base de datos existe: {dbContext.Database.CanConnect()}");
            
            Reserva reserva = new Reserva(cliente, propiedad, fechaInicio, fechaFin, cantidadHuespedes);
            Console.WriteLine($"Reserva creada con ID: {reserva.Id}, Estado: {reserva.Estado}, PrecioTotal: {reserva.PrecioTotal}");
            
            dbContext.Reservas.Add(reserva);
            Console.WriteLine("Reserva agregada al contexto");
            
            var result = dbContext.SaveChanges();
            Console.WriteLine($"SaveChanges result: {result} filas afectadas");
            
            if (result > 0)
            {
                // Verificar que la reserva se guardó
                var reservaGuardada = dbContext.Reservas.Find(reserva.Id);
                Console.WriteLine($"Reserva guardada en BD: {(reservaGuardada != null ? "Sí" : "No")}");
                if (reservaGuardada != null)
                {
                    Console.WriteLine($"Datos de la reserva guardada: Id={reservaGuardada.Id}, ClienteId={reservaGuardada.ClienteId}, PropiedadId={reservaGuardada.PropiedadId}");
                }
            }
            else
            {
                throw new Exception("No se pudo crear la reserva en la base de datos");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear reserva: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
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
        if (reservas.Count == 0)
            throw new Exception("No se encontraron reservas.");
        return reservas;    
    }
     /*
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

*/


























}
