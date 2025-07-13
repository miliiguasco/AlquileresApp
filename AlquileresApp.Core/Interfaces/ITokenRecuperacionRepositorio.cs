using AlquileresApp.Core.Entidades;
namespace AlquileresApp.Core.Interfaces;

public interface ITokenRecuperacionRepositorio
{
    void Guardar(TokenRecuperacion token);
    TokenRecuperacion? ObtenerPorToken(string token);
    TokenRecuperacion? ObtenerPorEmailYToken(string email, string token);
    void Eliminar(TokenRecuperacion token);
}
