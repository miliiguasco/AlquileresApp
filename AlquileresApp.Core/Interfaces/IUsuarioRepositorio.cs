namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IUsuarioRepositorio
{
    void RegistrarUsuario(Usuario usuario);
    void ModificarUsuario(Usuario usuario);
    public Usuario? ObtenerUsuarioPorId(int id);
    public List<Usuario> ListarUsuarios();
}

