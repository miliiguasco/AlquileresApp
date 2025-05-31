using System;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Servicios;
using AlquileresApp.Core.Entidades;     

namespace AlquileresApp.Core.CasosDeUso.Usuario;

public class CasoDeUsoIniciarSesion(IUsuarioRepositorio usuarioRepositorio, IServicioHashPassword servicioHashPassword, ServicioAutenticacion servicioAutenticacion)
{
    public async Task<Entidades.Usuario> Ejecutar(string correo, string contraseña)
    {
        var hashedPassword = servicioHashPassword.HashPassword(contraseña);
        var usuarioAutenticado = usuarioRepositorio.AutenticarUsuario(correo, hashedPassword);
        if (usuarioAutenticado == null)
            throw new Exception("Correo no registrado o contraseña incorrecta");
        await servicioAutenticacion.Autenticar(usuarioAutenticado);
        return usuarioAutenticado;
    }
}
