using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core.Interfaces
{
    public interface IPromocionRepositorio
    {
        List<Promocion> ObtenerTodas();
        void Guardar(Promocion promocion);
        void Actualizar(Promocion promocion);
        void Eliminar(Guid id);
        Promocion? ObtenerPorId(Guid id);
    }
}
