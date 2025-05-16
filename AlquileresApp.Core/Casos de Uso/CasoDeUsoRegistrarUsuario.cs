namespace AlquileresApp.Core;

public class CasoDeUsoRegistrarUsuario (IUsuarioRepositorio usuarioRepositorio, IUsuarioValidador usuarioValidador, IServicioHashPassword servicioHashPassword)
{
    public void Ejecutar(Usuario usuario)
    {
        usuarioValidador.ValidarDatos(usuario);
        String hashedPassword = servicioHashPassword.HashPassword(usuario.Password);
        usuarioRepositorio.RegistrarUsuario(usuario, hashedPassword);
    }
}
