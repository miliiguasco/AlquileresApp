namespace AlquileresApp.Core.CasosDeUso.Reserva;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoReservarPropiedad(IReservaRepositorio reservasRepositorio)
{
    public void Ejecutar(Propiedad propiedad, Usuario usuario)
    {
        // Validaciones si hacen falta...
        reservasRepositorio.ReservarPropiedad(propiedad, usuario);
    }
}
