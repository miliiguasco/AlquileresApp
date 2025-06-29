namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IChatRepositorio
{
    Task<Chat?> ObtenerConMensajesAsync(int chatId);
    Task AgregarMensajeAsync(Mensaje mensaje);
    Task CrearAsync(Chat chat);
    Task<Chat?> ObtenerChatPorReservaAsync(int reservaId);
}
