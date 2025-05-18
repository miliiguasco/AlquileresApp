public class PropiedadValidador : IPropiedadValidador

{
    public void ValidarPropiedad(Propiedad propiedad){
        if (String.IsNullOrWhiteSpace(propiedad.Titulo))
        {
            throw new Exception("El título de la propiedad es requerido");
        }
    }
}