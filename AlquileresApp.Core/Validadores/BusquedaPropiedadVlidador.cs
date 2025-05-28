using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AlquileresApp.Core.Validadores
{

    // Validación: FechaFin > FechaInicio
    public class FechaMayorQueAtributo : ValidationAttribute
    {
        private readonly string _propiedadComparada;

        public FechaMayorQueAtributo(string propiedadComparada)
        {
            _propiedadComparada = propiedadComparada;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valorActual = value as DateTime?;
            var propiedad = validationContext.ObjectType.GetProperty(_propiedadComparada);

            if (propiedad == null)
                return new ValidationResult($"La propiedad {_propiedadComparada} no existe.");

            var valorComparado = propiedad.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (valorActual.HasValue && valorComparado.HasValue)
            {
                if (valorActual <= valorComparado)
                {
                    return new ValidationResult(ErrorMessage ?? "La fecha debe ser posterior.");
                }
            }

            return ValidationResult.Success;
        }
    }

    // Validación: FechaInicio >= hoy
    public class FechaNoPasadaAtributo : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime fecha)
            {
                if (fecha < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "La fecha no puede ser en el pasado.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
