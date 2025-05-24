namespace AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public class CasoDeUsoRegistrarUsuario (IUsuarioRepositorio usuarioRepositorio, IUsuarioValidador usuarioValidador, IServicioHashPassword servicioHashPassword)
{
    public void Ejecutar(Usuario usuario)
    {
        usuarioValidador.ValidarDatos(usuario);
        String hashedPassword = servicioHashPassword.HashPassword(usuario.Password);
        usuarioRepositorio.RegistrarUsuario(usuario, hashedPassword);
    }
}
          /*                                                  
             reservaValidador.validarTarjeta(usuario)           //validador tiene 2 metodos                                       
             reservaValidador.validarFecha(usuario)
             reservaRepositorio.Alta(usuario)

*/