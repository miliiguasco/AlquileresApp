using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

public class PropiedadesRepositorio(AppDbContext dbContext) : IPropiedadRepositorio
{
    public void CargarPropiedad(Propiedad propiedad){
        verificarPropiedadDuplicada(propiedad.Titulo);
        dbContext.Propiedades.Add(propiedad);
        dbContext.SaveChanges();
    }

    public void EliminarPropiedad(Propiedad propiedad)
    {
            var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
            if (propiedadExistente == null)
                throw new Exception("La propiedad no existe");

            var tieneReservaActiva = dbContext.Reservas.Any(r => r.Propiedad.Id == propiedad.Id);
            if (tieneReservaActiva)
                throw new Exception("No se puede eliminar una propiedad con reserva activa");

            dbContext.Propiedades.Remove(propiedadExistente);
            dbContext.SaveChanges();
    }

    public List<Propiedad> ListarPropiedades(){
        List<Propiedad> propiedades = dbContext.Propiedades.ToList();
        if (propiedades.Count == 0)
            throw new Exception("No se encontraron propiedades.");
        return propiedades;
    }
        
    public void ModificarPropiedad(Propiedad propiedad) {
        var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
        if (propiedadExistente == null)
            throw new Exception("La propiedad no existe");

        // Si el título está cambiando, verificar que no exista otro con ese título
        verificarPropiedadDuplicada(propiedad.Titulo);

        dbContext.Entry(propiedadExistente).CurrentValues.SetValues(propiedad);
        dbContext.SaveChanges();
    }

    private void verificarPropiedadDuplicada(string nombre){
        bool existe = dbContext.Propiedades.Any(p => p.Titulo == nombre);
        if (existe){
            throw new Exception("La propiedad ya existe");
        }   
    }

      public List<Propiedad> ListarPropiedadesFiltrado(SearchFilters filtros)
    {
        var query = dbContext.Propiedades
        .Include(p => p.Imagenes)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtros.Localidad))
        {
            query = query.Where(p => p.Localidad.ToLower().Contains(filtros.Localidad.ToLower()));
        }


        if (filtros.CantidadHuespedes.HasValue)
        {
            query = query.Where(p => p.Capacidad >= filtros.CantidadHuespedes.Value);
        }

    
            query = query.Where(p =>
                !(p.Reservas.Any(r =>
                    (filtros.FechaInicio >= r.FechaInicio && filtros.FechaInicio < r.FechaFin) ||
                    (filtros.FechaFin > r.FechaInicio && filtros.FechaFin <= r.FechaFin) ||
                    (filtros.FechaInicio <= r.FechaInicio && filtros.FechaFin >= r.FechaFin)
                ))
            );

        return query.ToList();
    }

    public void ComprobarDisponibilidad(Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin) //
    {
        var reservasExistentes = dbContext.Reservas
            .Where(r => r.Propiedad.Id == propiedad.Id &&
                        r.FechaInicio <= fechaFin &&
                        r.FechaFin >= fechaInicio)
            .ToList();

        if (reservasExistentes.Any())
        {
            throw new Exception("La propiedad no está disponible en las fechas seleccionadas.");
        }
    }
    public bool ComprobarDisponibilidadModificacion(int propiedadId, DateTime fechaInicio, DateTime fechaFin, int reservaId)
{
    return !dbContext.Reservas.Any(r =>
        r.Propiedad.Id == propiedadId &&
        r.Id != reservaId &&
        r.FechaInicio <= fechaFin &&
        r.FechaFin >= fechaInicio);

    
}


    public void ValidarDisponibilidad(DateTime fechaInicio, DateTime fechaFin){ //verificar este metodo
        var reservasExistentes = dbContext.Reservas
            .Where(r => r.FechaInicio <= fechaFin && r.FechaFin >= fechaInicio)
            .ToList();       
    }

    public Propiedad? ObtenerPropiedadPorId(int id)
    {
        return dbContext.Propiedades
            .Include(p => p.Imagenes)
            .FirstOrDefault(p => p.Id == id);
    }
    public Propiedad? ObtenerPorId(int id)
{
    return dbContext.Propiedades
        .Include(p => p.Imagenes)
        .FirstOrDefault(p => p.Id == id);
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