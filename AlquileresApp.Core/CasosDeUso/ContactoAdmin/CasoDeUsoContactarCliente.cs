namespace AlquileresApp.Core.CasosDeUso.ContactarCliente;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;


public class CasoDeUsoContactarCliente(INotificadorEmail notificadorEmail)
{
    public void Ejecutar(Reserva reserva, string mensaje)
    {
        try
        {
            notificadorEmail.EnviarEmail(
                reserva.Cliente.Email,
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
