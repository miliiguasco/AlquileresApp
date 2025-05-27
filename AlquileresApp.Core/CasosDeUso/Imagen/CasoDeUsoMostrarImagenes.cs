namespace AlquileresApp.Core.CasosDeUso.Imagen;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoMostrarImagenes(IImagenesRepositorio imagenesRepositorio)
{
    public List<Imagen> Ejecutar(int propiedadId)
    {
        return imagenesRepositorio.MostrarImagenes(propiedadId);
    }
}
