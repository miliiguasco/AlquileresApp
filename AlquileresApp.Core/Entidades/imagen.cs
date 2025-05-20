namespace AlquileresApp.Core.Entidades
{
    public class Imagen
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int PropiedadId { get; set; }
        public Propiedad Propiedad { get; set; }
    }
}
