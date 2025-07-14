namespace AlquileresApp.Data;
using System.Net;
using System.Net.Mail;
using AlquileresApp.Core.Interfaces;

public class NotificadorEmail : INotificadorEmail
{
    private readonly string _remitente;
    private readonly string _clave;
    private readonly string _servidor;
    private readonly int _puerto;

    public NotificadorEmail(string remitente, string clave, string servidor = "smtp.gmail.com", int puerto = 587)
    {
        _remitente = remitente;
        _clave = clave;
        _servidor = servidor;
        _puerto = puerto;
    }

    public void EnviarEmail(string para, string asunto, string cuerpo, string? replyTo = null)
{
    using var cliente = new SmtpClient(_servidor, _puerto)
    {
        Credentials = new NetworkCredential(_remitente, _clave),
        EnableSsl = true
    };

    var mensaje = new MailMessage
    {
        From = new MailAddress(_remitente),
        Subject = asunto,
        Body = cuerpo,
        IsBodyHtml = true
    };

    mensaje.To.Add(para);

    if (!string.IsNullOrEmpty(replyTo))
    {
        mensaje.ReplyToList.Add(new MailAddress(replyTo));
    }

    cliente.Send(mensaje);
}


    public void EnviarCorreoBienvenida(string destinatario, string nombreUsuario)
    {
        string asunto = "¡Bienvenido a Alquilando!";
        string cuerpo = @$"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
            <meta charset='UTF-8'>
        </head>
        <body style='margin:0; padding:0; background-color:#fafafa;'>
            <table width='100%' cellpadding='0' cellspacing='0' border='0' style='font-family: Segoe UI, Tahoma, Geneva, Verdana, sans-serif;'>
                <tr>
                    <td align='center'>
                        <table width='600' cellpadding='0' cellspacing='0' border='0' style='box-shadow: 0 4px 10px rgba(31, 63, 72, 0.2);'>
                            <!-- Encabezado -->
                            <tr>
                                <td style='background-color:#1F3F48; padding:20px; text-align:center; color:#F0F0E1;'>
                                    <h1 style='margin:0; font-size:24px;'>¡Bienvenido/a, {nombreUsuario}!</h1>
                                </td>
                            </tr>

                            <!-- Cuerpo -->
                            <tr>
                                <td style='padding:20px; text-align:center; color:#000000;'>
                                    <p style='margin-bottom:8px;'>
                                        Gracias por registrarte en <strong>Alquilando</strong>.</p>
                                    <p> Ahora sos parte de una comunidad que facilita el alquiler de propiedades de forma segura y simple.</p>
                                    <p style='margin-bottom:22px;'>🏠 ¡Comenzá a explorar propiedades para encontrar tu lugar ideal!</p>
                                </td>
                            </tr>

                            <!-- Footer / Ayuda -->
                            <tr>
                                <td style='background-color:#f1771f; color:#F0F0E1; text-align:center; padding:14px; font-size:14px;'>
                                    <p style='margin:0;'>¿Necesitás ayuda? Contactanos en cualquier momento.</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";
        EnviarEmail(destinatario, asunto, cuerpo);
    }

    public void EnviarConfirmacionReserva(string destinatario, string nombreUsuario, string fechaInicio, string fechaFin, string propiedad)
    {
        string asunto = "Confirmación de Reserva";
        string cuerpo = @$"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
            <meta charset='UTF-8'>
        </head>
        <body style='margin:0; padding:0; background-color:#fafafa;'>
            <table width='100%' cellpadding='0' cellspacing='0' border='0' style='font-family: Segoe UI, Tahoma, Geneva, Verdana, sans-serif;'>
                <tr>
                    <td align='center'>
                        <table width='100%' cellpadding='0' cellspacing='0' border='0' style='box-shadow: 0 4px 10px rgba(31, 63, 72, 0.2);'>
                            <!-- Encabezado -->
                            <tr>
                                <td style='background-color:#1F3F48; padding:20px; text-align:center; color:#F0F0E1;'>
                                    <h1 style='margin:0; font-size:24px;'>¡Hola, {nombreUsuario}!</h1>
                                </td>
                            </tr>

                            <!-- Cuerpo -->
                            <tr>
                                <td style='padding:20px; text-align:center; color:#000000;'>
                                    <h1>¡Reserva Confirmada!</h1>
                                    <p>Tu reserva ha sido confirmada con éxito.</p>
                                    <p><strong>Fecha de Inicio:</strong> {fechaInicio}</p>
                                    <p><strong>Fecha de Fin:</strong> {fechaFin}</p>
                                    <p><strong>Lugar:</strong> {propiedad}</p>
                                    <p>Gracias por elegirnos. ¡Te esperamos!</p>
                                </td>
                            </tr>

                            <!-- Footer / Ayuda -->
                            <tr>
                                <td style='background-color:#f1771f; color:#F0F0E1; text-align:center; padding:14px; font-size:14px;'>
                                    <p style='margin:0;'>¿Necesitás ayuda? Contactanos en cualquier momento.</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";
        EnviarEmail(destinatario, asunto, cuerpo);
    }

    public void EnviarLinkRecuperacion(string destinatario, string nombreUsuario, string link)
    {
        string asunto = "Recuperá tu contraseña";
        string cuerpo = @$"
        <!DOCTYPE html>
        <html lang='es'>
        <head><meta charset='UTF-8'></head>
        <body style='margin:0; padding:0; background-color:#fafafa;'>
            <table width='100%' cellpadding='0' cellspacing='0' border='0' style='font-family: Segoe UI, Tahoma, Geneva, Verdana, sans-serif;'>
                <tr><td align='center'>
                    <table width='600' cellpadding='0' cellspacing='0' border='0' style='box-shadow: 0 4px 10px rgba(31, 63, 72, 0.2);'>

                        <!-- Encabezado -->
                        <tr>
                            <td style='background-color:#1F3F48; padding:20px; text-align:center; color:#F0F0E1;'>
                                <h1 style='margin:0; font-size:24px;'>Hola, {nombreUsuario}</h1>
                            </td>
                        </tr>

                        <!-- Contenido -->
                        <tr>
                            <td style='padding:20px; text-align:center; color:#000000;'>
                                <p>Solicitaste restablecer tu contraseña.</p>
                                <p>Hacé clic en el siguiente botón para crear una nueva:</p>
                                <a href='{link}' style='background-color:#f1771f; color:white; padding:12px 24px; text-decoration:none; border-radius:5px;'>Restablecer contraseña</a>
                                <p style='margin-top:20px;'>Si no pediste este cambio, ignorá este mensaje.</p>
                            </td>
                        </tr>

                        <!-- Footer -->
                        <tr>
                            <td style='background-color:#f1771f; color:#F0F0E1; text-align:center; padding:14px; font-size:14px;'>
                                <p style='margin:0;'>¿Necesitás ayuda? Contactanos en cualquier momento.</p>
                            </td>
                        </tr>
                    </table>
                </td></tr>
            </table>
        </body>
        </html>";

        EnviarEmail(destinatario, asunto, cuerpo);
    }
    
    public void EnviarCorreoModificacionReservaPorNoHabitable(string destinatario, string nombreUsuario, string propiedadOriginalTitulo, 
        string? nuevaPropiedadTitulo) 
    {
        string asunto;
        string cuerpoHtml;

        if (!string.IsNullOrEmpty(nuevaPropiedadTitulo))
        {
            // Caso 1: La reserva se reubicó en una nueva propiedad
            asunto = "¡Tu reserva en Alquilando ha sido modificada!";
            cuerpoHtml = @$"
            <!DOCTYPE html>
            <html lang='es'>
            <head>
                <meta charset='UTF-8'>
            </head>
            <body style='margin:0; padding:0; background-color:#fafafa;'>
                <table width='100%' cellpadding='0' cellspacing='0' border='0' style='font-family: Segoe UI, Tahoma, Geneva, Verdana, sans-serif;'>
                    <tr>
                        <td align='center'>
                            <table width='600' cellpadding='0' cellspacing='0' border='0' style='box-shadow: 0 4px 10px rgba(31, 63, 72, 0.2);'>
                                <tr>
                                    <td style='background-color:#1F3F48; padding:20px; text-align:center; color:#F0F0E1;'>
                                        <h1 style='margin:0; font-size:24px;'>¡Hola, {nombreUsuario}!</h1>
                                    </td>
                                </tr>

                                <tr>
                                    <td style='padding:20px; text-align:center; color:#000000;'>
                                        <p style='margin-bottom:8px;'>Te informamos que tu reserva para la propiedad <strong>{propiedadOriginalTitulo}</strong> ha sido modificada.</p>
                                        <p>Esto se debe a que la propiedad original ya no se encuentra habitable.</p>
                                        <p style='margin-bottom:22px;'>Hemos reubicado tu reserva en la propiedad: <strong>{nuevaPropiedadTitulo}</strong>. Por favor, revisa los detalles de tu reserva en nuestra plataforma.</p>
                                        <p style='margin-bottom:8px;'>Lamentamos los inconvenientes que esto pueda causar.</p>
                                    </td>
                                </tr>

                                <tr>
                                    <td style='background-color:#f1771f; color:#F0F0E1; text-align:center; padding:14px; font-size:14px;'>
                                        <p style='margin:0;'>¿Necesitás ayuda? Contactanos en cualquier momento.</p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
        }
        else
        {
            asunto = "¡Tu reserva en Alquilando ha sido cancelada!";
            cuerpoHtml = @$"
            <!DOCTYPE html>
            <html lang='es'>
            <head>
                <meta charset='UTF-8'>
            </head>
            <body style='margin:0; padding:0; background-color:#fafafa;'>
                <table width='100%' cellpadding='0' cellspacing='0' border='0' style='font-family: Segoe UI, Tahoma, Geneva, Verdana, sans-serif;'>
                    <tr>
                        <td align='center'>
                            <table width='600' cellpadding='0' cellspacing='0' border='0' style='box-shadow: 0 4px 10px rgba(31, 63, 72, 0.2);'>
                                <tr>
                                    <td style='background-color:#1F3F48; padding:20px; text-align:center; color:#F0F0E1;'>
                                        <h1 style='margin:0; font-size:24px;'>¡Hola, {nombreUsuario}!</h1>
                                    </td>
                                </tr>

                                <tr>
                                    <td style='padding:20px; text-align:center; color:#000000;'>
                                        <p style='margin-bottom:8px;'>Te informamos que tu reserva para la propiedad <strong>{propiedadOriginalTitulo}</strong> ha sido cancelada.</p>
                                        <p>Esto se debe a que la propiedad ya no se encuentra habitable y no fue posible reubicarlas en otra propiedad.</p>
                                        <p style='margin-bottom:22px;'>Lamentamos profundamente los inconvenientes que esto pueda causar. Puedes explorar otras propiedades disponibles en nuestra plataforma.</p>
                                        <p style='margin-bottom:8px;'>Si tienes alguna pregunta, no dudes en contactarnos.</p>
                                    </td>
                                </tr>

                                <tr>
                                    <td style='background-color:#f1771f; color:#F0F0E1; text-align:center; padding:14px; font-size:14px;'>
                                        <p style='margin:0;'>¿Necesitás ayuda? Contactanos en cualquier momento.</p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
        }

        EnviarEmail(destinatario, asunto, cuerpoHtml);
    }
}   
