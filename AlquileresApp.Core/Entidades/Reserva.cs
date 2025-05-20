namespace AlquileresApp.Core.Entidades
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int UsuarioRegistradoId { get; set; }
        public UsuarioRegistrado Usuario { get; set; }

        public int PropiedadId { get; set; }
        public Propiedad Propiedad { get; set; }

        public Tarjeta Tarjeta { get; set; }
    }
}
