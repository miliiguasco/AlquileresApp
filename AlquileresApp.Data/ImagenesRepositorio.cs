using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Data;
using System.IO;

public class ImagenesRepositorio(AppDbContext dbContext) : IImagenesRepositorio
{
    private readonly string[] _formatosPermitidos = { ".jpg", ".jpeg", ".png" };

    public Imagen CargarImagen(Imagen imagen, int propiedadId)
    {
        try
        {
            // Verificar que la propiedad existe
            var propiedad = dbContext.Propiedades.Find(propiedadId);
            if (propiedad == null)
                throw new Exception("La propiedad no existe");

            // Validar formato de la imagen
            var extension = Path.GetExtension(imagen.Url).ToLowerInvariant();
            if (!_formatosPermitidos.Contains(extension))
                throw new Exception("Solo se permiten imágenes en formato JPG o PNG");

            // Asignar la propiedad a la imagen
            imagen.PropiedadId = propiedadId;

            // Agregar la imagen a la base de datos
            dbContext.Imagenes.Add(imagen);
            dbContext.SaveChanges();

            // Retornar la URL de la imagen guardada
            return imagen;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al guardar la imagen: {ex.Message}");
        }
    }   

    public List<Imagen> MostrarImagenes(int propiedadId)
    {
        return dbContext.Imagenes.Where(i => i.PropiedadId == propiedadId).ToList();
    }

    public async Task EliminarImagenAsync(int imagenId)
{
    var imagen = await dbContext.Imagenes.FindAsync(imagenId);
    if (imagen != null)
    {
        dbContext.Imagenes.Remove(imagen);
        await dbContext.SaveChangesAsync();
    }
}
}
