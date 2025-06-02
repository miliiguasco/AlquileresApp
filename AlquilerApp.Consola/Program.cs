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
    // Crear una reserva para la propiedad
    var reserva = new Reserva
    {
        ClienteId = 1,
        PropiedadId = 6,
        FechaInicio = DateTime.Now,
        FechaFin = DateTime.Now.AddDays(5),
        PrecioTotal = 1000
    };
    Console.WriteLine($"Se crea variable Reserva");

    // Agregar la reserva a la base de datos
    dbContext.Reservas.Add(reserva);
    dbContext.SaveChanges();
    Console.WriteLine("Reserva creada exitosamente!");
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