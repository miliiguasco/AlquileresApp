/*using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlquileresApp.Data;

public class MensajeRepositorio : IMensajeRepositorio
{
    private readonly AppDbContext _context;

    public MensajeRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Mensaje>> ObtenerMensajesPorReservaAsync(int reservaId)
    {
        return await _context.Mensajes
            .Include(m => m.Cliente)
            .Include(m => m.Administrador)
            .Include(m => m.Reserva)
            .Where(m => m.ReservaId == reservaId)
            .OrderBy(m => m.Fecha)
            .ToListAsync();
    }

    public async Task<Mensaje> CrearMensajeAsync(Mensaje mensaje)
    {
        _context.Mensajes.Add(mensaje);
        await _context.SaveChangesAsync();
        return mensaje;
    }

    public async Task<List<Mensaje>> ObtenerMensajesPorClienteAsync(int clienteId)
    {
        return await _context.Mensajes
            .Include(m => m.Cliente)
            .Include(m => m.Administrador)
            .Include(m => m.Reserva)
            .Where(m => m.ClienteId == clienteId)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();
    }

    public async Task<List<Mensaje>> ObtenerMensajesPorAdministradorAsync(int administradorId)
    {
        return await _context.Mensajes
            .Include(m => m.Cliente)
            .Include(m => m.Administrador)
            .Include(m => m.Reserva)
            .Where(m => m.AdministradorId == administradorId)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();
    }

    public async Task<Mensaje?> ObtenerMensajePorIdAsync(int mensajeId)
    {
        return await _context.Mensajes
            .Include(m => m.Cliente)
            .Include(m => m.Administrador)
            .Include(m => m.Reserva)
            .FirstOrDefaultAsync(m => m.Id == mensajeId);
    }

    public async Task<bool> EliminarMensajeAsync(int mensajeId)
    {
        var mensaje = await _context.Mensajes.FindAsync(mensajeId);
        if (mensaje == null)
            return false;

        _context.Mensajes.Remove(mensaje);
        await _context.SaveChangesAsync();
        return true;
    }

} */