namespace AlquileresApp.Core.CasosDeUso.Calificacion;

using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public class CasoDeUsoAgregarCalificacion(ICalificacionRepositorio calificacionRepositorio, IPropiedadRepositorio propiedadRepositorio, IReservaRepositorio reservaRepositorio)
{
    public void Ejecutar(Calificacion calificacion)
    {
        var propiedadExiste = propiedadRepositorio.ObtenerPropiedadPorId(calificacion.PropiedadId);
        if (propiedadExiste == null)
        {
            throw new Exception("La propiedad no existe.");
        }

        List<Reserva> usuarioReservas = reservaRepositorio.ListarMisReservas(calificacion.UsuarioId);
        bool tieneReservaEnPropiedad = usuarioReservas.Any(r => r.PropiedadId == calificacion.PropiedadId);
        if (!tieneReservaEnPropiedad)
        {
            throw new Exception("El usuario no tiene reservas en esta propiedad.");
        }
        if (calificacion.Puntuacion < 1 || calificacion.Puntuacion > 5)
        {
            throw new Exception("La calificación debe estar entre 1 y 5.");
        }
        calificacionRepositorio.agregarCalificacion(calificacion);
        var propiedadParaActualizar = propiedadRepositorio.ObtenerPropiedadPorId(calificacion.PropiedadId);

        if (propiedadParaActualizar != null)
        {
            // 4. Recalcular el promedio
            double nuevoPromedio = propiedadParaActualizar.Calificaciones.Any()
                                   ? propiedadParaActualizar.Calificaciones.Average(c => (double)c.Puntuacion)
                                   : 0.0;

            // 5. ¡Ahora usamos el nuevo método del repositorio!
            propiedadRepositorio.ActualizarCalificacionPromedio(propiedadParaActualizar.Id, nuevoPromedio);
        }
    }
}