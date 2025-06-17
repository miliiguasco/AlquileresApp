using AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Data;
using Microsoft.EntityFrameworkCore;

public class TarjetaRepositorio(AppDbContext dbContext) : ITarjetaRepositorio
{
    public void RegistrarTarjeta(Tarjeta tarjeta){
        dbContext.Tarjetas.Add(tarjeta);
        dbContext.SaveChanges();
    }

    public void ModificarTarjeta(Tarjeta tarjeta){
        dbContext.Tarjetas.Update(tarjeta);
        dbContext.SaveChanges();
    }
    
    public Tarjeta ObtenerTarjetaPorId(int id){
        var tarjeta = dbContext.Tarjetas.Find(id);
        return tarjeta;

    }

    public bool Pagar(Tarjeta tarjeta, decimal monto)
    {
        Console.WriteLine($"Verificando saldo: Saldo actual={tarjeta.Saldo}, Monto a pagar={monto}");
        // Validar fecha de vencimiento
        var fechaActual = DateTime.Now;
        var fechaVencimiento = DateTime.ParseExact(tarjeta.FechaVencimiento, "MM/yy", null);
        if (fechaVencimiento < fechaActual)
        {
            Console.WriteLine("La tarjeta está vencida");
            return false;
        }

        if (tarjeta.Saldo < monto)
        {
            Console.WriteLine($"Saldo insuficiente: Saldo actual={tarjeta.Saldo}, Monto requerido={monto}");
            return false;
        }

        tarjeta.Saldo -= monto;
        dbContext.SaveChanges();
        Console.WriteLine($"Pago realizado. Nuevo saldo: {tarjeta.Saldo}");
        return true;
    }

    public bool ValidarSaldo(Tarjeta tarjeta, decimal monto)
    {
        Console.WriteLine($"Validando saldo: Saldo actual={tarjeta.Saldo}, Monto requerido={monto}");
        
        if (tarjeta.Saldo < monto)
        {
            var mensaje = $"Saldo insuficiente. Saldo actual: {tarjeta.Saldo}, Monto requerido: {monto}";
            Console.WriteLine(mensaje);
            throw new Exception(mensaje);
        }
        
        Console.WriteLine("Saldo suficiente");
        return true;
    }
    public void Reembolsar(Tarjeta tarjeta, decimal monto)
{
    Console.WriteLine($"Reembolsando monto: {monto} a la tarjeta ID: {tarjeta.Id}");

    tarjeta.Saldo += monto;

    dbContext.Tarjetas.Update(tarjeta); // opcional, ya que está siendo rastreada por el contexto
    dbContext.SaveChanges();

    Console.WriteLine($"Reembolso realizado. Nuevo saldo: {tarjeta.Saldo}");
}
public Tarjeta ObtenerPorClienteId(int clienteId)
{
    var tarjeta = dbContext.Tarjetas.FirstOrDefault(t => t.ClienteId == clienteId);
    return tarjeta;
}


    public void EliminarTarjeta(Tarjeta tarjeta){
        dbContext.Tarjetas.Remove(tarjeta);
        dbContext.SaveChanges();
    }

    public void PagarMontoRestante(Tarjeta tarjeta, decimal monto){
        tarjeta.Saldo -= monto;
        dbContext.SaveChanges();
    }
}