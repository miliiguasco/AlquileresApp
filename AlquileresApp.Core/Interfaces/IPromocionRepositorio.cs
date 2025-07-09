using AlquileresApp.Core.Entidades;

namespace AlquileresApp.Core.Interfaces
{
    public interface IPromocionRepositorio
    {
        List<Promocion> ObtenerTodas();
        List<Promocion> ObtenerTodasActivas();
        void Guardar(Promocion promocion, List<int> propiedadesSeleccionadas);
        void Actualizar(int id, string titulo, string descripcion, DateTime fechaInicio, DateTime fechaFin, DateTime fechaInicioReserva, DateTime fechaFinReserva,  decimal porcentajeDescuento, List<int> propiedadesSeleccionadas);
        void Eliminar(int id);
        Promocion? ObtenerPorId(int id);
    }
}
