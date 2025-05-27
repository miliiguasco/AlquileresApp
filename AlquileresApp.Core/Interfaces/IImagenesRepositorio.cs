namespace AlquileresApp.Core.Interfaces
{
    public interface IImagenesRepositorio
    {
        // Método para cargar una imagen asociada a una propiedad
        Entidades.Imagen CargarImagen(Entidades.Imagen imagen, int propiedadId);
    }
} 