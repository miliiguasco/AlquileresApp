using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;
using System;

public class PromocionRepositorio(AppDbContext dbContext) : IPromocionRepositorio
{
    public List<Promocion> ObtenerTodas()
{
    return dbContext.Promociones
        .Include(p => p.Propiedades) // 👈 Incluye la relación con Propiedades
        .ToList();
}

    public void Guardar(Promocion promocion, List<int> propiedadesSeleccionadas)
    {
        if (string.IsNullOrWhiteSpace(promocion.Titulo))
            throw new ArgumentException("El título es obligatorio.");
        if (string.IsNullOrWhiteSpace(promocion.Descripcion))
            throw new ArgumentException("La descripción es obligatoria.");
        if (promocion.FechaInicio >= promocion.FechaFin)
            throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");
        if (promocion.PorcentajeDescuento <= 0 || promocion.PorcentajeDescuento > 100)
            throw new ArgumentException("El porcentaje de descuento debe ser mayor a 0 y menor o igual a 100.");
        var existe = dbContext.Promociones
            .Any(p => p.Titulo.ToLower() == promocion.Titulo.ToLower() && !p.borrada);

        if (existe)
        {
            throw new Exception("Ya existe una promoción activa con el mismo título.");
        }
         var propiedades = dbContext.Propiedades
        .Where(p => propiedadesSeleccionadas.Contains(p.Id))
        .ToList();
        promocion.Propiedades = propiedades;
        promocion.borrada = false;

        dbContext.Promociones.Add(promocion);
        dbContext.SaveChanges();
    }

    public void Actualizar(int id, string titulo, string descripcion, DateTime fechaInicio, DateTime fechaFin,  DateTime fechaInicioReserva, DateTime fechaFinReserva, decimal porcentajeDescuento, List<int> propiedadesSeleccionadas)
{
    var promocion = dbContext.Promociones
        .Include(p => p.Propiedades) // 👈 Cargamos las propiedades asociadas
        .FirstOrDefault(p => p.Id == id && !p.borrada);

    if (promocion == null)
        throw new Exception("Promoción no encontrada.");

    var conflicto = dbContext.Promociones.Any(p =>
        p.Id != id &&
        p.Titulo.ToLower() == titulo.ToLower() &&
        !p.borrada);

    if (conflicto)
        throw new Exception("Ya existe otra promoción activa con el mismo título.");

    // Actualizar campos
    promocion.Titulo = titulo;
    promocion.Descripcion = descripcion;
    promocion.FechaInicio = fechaInicio;
    promocion.FechaFin = fechaFin;
    promocion.PorcentajeDescuento = porcentajeDescuento;

    promocion.Propiedades.Clear(); 
    var propiedades = dbContext.Propiedades
        .Where(p => propiedadesSeleccionadas.Contains(p.Id))
        .ToList();

    foreach (var propiedad in propiedades)
    {
        dbContext.Attach(propiedad); 
        promocion.Propiedades.Add(propiedad);
    }
    Console.WriteLine("Propiedades asociadas a la promoción:");
foreach (var propiedad in promocion.Propiedades)
{
    Console.WriteLine($"- {propiedad.Titulo} (ID: {propiedad.Id})");
}

    dbContext.SaveChanges();
}
    public void Eliminar(int id)
    {
        var promocion = dbContext.Promociones.Find(id);
        if (promocion != null)
        {
            promocion.borrada = true;
            dbContext.SaveChanges();
        }
    }

    public Promocion ObtenerPorId(int id)
{
    return dbContext.Promociones
        .Include(p => p.Propiedades) // 👈 Incluye las propiedades asociadas
        .FirstOrDefault(p => p.Id == id && !p.borrada);
}
  public List<Promocion> ObtenerTodasActivas()
{
    return dbContext.Promociones
        .Include(p => p.Propiedades) // 👈 Incluye las propiedades asociadas
        .Where(p => !p.borrada && p.FechaInicio <= DateTime.Today && p.FechaFin >= DateTime.Today)
        .ToList();
}
}
