using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using AlquileresApp.Core.CasosDeUso.Propiedad;
using AlquileresApp.Core;

namespace AlquilerApp.Consola;

public class PropiedadConsola
{
    private readonly AppDbContext _dbContext;
    private readonly PropiedadesRepositorio _propiedadRepo;
    private readonly PropiedadValidador _validador;
    private readonly CasoDeUsoAgregarPropiedad _casoDeUsoAgregarPropiedad;

    public PropiedadConsola()
    {
        _dbContext = new AppDbContext();
        _propiedadRepo = new PropiedadesRepositorio(_dbContext);
        _validador = new PropiedadValidador();
        _casoDeUsoAgregarPropiedad = new CasoDeUsoAgregarPropiedad(_propiedadRepo, _validador);
    }

    public void CargarPropiedadDePrueba()
    {
        try
        {
            var propiedad = new Propiedad
            {
                Titulo = "Casa de Playa Test",
                Descripcion = "Hermosa casa frente al mar",
                Direccion = "Calle Principal 123",
                PrecioPorNoche = 150.00m,
                Capacidad = 4
            };

            Console.WriteLine("Intentando cargar propiedad...");
            _casoDeUsoAgregarPropiedad.Ejecutar(propiedad);
            Console.WriteLine("Propiedad cargada exitosamente!");

            VerificarPersistencia(propiedad.Titulo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el proceso: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }

    private void VerificarPersistencia(string titulo)
    {
        Console.WriteLine("Verificando persistencia...");
        var propiedades = _propiedadRepo.ListarPropiedades();
        var propiedadCargada = propiedades.FirstOrDefault(p => p.Titulo == titulo);
        
        if (propiedadCargada != null)
        {
            Console.WriteLine("¡Propiedad encontrada en la base de datos!");
            Console.WriteLine($"Título: {propiedadCargada.Titulo}");
            Console.WriteLine($"Descripción: {propiedadCargada.Descripcion}");
            Console.WriteLine($"Dirección: {propiedadCargada.Direccion}");
            Console.WriteLine($"Precio por noche: ${propiedadCargada.PrecioPorNoche}");
            Console.WriteLine($"Capacidad: {propiedadCargada.Capacidad} personas");
        }
        else
        {
            Console.WriteLine("Error: Propiedad no encontrada en la base de datos");
        }
    }
} 