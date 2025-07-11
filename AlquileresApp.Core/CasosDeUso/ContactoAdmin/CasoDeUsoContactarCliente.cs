namespace AlquileresApp.Core.CasosDeUso.ContactarCliente;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoContactarCliente(INotificadorEmail notificadorEmail)
{
    public void Ejecutar(Reserva reserva, string mensaje)
    {
        try
        {
            /*string replyTo;

            if (reserva.FechaInicio < DateTime.Now)
            {
                replyTo = "manupedrob@gmail.com"; // mail del administrador
            }
            else
            {
                replyTo = reserva.Encargado.Email ?? "Sin encargado";
            }*/
            notificadorEmail.EnviarEmail(
                reserva.Cliente?.Email ?? "",
                reserva.Propiedad?.Titulo ?? "Sin título",
                mensaje,
                "email replyto"
                //replyTo
            );
        }
        catch (Exception ex)
        {
            throw new Exception("No se pudo enviar el mensaje al encargado", ex);
        }
    }
}
