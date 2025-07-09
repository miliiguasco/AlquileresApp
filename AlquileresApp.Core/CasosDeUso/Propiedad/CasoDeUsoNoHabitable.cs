namespace AlquileresApp.Core.CasosDeUso.Propiedad;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class CasoDeUsoMarcarPropiedadComoNoHabitable(IPropiedadRepositorio propiedadesRepositorio, IReservaRepositorio reservasRepositorio)
{
    public Dictionary<Reserva, List<Propiedad>> IdentificarReservasYAlternativas(Propiedad propiedad)
    {
        List<Reserva> reservasAfectadas = reservasRepositorio.ListarReservas()
            .Where(r => r.PropiedadId == propiedad.Id)
            .ToList();

        var opcionesDeReubicacion = new Dictionary<Reserva, List<Propiedad>>();

        foreach (var reserva in reservasAfectadas)
        {
            List<Propiedad> propiedadesEnLocalidad = propiedadesRepositorio.ListarPropiedades()
                .Where(p => p.Localidad == propiedad.Localidad && p.Id != propiedad.Id && !p.NoHabitable)
                .ToList();

            List<Propiedad> alternativasDisponiblesParaReserva = new List<Propiedad>();

            foreach (Propiedad propiedadAlternativa in propiedadesEnLocalidad)
            {
                try
                {
                    propiedadesRepositorio.ComprobarDisponibilidad(propiedadAlternativa, reserva.FechaInicio, reserva.FechaFin);
                    alternativasDisponiblesParaReserva.Add(propiedadAlternativa);
                }
                catch (Exception)
                {
                    // Propiedad no disponible, no la agregamos
                }
            }
            opcionesDeReubicacion.Add(reserva, alternativasDisponiblesParaReserva);
        }

        return opcionesDeReubicacion;
    }

    public void ActualizarEstadoNoHabitable(Propiedad propiedad)
    {
        propiedadesRepositorio.MarcarPropiedadComoNoHabitable(propiedad); // Este método ahora se llama aquí
    }
    
    public void ReasignarReserva(Reserva reserva, Propiedad nuevaPropiedad)
    {
        reserva.Propiedad = nuevaPropiedad;
        reserva.PropiedadId = nuevaPropiedad.Id;
        reservasRepositorio.ModificarReserva2(reserva);
    }
}