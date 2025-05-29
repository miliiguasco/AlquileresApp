namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IPropiedadRepositorio{
    void CargarPropiedad(Propiedad propiedad);
    void ModificarPropiedad(Propiedad propiedad);
    void EliminarPropiedad(Propiedad propiedad);
    //void MarcarPropiedadComoNoHabitable(Propiedad propiedad);
    public List<Propiedad> ListarPropiedades();
    bool TieneReservaActiva(int propiedadId);
}


