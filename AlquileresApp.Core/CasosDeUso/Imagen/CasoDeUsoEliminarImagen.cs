namespace AlquileresApp.Core.CasosDeUso.Imagen;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoEliminarImagen(IImagenesRepositorio imagenesRepositorio)
{
    public void Ejecutar(int imagenId)
    {
        imagenesRepositorio.EliminarImagen(imagenId);
    }
}
