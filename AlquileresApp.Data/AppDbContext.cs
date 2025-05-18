using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos; 
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AlquileresApp.Data
{
    public class AppDbContext : DbContext
    {
        private static string DbPath => 
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "AlquileresApp.Data", "Alquilando.db"));

        public DbSet<UsuarioRegistrado> UsuariosRegistrados { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Encargado> Encargados { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = DbPath;
            Console.WriteLine($"Configurando base de datos en: {path}");
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la herencia TPH (Table Per Hierarchy)
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<UsuarioRegistrado>("UsuarioRegistrado")
                .HasValue<Administrador>("Administrador")
                .HasValue<Encargado>("Encargado");

            // Email único para usuarios
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public void EnsureDatabaseCreated()
        {
            try
            {
                var path = DbPath;
                Console.WriteLine($"Intentando crear/verificar base de datos en: {path}");
                Console.WriteLine($"Directorio actual: {Directory.GetCurrentDirectory()}");
                
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Console.WriteLine($"Creando directorio: {directory}");
                    Directory.CreateDirectory(directory!);
                }

                var created = Database.EnsureCreated();
                Console.WriteLine($"Base de datos {(created ? "creada" : "ya existía")} en {path}");

                // Verificar las tablas creadas
                var tables = Database.SqlQuery<string>($"SELECT name FROM sqlite_master WHERE type='table';").ToList();
                Console.WriteLine("\nTablas creadas:");
                foreach (var table in tables)
                {
                    Console.WriteLine($"- {table}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear/verificar la base de datos: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
