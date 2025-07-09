namespace AlquileresApp.Core.Interfaces;

using AlquileresApp.Core.Entidades;

public interface ICalificacionRepositorio
{

    public void agregarCalificacion(Calificacion calificacion);
    public List<Calificacion> ObtenerCalificacionesPorPropiedad(int id);

}