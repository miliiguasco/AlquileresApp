using System;
using Microsoft.AspNetCore.Http;

namespace AlquileresApp.Core.Interfaces;

public interface IServicioSesion
{
    public Task InvokeAsync(HttpContext context);
}
