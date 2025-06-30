using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core.Interfaces
{
    public interface IPromocionRepositorio
    {
        List<Promocion> ObtenerTodas();
        List<Promocion> ObtenerTodasActivas();
        void Guardar(Promocion promocion);
        void Actualizar(int id, string titulo, string descripcion, DateTime fechaInicio, DateTime fechaFin, decimal porcentajeDescuento);
        void Eliminar(int id);
        Promocion? ObtenerPorId(int id);
    }
}
