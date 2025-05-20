using AlquileresApp.Core.Entidades;

public interface IPropiedadService
{
    Task<List<Propiedad>> BuscarPropiedadesAsync(string localidad, int cantidadHuespedes, DateTime fechaInicio, DateTime fechaFin);
}