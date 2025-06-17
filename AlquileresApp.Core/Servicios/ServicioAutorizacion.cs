using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using AlquileresApp.Core.Interfaces;
using Microsoft.Extensions.Internal;
using System.Security.Claims;

namespace AlquileresApp.Core.Servicios;

public class CustomOptions : AuthenticationSchemeOptions
{
}

public class ServicioAutorizacion : AuthenticationHandler<CustomOptions>
{
    private readonly IServicioSesion servicioSesion; 
    public ServicioAutorizacion(
        IOptionsMonitor<CustomOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IServicioSesion servicioSesion) : base(options, logger, encoder, clock)
    {
        this.servicioSesion = servicioSesion;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string token = Request.Cookies["auth_token"];
        if (string.IsNullOrEmpty(token))
            return AuthenticateResult.Fail("Authentication Failed");

        var userClaims = servicioSesion.VerificarUsuario(token);
        if(userClaims == null)
            return AuthenticateResult.Fail("Authentication Failed");

        var principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "JWT"));
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Context.Response.Redirect("/"); 
        return Task.CompletedTask;
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Context.Response.Redirect("/"); 
        return Task.CompletedTask;
    }
}
