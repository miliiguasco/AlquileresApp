namespace AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Validadores;
using System;

    public class ResultadoModificacionReserva
    {
        public bool EsExitosa { get; set; }
        public string Mensaje { get; set; }
    }
    public class ResultadoVistaPreviaModificacion
{
    public bool EsPosible { get; set; }
    public string Mensaje { get; set; }
    public decimal Diferencia { get; set; }
    public decimal MontoNuevo { get; set; }
    public decimal MontoAnterior { get; set; }
    public decimal MontoBase { get; set; }
}

    public class CasoDeUsoModificarReserva
    {
        private readonly IReservaRepositorio _reservaRepository;
        private readonly IPropiedadRepositorio _propiedadRepository;
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly ITarjetaRepositorio _tarjetaRepository;
        private readonly IFechaReservaValidador _fechaReservaValidador;
        private readonly INotificadorEmail _notificadorEmail;

        public CasoDeUsoModificarReserva(
            IReservaRepositorio reservaRepository,
            IPropiedadRepositorio propiedadRepository,
            IUsuarioRepositorio usuarioRepository,
            ITarjetaRepositorio tarjetaRepository,
            IFechaReservaValidador fechaReservaValidador, INotificadorEmail notificadorEmail)
        {
            _reservaRepository = reservaRepository;
            _propiedadRepository = propiedadRepository;
            _usuarioRepository = usuarioRepository;
            _tarjetaRepository = tarjetaRepository;
            _fechaReservaValidador = fechaReservaValidador;
            _notificadorEmail = notificadorEmail;
        }

        public ResultadoModificacionReserva Ejecutar(int reservaId, DateTime nuevaFechaInicio, DateTime nuevaFechaFin)
{
    var preview = CalcularModificacion(reservaId, nuevaFechaInicio, nuevaFechaFin);

    if (!preview.EsPosible)
        return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = preview.Mensaje };

    var reserva = _reservaRepository.ObtenerReservaPorId(reservaId);
    var cliente = _usuarioRepository.ObtenerUsuarioPorId(reserva.ClienteId) as Cliente;
    if (cliente == null)
        return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "Cliente no encontrado." };

    var tarjeta = _tarjetaRepository.ObtenerPorClienteId(reserva.ClienteId);
            if (tarjeta == null)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "No se encontró una tarjeta asociada al cliente." };

    var diferencia = preview.Diferencia;

    if (diferencia > 0)
    {
        if (tarjeta.Saldo < diferencia)
            return new ResultadoModificacionReserva
            {
                EsExitosa = false,
                Mensaje = $"Fondos insuficientes para cubrir el monto adicional de {diferencia:C}."
            };

        var pagoExitoso = _tarjetaRepository.Pagar(tarjeta, diferencia);
        if (!pagoExitoso)
            return new ResultadoModificacionReserva
            {
                EsExitosa = false,
                Mensaje = $"Error al procesar el pago adicional de {diferencia:C}."
            };
    }
    else if (diferencia < 0)
    {
        var reembolso = Math.Abs(diferencia);
        _tarjetaRepository.Reembolsar(tarjeta, reembolso);
    }

    reserva.FechaInicio = nuevaFechaInicio;
    reserva.FechaFin = nuevaFechaFin;
    reserva.MontoAPagar = preview.MontoNuevo;
    reserva.PrecioTotal = preview.MontoBase;

    _reservaRepository.Actualizar(reserva);

    string mensajeFinal = diferencia switch
    {
        > 0 => $"Modificación exitosa. Se abonó un adicional de {diferencia:C}.",
        < 0 => $"Modificación exitosa. Se reembolsó {Math.Abs(diferencia):C}.",
        _ => "Modificación exitosa. El monto total no se modificó."
    };
    // ✅ Enviar email al cliente
    string asunto = "Confirmación de modificación de reserva";
    string cuerpo = $"""
    Hola {cliente.Nombre},

    Tu reserva ha sido modificada exitosamente.

    Nueva fecha de inicio: {nuevaFechaInicio:dd/MM/yyyy}
    Nueva fecha de fin: {nuevaFechaFin:dd/MM/yyyy}

    {mensajeFinal}

    ¡Gracias por confiar en nosotros!

    Saludos,
    Equipo de Reservas
    """;

    _notificadorEmail.EnviarEmail(cliente.Email, asunto, cuerpo);
    return new ResultadoModificacionReserva
    {
        EsExitosa = true,
        Mensaje = mensajeFinal
    };
}

    public ResultadoVistaPreviaModificacion CalcularModificacion(int reservaId, DateTime nuevaFechaInicio, DateTime nuevaFechaFin)
{
    var reserva = _reservaRepository.ObtenerReservaPorId(reservaId);
    if (reserva == null)
        return new ResultadoVistaPreviaModificacion { EsPosible = false, Mensaje = "Reserva no encontrada." };

    if (reserva.Estado == EstadoReserva.Cancelada)
        return new ResultadoVistaPreviaModificacion { EsPosible = false, Mensaje = "La reserva ya está cancelada." };

    _fechaReservaValidador.FechaValidador(nuevaFechaInicio, nuevaFechaFin);

    var propiedad = _propiedadRepository.ObtenerPropiedadPorId(reserva.PropiedadId);
    if (propiedad == null)
        return new ResultadoVistaPreviaModificacion { EsPosible = false, Mensaje = "Propiedad no encontrada." };

    var disponible = _propiedadRepository.ComprobarDisponibilidadModificacion(propiedad.Id, nuevaFechaInicio, nuevaFechaFin, reserva.Id);
    if (!disponible)
        return new ResultadoVistaPreviaModificacion { EsPosible = false, Mensaje = "Fechas no disponibles." };

    var dias = (nuevaFechaFin.Date - nuevaFechaInicio.Date).Days;
    var nuevoMontoBase = propiedad.PrecioPorNoche * dias;

    var nuevoMonto = propiedad.TipoPago switch
    {
        TipoPago.SinAnticipo => 0,
        TipoPago.Parcial => nuevoMontoBase * 0.20m,
        TipoPago.Total => nuevoMontoBase,
        _ => throw new Exception("Tipo de pago inválido")
    };

    var diferencia = nuevoMonto - reserva.MontoAPagar;

    string mensaje = diferencia switch
    {
        > 0 => $"Se deberá abonar un adicional de {diferencia:C}.",
        < 0 => $"Se reembolsará {Math.Abs(diferencia):C}.",
        _ => "No hay diferencia en el monto a pagar."
    };

    return new ResultadoVistaPreviaModificacion
    {
        EsPosible = true,
        Mensaje = mensaje,
        Diferencia = diferencia,
        MontoNuevo = nuevoMonto,
        MontoAnterior = reserva.MontoAPagar,
        MontoBase = nuevoMontoBase
    };
}


    }