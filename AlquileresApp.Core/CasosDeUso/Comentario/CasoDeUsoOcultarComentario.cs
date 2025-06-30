namespace AlquileresApp.Core.CasosDeUso.Comentario;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoOcultarComentario(IComentarioRepositorio comentarioRepositorio, IUsuarioRepositorio usuarioRepositorio)
{
    public void Ejecutar(int idComentario, int usuarioId)
    {

        var usuario = usuarioRepositorio.ObtenerUsuarioPorId(usuarioId);
        if (usuario == null)
            throw new Exception("Usuario no encontrado.");

        if (usuario.Rol == RolUsuario.Cliente)
            throw new Exception("No tiene permisos para ocultar comentarios.");

        var comentario = comentarioRepositorio.ObtenerComentarioPorId(idComentario);
        if (comentario == null)
        throw new Exception("El comentario no existe.");

        // Cambiar la visibilidad del comentario a falso
        comentario.Visible = false;

        // Actualizar el comentario en el repositorio
        comentarioRepositorio.OcultarComentario(idComentario);
    }
}