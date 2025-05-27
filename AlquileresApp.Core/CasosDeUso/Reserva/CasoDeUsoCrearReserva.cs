namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
//using AlquileresApp.Core.Validadores;

public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio, ITarjetaRepositorio tarjetaRepositorio)
{


    
    public void Ejecutar(Propiedad propiedad, Cliente cliente, DateTime fechaInicio, DateTime fechaFin)
    {
        //validar disponibilidad de fechas
        //reservasRepositorio.ValidarDisponibilidad(propiedad, fechaInicio, fechaFin);

        //validar tarjeta
        Tarjeta? tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(cliente.Id);
        if (tarjeta != null){
            tarjetaRepositorio.ValidarSaldo(tarjeta,propiedad.MontoAPagar);
            tarjetaRepositorio.Pagar(tarjeta,propiedad.MontoAPagar); //repositorio o llamo al caso de uso de pagar?
            reservasRepositorio.CrearReserva(cliente, propiedad, fechaInicio, fechaFin);
        }
        else{
            throw new Exception("No se pudo validar la tarjeta");
        }

    }
}