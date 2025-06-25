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

    public void Ejecutar(Promocion promocion)
    {
        _repositorio.Actualizar(promocion);
    }
}