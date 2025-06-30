namespace AlquileresApp.Core.CasosDeUso.Promocion;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoCrearPromocion
{
    private readonly IPromocionRepositorio _repositorio;

    public CasoDeUsoCrearPromocion(IPromocionRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar(Promocion promocion)
    {
    if (string.IsNullOrWhiteSpace(promocion.Titulo))
        throw new ArgumentException("El título es obligatorio.");

    if (string.IsNullOrWhiteSpace(promocion.Descripcion))
        throw new ArgumentException("La descripción es obligatoria.");

    if (promocion.FechaInicio.Date < DateTime.Today)
        throw new ArgumentException("La fecha de inicio no puede ser anterior a hoy.");

    if (promocion.FechaFin.Date < promocion.FechaInicio.Date)
        throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

    if (promocion.PorcentajeDescuento <= 0 || promocion.PorcentajeDescuento > 100)
        throw new ArgumentException("El porcentaje de descuento debe ser mayor a 0 y menor o igual a 100.");
        _repositorio.Guardar(promocion);
    }
}