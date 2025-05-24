using System;
using System.Security.Claims;
using AlquileresApp.Core.Interfaces;        
using AlquileresApp.Core.Servicios;

namespace AlquileresApp.Core.CasosDeUso.Usuario;

public class CasoDeUsoIniciarSesion(IServicioIniciarSesion servicioIniciarSesion)
{
    public ClaimsPrincipal? Ejecutar(string email, string contraseña)
    {
        return servicioIniciarSesion.IniciarSesion(email, contraseña);
    }
}
