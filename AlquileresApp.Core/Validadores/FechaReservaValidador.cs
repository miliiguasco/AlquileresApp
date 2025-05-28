using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class FechaReservaValidador : IFechaReservaValidador
{
    public void FechaValidador(DateTime fechaInicio, DateTime fechaFin) 
        {
            ValidarFechaReserva(fechaInicio, fechaFin);
        }

    public void ValidarFechaReserva(DateTime fechaInicio, DateTime fechaFin){
        if (fechaInicio < DateTime.Now)
            throw new Exception("La fecha de inicio de la reserva no puede ser en el pasado");
            
        if (fechaFin < DateTime.Now) 
            throw new Exception("La fecha de fin de la reserva no puede ser en el pasado");
            
        if (fechaInicio > fechaFin)
            throw new Exception("La fecha de inicio de la reserva no puede ser mayor a la fecha de fin");
    }


}
