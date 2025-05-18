using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.CasosDeUso.Usuario;
using AlquileresApp.Data;
using AlquileresApp.Core;
using AlquileresApp.Core.Servicios;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace TestRegistroUsuario;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine($"Directorio actual: {Directory.GetCurrentDirectory()}");

            // Crear instancias necesarias
            Console.WriteLine("\nCreando contexto de base de datos...");
            var dbContext = new AppDbContext();
            
            // Asegurar que la base de datos existe
            dbContext.EnsureDatabaseCreated();

            // Crear instancia de usuario registrado
            var usuario = new UsuarioRegistrado
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = $"juan.perez{DateTime.Now.Ticks}@test.com",
                Telefono = "1234567890",
                Password = "Password123!",
                FechaNacimiento = new DateTime(1990, 1, 1),
                FechaRegistro = DateTime.Now
            };

            var usuarioRepo = new UsuarioRepositorio(dbContext);
            var validador = new UsuarioValidador();
            var hashService = new ServicioHashPassword();

            // Crear caso de uso
            var registrarUsuario = new CasoDeUsoRegistrarUsuario(usuarioRepo, validador, hashService);

            // Ejecutar el registro
            Console.WriteLine("\nIntentando registrar usuario...");
            Console.WriteLine($"Email a registrar: {usuario.Email}");
            registrarUsuario.Ejecutar(usuario);
            Console.WriteLine("Usuario registrado exitosamente!");

            // Verificar que el usuario fue persistido usando SQL directo
            Console.WriteLine("\nConsultando la base de datos directamente:");
            
            // Mostrar todas las tablas
            var command = dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
            dbContext.Database.OpenConnection();
            
            Console.WriteLine("\nTablas en la base de datos:");
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    Console.WriteLine($"- {result.GetString(0)}");
                }
            }

            // Mostrar contenido de la tabla Usuario
            Console.WriteLine("\nContenido de la tabla Usuario:");
            command.CommandText = "SELECT * FROM Usuario;";
            using (var result = command.ExecuteReader())
            {
                // Mostrar nombres de columnas
                var columnNames = new List<string>();
                for (int i = 0; i < result.FieldCount; i++)
                {
                    columnNames.Add(result.GetName(i));
                }
                Console.WriteLine(string.Join(" | ", columnNames));
                Console.WriteLine(new string('-', columnNames.Count * 15));

                // Mostrar datos
                while (result.Read())
                {
                    var values = new List<object>();
                    for (int i = 0; i < result.FieldCount; i++)
                    {
                        values.Add(result.GetValue(i));
                    }
                    Console.WriteLine(string.Join(" | ", values));
                }
            }

            Console.WriteLine($"\nPuedes encontrar la base de datos en: {Path.GetFullPath("Alquilando.db")}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el proceso: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
}
