namespace AlquileresApp.Core.Entidades;

public class Comentario
{
     public int Id { get; set; }
    public string Contenido { get; set; }
    public DateTime FechaCreacion { get; set; } 

    public bool Visible { get; set; } = true;

    public int PropiedadId { get; set; }
    public Propiedad Propiedad { get; set; } 
    public int? UsuarioId { get; set; } 
    public Usuario Usuario { get; set; } 
}