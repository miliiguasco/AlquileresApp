using System;
using System.Security.Claims;
using AlquileresApp.Core.Entidades; 
namespace AlquileresApp.Core.Interfaces;

public interface IServicioSesion
{
    public Task<string> Autenticar(Usuario usuario);
    public Task eliminarCookie();
    public Task<IEnumerable<Claim>?> VerificarUsuario();
    public IEnumerable<Claim>? VerificarUsuario(string token);
}
