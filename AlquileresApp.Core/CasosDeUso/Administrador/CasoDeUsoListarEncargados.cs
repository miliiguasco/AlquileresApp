namespace AlquileresApp.Core.CasosDeUso.Administrador;

using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
public class CasoDeUsoListarEncargados(IUsuarioRepositorio usuarioRepositorio)
{
    public List<Encargado> Ejecutar() 
    {
        try 
        {
            return usuarioRepositorio.ListarEncargados();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
}
