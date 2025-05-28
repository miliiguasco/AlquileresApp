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
        if (tarjeta == null)
            throw new Exception("La tarjeta no existe");
        return tarjeta;

    }

    public bool Pagar(Tarjeta tarjeta, decimal monto){
        if (tarjeta.Saldo < monto)
            return false;
        tarjeta.Saldo -= monto;
        dbContext.SaveChanges();
        return true;
    }

    public bool ValidarSaldo(Tarjeta tarjeta, decimal monto){
        if (tarjeta.Saldo < monto)
            throw new Exception("Saldo insuficiente");
        return true;
    }
}