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

        // Constructor por defecto para pruebas y consola
        public AppDbContext() : base()
        {
        }

        // Constructor requerido para inyección de dependencias y AddDbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Encargado> Encargados { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Promocion> Promociones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Solo configurar si no está configurado desde afuera (evitar conflicto con AddDbContext)
            var path = DbPath;
            Console.WriteLine($"Configurando base de datos en: {path}");
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configuración TPH
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator(u => u.Rol)
                .HasValue<Cliente>(RolUsuario.Cliente)
                .HasValue<Administrador>(RolUsuario.Administrador)
                .HasValue<Encargado>(RolUsuario.Encargado);
modelBuilder.Entity<Promocion>()
    .HasMany(p => p.Propiedades)
    .WithMany(p => p.Promociones);
            
            // Email único
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Propiedad>()
                .Property(p => p.Localidad)
                .IsRequired();

            // Configuración de Propiedad
            modelBuilder.Entity<Propiedad>()
                .Property(p => p.TipoPago)
                .IsRequired();

            // Configuración de Reserva
            modelBuilder.Entity<Reserva>()
    .HasOne(r => r.Cliente)
    .WithMany(c => c.Reservas)
    .HasForeignKey(r => r.ClienteId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Propiedad)
                .WithMany(p => p.Reservas)
                .HasForeignKey(r => r.PropiedadId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuración de Tarjeta
            modelBuilder.Entity<Tarjeta>()
                .HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
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
