using AlquileresApp.Core.Validadores;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Core.CasosDeUso.Reserva;

public class CasoDeUsoCancelarReserva
{
    private readonly IReservaRepositorio _reservaRepository;
    private readonly ITarjetaRepositorio _tarjetaRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly INotificadorEmail _notificadorEmail;

    public CasoDeUsoCancelarReserva(
        IReservaRepositorio reservaRepository,
        ITarjetaRepositorio tarjetaRepositorio,
        IUsuarioRepositorio usuarioRepositorio,
        INotificadorEmail notificadorEmail)
    {
        _reservaRepository = reservaRepository;
        _tarjetaRepositorio = tarjetaRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
        _notificadorEmail = notificadorEmail;
    }
    public class ResultadoCancelacionReserva
{
    public bool EsExitosa { get; set; }
    public string Mensaje { get; set; }
    public decimal? MontoReembolsado { get; set; }
}
    public ResultadoCancelacionReserva Ejecutar(int reservaId)
    {
        var resultado = new ResultadoCancelacionReserva();

        var reserva = _reservaRepository.ObtenerReservaPorId(reservaId);
        if (reserva == null)
            return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "La reserva no existe." };

        if (reserva.Estado == EstadoReserva.Cancelada)
            return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "La reserva ya está cancelada." };

        var ahora = DateTime.Now;
        var horasAntesInicio = (reserva.FechaInicio - ahora).TotalHours;

        var politica = reserva.Propiedad?.PoliticaCancelacion;
        if (politica == null)
            return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "No se pudo obtener la política de cancelación." };

        decimal? montoReembolsado = null;

        switch (politica)
        {
            case PoliticasDeCancelacion.SinAnticipo_NoCancelable:
                return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "Esta reserva no puede cancelarse según la política establecida." };

            case PoliticasDeCancelacion.Anticipo20_72hs:
                if (horasAntesInicio < 72)
                    return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "La reserva solo puede cancelarse hasta 72 horas antes del inicio." };
                montoReembolsado = reserva.PrecioTotal * 0.20m;
                break;

            case PoliticasDeCancelacion.PagoTotal_48hs_50:
                if (horasAntesInicio < 48)
                    return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "La reserva solo puede cancelarse hasta 48 horas antes del inicio." };
                montoReembolsado = reserva.PrecioTotal * 0.50m;
                break;

            default:
                return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "Política de cancelación desconocida." };
        }

        if (montoReembolsado.HasValue)
        {
            var tarjeta = _tarjetaRepositorio.ObtenerPorClienteId(reserva.ClienteId);
            if (tarjeta == null)
                return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "No se encontró una tarjeta asociada al cliente." };

            _tarjetaRepositorio.Reembolsar(tarjeta, montoReembolsado.Value);
        }

        reserva.Estado = EstadoReserva.Cancelada;
        _reservaRepository.Actualizar(reserva);

        // Enviar correo al cliente
        var cliente = _usuarioRepositorio.ObtenerUsuarioPorId(reserva.ClienteId);
        if (cliente != null)
        {
            string asunto = "Confirmación de cancelación de reserva";
            string cuerpo = montoReembolsado.HasValue
                ? $"""
                Hola {cliente.Nombre},

                Te informamos que tu reserva ha sido cancelada exitosamente.

                Monto reembolsado: {montoReembolsado.Value:C}

                El reembolso será procesado a la tarjeta registrada en tu cuenta.

                ¡Gracias por confiar en nosotros!

                Saludos,
                Equipo de Reservas
                """
                : $"""
                Hola {cliente.Nombre},

                Te informamos que tu reserva ha sido cancelada exitosamente.

                ¡Gracias por confiar en nosotros!

                Saludos,
                Equipo de Reservas
                """;

            _notificadorEmail.EnviarEmail(cliente.Email, asunto, cuerpo);
        }

        resultado.EsExitosa = true;
        resultado.Mensaje = "Reserva cancelada exitosamente.";
        resultado.MontoReembolsado = montoReembolsado;
        return resultado;
    }
}
