namespace AlquileresApp.Core.Entidades
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int PropiedadId { get; set; }
        public int UsuarioId { get; set; }
        public int Puntuacion { get; set; } // Valor entre 1 y 5
        public DateTime FechaCalificacion { get; set; } = DateTime.Now;
        // Relación con Propiedad
        public Propiedad Propiedad { get; set; } = null!;
        
        // Relación con Usuario
        public Usuario Usuario { get; set; } = null!;
    }
}