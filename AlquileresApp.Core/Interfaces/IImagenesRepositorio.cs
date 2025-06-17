namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IImagenesRepositorio{
    Imagen CargarImagen(Imagen imagen, int propiedadId);
    List<Imagen> MostrarImagenes(int propiedadId);
    Task EliminarImagenAsync(int imagenId);
} 
 