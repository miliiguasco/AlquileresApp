namespace AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
public class CasoDeUsoRegistrarUsuario (IUsuarioRepositorio usuarioRepositorio, IUsuarioValidador usuarioValidador, IServicioHashPassword servicioHashPassword, INotificadorEmail notificadorEmail)
{
    public void Ejecutar(Usuario usuario)
    {
        try {
            usuarioValidador.ValidarDatos(usuario);
            String hashedPassword = servicioHashPassword.HashPassword(usuario.Contraseña);   
            usuario.Contraseña = hashedPassword;
            usuarioRepositorio.RegistrarUsuario(usuario);
            notificadorEmail.EnviarCorreoBienvenida(usuario.Email, usuario.Nombre);
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
}