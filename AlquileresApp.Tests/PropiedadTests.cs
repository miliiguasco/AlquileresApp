using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using AlquileresApp.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AlquileresApp.Tests;

public class PropiedadTests
{
    private readonly AppDbContext _context;
    private readonly PropiedadesRepositorio _repositorio;

    public PropiedadTests()
    {
        // Usar la base de datos real
        _context = new AppDbContext();
        _repositorio = new PropiedadesRepositorio(_context);
    }

    [Fact]
    public void Propiedad_DeberiaPersistirEnBaseDeDatos()
    {
        // Arrange
        var propiedad = new Propiedad
        {
            Titulo = "Casa de Playa Test",
            Descripcion = "Hermosa casa frente al mar",
            Direccion = "Calle Principal 123",
            PrecioPorNoche = 150.00m,
            Capacidad = 4
        };

        try
        {
            // Act
            _repositorio.CargarPropiedad(propiedad);

            // Assert
            var propiedades = _repositorio.listarPropiedades();
            var propiedadGuardada = propiedades.FirstOrDefault(p => p.Titulo == "Casa de Playa Test");

            Assert.NotNull(propiedadGuardada);
            Assert.Equal("Casa de Playa Test", propiedadGuardada.Titulo);
            Assert.Equal("Hermosa casa frente al mar", propiedadGuardada.Descripcion);
            Assert.Equal("Calle Principal 123", propiedadGuardada.Direccion);
            Assert.Equal(150.00m, propiedadGuardada.PrecioPorNoche);
            Assert.Equal(4, propiedadGuardada.Capacidad);
        }
        finally
        {
            // Limpieza - Eliminar la propiedad de prueba
            var propiedadAEliminar = _repositorio.listarPropiedades()
                .FirstOrDefault(p => p.Titulo == "Casa de Playa Test");
            if (propiedadAEliminar != null)
            {
                _repositorio.eliminarPropiedad(propiedadAEliminar);
            }
        }
    }

    [Fact]
    public void Propiedad_NoDeberiaPermitirDuplicados()
    {
        // Arrange
        var propiedad1 = new Propiedad
        {
            Titulo = "Casa de Playa Test Duplicada",
            Descripcion = "Hermosa casa frente al mar",
            Direccion = "Calle Principal 123",
            PrecioPorNoche = 150.00m,
            Capacidad = 4
        };

        var propiedad2 = new Propiedad
        {
            Titulo = "Casa de Playa Test Duplicada",
            Descripcion = "Otra casa frente al mar",
            Direccion = "Calle Principal 456",
            PrecioPorNoche = 200.00m,
            Capacidad = 6
        };

        try
        {
            // Act & Assert
            _repositorio.CargarPropiedad(propiedad1);
            Assert.Throws<Exception>(() => _repositorio.CargarPropiedad(propiedad2));
        }
        finally
        {
            // Limpieza
            var propiedadAEliminar = _repositorio.listarPropiedades()
                .FirstOrDefault(p => p.Titulo == "Casa de Playa Test Duplicada");
            if (propiedadAEliminar != null)
            {
                _repositorio.eliminarPropiedad(propiedadAEliminar);
            }
        }
    }
} 