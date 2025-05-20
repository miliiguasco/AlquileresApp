using System;
using System.Collections.Generic;
using System.Linq;
using alquileresapp.core.Entidades;
using alquileresapp.core.Enumerativos;

namespace alquileresapp.data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
{
    // Si ya hay datos, no cargamos nada
    if (context.Propiedades.Any()) return;

    // Crear encargados
    var encargado1 = new Encargado
    {
        Nombre = "María",
        Apellido = "Pérez",
        Dni = "12345678",
        Telefono = "2211234567",
        Email = "maria.perez@example.com"
    };

    var encargado2 = new Encargado
    {
        Nombre = "José",
        Apellido = "Gómez",
        Dni = "87654321",
        Telefono = "2237654321",
        Email = "jose.gomez@example.com"
    };

    context.Encargados.AddRange(encargado1, encargado2);

    // Crear usuarios registrados
    var usuario1 = new UsuarioRegistrado
    {
        Nombre = "Carlos",
        Apellido = "Ruiz",
        Dni = "23456789",
        Telefono = "2241122334",
        Email = "carlos@example.com"
    };

    var usuario2 = new UsuarioRegistrado
    {
        Nombre = "Laura",
        Apellido = "Díaz",
        Dni = "34567890",
        Telefono = "2255566778",
        Email = "laura@example.com"
    };

    context.UsuariosRegistrados.AddRange(usuario1, usuario2);

    context.SaveChanges(); // guardo para que se generen los Ids

    // Crear propiedades
    var propiedad1 = new Propiedad
    {
        Localidad = "La Plata",
        Direccion = "Calle 12 N°123",
        Descripcion = "Departamento moderno en el centro",
        Capacidad = 2,
        PrecioPorNoche = 15000,
        ServiciosDisponibles = new List<Servicios> { Servicios.Wifi, Servicios.Cocina },
        EncargadoId = encargado1.Id,
        Imagenes = new List<Imagen>()
    };

    var propiedad2 = new Propiedad
    {
        Localidad = "Mar del Plata",
        Direccion = "Av. Costanera 456",
        Descripcion = "Casa frente al mar",
        Capacidad = 4,
        PrecioPorNoche = 30000,
        ServiciosDisponibles = new List<Servicios> { Servicios.Wifi, Servicios.Piscina },
        EncargadoId = encargado2.Id,
        Imagenes = new List<Imagen>()
    };

    context.Propiedades.AddRange(propiedad1, propiedad2);
    context.SaveChanges();

    // Crear reservas
    var reserva1 = new Reserva
    {
        FechaInicio = new DateTime(2025, 6, 10),
        FechaFin = new DateTime(2025, 6, 15),
        PropiedadId = propiedad1.Id,
        UsuarioRegistradoId = usuario1.Id
    };

    var reserva2 = new Reserva
    {
        FechaInicio = new DateTime(2025, 6, 20),
        FechaFin = new DateTime(2025, 6, 25),
        PropiedadId = propiedad2.Id,
        UsuarioRegistradoId = usuario2.Id
    };

    context.Reservas.AddRange(reserva1, reserva2);
    context.SaveChanges();
}

    }
}
