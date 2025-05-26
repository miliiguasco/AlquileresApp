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

        // var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Titulo == propiedad.Titulo);
        // if (propiedadExistente == null)
        //     throw new Exception("La propiedad no existe");

        // var tieneReservaActiva = dbContext.Reservas.Any(r => r.PropiedadId == propiedad.Id && r.Estado == EstadoReserva.Activa);
        // if (tieneReservaActiva)
        //     throw new Exception("No se puede eliminar una propiedad con reserva activa");

        // dbContext.Propiedades.Remove(propiedadExistente);
        // dbContext.SaveChanges();
    }

    public List<Propiedad> ListarPropiedades(){
        List<Propiedad> propiedades = dbContext.Propiedades.ToList();
        if (propiedades.Count == 0)
            throw new Exception("No se encontraron propiedades.");
        return propiedades;
    }

    public void ModificarPropiedad(Propiedad propiedad) {
        {
            var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Titulo == propiedad.Titulo);
            if (propiedadExistente == null)
                throw new Exception("La propiedad no existe");

            if (propiedadExistente.Titulo != propiedad.Titulo)
                verificarPropiedadDuplicada(propiedad.Titulo);

            dbContext.Entry(propiedadExistente).CurrentValues.SetValues(propiedad);
            dbContext.SaveChanges();
        }
    }
    private void verificarPropiedadDuplicada(string nombre){
        bool existe = dbContext.Propiedades.Any(p => p.Titulo == nombre);
        if (existe){
            throw new Exception("La propiedad ya existe");
        }   
    }

     public List<Propiedad> BuscarDisponiblesAsync(SearchFilters filtros)
    {
        Console.WriteLine("📡 Llamado a BuscarDisponiblesAsync");
        Console.WriteLine($"📍 Localidad buscada: {filtros.Localidad}");
        
        var query = dbContext.Propiedades.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtros.Localidad))
        {
            query = query.Where(p => string.Equals(p.Localidad, filtros.Localidad, StringComparison.OrdinalIgnoreCase));
        }

        if (filtros.CantidadHuespedes > 0)
        {
            query = query.Where(p => p.Capacidad >= filtros.CantidadHuespedes);
        }

        var propiedades = query.ToList();
        Console.WriteLine($"📊 Propiedades encontradas: {propiedades.Count}");
        return propiedades;
    }

     public void ComprobarDisponibilidad(Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin) //REVISAR ESTO 
    {
        var reservasExistentes = dbContext.Set<Reserva>()
            .Where(r => r.PropiedadId == propiedad.Id &&
                        r.FechaInicio <= fechaFin &&
                        r.FechaFin >= fechaInicio)
            .ToList();

        if (reservasExistentes.Any())
        {
            throw new Exception("La propiedad no está disponible en las fechas seleccionadas.");
        }
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