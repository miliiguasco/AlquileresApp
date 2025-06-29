namespace AlquileresApp.Core.CasosDeUso.Chat;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoEnviarMensaje(IChatRepositorio ChatRepositorio)
{

    public async Task EnviarMensaje(int chatId, int emisorId, string contenido)
    {
        var mensaje = new Mensaje {
            ChatId = chatId,
            EmisorId = emisorId,
            Contenido = contenido,
            FechaEnvio = DateTime.UtcNow
        };

        await ChatRepositorio.AgregarMensajeAsync(mensaje);
    }

}


