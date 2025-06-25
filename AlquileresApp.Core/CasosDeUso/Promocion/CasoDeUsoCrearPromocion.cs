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
        _repositorio.Guardar(promocion);
    }
}