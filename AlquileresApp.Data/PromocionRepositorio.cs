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
            .ToList();
    }

    public void Guardar(Promocion promocion)
    {
        var existe = dbContext.Promociones
            .Any(p => p.Titulo.ToLower() == promocion.Titulo.ToLower() && !p.borrada);

        if (existe)
        {
            throw new Exception("Ya existe una promoción activa con el mismo título.");
        }

        promocion.borrada = false;

        dbContext.Promociones.Add(promocion);
        dbContext.SaveChanges();
    }

    public void Actualizar(int id, string titulo, string descripcion, DateTime fechaInicio, DateTime fechaFin, decimal porcentajeDescuento)
    {
        var promocion = dbContext.Promociones.Find(id);
        if (promocion == null)
        {
            throw new Exception("Promoción no encontrada.");
        }
        var conflicto = dbContext.Promociones.Any(p =>
            p.Id != id &&
            p.Titulo.ToLower() == titulo.ToLower() &&
            !p.borrada);

        if (conflicto)
        {
            throw new Exception("Ya existe otra promoción activa con el mismo título.");
        }
        else
        {

            promocion.Titulo = titulo;
            promocion.Descripcion = descripcion;
            promocion.FechaInicio = fechaInicio;
            promocion.FechaFin = fechaFin;
            promocion.PorcentajeDescuento = porcentajeDescuento;
            dbContext.Promociones.Update(promocion);
            dbContext.SaveChanges();
        }
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
            .FirstOrDefault(p => p.Id == id && !p.borrada);
    }
    public List<Promocion> ObtenerTodasActivas()
    {
        return dbContext.Promociones
            .Where(p => !p.borrada && p.FechaFin >= DateTime.Today)
            .ToList();
    }
}
