namespace AlquileresApp.Core.CasosDeUso.Promocion;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;    
public class CasoDeUsoListarPromocionesActivas
{
    private readonly IPromocionRepositorio _repositorio;

    public CasoDeUsoListarPromocionesActivas(IPromocionRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public List<Promocion> Ejecutar() => _repositorio.ObtenerTodasActivas();
}