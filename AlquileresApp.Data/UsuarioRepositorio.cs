using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Enumerativos;
using Microsoft.EntityFrameworkCore;

namespace AlquileresApp.Data;

public class UsuarioRepositorio(AppDbContext dbContext) : IUsuarioRepositorio
{
    public void RegistrarUsuario(Usuario usuario)
    {
        verificarCorreoExistente(usuario.Email);
        dbContext.Usuarios.Add(usuario);
        dbContext.SaveChanges();
    }

    public void ModificarUsuario(Usuario usuario) 
    {
        var usuarioExistente = dbContext.Usuarios.Find(usuario.Id);
        if (usuarioExistente == null)
            throw new Exception("Usuario no encontrado");
            
        dbContext.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
        dbContext.SaveChanges();
    }

    public Usuario? ObtenerUsuarioPorId(int id)
    {
        return dbContext.Usuarios.Find(id);
    }   

    public List<Usuario> ListarUsuarios()
    {
        var usuarios = dbContext.Usuarios.ToList();
        if (usuarios.Count == 0)
            throw new Exception("No se encontraron usuarios.");
        return usuarios;
    }

    public List<Cliente> ListarClientes()
    {
        return dbContext.Usuarios.OfType<Cliente>().ToList();
    }

    public List<Administrador> ListarAdministradores()
    {
        return dbContext.Usuarios.OfType<Administrador>().ToList();
    }

    public List<Encargado> ListarEncargados()
    {
        return dbContext.Usuarios.OfType<Encargado>().ToList();
    }

    public bool AutenticarUsuario(Usuario usuario, String hashContraseña)
    {
        var usuarioComparar = dbContext.Usuarios
            .SingleOrDefault(u => u.Email == usuario.Email && u.Password == hashContraseña);

        if (usuarioComparar != null)
        {
            return true;
        }
        throw new Exception("Correo o contraseña invalido.");
    }

    public Usuario? ObtenerUsuarioPorEmail(string email)
    {
        return dbContext.Usuarios
            .SingleOrDefault(u => u.Email == email);
    }

    private void verificarCorreoExistente(String correo)
    {
        bool existe = dbContext.Usuarios.Any(u => u.Email == correo);

        if (existe)
        {
            throw new Exception("El correo ya se encuentra registrado");
        }
    }
}
