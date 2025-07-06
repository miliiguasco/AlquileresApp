namespace AlquileresApp.Core.Entidades;
using System.ComponentModel.DataAnnotations;

public class Promocion
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Promocion), nameof(ValidarFechaInicio))]
    public DateTime FechaInicio { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Promocion), nameof(ValidarFechaFin))]
    public DateTime FechaFin { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Promocion), nameof(ValidarFechaInicio))]
    public DateTime FechaInicioReserva { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Promocion), nameof(ValidarFechaFin))]
    public DateTime FechaFinReserva { get; set; }

    [Range(1, 100, ErrorMessage = "El descuento debe ser mayor a 0 y menor o igual al 100%.")]
    public decimal PorcentajeDescuento { get; set; }

    public bool borrada { get; set; } = false;
    public List<Propiedad> Propiedades { get; set; } = new();
    
    public static ValidationResult? ValidarFechaInicio(DateTime fecha, ValidationContext context)
    {
        if (fecha < DateTime.Today)
            return new ValidationResult("La fecha de inicio no puede ser anterior a hoy.");
        return ValidationResult.Success;
    }

    public static ValidationResult? ValidarFechaFin(DateTime fechaFin, ValidationContext context)
    {
        var instance = context.ObjectInstance as Promocion;
        if (instance == null) return ValidationResult.Success;

        if (fechaFin < instance.FechaInicio)
            return new ValidationResult("La fecha de fin no puede ser anterior a la fecha de inicio.");

        return ValidationResult.Success;
    }
}
