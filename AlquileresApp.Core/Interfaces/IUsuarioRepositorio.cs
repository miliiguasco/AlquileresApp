using System;

namespace AlquileresApp.Core.Interfaces;

public interface IUsuarioRepositorio
{
    public void RegistrarUsuario(Usuario usuario);
    public void ModificarUsuario(Usuario usuario);
    public Usuario ObtenerUsuarioPorId(int id);
    public List<Usuario> ListarUsuarios();
}

