
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
            // Verificar si ya existen datos
            if (context.Usuarios.Any())
            {
                return; // La base de datos ya está inicializada
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
                    Contraseña = "password123", // En producción, debe estar hasheada
                    FechaNacimiento = new DateTime(1990, 1, 1),
                    //Rol = RolUsuario.Cliente
                },
                new Cliente
                {
                    Nombre = "María",
                    Apellido = "García",
                    Email = "maria.garcia@test.com",
                    Telefono = "987654321",
                    Contraseña = "password456",
                    FechaNacimiento = new DateTime(1985, 5, 15),
                    //Rol = RolUsuario.Cliente
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
                    Descripcion = "Hermosa casa frente al mar con vista panorámica y acceso directo a la playa",
                    Direccion = "Av. Costanera 123",
                    Localidad = "Mar del Plata",
                    PrecioPorNoche = 150.00m,
                    Capacidad = 6,
                    ServiciosDisponibles = new List<ServiciosPropiedad> 
                    { 
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.AireAcondicionado,
                        ServiciosPropiedad.Piscina,
                        ServiciosPropiedad.Estacionamiento
                    }
                },
                new Propiedad
                {
                    Titulo = "Cabaña en la montaña",
                    Descripcion = "Acogedora cabaña con vista a la montaña y chimenea",
                    Direccion = "Cerro Catedral 456",
                    Localidad = "Bariloche",
                    PrecioPorNoche = 120.00m,
                    Capacidad = 4,
                    ServiciosDisponibles = new List<ServiciosPropiedad>
                    {
                        ServiciosPropiedad.Calefaccion,
                        ServiciosPropiedad.Estacionamiento,
                        ServiciosPropiedad.Wifi
                    }
                },
                new Propiedad
                {
                    Titulo = "Departamento céntrico",
                    Descripcion = "Moderno departamento en el centro de la ciudad",
                    Direccion = "Av. Corrientes 789",
                    Localidad = "Buenos Aires",
                    PrecioPorNoche = 80.00m,
                    Capacidad = 2,
                    ServiciosDisponibles = new List<ServiciosPropiedad>
                    {
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.AireAcondicionado,
                        ServiciosPropiedad.Estacionamiento
                    }
                },
                new Propiedad
                {
                    Titulo = "Casa de campo",
                    Descripcion = "Espaciosa casa de campo con jardín y parrilla",
                    Direccion = "Ruta 8 Km 45",
                    Localidad = "Pilar",
                    PrecioPorNoche = 200.00m,
                    Capacidad = 8,
                    ServiciosDisponibles = new List<ServiciosPropiedad>
                    {
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.Piscina,
                        ServiciosPropiedad.Estacionamiento,
                        ServiciosPropiedad.AireAcondicionado
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
                    //PrecioTotal = 750.00m, // 5 noches * 150
                },
                new Reserva
                {
                    ClienteId = usuarios[1].Id,
                    PropiedadId = propiedades[1].Id,
                    FechaInicio = DateTime.Now.AddDays(20),
                    FechaFin = DateTime.Now.AddDays(25),
                    //PrecioTotal = 600.00m, // 5 noches * 120
                },
                new Reserva
                {
                    ClienteId = usuarios[2].Id,
                    PropiedadId = propiedades[2].Id,
                    FechaInicio = DateTime.Now.AddDays(5),
                    FechaFin = DateTime.Now.AddDays(7),
                    //PrecioTotal = 160.00m, // 2 noches * 80
                },
                new Reserva
                {
                    ClienteId = usuarios[0].Id,
                    PropiedadId = propiedades[3].Id,
                    FechaInicio = DateTime.Now.AddDays(30),
                    FechaFin = DateTime.Now.AddDays(35),
                    //PrecioTotal = 1000.00m, // 5 noches * 200
                }
            };
            context.Reservas.AddRange(reservas);
            context.SaveChanges();
        }
    }
}
