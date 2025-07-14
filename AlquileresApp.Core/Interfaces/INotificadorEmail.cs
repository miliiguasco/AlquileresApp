
namespace AlquileresApp.Core.Interfaces
{
    public interface INotificadorEmail
    {
        void EnviarEmail(string destinatario, string asunto, string mensaje, string? replyTo = null);
        public void EnviarCorreoBienvenida(string destinatario, string nombreUsuario);
        public void EnviarConfirmacionReserva(string destinatario, string nombreUsuario, string fechaInicio, string fechaFin, string propiedad);

        public void EnviarCorreoModificacionReservaPorNoHabitable(string destinatario, string nombreUsuario, string propiedadOriginalTitulo, 
            string? nuevaPropiedadTitulo);
        public void EnviarLinkRecuperacion(string destinatario, string nombreUsuario, string link);
    }
    
}