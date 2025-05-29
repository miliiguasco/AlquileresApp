
using AlquileresApp.Core.Entidades;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using AlquileresApp.Core.Enumerativos;

namespace AlquileresApp.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
        
            // Crear reservas
            var reservas = new List<Reserva>
            {
                new Reserva
                {
                    ClienteId = context.Usuarios.First().Id,
                    PropiedadId = context.Propiedades.First().Id,
                    FechaInicio = DateTime.Now.AddDays(10),
                    FechaFin = DateTime.Now.AddDays(15),
                    PrecioTotal = 500.00m,
                },
                new Reserva
                {
                    ClienteId = context.Usuarios.Skip(1).First().Id,
                    PropiedadId = context.Propiedades.Skip(1).First().Id,
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
