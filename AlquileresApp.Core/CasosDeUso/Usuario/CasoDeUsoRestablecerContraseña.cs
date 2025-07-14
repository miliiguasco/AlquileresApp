using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using System.Net;
namespace AlquileresApp.Core.CasosDeUso.Usuario;

public class CasoDeUsoRestablecerContraseña (IUsuarioRepositorio usuarioRepositorio, ITokenRecuperacionRepositorio tokenRecuperacionRepositorio, IServicioHashPassword hashPassword)
{
    public async Task Ejecutar(string email, string token, string nuevaContraseña)
        {
            var tokenRecuperacion = tokenRecuperacionRepositorio.ObtenerPorEmailYToken(email, token);

            if (tokenRecuperacion == null || tokenRecuperacion.FechaExpiracion < DateTime.UtcNow)
            {
                throw new Exception("El enlace de recuperación es inválido o ha expirado.");
            }

            var usuario = usuarioRepositorio.ObtenerUsuarioPorEmail(email);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var hashContraseña = hashPassword.HashPassword(nuevaContraseña); 
            usuarioRepositorio.modificarContraseña(usuario.Id, hashContraseña);

            tokenRecuperacionRepositorio.Eliminar(tokenRecuperacion);
        }
}
