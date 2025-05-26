namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
//using AlquileresApp.Core.Validadores;

public class CasoDeUsoCrearReserva(IReservaRepositorio reservasRepositorio,ITarjetaValidador tarjetaValidador, IPropiedadValidador propiedadValidador,  IFechaReservaValidador fechaReservaValidador)
{

// que parametros recibo aca?
    
    public void Ejecutar(Propiedad propiedad, Usuario usuario, DateTime fechaInicio, DateTime fechaFin){
        fechaReservaValidador.FechaValidador(fechaInicio, fechaFin);
        propiedadValidador.ValidarPropiedad(propiedad);
        propiedad.ComprobarDisponibilidad(propiedad, fechaInicio, fechaFin);
        var tarjeta = tarjetaValidador.ObtenerTarjetaPorId(usuario.TarjetaId);
        tarjetaValidador.ValidarTarjeta(tarjeta);

        reservasRepositorio.ReservarPropiedad(propiedad, usuario, fechaInicio, fechaFin);
    }
}