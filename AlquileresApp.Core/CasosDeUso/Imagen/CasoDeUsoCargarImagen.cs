namespace AlquileresApp.Core.CasosDeUso.Imagen;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoCargarImagen(IImagenesRepositorio imagenesRepositorio)
{
    public Imagen Ejecutar(Imagen imagen, int propiedadId)
    {
       return imagenesRepositorio.CargarImagen(imagen, propiedadId);
    }
}
