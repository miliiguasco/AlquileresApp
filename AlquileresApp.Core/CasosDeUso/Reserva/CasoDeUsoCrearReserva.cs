namespace AlquileresApp.Core.CasosDeUso.Reserva;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
//using AlquileresApp.Core.CasosDeUso;
using AlquileresApp.Core.Validadores;


public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio, IPropiedadRepositorio propiedadRepositorio, IUsuarioRepositorio usuarioRepositorio,  ITarjetaRepositorio tarjetaRepositorio, IFechaReservaValidador fechaReservaValidador)
{
    public async Task Ejecutar(int clienteId, int propiedadId, DateTime fechaInicio, DateTime fechaFin, int cantidadHuespedes)
    {
        Console.WriteLine($"Iniciando creación de reserva: ClienteId={clienteId}, PropiedadId={propiedadId}");
        
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
        var cliente = usuarioRepositorio.ObtenerUsuarioPorId(clienteId) as Cliente;
        if (cliente == null)
        {
            throw new Exception("Cliente no encontrado");
        }
        Console.WriteLine($"Cliente encontrado: {cliente.Nombre} {cliente.Apellido}");

        propiedadRepositorio.ComprobarDisponibilidad(propiedad, fechaInicio, fechaFin);
        Console.WriteLine("Disponibilidad verificada");

        try
        {
            // Obtener y validar tarjeta
            var tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(cliente.Id);
            if (tarjeta == null)
            {
                throw new Exception("No se pudo validar la tarjeta");
            }

            // Calcular el monto total de la reserva
            var dias = (fechaFin - fechaInicio).Days;
            var montoTotal = propiedad.PrecioPorNoche * dias;
            Console.WriteLine($"Monto total a pagar: {montoTotal} (Precio por noche: {propiedad.PrecioPorNoche}, Días: {dias})");

            // Realizar pago (que incluye la validación de saldo)
            if (!tarjetaRepositorio.Pagar(tarjeta, montoTotal))
            {
                throw new Exception($"Saldo insuficiente. Saldo actual: {tarjeta.Saldo}, Monto requerido: {montoTotal}");
            }
            Console.WriteLine("Pago procesado correctamente");

            // Crear reserva
            reservasRepositorio.CrearReserva(cliente, propiedad, fechaInicio, fechaFin, cantidadHuespedes);
            Console.WriteLine("Reserva creada exitosamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en el proceso de reserva: {ex.Message}");
            throw new Exception($"Error al procesar la reserva: {ex.Message}");
        }
    }
}