namespace AlquileresApp.Core.CasosDeUso.Reserva;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
//using AlquileresApp.Core.CasosDeUso;
using AlquileresApp.Core.Validadores;


public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio, IPropiedadRepositorio propiedadRepositorio, IUsuarioRepositorio usuarioRepositorio,  ITarjetaRepositorio tarjetaRepositorio, IFechaReservaValidador fechaReservaValidador)
{
    public async Task Ejecutar(int clienteId, int propiedadId, DateTime fechaInicio, DateTime fechaFin)
    {
        //validar fecha
        fechaReservaValidador.FechaValidador(fechaInicio, fechaFin);
        // Obtener la propiedad
        var propiedad = propiedadRepositorio.ObtenerPropiedadPorId(propiedadId);
        if (propiedad == null)
        {
            throw new Exception("Propiedad no encontrada"); //validador 
        }

        // Obtener el cliente
        var cliente = usuarioRepositorio.ObtenerUsuarioPorId(clienteId) as Cliente;
        if (cliente == null)
        {
            throw new Exception("Cliente no encontrado"); //validador 
        }

        propiedadRepositorio.ComprobarDisponibilidad(propiedad, fechaInicio, fechaFin);

        //validar tarjeta
        var tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(cliente.Id);
        if (tarjeta == null){
            throw new Exception("No se pudo validar la tarjeta");
        }
        tarjetaRepositorio.ValidarSaldo(tarjeta,propiedad.MontoAPagar);
        tarjetaRepositorio.Pagar(tarjeta,propiedad.MontoAPagar); //repositorio o llamo al caso de uso de pagar?
        reservasRepositorio.CrearReserva(cliente, propiedad, fechaInicio, fechaFin);

    }
}