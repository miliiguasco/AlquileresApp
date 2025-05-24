using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using AlquileresApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlquileresApp.Core.Servicios;

public class ServicioSesion : IServicioSesion      
{
    private readonly RequestDelegate _next;
    public static IDictionary<Guid, ClaimsPrincipal> Logins { get; private set; } = new ConcurrentDictionary<Guid, ClaimsPrincipal>();
    public ServicioSesion(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next)); 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/Login" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]);
            var claim = Logins[key];
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);
            context.Response.Redirect("/");
        }
        else 
        {
            await _next(context);
        }
    }
}   
