using AlquileresApp.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using AlquileresApp.Core.Enumerativos;
using AlquileresApp.Core.Servicios;

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

            var hashService = new ServicioHashPassword();

            // Crear usuarios registrados
            var usuarios = new List<Usuario>
            {
                new Administrador
                {
                    Nombre = "Milagros",
                    Apellido = "Guasco",
                    Email = "milagrosguasco11@gmail.com",
                    Telefono = "123456789",
                    Contraseña = hashService.HashPassword("password123"),
                    FechaNacimiento = new DateTime(1990, 1, 1),
                },
                new Cliente
                {
                    Nombre = "María",
                    Apellido = "García",
                    Email = "maria.garcia@test.com",
                    Telefono = "987654321",
                    Contraseña = hashService.HashPassword("password456"),
                    FechaNacimiento = new DateTime(1985, 5, 15),
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
                    PrecioPorNoche = 750.00m,
                    Capacidad = 6,
                    ServiciosDisponibles = new List<ServiciosPropiedad> 
                    { 
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.AireAcondicionado,
                        ServiciosPropiedad.Piscina,
                        ServiciosPropiedad.Estacionamiento
                    },
                    PoliticaCancelacion = PoliticasDeCancelacion.PagoTotal_48hs_50
                },
                new Propiedad
                {
                    Titulo = "Casa en la playa 2",
                    Descripcion = "Hermosa casa frente al mar con vista panorámica y acceso directo a la playa",
                    Direccion = "Av. Costanera 123",
                    Localidad = "Mar del Plata",
                    PrecioPorNoche = 750.00m,
                    Capacidad = 6,
                    ServiciosDisponibles = new List<ServiciosPropiedad> 
                    { 
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.AireAcondicionado,
                        ServiciosPropiedad.Piscina,
                        ServiciosPropiedad.Estacionamiento
                    },
                    PoliticaCancelacion = PoliticasDeCancelacion.PagoTotal_48hs_50
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
                    },
                    PoliticaCancelacion = PoliticasDeCancelacion.Anticipo20_72hs
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
                    },
                    PoliticaCancelacion = PoliticasDeCancelacion.SinAnticipo_NoCancelable
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
                    },
                    PoliticaCancelacion = PoliticasDeCancelacion.Anticipo20_72hs,
                }

            };
            propiedades[0].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/casa1.jpg" });
            propiedades[0].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/pileta1.jpg" });

            propiedades[1].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/casa2.jpg" });
            propiedades[1].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/pileta2.jpg" });

             context.Propiedades.AddRange(propiedades);
            context.SaveChanges();



            // Crear reservas
            var reservas = new List<Reserva>
            {
                new Reserva
                {
                    ClienteId = context.Usuarios.First().Id,
                    PropiedadId = context.Propiedades.First().Id,
                    FechaInicio = DateTime.Now.AddDays(10),
                    FechaFin = DateTime.Now.AddDays(15),
                    PrecioTotal = 3750,
                    MontoAPagar = 3750, // 5 noches * 150
                },
                new Reserva
                {
                    ClienteId = usuarios[0].Id,
                    PropiedadId = propiedades[0].Id,
                    FechaInicio = DateTime.Now.AddDays(20),
                    FechaFin = DateTime.Now.AddDays(25),
                    PrecioTotal = 3750, 
                    MontoAPagar = 3750,
                },
                new Reserva
                {
                    ClienteId = usuarios[0].Id,
                    PropiedadId = propiedades[2].Id,
                    FechaInicio = DateTime.Now.AddDays(5),
                    FechaFin = DateTime.Now.AddDays(7),
                    PrecioTotal = 160,
                },
                new Reserva
                {
                    ClienteId = usuarios[0].Id,
                    PropiedadId = propiedades[2].Id,
                    FechaInicio = DateTime.Now.AddDays(2),
                    FechaFin = DateTime.Now.AddDays(35),
                    PrecioTotal = 1000,
                }
            };
            context.Reservas.AddRange(reservas);
            context.SaveChanges();

            //Tarjetas de prueba
            var tarjetas = new List<Tarjeta>
            {
                /*new Tarjeta
                {
                    NumeroTarjeta = "1234567890123456",
                    Titular = "Juan Pérez", 
                    FechaVencimiento = "12/2025",
                    CVV = "123",
                    Saldo = 4000.00m,
                    ClienteId = usuarios[0].Id
                },*/
                new Tarjeta
                {
                    NumeroTarjeta = "9876543210987654",
                    Titular = "María García",
                    FechaVencimiento = "12/2025",
                    CVV = "456",
                    Saldo = 500.00m,
                    ClienteId = usuarios[1].Id
                }
            };
            context.Tarjetas.AddRange(tarjetas);
            context.SaveChanges();
            
            
        }
    }
}
