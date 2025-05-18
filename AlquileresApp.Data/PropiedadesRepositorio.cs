public class PropiedadesRepositorio(AppDbContext dbContext) : IPropiedadesRepositorio
{
    public void CargarPropiedad(Propiedad propiedad){

        verificarPropiedadDuplicada(propiedad.Titulo);
        dbContext.Propiedades.Add(propiedad);
        dbContext.SaveChanges();
    }

    public void eliminarPropiedad(Propiedad propiedad){
        
        var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
        if (propiedadExistente == null)
            throw new Exception("La propiedad no existe");

        var tieneReservaActiva = dbContext.Reservas.Any(r => r.PropiedadId == propiedad.Id && r.Estado == EstadoReserva.Activa);
        if (tieneReservaActiva)
            throw new Exception("No se puede eliminar una propiedad con reserva activa");

        dbContext.Propiedades.Remove(propiedadExistente);
        dbContext.SaveChanges();
    }

    public List<Propiedad> listarPropiedades(){
        var propiedades = dbContext.Propiedades.ToList();
        if (propiedades.Count == 0)
            throw new Exception("No se encontraron propiedades.");
        return propiedades;
    }

    public void modificarPropiedad(Propiedad propiedad){
        {
        var propiedadExistente = dbContext.Propiedades.FirstOrDefault(p => p.Id == propiedad.Id);
        if (propiedadExistente == null)
            throw new Exception("La propiedad no existe");

        if (propiedadExistente.Titulo != propiedad.Titulo)
            verificarPropiedadDuplicada(propiedad.Titulo);

        dbContext.Entry(propiedadExistente).CurrentValues.SetValues(propiedad);
        dbContext.SaveChanges();
        }

    private void verificarPropiedadDuplicada(string nombre){
        bool existe = dbContext.Propiedades.Any(p => p.Titulo == nombre);
        if (existe){
            throw new Exception("La propiedad ya existe");
        }   
    }
}
