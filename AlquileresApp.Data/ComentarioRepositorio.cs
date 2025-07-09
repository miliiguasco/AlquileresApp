using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlquileresApp.Data;


public class ComentarioRepositorio(AppDbContext dbContext) : IComentarioRepositorio
{
    public void AgregarComentario(Comentario comentario)
    {
        dbContext.Comentarios.Add(comentario);
        dbContext.SaveChanges();
    }

    public void OcultarComentario(int id)
    {
        var comentario = dbContext.Comentarios.Find(id);
        if (comentario != null)
        {
            comentario.Visible = false;
            dbContext.SaveChanges();
        }
        else
        {
            throw new Exception("Comentario no encontrado.");
        }
    }

    public List<Comentario> ListarComentariosPorPropiedad(int propiedadId)
    {
        return dbContext.Comentarios.Include(c => c.Usuario)
            .Where(c => c.PropiedadId == propiedadId)
            .ToList();
    }

    public Comentario? ObtenerComentarioPorId(int comentarioId)
    {
        return dbContext.Comentarios.FirstOrDefault(c => c.Id == comentarioId);
    }

}