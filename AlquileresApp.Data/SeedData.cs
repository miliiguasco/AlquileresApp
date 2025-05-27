/*
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
                    Descripcion = "Hermosa casa frente al mar",
                    Direccion = "Av. Costanera 123",
                    Localidad = "Mar del Plata",
                    PrecioPorNoche = 100.00m,
                    Capacidad = 6,
                    ServiciosDisponibles = new List<ServiciosPropiedad> 
                    { 
                        ServiciosPropiedad.Wifi,
                        ServiciosPropiedad.AireAcondicionado
                    },
                    TipoPago = TipoPagoReserva.Total,
                    PorcentajeAnticipo = 0,
                    MontoAPagar = 200m
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
                    },
                    TipoPago = TipoPagoReserva.Parcial,
                    PorcentajeAnticipo = 20,
                    MontoAPagar = 160m
                }
            };

            context.Propiedades.AddRange(propiedades);
            context.SaveChanges();

            // Crear reservas
            var reservas = new List<Reserva>
            {
                new Reserva(
                    cliente: usuarios[0],
                    propiedad: propiedades[0],
                    fechaInicio: DateTime.Now.AddDays(10),
                    fechaFin: DateTime.Now.AddDays(15),
                    precioTotal: 500.00m
                ),
                new Reserva(
                    cliente: usuarios[1],
                    propiedad: propiedades[1],
                    fechaInicio: DateTime.Now.AddDays(20),
                    fechaFin: DateTime.Now.AddDays(25),
                    precioTotal: 400.00m
                )
            };
            context.Reservas.AddRange(reservas);
            context.SaveChanges();
        }
    }
}
*/