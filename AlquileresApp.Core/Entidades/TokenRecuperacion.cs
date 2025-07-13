using System;

namespace AlquileresApp.Core.Entidades;

// En Core > Entidades
public class TokenRecuperacion
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
}
