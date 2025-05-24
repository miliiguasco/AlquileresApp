using System;
using System.Security.Claims;
using System.Collections.Generic;
using AlquileresApp.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace AlquileresApp.Core.Servicios;

public class ServicioIniciarSesion(IUsuarioRepositorio usuarioRepositorio, IServicioHashPassword servicioHashPassword) : IServicioIniciarSesion
{
    public ClaimsPrincipal IniciarSesion(string email, string contraseña) 
    {
        Console.WriteLine($"[ServicioIniciarSesion] Intentando autenticar usuario con email: {email}");
        var hashedContraseña = servicioHashPassword.HashPassword(contraseña);
        Console.WriteLine($"[ServicioIniciarSesion] Contraseña hasheada: {hashedContraseña}");
        
        try 
        {
            var usuario = usuarioRepositorio.AutenticarUsuario(email, hashedContraseña);
            if (usuario == null)
            {
                Console.WriteLine("[ServicioIniciarSesion] Usuario no encontrado en la base de datos");
                throw new Exception("Email o contraseña incorrectos");
            }

            Console.WriteLine($"[ServicioIniciarSesion] Usuario encontrado: {usuario.Nombre} {usuario.Apellido} con rol {usuario.Rol}");
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
                new Claim("UserId", usuario.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            Console.WriteLine("[ServicioIniciarSesion] Claims creados exitosamente");
            Console.WriteLine($"[ServicioIniciarSesion] IsAuthenticated: {principal.Identity?.IsAuthenticated}");

            return principal;
        } 
        catch (Exception ex) 
        {
            Console.WriteLine($"[ServicioIniciarSesion] Error durante la autenticación: {ex.Message}");
            throw;
        }
    }
}
