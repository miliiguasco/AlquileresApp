using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using AlquileresApp.Core.Servicios;
using AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core;
using AlquileresApp.Core.Enumerativos;

try
{
    // Crear instancias necesarias
    var dbContext = new AppDbContext();
    var PropiedadRepo = new PropiedadesRepositorio(dbContext);
    var casoDeUsoEliminar = new CasoDeUsoEliminarPropiedad(PropiedadRepo);

    // Buscar una propiedad existente
    Console.WriteLine("Buscando propiedades en la base de datos...");
    var propiedades = PropiedadRepo.ListarPropiedades();

    if (propiedades.Count == 0)
    {
        Console.WriteLine("No se encontraron propiedades en la base de datos.");
        return;
    }

    // Tomar la primera propiedad encontrada
    var propiedadAEliminar = propiedades.First();
    Console.WriteLine($"Propiedad encontrada: {propiedadAEliminar.Titulo}");

    // Crear una reserva para la propiedad
    var reserva = new Reserva
    {
        UsuarioRegistradoId = 1,
        PropiedadId = propiedadAEliminar.Id,
        FechaInicio = DateTime.Now,
        FechaFin = DateTime.Now.AddDays(5),
        PrecioTotal = propiedadAEliminar.PrecioPorNoche * 5
    };
    Console.WriteLine($"Se crea variable Reserva");

    // Agregar la reserva a la base de datos
    dbContext.Reservas.Add(reserva);
    dbContext.SaveChanges();
    Console.WriteLine("Reserva creada exitosamente!");

    // Intentar eliminar la propiedad (debería fallar por tener una reserva activa)
    Console.WriteLine("\nIntentando eliminar propiedad con reserva activa...");
    try
    {
        casoDeUsoEliminar.Ejecutar(propiedadAEliminar);
        Console.WriteLine("¡Error! La propiedad se eliminó cuando no debería ser posible");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Resultado esperado: {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error durante el proceso: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
     if (ex.InnerException != null)
    {
        Console.WriteLine("Inner exception: " + ex.InnerException.Message);
    }

    if (ex.InnerException?.InnerException != null)
    {
        Console.WriteLine("Inner-inner exception: " + ex.InnerException.InnerException.Message);
    }
}