
namespace AlquileresApp.Core.Interfaces
{
    
public interface INotificadorEmail
{
    void EnviarEmail(string destinatario, string asunto, string mensaje, string? replyTo = null);
    void EnviarCorreoBienvenida(string destinatario, string nombreUsuario);
    void EnviarConfirmacionReserva(string destinatario, string nombreUsuario, string fechaInicio, string fechaFin, string propiedad);
}

    
}