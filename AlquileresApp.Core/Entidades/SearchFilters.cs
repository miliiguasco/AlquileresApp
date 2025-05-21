namespace AlquileresApp.Core.Entidades
{
    public class SearchFilters
    {
        public string Localidad { get; set; } = string.Empty;
        public int? CantidadHabitaciones { get; set; }
        public int? CantidadHuespedes { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}