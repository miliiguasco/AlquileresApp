using AlquileresApp.Core.Enumerativos;
namespace AlquileresApp.Core.Entidades;

public class Reserva
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int PropiedadId { get; set; }
    public Cliente? Cliente { get; set; }
    public Propiedad? Propiedad { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public EstadoReserva Estado { get; set; }
    public TipoPago TipoPago { get; set; }
    public decimal PrecioTotal { get; set; }
    public decimal MontoAPagar { get; set; }
    public decimal MontoRestante { get; set; }
    public int CantidadHuespedes { get; set; }
    public DateTime? FechaCheckOut { get; set; }
    public int? EmpleadoQueRealizoCheckOutId { get; set; }

    public Reserva() { }

    public Reserva(Cliente cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin, int cantidadHuespedes, decimal montoTotal, decimal montoAPagar)
    {
        Cliente = cliente;
        ClienteId = cliente.Id;
        Propiedad = propiedad;
        PropiedadId = propiedad.Id;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Estado = EstadoReserva.Pendiente;
        PrecioTotal = montoTotal;
        TipoPago = propiedad.TipoPago;
        CantidadHuespedes = cantidadHuespedes;
        MontoAPagar = montoAPagar;
        //falta monto restante
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

