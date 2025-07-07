namespace AlquileresApp.Core.CasosDeUso.Promocion;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;    
public class CasoDeUsoListarPromociones
{
    private readonly IPromocionRepositorio _repositorio;

    public CasoDeUsoListarPromociones(IPromocionRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public List<Promocion> Ejecutar() => _repositorio.ObtenerTodas();
}