using AlquileresApp.Core.Enumerativos;
namespace AlquileresApp.Core.Entidades;

public class Reserva
{
    public int Id { get; set; }
    public Cliente? Cliente { get; set; }
    public Propiedad? Propiedad { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public EstadoReserva Estado { get; set; }
    public TipoPagoReserva TipoPago { get; set; }
    public decimal PrecioTotal { get; set; }
    public decimal MontoAPagar { get; set; }
    //guardar id_usuario ?
    //guardar id_propiedad ?
    //guardar id_tarjeta ?

    public Reserva() { }

    public Reserva(Cliente cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin)
    {
        Cliente = cliente;
        Propiedad = propiedad;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Estado = EstadoReserva.Pendiente;
        PrecioTotal = propiedad.PrecioPorNoche * (decimal)(fechaFin - fechaInicio).TotalDays;
        TipoPago = propiedad.TipoPago;
        
        switch (TipoPago)
        {
            case TipoPagoReserva.Parcial:
                MontoAPagar = PrecioTotal * 0.2m;
                break;
            case TipoPagoReserva.Total:
                MontoAPagar = PrecioTotal;
                break;
            case TipoPagoReserva.SinAnticipo:
                MontoAPagar = 0;
                break;
            default:
                throw new Exception($"Tipo de pago no válido: {TipoPago}");
        }
    }

    public bool seSuperpone(DateTime inicio, DateTime fin)
    {
        // Caso 1: La nueva reserva empieza durante una existente
        if (inicio >= FechaInicio && inicio < FechaFin)
            return true;
            
        // Caso 2: La nueva reserva termina durante una existente
        if (fin > FechaInicio && fin <= FechaFin)
            return true;
            
        // Caso 3: La nueva reserva engloba completamente a una existente
        if (inicio <= FechaInicio && fin >= FechaFin)
            return true;

        return false;
    }
}


