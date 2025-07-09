namespace AlquileresApp.Core.CasosDeUso.Reserva;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Validadores;
using AlquileresApp.Core.Servicios;


public class CasoDeUsoCrearReserva(
    IReservaRepositorio reservasRepositorio, 
    IPropiedadRepositorio propiedadRepositorio, 
    IUsuarioRepositorio usuarioRepositorio,  
    ITarjetaRepositorio tarjetaRepositorio, 
    IFechaReservaValidador fechaReservaValidador,
    INotificadorEmail notificadorEmail, IPromocionRepositorio promocionRepositorio)
{
    public async Task Ejecutar(int clienteId, int propiedadId, DateTime fechaInicio, DateTime fechaFin, int cantidadHuespedes)
    {
        Console.WriteLine($"Iniciando creación de reserva: ClienteId={clienteId}, PropiedadId={propiedadId}");
        Console.WriteLine($"Fechas recibidas - Inicio: {fechaInicio:yyyy-MM-dd}, Fin: {fechaFin:yyyy-MM-dd}");
        
        // Validar fechas
        if (fechaInicio < DateTime.Today)
        {
            throw new Exception("La fecha de inicio no puede ser anterior a hoy");
        }

        if (fechaFin <= fechaInicio)
        {
            throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio");
        }

        //validar fecha
        fechaReservaValidador.FechaValidador(fechaInicio, fechaFin);
        
        // Obtener la propiedad
        var propiedad = propiedadRepositorio.ObtenerPropiedadPorId(propiedadId);
        if (propiedad == null)
        {
            throw new Exception("Propiedad no encontrada");
        }
        Console.WriteLine($"Propiedad encontrada: {propiedad.Titulo}");

        // Obtener el cliente
        var usuario = usuarioRepositorio.ObtenerUsuarioPorId(clienteId);
        if (usuario == null)
        {
            throw new Exception($"No se encontró usuario con ID: {clienteId}");
        }

        if (usuario is not Cliente cliente)
        {
            throw new Exception($"El usuario con ID {clienteId} no es un cliente");
        }

        Console.WriteLine($"Cliente encontrado: {cliente.Nombre} {cliente.Apellido}");

        propiedadRepositorio.ComprobarDisponibilidad(propiedad, fechaInicio, fechaFin);
        Console.WriteLine("Disponibilidad verificada");

        try
        {
            // Obtener y validar tarjeta
            var tarjeta = tarjetaRepositorio.ObtenerPorClienteId(cliente.Id);
            if (tarjeta == null )
            {
                throw new Exception("El cliente no tiene una tarjeta registrada. Por favor, registre una tarjeta antes de realizar la reserva.");
            }


            // Validar fecha de vencimiento de la tarjeta
            if (!DateTime.TryParseExact(tarjeta.FechaVencimiento, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaVencimiento))
            {
                throw new Exception("Formato de fecha de vencimiento de tarjeta inválido");
            }

            if (fechaVencimiento < DateTime.Today)
            {
                throw new Exception("La tarjeta ha expirado");
            }

            // Calcular el monto total de la reserva
            var dias = (fechaFin - fechaInicio).Days;
            var montoBase = propiedadRepositorio.CalcularPrecioConPromocion(propiedad, DateTime.Now, fechaInicio, fechaFin ) * dias;
            var montoTotal = propiedad.TipoPago switch //actualizar tmb deuda 
            {
                TipoPago.SinAnticipo => 0,
                TipoPago.Parcial => montoBase * 0.20m,
                TipoPago.Total => montoBase,
                _ => throw new Exception("Tipo de pago no válido")
            };
            Console.WriteLine($"Monto total a pagar: {montoTotal} (Precio por noche: {propiedad.PrecioPorNoche}, Días: {dias})");


            // Realizar pago (que incluye la validación de saldo)
            if (!tarjetaRepositorio.Pagar(tarjeta, montoTotal))
            {
                throw new Exception($"Saldo insuficiente.");
            }

            Console.WriteLine("Pago procesado correctamente");
            Reserva reserva = new Reserva(cliente, propiedad, fechaInicio, fechaFin, cantidadHuespedes,montoBase, montoTotal);
            if (reserva.FechaInicio == DateTime.Today) 
                reserva.Estado = EstadoReserva.Activa;
            else    
                reserva.Estado = EstadoReserva.Pendiente; 
            reserva.MontoRestante = propiedad.TipoPago switch
            {
                TipoPago.SinAnticipo => montoBase,
                TipoPago.Parcial => montoBase - (montoBase * 0.20m),
                TipoPago.Total => 0,
                _ => throw new Exception("Tipo de pago no válido")
            };
            
            // Crear reserva
            reservasRepositorio.CrearReserva(reserva);
            Console.WriteLine("Reserva creada exitosamente");
            notificadorEmail.EnviarConfirmacionReserva(cliente.Email, cliente.Nombre, fechaInicio.ToString("dd/MM/yyyy"), fechaFin.ToString("dd/MM/yyyy"), propiedad.Titulo);
            // Programar tareas automáticas

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear la reserva: {ex.Message}");
            throw;
        }
    }
}