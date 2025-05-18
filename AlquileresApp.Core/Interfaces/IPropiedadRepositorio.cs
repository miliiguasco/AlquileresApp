namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IPropiedadRepositorio{
    void AgregarPropiedad(Propiedad propiedad);
    void ModificarPropiedad(Propiedad propiedad);
    void EliminarPropiedad(Propiedad propiedad);
    public List<Propiedad> ListarPropiedades();
}


