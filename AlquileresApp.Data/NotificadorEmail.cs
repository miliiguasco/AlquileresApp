namespace AlquileresApp.Data;
using System.Net;
using System.Net.Mail;
using AlquileresApp.Core.Interfaces;

public class NotificadorEmail: INotificadorEmail
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

    public void EnviarEmail(string para, string asunto, string cuerpo)
    {
        using var cliente = new SmtpClient(_servidor, _puerto)
        {
            Credentials = new NetworkCredential(_remitente, _clave),
            EnableSsl = true
        };

        var mensaje = new MailMessage(_remitente, para, asunto, cuerpo)
        {
            IsBodyHtml = false // ponelo en true si querés enviar HTML
        };

        cliente.Send(mensaje);
    }
}
