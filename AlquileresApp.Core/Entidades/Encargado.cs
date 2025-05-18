namespace AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class Encargado : Usuario
{
    public string Zona { get; set; } = string.Empty;

    public void AgregarPropiedad(Propiedad propiedad)
    {
        if (propiedad == null)
            throw new ArgumentNullException(nameof(propiedad), "La propiedad no puede ser nula");

        if (string.IsNullOrEmpty(propiedad.Titulo))
            throw new ArgumentException("La propiedad debe tener un título", nameof(propiedad));

        if (Propiedades.Any(p => p.Titulo == propiedad.Titulo))
            throw new InvalidOperationException($"Ya existe una propiedad con el título '{propiedad.Titulo}'");

        Propiedades.Add(propiedad);
    }

    public void EliminarPropiedad(string titulo)
    {
        var propiedad = Propiedades.FirstOrDefault(p => p.Titulo == titulo);
        if (propiedad == null)
            throw new InvalidOperationException($"No se encontró una propiedad con el título '{titulo}'");

        Propiedades.Remove(propiedad);
    }

    public Propiedad? BuscarPropiedad(string titulo)
    {
        return Propiedades.FirstOrDefault(p => p.Titulo == titulo);
    }

    public List<Propiedad> ObtenerPropiedadesPorZona()
    {
        return Propiedades.Where(p => p.Direccion.Contains(Zona)).ToList();
    }
} 