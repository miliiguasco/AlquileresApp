namespace AlquileresApp.Core.CasosDeUso.ContactarEncargado;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoContactarEncargado(INotificadorEmail notificadorEmail)
{
    public void Ejecutar(Reserva reserva, string mensaje)
    {
        try
        {
            /*
            string responsable;

            if (reserva.FechaInicio < DateTime.Now)
            {
                responsable = "manupedrob@gmail.com"; // mail del administrador
            }
            else
            {
                responsable = reserva.Encargado.Email ?? "Sin encargado";
            }*/
            notificadorEmail.EnviarEmail(
                "destinatario",
                //responsable,
                reserva.Propiedad?.Titulo ?? "Sin título",
                mensaje,
                reserva.Cliente?.Email ?? "Sin email"
            );
        }
        catch (Exception ex)
        {
            throw new Exception("No se pudo enviar el mensaje al encargado", ex);
        }
    }
}
