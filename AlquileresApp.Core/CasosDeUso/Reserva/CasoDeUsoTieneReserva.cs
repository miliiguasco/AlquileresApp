namespace AlquileresApp.Core.Interfaces;

using AlquileresApp.Core.Entidades;

public class CasoDeUsoTieneReserva(IReservaRepositorio reservaRepositorio)
{
    public bool Ejecutar(int propiedadId)
    {
        if (propiedadId <= 0)
            throw new ArgumentException("El ID de la propiedad debe ser un número positivo.");

        return reservaRepositorio.TieneReservasActivas(propiedadId);
    }
}