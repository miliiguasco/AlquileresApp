using AlquileresApp.Core;
namespace AlquileresApp.Data;


public class UsuarioRepositorio(AppDbContext dbContext) : IUsuarioRepositorio
{
    public void RegistrarUsuario(Usuario usuario, String hashedPassword)
    {
        verificarCorreoExistente(usuario.Correo);
        usuario.Password = hashedPassword;
        dbContext.Usuarios.Add(usuario);
        dbContext.SaveChanges();
    }

    public void ModificarUsuario(Usuario usuario) 
    {

    }

    private void verificarCorreoExistente(String correo)
    {
        bool existe = dbContext.Usuarios.Any(u => u.Correo == correo);
        if (existe)
        {
            throw new Exception("El correo ya está en uso");
        }
    }
    
}
