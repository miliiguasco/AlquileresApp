using System;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Validadores;
namespace AlquileresApp.Core.CasosDeUso.Reserva;


public class CasoDeUsoCancelarReserva
{
    private readonly IReservaRepositorio _reservaRepository;
    private readonly ITarjetaRepositorio _tarjetaRepositorio;

    public CasoDeUsoCancelarReserva(IReservaRepositorio reservaRepository, ITarjetaRepositorio tarjetaRepositorio)
    {
        _reservaRepository = reservaRepository;
        _tarjetaRepositorio = tarjetaRepositorio;
    }


    public void Ejecutar(int reservaId)
    {
        var reserva = _reservaRepository.ObtenerReservaPorId(reservaId);
        if (reserva == null)
            throw new Exception("La reserva no existe.");

        if (reserva.Estado == EstadoReserva.Cancelada)
            throw new Exception("La reserva ya está cancelada.");

        var ahora = DateTime.Now;
        var horasAntesInicio = (reserva.FechaInicio - ahora).TotalHours;

        // Obtener política desde la propiedad
        var politica = reserva.Propiedad?.PoliticaCancelacion 
                       ?? throw new Exception("No se pudo obtener la política de cancelación.");

        switch (politica)
        {
            case PoliticasDeCancelacion.SinAnticipo_NoCancelable:
                throw new Exception("Esta reserva no puede cancelarse según la política establecida.");

            case PoliticasDeCancelacion.Anticipo20_72hs:
                if (horasAntesInicio < 72)
                    throw new Exception("La reserva solo puede cancelarse hasta 72 horas antes del inicio.");
                // Reembolsar 20%
                var anticipo = reserva.PrecioTotal * 0.20m;
                Reembolsar(reserva.Id, anticipo);
                break;

            case PoliticasDeCancelacion.PagoTotal_48hs_50:
                if (horasAntesInicio < 48)
                    throw new Exception("La reserva solo puede cancelarse hasta 48 horas antes del inicio.");
                // Reembolsar 50%
                var mitad = reserva.PrecioTotal * 0.50m;
                Reembolsar(reserva.Id, mitad);
                break;

            default:
                throw new Exception("Política de cancelación desconocida.");
        }

        reserva.Estado = EstadoReserva.Cancelada;
        _reservaRepository.Actualizar(reserva);
    }

    private void Reembolsar(int reservaId, decimal monto)
    {
        var reserva = _reservaRepository.ObtenerReservaPorId(reservaId);
        if (reserva == null)
            throw new Exception("Reserva no encontrada.");
        var tarjeta = _tarjetaRepositorio.ObtenerPorClienteId(reserva.ClienteId);
        _tarjetaRepositorio.Reembolsar(tarjeta, monto);
    }
}
