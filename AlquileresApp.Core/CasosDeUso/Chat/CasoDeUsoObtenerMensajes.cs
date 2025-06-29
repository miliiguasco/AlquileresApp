namespace AlquileresApp.Core.CasosDeUso.Chat;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoObtenerMensajes(IChatRepositorio ChatRepositorio)
{
    public async Task<Chat> ObtenerChatConMensajes(int chatId)
        => await ChatRepositorio.ObtenerConMensajesAsync(chatId);
}




    