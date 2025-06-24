namespace AlquileresApp.Core.CasosDeUso.Administrador;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public class CasoDeUsoRegistrarEncargado(IUsuarioRepositorio usuarioRepositorio, IUsuarioValidador usuarioValidador, IServicioHashPassword servicioHashPassword, INotificadorEmail notificadorEmail)
{
    public void Ejecutar(Encargado encargado)
    {
        try {
            usuarioValidador.ValidarDatos(encargado);
            String hashedPassword = servicioHashPassword.HashPassword(encargado.Contraseña);   
            encargado.Contraseña = hashedPassword;
            usuarioRepositorio.RegistrarUsuario(encargado);
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
}
