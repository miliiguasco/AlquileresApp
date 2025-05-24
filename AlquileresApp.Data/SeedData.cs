using AlquileresApp.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Usuarios.Any())
            {
                // La base de datos ya tiene datos, no hacemos nada.
                return;
            }

            // Crear usuarios registrados
            var usuarios = new List<Cliente>
            {
                new Cliente
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan.perez@test.com",
                    Telefono = "123456789",
                    Password = "password123", // En producción, debe estar hasheada
                    FechaNacimiento = new DateTime(1990, 1, 1)
                },
                new Cliente
                {
                    Nombre = "María",
                    Apellido = "García",
                    Email = "maria.garcia@test.com",
                    Telefono = "987654321",
                    Password = "password456",
                    FechaNacimiento = new DateTime(1985, 5, 15)
                }
            };
            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            // Crear propiedades de prueba
            var propiedades = new List<Propiedad>
            {
                new Propiedad
                {
                    Titulo = "Casa en la playa",
                    Descripcion = "Hermosa casa frente al mar",
                    Direccion = "Av. Costanera 123",
                    Localidad = "Mar del Plata",
                    PrecioPorNoche = 100.00m,
                    Capacidad = 6,
                    ServiciosDisponibles = new List<ServiciosPropiedad> 
                    { 
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.AireAcondicionado
                    }
                },
                new Propiedad
                {
                    Titulo = "Cabaña en la montaña",
                    Descripcion = "Acogedora cabaña con vista a la montaña",
                    Direccion = "Cerro Catedral 456",
                    Localidad = "Bariloche",
                    PrecioPorNoche = 80.00m,
                    Capacidad = 4,
                    ServiciosDisponibles = new List<ServiciosPropiedad>
                    {
                        ServiciosPropiedad.Calefaccion,
                        ServiciosPropiedad.Estacionamiento
                    }
                }
            };

            context.Propiedades.AddRange(propiedades);
            context.SaveChanges();

            // Crear reservas
            var reservas = new List<Reserva>
            {
                new Reserva
                {
                    ClienteId = usuarios[0].Id,
                    PropiedadId = propiedades[0].Id,
                    FechaInicio = DateTime.Now.AddDays(10),
                    FechaFin = DateTime.Now.AddDays(15),
                    PrecioTotal = 500.00m,
                },
                new Reserva
                {
                    ClienteId = usuarios[1].Id,
                    PropiedadId = propiedades[1].Id,
                    FechaInicio = DateTime.Now.AddDays(20),
                    FechaFin = DateTime.Now.AddDays(25),
                    PrecioTotal = 400.00m,
                }
            };
            context.Reservas.AddRange(reservas);
            context.SaveChanges();
        }
    }
}
