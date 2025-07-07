namespace AlquileresApp.Core.CasosDeUso.Promocion;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
public class CasoDeUsoModificarPromocion
{
    private readonly IPromocionRepositorio _repositorio;

    public CasoDeUsoModificarPromocion(IPromocionRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar(int id, string titulo, string descripcion, DateTime fechaInicio, DateTime fechaFin, DateTime fechaInicioReserva, DateTime fechaFinReserva, decimal porcentajeDescuento, List<int> propiedadesSeleccionadas)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        throw new ArgumentException("El título es obligatorio.");

    if (string.IsNullOrWhiteSpace(descripcion))
        throw new ArgumentException("La descripción es obligatoria.");

    if (fechaInicio.Date < DateTime.Today)
        throw new ArgumentException("La fecha de inicio no puede ser anterior a hoy.");

    if (fechaFin.Date < fechaInicio.Date)
        throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

    if (porcentajeDescuento <= 0 || porcentajeDescuento > 100)
        throw new ArgumentException("El porcentaje de descuento debe ser mayor a 0 y menor o igual a 100.");
        _repositorio.Actualizar(id, titulo, descripcion, fechaInicio, fechaFin,fechaInicioReserva,fechaFinReserva, porcentajeDescuento, propiedadesSeleccionadas);
    }
}