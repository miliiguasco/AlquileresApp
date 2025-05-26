using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core.CasosDeUso.Imagen;

public class CasoDeUsoCargarImagen
{
    private readonly IImagenesRepositorio _imagenesRepositorio;

    public CasoDeUsoCargarImagen(IImagenesRepositorio imagenesRepositorio)
    {
        _imagenesRepositorio = imagenesRepositorio;
    }

    public AlquileresApp.Core.Entidades.Imagen Ejecutar(AlquileresApp.Core.Entidades.Imagen imagen, int propiedadId)
    {
        return _imagenesRepositorio.CargarImagen(imagen, propiedadId);
    }
}
