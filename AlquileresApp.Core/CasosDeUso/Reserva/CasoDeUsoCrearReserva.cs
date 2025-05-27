namespace AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
//using AlquileresApp.Core.CasosDeUso.Tarjeta;
//using AlquileresApp.Core.Validadores;


public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio, IPropiedadRepositorio propiedadRepositorio, IUsuarioRepositorio usuarioRepositorio  /*ITarjetaRepositorio tarjetaRepositorio*/)
{
    public async Task Ejecutar(int clienteId, int propiedadId, DateTime fechaInicio, DateTime fechaFin)
    {
        // Obtener la propiedad
        var propiedades = propiedadRepositorio.ListarPropiedades();
        var propiedad = propiedades.FirstOrDefault(p => p.Id == propiedadId);
        if (propiedad == null)
        {
            throw new Exception("Propiedad no encontrada");
        }

        // Obtener el cliente
        var cliente = usuarioRepositorio.ObtenerUsuarioPorId(clienteId) as Cliente;
        if (cliente == null)
        {
            throw new Exception("Cliente no encontrado");
        }
        /*
        //validar tarjeta
        Tarjeta? tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(cliente.Id);
        if (tarjeta != null){
            tarjetaRepositorio.ValidarSaldo(tarjeta,propiedad.MontoAPagar);
            tarjetaRepositorio.Pagar(tarjeta,propiedad.MontoAPagar); //repositorio o llamo al caso de uso de pagar?
        }
        else{
            throw new Exception("No se pudo validar la tarjeta");
        }
            */
        reservasRepositorio.CrearReserva(cliente, propiedad, fechaInicio, fechaFin);

    }
}