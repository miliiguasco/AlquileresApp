namespace AlquileresApp.Data;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CalificacionRepositorio(AppDbContext dbContext) : ICalificacionRepositorio
{
    public void agregarCalificacion(Calificacion calificacion)
    {
        dbContext.Calificaciones.Add(calificacion);
        dbContext.SaveChanges();
    }

    public List<Calificacion> ObtenerCalificacionesPorPropiedad(int propiedadId)
    {
        return dbContext.Calificaciones.Where(c => c.PropiedadId == propiedadId).ToList();
    }
}