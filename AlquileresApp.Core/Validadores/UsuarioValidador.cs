using System.Text.RegularExpressions;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core.Validadores;

public class UsuarioValidador : IUsuarioValidador
{
    public void ValidarDatos(Usuario usuario) 
    {
        ValidarCorreo(usuario.Email);
        ValidarContraseña(usuario.Contraseña);
    }
    private void ValidarCorreo(String correo)
    {
        if (!Regex.IsMatch(correo, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            throw new Exception("El correo no es válido");
        }
    }

    private void ValidarContraseña(String contraseña)
    {
        if (String.IsNullOrEmpty(contraseña))
            throw new Exception("La contraseña es requerida");
        if (contraseña.Length < 8)
            throw new Exception("La contraseña debe tener al menos 8 caracteres"); 
    }
}
