namespace AlquileresApp.Core.CasosDeUso.ContactarAdmin;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoContactarAdmin(INotificadorEmail notificadorEmail)
{
    public void Ejecutar(Reserva reserva, string mensaje)
    {
        try
        {
            notificadorEmail.EnviarEmail(
                "manupedrob@gmail.com",
                reserva.Propiedad?.Titulo ?? "Sin título",
                mensaje,
                reserva.Cliente?.Email ?? ""
            );
        }
        catch (Exception ex)
        {
            throw new Exception("No se pudo enviar el mensaje al administrador", ex);
        }
    }
}
