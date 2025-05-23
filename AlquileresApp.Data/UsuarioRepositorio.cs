using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Data;


public class UsuarioRepositorio(AppDbContext dbContext) : IUsuarioRepositorio
{
    public void RegistrarUsuario(Usuario usuario)
    {
        verificarCorreoExistente(usuario.Email);
        
        if (usuario is UsuarioRegistrado usuarioRegistrado)
            dbContext.UsuariosRegistrados.Add(usuarioRegistrado);
        else if (usuario is Administrador administrador)
            dbContext.Administradores.Add(administrador);
        else if (usuario is Encargado encargado)
            dbContext.Encargados.Add(encargado);

        dbContext.SaveChanges();
    }

    public void ModificarUsuario(Usuario usuario) 
    {
        // Implementación pendiente
    }

    public Usuario? ObtenerUsuarioPorId(int id)
    {
        Usuario? usuario = dbContext.UsuariosRegistrados.FirstOrDefault(u => u.Id == id);
        if (usuario != null) return usuario;

        usuario = dbContext.Administradores.FirstOrDefault(u => u.Id == id);
        if (usuario != null) return usuario;

        return dbContext.Encargados.FirstOrDefault(u => u.Id == id);
    }   

    public List<Usuario> ListarUsuarios()
    {
        var usuarios = new List<Usuario>();
        usuarios.AddRange(dbContext.UsuariosRegistrados.ToList());
        usuarios.AddRange(dbContext.Administradores.ToList());
        usuarios.AddRange(dbContext.Encargados.ToList());

        if (usuarios.Count == 0)
            throw new Exception("ListarUsuarios: no se encontraron usuarios.");
        return usuarios;
    }

    private void verificarCorreoExistente(String correo)
    {
        bool existe = dbContext.UsuariosRegistrados.Any(u => u.Email == correo) ||
                     dbContext.Administradores.Any(u => u.Email == correo) ||
                     dbContext.Encargados.Any(u => u.Email == correo);

        if (existe)
        {
            throw new Exception("El correo ya se encuentra registrado");
        }
    }
}
