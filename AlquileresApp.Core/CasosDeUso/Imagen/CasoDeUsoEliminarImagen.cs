namespace AlquileresApp.Core.CasosDeUso.Imagen;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoEliminarImagen(IImagenesRepositorio imagenesRepositorio)
{
    public async Task Ejecutar(int imagenId)
    {
        await imagenesRepositorio.EliminarImagenAsync(imagenId);
    }
}
