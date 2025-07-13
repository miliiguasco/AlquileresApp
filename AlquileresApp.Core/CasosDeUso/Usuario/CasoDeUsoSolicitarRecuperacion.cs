using AlquileresApp.Core.Interfaces; 
using AlquileresApp.Core.Entidades; 
using System.Net;

namespace AlquileresApp.Core.CasosDeUso.Usuario;

public class CasoDeUsoSolicitarRecuperacion(IUsuarioRepositorio usuarioRepositorio, INotificadorEmail notificadorEmail, ITokenRecuperacionRepositorio tokenRepositorio)
{
    public void Ejecutar(string email)
    {
        try
        {
            var usuario = usuarioRepositorio.ObtenerUsuarioPorEmail(email);
            if (usuario == null) return;

            var token = Guid.NewGuid().ToString("N");
            var tokenRecuperacion = new TokenRecuperacion
            {
                Email = email,
                Token = token,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddHours(1)
            };

            tokenRepositorio.Guardar(tokenRecuperacion);

            var url = $"https://localhost:7234/reset-password?email={email}&token={WebUtility.UrlEncode(token)}";

            notificadorEmail.EnviarLinkRecuperacion(email, usuario.Nombre, url);
        }
        catch (Exception ex) 
        {
            throw new Exception(ex.Message);
        }
    }
}
