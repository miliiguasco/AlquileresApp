using System.Security.Claims;

namespace AlquileresApp.Core.Interfaces;

public interface IServicioIniciarSesion
{
    ClaimsPrincipal IniciarSesion(string email, string contraseña);
} 