using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using AlquileresApp.Core.Entidades; 
using System.IdentityModel.Tokens.Jwt;
using AlquileresApp.Core.Interfaces;
namespace AlquileresApp.Core.Servicios;

public class ServicioAutenticacion(ServicioSesion servicioSesion) : AuthenticationStateProvider, IServicioAutenticacion
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userClaim = await servicioSesion.VerificarUsuario();
            if (userClaim != null)
            {
                var identity = new ClaimsIdentity(userClaim, "JWT");
                var user = new ClaimsPrincipal(identity);
                return await Task.FromResult(new AuthenticationState(user));
            }
            else
            {
                await servicioSesion.eliminarCookie();
                return new AuthenticationState(_anonymous);
            }
        }
        catch (Exception)
        {
            return new AuthenticationState(_anonymous);
        } 
    }

    public async Task Autenticar(Usuario usuario)
    {
        var token = await servicioSesion.Autenticar(usuario);
        if (!string.IsNullOrEmpty(token))
        {
            var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
    }

    public async Task Logout()
    {
        await servicioSesion.eliminarCookie();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
