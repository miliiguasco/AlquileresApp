using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core;
public class PropiedadValidador : IPropiedadValidador

{

    public void validarPropiedad(Propiedad propiedad){
        if (String.IsNullOrWhiteSpace(propiedad.Titulo))
        {
            throw new Exception("El título de la propiedad es requerido");
        }

        if (String.IsNullOrWhiteSpace(propiedad.Direccion))
        {
            throw new Exception("La dirección de la propiedad es requerida");
        }

        if (propiedad.Latitud < -90 || propiedad.Latitud > 90)
        {
            throw new Exception("La latitud debe estar entre -90 y 90 grados");
        }

        if (propiedad.Longitud < -180 || propiedad.Longitud > 180)
        {
            throw new Exception("La longitud debe estar entre -180 y 180 grados");
        }
    }
}