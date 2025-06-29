namespace AlquileresApp.Core.CasosDeUso.Chat;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoIniciarChat(IChatRepositorio chatRepositorio)
{
    public async Task<Chat> Ejecutar(int reservaId, int clienteId, int adminId)
    {
        var chatExistente = await chatRepositorio.ObtenerChatPorReservaAsync(reservaId);

        if (chatExistente != null)
            return chatExistente;

        var nuevoChat = new Chat
        {
            ClienteId = clienteId,
            AdministradorId = adminId,
            ReservaId = reservaId,
            FechaInicio = DateTime.UtcNow
        };

        await chatRepositorio.CrearAsync(nuevoChat);
        return nuevoChat;
    }
}