namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Interfaces;
//using AlquileresApp.Core.Validadores;

public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio, ITarjetaRepositorio tarjetaRepositorio, ITarjetaValidador tarjetaValidador)
{


    
    public void Ejecutar(Propiedad propiedad, Cliente cliente, DateTime fechaInicio, DateTime fechaFin)
    {
        //validar disponibilidad de fechas
        reservasRepositorio.ValidarDisponibilidad(propiedad, fechaInicio, fechaFin);

        //validar tarjeta
        Tarjeta? tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(cliente.Id);
        if (tarjeta != null){

            decimal monto = 0;

            if (propiedad.TipoPago == TipoPagoReserva.Parcial)
                monto = propiedad.MontoAnticipo;
            else if (propiedad.TipoPago == TipoPagoReserva.Total)
                monto = propiedad.MontoAnticipo;
            else if (propiedad.TipoPago == TipoPagoReserva.SinAnticipo)
                monto = 0;

            tarjetaRepositorio.ValidarSaldo(tarjeta,monto);
            tarjetaRepositorio.Pagar(tarjeta,monto);
            reservasRepositorio.CrearReserva(cliente, propiedad, fechaInicio, fechaFin);
        }

    }
}