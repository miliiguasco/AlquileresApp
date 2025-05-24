using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace AlquileresApp.Core.Servicios;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
    private readonly AuthenticationState _anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }
        return Task.FromResult(_anonymous);
    }

    public void ActualizarEstadoAutenticacion(ClaimsPrincipal usuario)
    {
        if (usuario.Identity?.IsAuthenticated == true)
        {
            _currentUser = usuario;
            var authState = Task.FromResult(new AuthenticationState(_currentUser));
            NotifyAuthenticationStateChanged(authState);
        }
        else
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }

    public void LimpiarEstadoAutenticacion()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
} 