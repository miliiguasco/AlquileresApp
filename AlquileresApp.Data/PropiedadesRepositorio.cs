using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

using AlquileresApp.Core.Enumerativos;
using System;

public class PropiedadesRepositorio(AppDbContext dbContext) : IPropiedadRepositorio
{
    public void CargarPropiedad(Propiedad propiedad)
    {
        verificarPropiedadDuplicada(propiedad.Titulo);
        dbContext.Propiedades.Add(propiedad);
        dbContext.SaveChanges();
    }

    public bool EliminarPropiedad(Propiedad propiedad)
    {
        var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
        if (propiedadExistente == null)
            throw new Exception("La propiedad no existe");

        var tieneReserva = dbContext.Reservas.Any(r => r.PropiedadId == propiedad.Id);
        if (tieneReserva)
        {
            return false;
        }

        dbContext.Propiedades.Remove(propiedadExistente);
        dbContext.SaveChanges();
        return true;
    }

    public List<Propiedad> ListarPropiedades()
    {
        List<Propiedad> propiedades = dbContext.Propiedades
            .Include(p => p.Imagenes)
            .Include(p => p.Promociones) // 👈 Incluye las promociones asociadas
            .ToList();

        if (propiedades.Count == 0)
            throw new Exception("No se encontraron propiedades.");

        return propiedades;
    }

    public void ModificarPropiedad(Propiedad propiedad)
    {
        var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
        if (propiedadExistente == null)
            throw new Exception("La propiedad no existe");

        // Solo verificar duplicado si el título realmente cambió
        if (!string.Equals(propiedadExistente.Titulo, propiedad.Titulo, StringComparison.OrdinalIgnoreCase))
        {
            verificarPropiedadDuplicada(propiedad.Titulo);
        }

        dbContext.Entry(propiedadExistente).CurrentValues.SetValues(propiedad);
        dbContext.SaveChanges();
    }
    public void ComprobarDisponibilidad(Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin)
    {
        var reservasExistentes = dbContext.Reservas
            .Where(r => r.Propiedad.Id == propiedad.Id &&
                        r.FechaInicio <= fechaFin &&
                        r.FechaFin >= fechaInicio &&
                        r.Estado != EstadoReserva.Cancelada &&
                        r.Estado != EstadoReserva.Finalizada)
            .ToList();

        if (reservasExistentes.Any())
        {
            throw new Exception("La propiedad no está disponible en las fechas seleccionadas.");
        }
    }

    public void MarcarPropiedadComoNoHabitable(Propiedad propiedad)
    {
        dbContext.Propiedades.Update(propiedad);
        dbContext.SaveChanges();
    }

    private void verificarPropiedadDuplicada(string nombre)
    {

        string nombreTrimmed = nombre.Trim().ToLower();

        bool existe = dbContext.Propiedades
        .Any(p => p.Titulo.Trim().ToLower() == nombreTrimmed);
        if (existe)
        {
            throw new Exception("La propiedad ya existe");
        }
    }

    public List<Propiedad> ListarPropiedadesFiltrado(SearchFilters filtros)
    {
        var query = dbContext.Propiedades
            .Include(p => p.Imagenes)
            .Include(p => p.Reservas)
            .Include(p => p.Promociones)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtros.Localidad))
        {
            query = query.Where(p => p.Localidad.ToLower().Contains(filtros.Localidad.ToLower()));
        }

        if (filtros.CantidadHuespedes.HasValue)
        {
            query = query.Where(p => p.Capacidad >= filtros.CantidadHuespedes.Value);
        }

        // Verificar disponibilidad por fechas ignorando reservas canceladas o finalizadas
        query = query.Where(p =>
            !p.Reservas.Any(r =>
                r.Estado != EstadoReserva.Cancelada &&
                r.Estado != EstadoReserva.Finalizada &&
                (
                    (filtros.FechaInicio >= r.FechaInicio && filtros.FechaInicio < r.FechaFin) ||
                    (filtros.FechaFin > r.FechaInicio && filtros.FechaFin <= r.FechaFin) ||
                    (filtros.FechaInicio <= r.FechaInicio && filtros.FechaFin >= r.FechaFin)
                )
            )
        );

        return query.ToList();
    }

    public decimal CalcularPrecioConPromocion(Propiedad propiedad, DateTime fechaActual, DateTime fechaInicio, DateTime fechaFin)
    {
        var promocionesActivas = propiedad.Promociones
        .Where(p => !p.borrada &&
                    p.FechaInicio.Date <= fechaActual.Date &&
                    p.FechaFin.Date >= fechaActual.Date &&
                    p.FechaInicioReserva.Date <= fechaInicio.Date &&
                    p.FechaFinReserva.Date >= fechaFin.Date)
        .ToList();


        if (!promocionesActivas.Any())
            return propiedad.PrecioPorNoche;

        // Elegí la mayor promoción, o aplicá todas si así lo querés.
        var mejorPromocion = promocionesActivas
            .OrderByDescending(p => p.PorcentajeDescuento)
            .First();

        var descuento = propiedad.PrecioPorNoche * (mejorPromocion.PorcentajeDescuento / 100);
        return propiedad.PrecioPorNoche - descuento;
    }
    public bool ComprobarDisponibilidadModificacion(int propiedadId, DateTime fechaInicio, DateTime fechaFin, int reservaId)
    {
        return !dbContext.Reservas.Any(r =>
            r.Propiedad.Id == propiedadId &&
            r.Id != reservaId &&
            r.FechaInicio <= fechaFin &&
            r.FechaFin >= fechaInicio);


    }


    // public void ValidarDisponibilidad(DateTime fechaInicio, DateTime fechaFin){ //verificar este metodo
    public void ValidarDisponibilidad(DateTime fechaInicio, DateTime fechaFin)
    { //verificar este metodo
        var reservasExistentes = dbContext.Reservas
            .Where(r => r.FechaInicio <= fechaFin && r.FechaFin >= fechaInicio)
            .ToList();
    }

    public Propiedad? ObtenerPropiedadPorId(int id)
    {
        return dbContext.Propiedades
            .Include(p => p.Imagenes)
            .Include(p => p.Promociones)
            .Include(p => p.Reservas)
            .ThenInclude(r => r.Cliente)
            .Include(p => p.Calificaciones)
            .FirstOrDefault(p => p.Id == id);
    }
    public Propiedad? ObtenerPorId(int id)
    {
        return dbContext.Propiedades
            .Include(p => p.Imagenes)
            .Include(p => p.Promociones)
            .Include(p => p.Reservas)
            .ThenInclude(r => r.Cliente)
            .Include(p => p.Calificaciones)
            .FirstOrDefault(p => p.Id == id);
    }

    public void ActualizarCalificacionPromedio(int propiedadId, double nuevoPromedio)
    {
        var propiedad = dbContext.Propiedades.Find(propiedadId); // Usamos Find para obtener la entidad directamente por PK

        if (propiedad == null)
        {
            throw new Exception($"La propiedad con ID {propiedadId} no fue encontrada para actualizar su calificación promedio.");
        }

        propiedad.CalificacionPromedio = nuevoPromedio; // Actualizamos solo este campo
        dbContext.SaveChanges(); // Guardamos los cambios
    }

    private Boolean existePropiedad(string titulo)
    {
        return dbContext.Propiedades.Any(p => p.Titulo == titulo);
    }
    public List<Propiedad> obtenerPropiedades()
    {
        var propiedades = dbContext.Propiedades
       .Include(p => p.Imagenes)
       .Include(p => p.Promociones)
       .Include(p => p.Reservas)
       .Where(p => !p.NoHabitable)
       .ToList();


        return propiedades;
    }
    public List<Propiedad> ObtenerDestacadas()
{
    return dbContext.Propiedades
        .Include(p => p.Imagenes)
        .Where(p => p.Destacada) 
        .ToList();
}
}
/*
    public List<Propiedad> ListarPropiedadesConReservas()
    {
        var propiedadesConReservas = dbContext.Propiedades
            .Where(p => dbContext.Reservas.Any(r => r.PropiedadId == p.Id))
            .ToList();

        Console.WriteLine($"📊 Se encontraron {propiedadesConReservas.Count} propiedades con reservas");
        
        if (!propiedadesConReservas.Any())
        {
            throw new Exception("No se encontraron propiedades con reservas.");
        }

        return propiedadesConReservas;
    }
*/