
namespace AlquileresApp.Core.Interfaces
{
    public interface INotificadorEmail
    {
        void EnviarEmail(string destinatario, string asunto, string mensaje);
        public void EnviarCorreoBienvenida(string destinatario, string nombreUsuario);
        public void EnviarConfirmacionReserva(string destinatario, string nombreUsuario, string fechaInicio, string fechaFin, string propiedad);
    }
    
}