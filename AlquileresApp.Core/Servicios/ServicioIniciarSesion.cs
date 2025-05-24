using System;
using System.Security.Claims;
using System.Collections.Generic;
using AlquileresApp.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace AlquileresApp.Core.Servicios;

public class ServicioIniciarSesion(IUsuarioRepositorio usuarioRepositorio, IServicioHashPassword servicioHashPassword) : IServicioIniciarSesion
{
    public ClaimsPrincipal IniciarSesion(string email, string contraseña) 
    {
        var hashedContraseña = servicioHashPassword.HashPassword(contraseña);
        try 
        {
            var usuario = usuarioRepositorio.AutenticarUsuario(email, hashedContraseña);
            if (usuario == null)
                throw new Exception("Email o contraseña incorrectos");

            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));
        } 
        catch (Exception) 
        {
            throw new Exception("Email o contraseña incorrectos");
        }
    }
}
