using AlquileresApp.Core.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AlquileresApp.Core.Interfaces;
using Microsoft.Extensions.Logging;
namespace AlquileresApp.Core.Servicios;

public class ServicioSesion(ServicioCookies servicioCookies, ILogger<ServicioSesion> logger) : IServicioSesion
{
    private readonly string cookiesKey = "token";
    private readonly string key = "AlquileresApp_SuperSecretKey_MinLength32Bytes!!";
    public async Task<string> Autenticar(Usuario usuario)
    {
        logger.LogInformation($"Generando token para usuario: {usuario.Email}");
        var token = GenerarToken(usuario);
        await servicioCookies.SetCookie(cookiesKey, token, 1);
        return token;
    }

    public async Task eliminarCookie()
    {
        await servicioCookies.DeleteCookie(cookiesKey);
    }

    public async Task<IEnumerable<Claim>?> VerificarUsuario()
    {
        var token = await servicioCookies.GetCookie(cookiesKey);
        if (token == null)
        {
            logger.LogWarning("No se encontró token en las cookies");
            return null;
        }
        return VerificarUsuario(token);
    }

    public IEnumerable<Claim>? VerificarUsuario(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,    
            ValidIssuer = "AlquileresApp",
            ValidAudience = "AlquileresApp",
            IssuerSigningKey = securityKey
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            if (jsonToken != null)
            {
                logger.LogInformation("Token validado correctamente");
                var claims = jsonToken.Claims.ToList();
                logger.LogInformation("Claims encontrados en el token:");
                foreach (var claim in claims)
                {
                    logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
                }
                return claims;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error al validar token: {ex.Message}");
        }
        return null;
    }
    
    private string GenerarToken(Usuario usuario)
    {
        logger.LogInformation($"Generando token para usuario con email: {usuario.Email}");
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())


        };

        logger.LogInformation("Claims generados:");
        foreach (var claim in claims)
        {
            logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
        }

        var token = new JwtSecurityToken(
            issuer: "AlquileresApp",
            audience: "AlquileresApp",
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        logger.LogInformation("Token generado exitosamente");
        return tokenString;
    }
}
