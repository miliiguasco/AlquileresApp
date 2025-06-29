using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlquileresApp.Data;

public class ChatRepositorio(AppDbContext dbContext) : IChatRepositorio

{

    public async Task<Chat?> ObtenerChatPorReservaAsync(int reservaId)
    {
        return await dbContext.Chats
            .FirstOrDefaultAsync(c => c.ReservaId == reservaId);
    }

    public async Task CrearAsync(Chat chat)
    {
        dbContext.Chats.Add(chat);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Chat?> ObtenerConMensajesAsync(int chatId)
    {
        return await dbContext.Chats
            .Include(c => c.Mensajes)
            .FirstOrDefaultAsync(c => c.Id == chatId);
    }

    public async Task AgregarMensajeAsync(Mensaje mensaje)
    {
        dbContext.Mensajes.Add(mensaje);
        await dbContext.SaveChangesAsync();
    }

}