using System.Text.RegularExpressions;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
namespace AlquileresApp.Core;

public class UsuarioValidador : IUsuarioValidador
{
    public void ValidarDatos(Usuario usuario) 
    {
        ValidarCorreo(usuario.Email);
        ValidarPassword(usuario.Password);
        VerificarFechaDeNacimiento(usuario.FechaNacimiento);
    }
    private void ValidarCorreo(String correo)
    {
        if (String.IsNullOrEmpty(correo))
        {
            throw new Exception("El correo es requerido");
        }
        if (!Regex.IsMatch(correo, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            throw new Exception("El correo no es válido");
        }
    }

    private void ValidarPassword(String password)
    {
        if (String.IsNullOrEmpty(password))
            throw new Exception("La contraseña es requerida");
        if (password.Length < 8)
            throw new Exception("La contraseña debe tener al menos 8 caracteres");
    }

    private void VerificarFechaDeNacimiento(DateTime fechaNacimiento)
    {
        if (fechaNacimiento >= DateTime.Now.AddYears(-18))
            throw new Exception("Debes tener al menos 18 años para registrarte");
    }
}
