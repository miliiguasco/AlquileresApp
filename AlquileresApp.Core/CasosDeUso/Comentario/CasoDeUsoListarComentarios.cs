namespace AlquileresApp.Core.CasosDeUso.Comentario;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoListarComentarios(IComentarioRepositorio comentarioRepositorio, IPropiedadRepositorio propiedadRepositorio)
{
    public List<Comentario> Ejecutar(int propiedadId)
    {
        // Verificar si la propiedad existe
        var propiedad = propiedadRepositorio.ObtenerPropiedadPorId(propiedadId);
        if (propiedad == null)
        {
            throw new Exception("La propiedad no existe.");
        }

        // Obtener los comentarios de la propiedad
        var comentarios = comentarioRepositorio.ListarComentariosPorPropiedad(propiedadId);

        return comentarios;
    }
}