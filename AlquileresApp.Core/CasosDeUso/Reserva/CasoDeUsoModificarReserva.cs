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

    public class CasoDeUsoModificarReserva
    {
        private readonly IReservaRepositorio _reservaRepository;
        private readonly IPropiedadRepositorio _propiedadRepository;
        private readonly IUsuarioRepositorio _usuarioRepository;
        private readonly ITarjetaRepositorio _tarjetaRepository;
        private readonly IFechaReservaValidador _fechaReservaValidador;

        public CasoDeUsoModificarReserva(
            IReservaRepositorio reservaRepository,
            IPropiedadRepositorio propiedadRepository,
            IUsuarioRepositorio usuarioRepository,
            ITarjetaRepositorio tarjetaRepository,
            IFechaReservaValidador fechaReservaValidador)
        {
            _reservaRepository = reservaRepository;
            _propiedadRepository = propiedadRepository;
            _usuarioRepository = usuarioRepository;
            _tarjetaRepository = tarjetaRepository;
            _fechaReservaValidador = fechaReservaValidador;
        }

        public  ResultadoModificacionReserva Ejecutar(int reservaId, DateTime nuevaFechaInicio, DateTime nuevaFechaFin)
        {
            var reserva =  _reservaRepository.ObtenerReservaPorId(reservaId);
            if (reserva == null)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "Reserva no encontrada." };

            if (reserva.Estado == EstadoReserva.Cancelada)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "La reserva ya se encuentra cancelada." };

            _fechaReservaValidador.FechaValidador(nuevaFechaInicio, nuevaFechaFin);

            var propiedad =  _propiedadRepository.ObtenerPropiedadPorId(reserva.PropiedadId);
            if (propiedad == null)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "Propiedad no encontrada." };

            // Validar disponibilidad (excluyendo la reserva actual para evitar conflicto consigo misma)
            var disponible = _propiedadRepository.ComprobarDisponibilidadModificacion(propiedad.Id, nuevaFechaInicio, nuevaFechaFin, reserva.Id);
            if (!disponible)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "No hay disponibilidad para las fechas seleccionadas." };

            var cliente =  _usuarioRepository.ObtenerUsuarioPorId(reserva.ClienteId) as Cliente;
            if (cliente == null)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "Cliente no encontrado." };

            var tarjeta =  _tarjetaRepository.ObtenerTarjetaPorId(cliente.Id);
            if (tarjeta == null)
                return new ResultadoModificacionReserva { EsExitosa = false, Mensaje = "No se encontró una tarjeta asociada al cliente." };
            Console.WriteLine($"fecha inicio: {nuevaFechaInicio} Fecha fin:  {nuevaFechaFin}");
            var dias = (nuevaFechaFin.Date - nuevaFechaInicio.Date).Days;
            Console.WriteLine($"Cantidad de días: {dias}");
            var nuevoMontoBase = propiedad.PrecioPorNoche * dias;
            Console.WriteLine($"Nuevo monto base (PrecioPorNoche x días)({propiedad.PrecioPorNoche} x {dias}): {nuevoMontoBase:C}");
            var nuevoMonto = propiedad.TipoPago switch
            {
                TipoPago.SinAnticipo => 0,
                TipoPago.Parcial => nuevoMontoBase * 0.20m,
                TipoPago.Total => nuevoMontoBase,
                _ => throw new Exception("Tipo de pago inválido")
            };

            var diferencia = nuevoMonto - reserva.MontoAPagar;
            Console.WriteLine($"Diferencia entre nuevo monto y monto actual: {diferencia:C} (nuevomonto:{nuevoMonto} - monto a pagar:{reserva.MontoAPagar})");

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

            // Actualizar reserva
            reserva.FechaInicio = nuevaFechaInicio;
            reserva.FechaFin = nuevaFechaFin;
            reserva.MontoAPagar = nuevoMonto;
            reserva.PrecioTotal = nuevoMontoBase;

             _reservaRepository.Actualizar(reserva);

            string mensajeFinal = diferencia switch
            {
                > 0 => $"Modificación exitosa. Se abonó un adicional de {diferencia:C}.",
                < 0 => $"Modificación exitosa. Se reembolsó {Math.Abs(diferencia):C}.",
                _ => "Modificación exitosa. El monto total no se modificó."
            };

            return new ResultadoModificacionReserva
            {
                EsExitosa = true,
                Mensaje = mensajeFinal
            };
        }
    }

