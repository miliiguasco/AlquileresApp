namespace AlquileresApp.Core.Interfaces;

using AlquileresApp.Core.Entidades;

public interface IComentarioRepositorio
{
    public void AgregarComentario(Comentario comentario);
    public void OcultarComentario(int id);
    public List<Comentario> ListarComentariosPorPropiedad(int propiedadId);
    public Comentario? ObtenerComentarioPorId(int idComentario);
}
