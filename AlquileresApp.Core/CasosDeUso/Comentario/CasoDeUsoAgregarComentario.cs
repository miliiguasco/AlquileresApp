namespace AlquileresApp.Core.CasosDeUso.Comentario;

using System.Linq;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Enumerativos;

public class CasoDeUsoAgregarComentario(IComentarioRepositorio comentarioRepositorio, IPropiedadRepositorio propiedadRepositorio, IReservaRepositorio reservaRepositorio, IUsuarioRepositorio usuarioRepositorio)
{
    public void Ejecutar(Comentario comentario, int propiedadId)
    {
        // Verificar si la propiedad existe
        var propiedad = propiedadRepositorio.ObtenerPropiedadPorId(propiedadId);
        if (propiedad == null)
        {
            throw new Exception("La propiedad no existe.");
        }
        // Verificar que el comentario no esté vacío
        if (string.IsNullOrWhiteSpace(comentario.Contenido))
        {
            throw new Exception("El comentario no puede estar vacío.");
        }
        // Verificar que el usuario haya iniciado sesión
        if (comentario.UsuarioId <= 0)
        {
            throw new Exception("El usuario debe estar autenticado para comentar.");
        }
        
        var usuario = usuarioRepositorio.ObtenerUsuarioPorId((int)comentario.UsuarioId);
        if (usuario == null)
            throw new Exception("Usuario no encontrado.");

        if (usuario.Rol == RolUsuario.Cliente)
        {
         bool tieneReserva = reservaRepositorio.ObtenerReservasPorUsuarioYPropiedad(comentario.UsuarioId, propiedadId).Any();

        if (!tieneReserva)
            throw new Exception("No tiene reserva en la propiedad");
        }

        // Asignar la propiedad al comentario
        comentario.PropiedadId = propiedadId;

        // Agregar el comentario
        comentarioRepositorio.AgregarComentario(comentario);
    }
}