using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
namespace AlquileresApp.Data;

public class TokenRecuperacionRepositorio : ITokenRecuperacionRepositorio 
{
    private readonly AppDbContext _context;

    public TokenRecuperacionRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void Guardar(TokenRecuperacion token)
    {
        _context.TokensRecuperacion.Add(token);
        _context.SaveChanges();
    }

    public TokenRecuperacion? ObtenerPorToken(string token)
    {
        return _context.TokensRecuperacion.FirstOrDefault(t => t.Token == token && t.FechaExpiracion > DateTime.UtcNow);
    }
    public TokenRecuperacion? ObtenerPorEmailYToken(string email, string token)
        {
            return _context.TokensRecuperacion
                .FirstOrDefault(t => t.Email == email && t.Token == token);
        }

    public void Eliminar(TokenRecuperacion token)
    {
        _context.TokensRecuperacion.Remove(token);
        _context.SaveChanges();
    }
}

