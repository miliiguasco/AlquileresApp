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
            .Where(p => !p.borrada)
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

    public void Actualizar(Promocion promocion)
    {
        var conflicto = dbContext.Promociones.Any(p =>
            p.Id != promocion.Id &&
            p.Titulo.ToLower() == promocion.Titulo.ToLower() &&
            !p.borrada);

        if (conflicto)
        {
            throw new Exception("Ya existe otra promoción activa con el mismo título.");
        }
        else
        {

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
}
