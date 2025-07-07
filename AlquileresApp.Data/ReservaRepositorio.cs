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

    public async Task EliminarAsync(int id)
    {
        var reserva = await dbContext.Reservas.FindAsync(id);
        if (reserva != null)
        {
            dbContext.Reservas.Remove(reserva);
            await dbContext.SaveChangesAsync();
        }
    }
    public void Actualizar(Reserva reserva)
    {
        // Asegura que la entidad está siendo rastreada por el contexto
        dbContext.Reservas.Update(reserva);
        dbContext.SaveChanges();
    }
    public void ModificarReserva(Reserva reserva)
    {
        var reservaExistente = dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .FirstOrDefault(r => r.Id == reserva.Id);
        if (reservaExistente == null)
            throw new Exception("Reserva no encontrada");

        dbContext.Entry(reservaExistente).CurrentValues.SetValues(reserva);
        dbContext.SaveChanges();
    }
    public async Task<Reserva?> ObtenerReservaPorId(int id)
    {
        return await dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    public IEnumerable<Reserva> ObtenerReservasPorUsuarioYPropiedad(int? usuarioId, int propiedadId)
    {
       
        return dbContext.Reservas
                       .Include(r => r.Cliente)
                       .Include(r => r.Propiedad)
                       .Where(r => r.ClienteId == usuarioId && r.PropiedadId == propiedadId)
                       .ToList(); // Materializa la consulta para devolver una lista
    }

    public void ModificarReserva2(Reserva reserva)
    {
        var existingReserva = dbContext.Reservas.Find(reserva.Id);

        if (existingReserva != null)
        {
            // Actualizar las propiedades necesarias
            existingReserva.PropiedadId = reserva.Propiedad.Id; 
            existingReserva.Propiedad = reserva.Propiedad;    

            dbContext.Reservas.Update(existingReserva); 
            dbContext.SaveChanges();
        }
        else
        {
            // Manejar el caso en que la reserva no existe
            throw new Exception($"No se encontró la reserva con ID: {reserva.Id}");
        }
    }


    public List<Reserva> ListarReservas()
    {
        Console.WriteLine("📊 Iniciando ListarReservas...");
        var reservas = dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .ToList();
        Console.WriteLine($"✅ Se encontraron {reservas.Count} reservas");
        foreach (var reserva in reservas)
        {
            Console.WriteLine($"Reserva ID: {reserva.Id}, Cliente: {reserva.Cliente?.Nombre}, Propiedad: {reserva.Propiedad?.Titulo}");
        }
        return reservas;
    }

    public List<Reserva> ListarMisReservas(int usuario)
    {
        var reservas = dbContext.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Propiedad)
            .Where(r => r.Cliente.Id == usuario)
            .ToList();
        return reservas;
    }


    public async Task<bool> SeSuperponeAsync(int propiedadId, DateTime inicio, DateTime fin)
    {
        return await dbContext.Reservas
            .Where(r => r.PropiedadId == propiedadId)
            .AnyAsync(r => r.seSuperpone(inicio, fin));
    }


    public void RegistrarCheckout(Reserva reserva)
    {
        reserva.Estado = EstadoReserva.Finalizada;
        reserva.FechaCheckOut = DateTime.Now;
        dbContext.SaveChanges();
    }
}



















