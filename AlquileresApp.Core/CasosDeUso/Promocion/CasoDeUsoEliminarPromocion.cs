namespace AlquileresApp.Core.CasosDeUso.Promocion;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoEliminarPromocion
{
    private readonly IPromocionRepositorio _repositorio;

    public CasoDeUsoEliminarPromocion(IPromocionRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public void Ejecutar(Guid id)
    {
        _repositorio.Eliminar(id);
    }
}