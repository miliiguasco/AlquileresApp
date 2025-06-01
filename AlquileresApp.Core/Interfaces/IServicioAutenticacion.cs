using System;  
using Microsoft.AspNetCore.Components.Authorization;    
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core.Interfaces;

public interface IServicioAutenticacion
{
    public Task Autenticar(Usuario usuario);
    public Task Logout();
}
