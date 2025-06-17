using System;
using AlquileresApp.Core.Interfaces;   
using AlquileresApp.Core.Servicios;
namespace AlquileresApp.Core.CasosDeUso.Usuario;

public class CasoDeUsoCerrarSesion(IServicioAutenticacion servicioAutenticacion)
{
    public async Task Ejecutar()
    {
        await servicioAutenticacion.Logout();
    }
}
