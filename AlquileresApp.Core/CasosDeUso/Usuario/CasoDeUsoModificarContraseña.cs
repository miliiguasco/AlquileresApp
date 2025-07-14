namespace AlquileresApp.Core.CasosDeUso.Usuario;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoModificarContraseña(IUsuarioRepositorio usuarioRepositorio, IServicioHashPassword servicioHashPassword, IUsuarioValidador usuarioValidador)
{
    public void Ejecutar(Usuario usuario, string nuevaContraseña)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");
        
        if (nuevaContraseña == usuario.Contraseña)
            throw new ArgumentException("La nueva contraseña no puede ser la misma que la actual.", nameof(nuevaContraseña));
        
        string hashedPassword = servicioHashPassword.HashPassword(nuevaContraseña);   
    
        usuarioValidador.ValidarDatos(usuario); 

        usuarioRepositorio.modificarContraseña(usuario.Id, hashedPassword);
    }
}