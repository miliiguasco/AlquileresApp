namespace AlquileresApp.Core.CasosDeUso.Promocion;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
public class CasoDeUsoObtenerPromocion
{
private readonly IPromocionRepositorio _repo;

    public CasoDeUsoObtenerPromocion(IPromocionRepositorio repositorio)
    {
        _repo = repositorio;
    }

    public Promocion Ejecutar(int id)
    {
        return _repo.ObtenerPorId(id);
    }
}