namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IPropiedadRepositorio
{
    void CargarPropiedad(Propiedad propiedad);
    void ModificarPropiedad(Propiedad propiedad);
    bool EliminarPropiedad(Propiedad propiedad);
    void MarcarPropiedadComoNoHabitable(Propiedad propiedad);
    public List<Propiedad> ListarPropiedades();
    public Propiedad? ObtenerPropiedadPorId(int id);
    List<Propiedad> ListarPropiedadesFiltrado(SearchFilters filtros);
    public void ComprobarDisponibilidad(Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin);

    //public List<Propiedad> BuscarDisponiblesAsync(SearchFilters filtros);
    Propiedad? ObtenerPorId(int id);
    public bool ComprobarDisponibilidadModificacion(int propiedadId, DateTime fechaInicio, DateTime fechaFin, int reservaId);
    public decimal CalcularPrecioConPromocion(Propiedad propiedad, DateTime fecha, DateTime fechaInicio, DateTime fechaFin);
    public List<Propiedad> obtenerPropiedades();
    List<Propiedad> ObtenerDestacadas(); 
    
    void ActualizarCalificacionPromedio(int propiedadId, double nuevoPromedio); 
}



