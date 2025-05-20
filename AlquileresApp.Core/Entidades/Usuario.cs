namespace AlquileresApp.Core.Entidades
{
    public abstract class Usuario : Persona
    {
        public string UsuarioNombre { get; set; }
        public string Contraseña { get; set; }
    }
}
