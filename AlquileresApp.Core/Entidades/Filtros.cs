using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AlquileresApp.Core.Validadores;
namespace AlquileresApp.Core.Entidades
{


public class SearchFilters
{
    [StringLength(100, ErrorMessage = "La localidad no puede tener más de 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\-']*$", ErrorMessage = "La localidad contiene caracteres inválidos.")]
    public string? Localidad { get; set; }

    [Range(1, 20, ErrorMessage = "La cantidad de huéspedes debe estar entre 1 y 20.")]
    public int? CantidadHuespedes { get; set; }

   [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
   [DataType(DataType.Date)]
    [FechaNoPasadaAtributo(ErrorMessage = "La fecha de inicio no puede ser anterior a hoy.")]
    public DateTime FechaInicio { get; set; }

    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    [DataType(DataType.Date)]
    [FechaMayorQueAtributo(nameof(FechaInicio), ErrorMessage = "La fecha de fin debe ser posterior a la fecha de inicio.")]
    public DateTime FechaFin { get; set; }
}
}