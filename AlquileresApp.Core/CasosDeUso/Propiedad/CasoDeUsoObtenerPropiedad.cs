namespace AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
public class CasoDeUsoObtenerPropiedad(IPropiedadRepositorio _repo)
{

    public Propiedad Ejecutar(int id)
    {
        return _repo.ObtenerPorId(id);
    }
}
