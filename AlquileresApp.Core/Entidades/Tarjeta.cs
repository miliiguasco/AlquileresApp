namespace AlquileresApp.Core.Entidades
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Cvv { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
    }
}
