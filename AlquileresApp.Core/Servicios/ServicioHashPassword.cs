using System.Security.Cryptography;
using System.Text;
using AlquileresApp.Core.Interfaces;

namespace AlquileresApp.Core.Servicios;

public class ServicioHashPassword : IServicioHashPassword
{
    public String HashPassword(String password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
