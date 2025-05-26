using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class FechaReservaValidador : IReservaValidador

{
    public void ValidarFechaReserva(Reserva reserva){
        if (reserva.FechaInicio < DateTime.Now)
            throw new Exception("La fecha de inicio de la reserva no puede ser en el pasado");
            
        if (reserva.FechaFin < DateTime.Now) 
            throw new Exception("La fecha de fin de la reserva no puede ser en el pasado");
            
        if (reserva.FechaInicio > reserva.FechaFin)
            throw new Exception("La fecha de inicio de la reserva no puede ser mayor a la fecha de fin");
    }

}
