using Microsoft.EntityFrameworkCore;
using alquileresapp.core.Entidades;

namespace alquileresapp.data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRegistrado> UsuariosRegistrados { get; set; }
        public DbSet<UsuarioNoRegistrado> UsuariosNoRegistrados { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Encargado> Encargados { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Esto crea o usa la base de datos SQLite en el proyecto
            optionsBuilder.UseSqlite("Data Source=Alquilando.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Relación 1 UsuarioRegistrado - muchas Reservas
    modelBuilder.Entity<Reserva>()
        .HasOne(r => r.Usuario)
        .WithMany(u => u.Reservas)
        .HasForeignKey(r => r.UsuarioRegistradoId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación 1 Propiedad - muchas Imagenes
    modelBuilder.Entity<Imagen>()
        .HasOne(i => i.Propiedad)
        .WithMany(p => p.Imagenes)
        .HasForeignKey(i => i.PropiedadId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relación 1 Propiedad - 1 Encargado
    modelBuilder.Entity<Propiedad>()
        .HasOne(p => p.Encargado)
        .WithMany(e => e.Propiedades)
        .HasForeignKey(p => p.EncargadoId);

    // Relación 1 Reserva - 1 Tarjeta (Opcional)
    modelBuilder.Entity<Reserva>()
        .HasOne(r => r.Tarjeta)
        .WithOne(t => t.Reserva)
        .HasForeignKey<Tarjeta>(t => t.ReservaId);

    // Ejemplo: Configurar propiedad como requerida y con tamaño máximo
    modelBuilder.Entity<Persona>()
        .Property(p => p.Email)
        .IsRequired()
        .HasMaxLength(100);

    // Si querés mapear enums como string en la base (más legible)
    modelBuilder.Entity<Propiedad>()
        .Property(p => p.ServiciosDisponibles)
        .HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => Enum.Parse<alquileresapp.core.Enumerativos.Servicios>(s))
                  .ToList()
        );
        modelBuilder.Entity<UsuarioRegistrado>()
        .HasIndex(u => u.Email)
        .IsUnique();
}

    }
}
