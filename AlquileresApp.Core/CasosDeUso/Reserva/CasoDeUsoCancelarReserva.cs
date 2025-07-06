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
        {
            resultado.EsExitosa = false;
            resultado.Mensaje = "La reserva no existe.";
            return resultado;
        }
        resultado = calcularCancelacion(reserva);

        if (!resultado.EsExitosa)
            return resultado;

        if (resultado.MontoReembolsado.HasValue && resultado.MontoReembolsado.Value > 0)
        {
            var tarjeta = _tarjetaRepositorio.ObtenerPorClienteId(reserva.ClienteId);
            if (tarjeta == null)
                return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "No se encontró una tarjeta asociada al cliente." };

            _tarjetaRepositorio.Reembolsar(tarjeta, resultado.MontoReembolsado.Value);
        }

        reserva.Estado = EstadoReserva.Cancelada;
        _reservaRepository.Actualizar(reserva);

        // Enviar correo al cliente
        var cliente = _usuarioRepositorio.ObtenerUsuarioPorId(reserva.ClienteId);
        if (cliente != null)
        {
            string asunto = "Confirmación de cancelación de reserva";
            string cuerpo = resultado.MontoReembolsado.HasValue
                ? $"""
                Hola {cliente.Nombre},

                Te informamos que tu reserva ha sido cancelada exitosamente.

                Monto reembolsado: {resultado.MontoReembolsado.Value:C}

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
        if (resultado.MontoReembolsado.HasValue && resultado.MontoReembolsado.Value > 0)
        {
            resultado.Mensaje = $"Reserva cancelada exitosamente. Se aplicó un reembolso de {resultado.MontoReembolsado.Value:C}.";
        }
        else
        {
            if (reserva.Propiedad?.PoliticaCancelacion == PoliticasDeCancelacion.Anticipo20_72hs && (reserva.FechaInicio - DateTime.Now).TotalHours < 72)
            {
                resultado.Mensaje = "Reserva cancelada exitosamente. No se aplicó reembolso porque la cancelación fue realizada con menos de 72 horas de anticipación.";
            }
            else if (reserva.Propiedad?.PoliticaCancelacion == PoliticasDeCancelacion.PagoTotal_48hs_50 && (reserva.FechaInicio - DateTime.Now).TotalHours < 48)
            {
                resultado.Mensaje = "Reserva cancelada exitosamente. No se aplicó reembolso porque la cancelación fue realizada con menos de 48 horas de anticipación.";
            }
            else
            {
                resultado.Mensaje = "Reserva cancelada exitosamente. No se aplicó reembolso ya que la reserva no tiene anticipo.";
            }
        }


        return resultado;
    }

    public ResultadoCancelacionReserva calcularCancelacion(AlquileresApp.Core.Entidades.Reserva reserva)
    {
        var resultado = new ResultadoCancelacionReserva();


        if (reserva.Estado == EstadoReserva.Cancelada)
        {
            resultado.EsExitosa = false;
            resultado.Mensaje = "La reserva ya está cancelada.";
            return resultado;
        }

        var ahora = DateTime.Now;
        var horasAntesInicio = (reserva.FechaInicio - ahora).TotalHours;

        var politica = reserva.Propiedad?.PoliticaCancelacion;
        if (politica == null)
        {
            resultado.EsExitosa = false;
            resultado.Mensaje = "No se pudo obtener la política de cancelación.";
            return resultado;
        }


        switch (politica)
        {
            case PoliticasDeCancelacion.SinAnticipo_NoCancelable:
                resultado.Mensaje = "No se aplicara reembolso ya que la reserva no tiene anticipo.";
                resultado.MontoReembolsado = 0;
                break;
            case PoliticasDeCancelacion.Anticipo20_72hs:
                if (horasAntesInicio < 72)
                {
                    resultado.Mensaje = "No se aplicara reembolso ya que la reserva comienza en menos de 72 horas.";
                    resultado.MontoReembolsado = 0;
                }
                else
                    resultado.Mensaje = "Se aplicara un reembolso del 20% del total de la reserva.";
                resultado.MontoReembolsado = reserva.PrecioTotal * 0.20m;
                break;

            case PoliticasDeCancelacion.PagoTotal_48hs_50:
                if (horasAntesInicio < 48)
                {
                    resultado.Mensaje = "No se aplicara reembolso ya que la reserva comienza en menos de 48 horas.";
                    resultado.MontoReembolsado = 0;
                }
                else
                    // Reembolso del 50% del total si se cancela con más de 48 horas de anticipación
                    resultado.Mensaje = "Se aplicara un reembolso del 50% del total de la reserva.";
                resultado.MontoReembolsado = reserva.PrecioTotal * 0.50m;
                break;

            default:
                return new ResultadoCancelacionReserva { EsExitosa = false, Mensaje = "Política de cancelación desconocida." };
        }
        resultado.EsExitosa = true;
        Console.WriteLine($"[Cancelación] ReservaId: {reserva.Id}, Estado: {reserva.Estado}, Política: {politica}, Horas antes del inicio: {horasAntesInicio}, Monto reembolsado: {resultado.MontoReembolsado}, Mensaje: {resultado.Mensaje}");
        return resultado;

    }
    
    public ResultadoCancelacionReserva EjecutarParaInhabitabilidad(int reservaId)
    {
        var resultado = new ResultadoCancelacionReserva();
        var reserva = _reservaRepository.ObtenerReservaPorId(reservaId);

        if (reserva == null)
        {
            resultado.EsExitosa = false;
            resultado.Mensaje = "La reserva no existe.";
            return resultado;
        }

        // Si la reserva ya está cancelada, no hacemos nada más
        if (reserva.Estado == EstadoReserva.Cancelada)
        {
            resultado.EsExitosa = false;
            resultado.Mensaje = "La reserva ya está cancelada.";
            return resultado;
        }

        // Aplicamos la regla específica: NO hay reembolso para este tipo de cancelación
        resultado.EsExitosa = true; 
        resultado.Mensaje = "Reserva cancelada debido a que la propiedad fue marcada como no habitable.";

        // Actualizar estado de la reserva
        reserva.Estado = EstadoReserva.Cancelada;
        _reservaRepository.Actualizar(reserva);


        Console.WriteLine($"{resultado.Mensaje}");
        return resultado;
    }
}
