// using AlquileresApp.Core.Entidades;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using AlquileresApp.Core.Enumerativos;

// namespace AlquileresApp.Data
// {
//     public static class SeedData
//     {
//         public static void Initialize(AppDbContext context)
//         {
//             if (context.Propiedades.Any())
//             {
//                 // La base de datos ya tiene datos, no hacemos nada.
//                 return;
//             }

//             // Crear usuarios registrados
//             var usuarios = new List<UsuarioRegistrado>
//             {
//                 new UsuarioRegistrado
//                 {
//                     Nombre = "Ana",
//                     Apellido = "Gonzalez",
//                     Email = "ana@example.com",
//                     Telefono = "123456789",
//                     Password = "password123", // En producción, debe estar hasheada
//                     FechaNacimiento = new DateTime(1990, 5, 21)
//                 },
//                 new UsuarioRegistrado
//                 {
//                     Nombre = "Luis",
//                     Apellido = "Martinez",
//                     Email = "luis@example.com",
//                     Telefono = "987654321",
//                     Password = "password456",
//                     FechaNacimiento = new DateTime(1985, 11, 30)
//                 }
//             };
//             context.UsuariosRegistrados.AddRange(usuarios);
//             context.SaveChanges();

//             // Crear propiedades de prueba
//             var propiedades = new List<Propiedad>
//             {
//                 new Propiedad
//                 {
//                     Titulo = "Casa de Playa",
//                     Descripcion = "Hermosa casa frente al mar",
//                     Direccion = "Calle Principal 123",
//                     Localidad = "Mar del Plata",
//                     PrecioPorNoche = 150.00m,
//                     Capacidad = 4,
//                     ServiciosDisponibles = new List<ServiciosPropiedad> 
//                     { 
//                         ServiciosPropiedad.Wifi,
//                         ServiciosPropiedad.AireAcondicionado,
//                         ServiciosPropiedad.Piscina
//                     }
//                 },
//                 new Propiedad
//                 {
//                     Titulo = "Departamento Centro",
//                     Descripcion = "Moderno departamento en el centro",
//                     Direccion = "Av. Central 456",
//                     Localidad = "Buenos Aires",
//                     PrecioPorNoche = 100.00m,
//                     Capacidad = 2,
//                     ServiciosDisponibles = new List<ServiciosPropiedad>
//                     {
//                         ServiciosPropiedad.Wifi,
//                         ServiciosPropiedad.Calefaccion,
//                         ServiciosPropiedad.Estacionamiento
//                     }
//                 }
//             };

//             context.Propiedades.AddRange(propiedades);
//             context.SaveChanges();

//             // Crear reservas
//             var reservas = new List<Reserva>
//             {
//                 new Reserva
//                 {
//                     UsuarioRegistradoId = usuarios[0].Id,
//                     PropiedadId = propiedades[0].Id,
//                     FechaInicio = DateTime.Today.AddDays(5),
//                     FechaFin = DateTime.Today.AddDays(10),
//                     PrecioTotal = 1500m * 5,
//                 },
//                 new Reserva
//                 {
//                     UsuarioRegistradoId = usuarios[1].Id,
//                     PropiedadId = propiedades[1].Id,
//                     FechaInicio = DateTime.Today.AddDays(3),
//                     FechaFin = DateTime.Today.AddDays(7),
//                     PrecioTotal = 2000m * 4,
//                 }
//             };
//             context.Reservas.AddRange(reservas);
//             context.SaveChanges();
//         }
//     }
// }
