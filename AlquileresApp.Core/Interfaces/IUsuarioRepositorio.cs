namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IUsuarioRepositorio
{
    void RegistrarUsuario(Usuario usuario);
    void ModificarUsuario(Usuario usuario);
    Usuario? ObtenerUsuarioPorId(int id);
    Usuario? ObtenerUsuarioPorEmail(string email);
    List<Usuario> ListarUsuarios();
    List<Cliente> ListarClientes();
    List<Encargado> ListarEncargados();
    Usuario? AutenticarUsuario(string correo, String hashedContraseña);
    bool tieneTarjeta(Usuario usuario);
    void EliminarEncargado(int id);
}

