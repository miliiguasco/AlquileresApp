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

            var hashService = new ServicioHashPassword();

            // Crear usuarios registrados solo si no existen
            if (!context.Usuarios.Any())
            {
                var usuariosSeed = new List<Usuario>
                {
                    new Administrador
                    {
                        Nombre = "Fran",
                        Apellido = "Admin",
                        Email = "admi@gmail.com",
                        Contraseña = hashService.HashPassword("password123"),
                    },
                    new Cliente
                    {
                        Nombre = "prueba",
                        Apellido = "test",
                        Email = "prueba@test.com",
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
                    },
                    new Encargado
                    {
                        Nombre = "Pablo",
                        Apellido = "Gomez",
                        Email = "pablogomez@test.com",
                        Contraseña = hashService.HashPassword("Encargado1"),
                    }
                };
                context.Usuarios.AddRange(usuariosSeed);
                context.SaveChanges();
            }

            // Obtener usuarios actualizados
            var usuarios = context.Usuarios.ToList();
            var encargado = context.Usuarios.OfType<Encargado>().FirstOrDefault();
            if (encargado == null)
            {
                // Si no hay encargado, no se puede continuar con el seed
                throw new Exception("No hay encargado registrado en la base de datos para asignar a las propiedades.");
            }


            // Crear propiedades de prueba solo si no existen
            if (!context.Propiedades.Any())
            {
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
                        PoliticaCancelacion = PoliticasDeCancelacion.PagoTotal_48hs_50,
                        TipoPago = TipoPago.Total,
                        Encargado = encargado
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
                        PoliticaCancelacion = PoliticasDeCancelacion.PagoTotal_48hs_50,
                        TipoPago = TipoPago.Total,
                        Encargado = encargado
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
                        PoliticaCancelacion = PoliticasDeCancelacion.Anticipo20_72hs,
                        TipoPago = TipoPago.Parcial,
                        Encargado = encargado
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
                        , TipoPago = TipoPago.SinAnticipo,
                        Encargado = encargado
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
                        TipoPago = TipoPago.Parcial,
                        Encargado = encargado
                    }

                };
                propiedades[0].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/casa1.jpg" });
                propiedades[0].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/pileta1.jpg" });

                propiedades[1].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/casa2.jpg" });
                propiedades[1].Imagenes.Add(new Imagen { Url = "/Imagenes/Propiedades/pileta2.jpg" }); 

                context.Propiedades.AddRange(propiedades);
                context.SaveChanges();
            }

             // Obtener propiedades actualizadas desde la base de datos
            var propiedadesDb = context.Propiedades.ToList();

            // Crear reservas de prueba
            if (!context.Reservas.Any())
            {
                var reservas = new List<Reserva>
                {
                    new Reserva
                    {
                        ClienteId = usuarios[1].Id,
                        PropiedadId = propiedadesDb[0].Id, // <-- Ahora sí existe
                        FechaInicio = DateTime.Now,
                        FechaFin = DateTime.Now.AddDays(15),
                        Estado = EstadoReserva.Activa,
                        PrecioTotal = 3750,
                        MontoAPagar = 3750,
                        MontoRestante = 0,
                        TipoPago = TipoPago.Total,
                        CantidadHuespedes = 4,
                    },
                    new Reserva
                    {
                        ClienteId = usuarios[2].Id, // María García
                        PropiedadId = propiedadesDb[2].Id, // Cabaña en la montaña
                        FechaInicio = DateTime.Now.AddDays(5),
                        FechaFin = DateTime.Now.AddDays(7),
                        Estado = EstadoReserva.Pendiente,
                        PrecioTotal = 240,
                        MontoAPagar = 240,
                        MontoRestante = 0,
                        TipoPago = TipoPago.Total,
                        CantidadHuespedes = 2
                    },
                    new Reserva
                    {
                        ClienteId = usuarios[1].Id, // Milagros Guasco
                        PropiedadId = propiedadesDb[3].Id, // Departamento céntrico
                        FechaInicio = DateTime.Now.AddDays(20),
                        FechaFin = DateTime.Now.AddDays(25),
                        Estado = EstadoReserva.Pendiente,
                        PrecioTotal = 400,
                        MontoAPagar = 400,
                        MontoRestante = 0,
                        TipoPago = TipoPago.Total,
                        CantidadHuespedes = 2
                    }
                };
                context.Reservas.AddRange(reservas);
                context.SaveChanges();
            }

            //Crear preguntas frecuentes de prueba
            if (!context.PreguntasFrecuentes.Any())
            {
                var preguntasFrecuentes = new List<PreguntaFrecuente>
                {
                    new PreguntaFrecuente { Pregunta = "¿Cómo puedo reservar una propiedad?", Respuesta = "Para reservar una propiedad, debes iniciar sesión y seleccionar la propiedad que deseas alquilar. Luego, completa los datos de contacto y confirma la reserva." },
                    new PreguntaFrecuente { Pregunta = "¿Cuáles son los métodos de pago disponibles?", Respuesta = "Disponemos de varios métodos de pago, como tarjeta de crédito, transferencia bancaria y pago en efectivo. Puedes seleccionar el método que prefieras al momento de realizar la reserva." },
                };
                context.PreguntasFrecuentes.AddRange(preguntasFrecuentes);
                context.SaveChanges();
            }
            
            //Tarjetas de prueba
            if (!context.Tarjetas.Any())
            {
                var tarjetas = new List<Tarjeta>
                {
                    new Tarjeta
                    {
                        NumeroTarjeta = "1234567890123456",
                        Titular = "Maria Garcia",
                        FechaVencimiento = "12/25",
                        CVV = "123",
                        Saldo = 99999999999999m,
                        ClienteId = usuarios[0].Id
                    }
                    ,
                     new Tarjeta
                    {
                        NumeroTarjeta = "9876543210987654",
                        Titular = "Milagros Guasco",
                        FechaVencimiento = "12/25",
                        CVV = "456",
                        Saldo = 5000.00m,
                        ClienteId = usuarios[1].Id
                     }
                };
                context.Tarjetas.AddRange(tarjetas);
                context.SaveChanges();
            }
        

        } 
    }
}
