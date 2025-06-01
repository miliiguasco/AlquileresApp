namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface ITarjetaRepositorio
{
    void RegistrarTarjeta(Tarjeta tarjeta);
    void ModificarTarjeta(Tarjeta tarjeta);
    Tarjeta? ObtenerTarjetaPorId(int id);
    bool Pagar(Tarjeta tarjeta, decimal monto);
    bool ValidarSaldo(Tarjeta tarjeta, decimal monto);
    void EliminarTarjeta(Tarjeta tarjeta);
    List<Tarjeta> ObtenerTarjetasPorUsuario(int usuarioId);
    void PagarMontoRestante(Tarjeta tarjeta, decimal monto);
    void Reembolsar(Tarjeta tarjeta, decimal monto);
    Tarjeta ObtenerPorClienteId(int clienteId);
}

