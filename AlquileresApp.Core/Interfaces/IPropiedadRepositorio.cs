namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IPropiedadRepositorio{
    void CargarPropiedad(Propiedad propiedad);
    void ModificarPropiedad(Propiedad propiedad);
    void EliminarPropiedad(Propiedad propiedad);
    public List<Propiedad> ListarPropiedades();
    public List<Propiedad> BuscarDisponiblesAsync(SearchFilters filtros);
    public Propiedad? ObtenerPropiedadPorId(int id);
}


