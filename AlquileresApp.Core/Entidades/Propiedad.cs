using AlquileresApp.Core.Enumerativos;



namespace AlquileresApp.Core.Entidades
{
    public class Propiedad
    {
        public int Id { get; set; }
         // Para búsqueda general
        public string Localidad { get; set; }
        public int Capacidad { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public List<ServiciosPropiedad> ServiciosDisponibles { get; set; } = new();
        public List<Imagen> Imagenes { get; set; } = new();

        public int EncargadoId { get; set; }
        public Encargado Encargado { get; set; }
    }
}
