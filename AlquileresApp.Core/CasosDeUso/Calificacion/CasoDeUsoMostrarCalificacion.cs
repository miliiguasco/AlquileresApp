namespace AlquileresApp.Core.CasosDeUso.Calificacion;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using System;
using System.Linq;

public class CasoDeUsoMostrarCalificacion(ICalificacionRepositorio calificacionRepositorio)
{
    public double Ejecutar(int propiedadId)
    {
        if (propiedadId <= 0)
        {
            throw new ArgumentException("El ID de la propiedad debe ser mayor que cero.");
        }

        var calificaciones = calificacionRepositorio.ObtenerCalificacionesPorPropiedad(propiedadId);
        if (calificaciones == null || !calificaciones.Any())
        {
            throw new Exception("No se encontraron calificaciones para esta propiedad.");
        }
        // Retornar el promedio de las calificaciones
        var calificacionPromedio = calificaciones.Average(c => c.Puntuacion);
        
         if (calificacionPromedio == null)
        {
            return 0;
        } 
        return calificacionPromedio;
    }
}