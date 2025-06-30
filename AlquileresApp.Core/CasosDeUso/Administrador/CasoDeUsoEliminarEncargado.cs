namespace AlquileresApp.Core.CasosDeUso.Administrador;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoEliminarEncargado(IUsuarioRepositorio usuarioRepositorio)
{
    public void Ejecutar(int id) 
    {
        try 
        {
            usuarioRepositorio.EliminarEncargado(id); 
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    } 
}
