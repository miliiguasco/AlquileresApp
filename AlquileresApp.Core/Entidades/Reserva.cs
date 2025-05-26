namespace AlquileresApp.Core.Entidades;

public class Reserva
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public Propiedad Propiedad { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public EstadoReserva Estado { get; set; }
    //public TipoPagoReserva TipoPago { get; set; }
    public decimal PrecioTotal { get; set; }
    public decimal? MontoPagoParcial { get; set; }
    //promociones?
    //guardar id_usuario ?
    //guardar id_propiedad ?
    //guardar id_tarjeta ?



        public Reserva(Cliente cliente, Propiedad propiedad, DateTime fechaInicio, DateTime fechaFin) {
            Cliente = cliente;
            Propiedad = propiedad;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Estado = EstadoReserva.Pendiente;
            PrecioTotal = propiedad.PrecioPorNoche * (fechaFin - fechaInicio).Days;
            //TipoPago = tipoPago;
            //MontoPagoParcial = montoPagoParcial;
        }











}

    //public int PropiedadId { get; set; }
    //public int ClienteId { get; set; } es necesario?
    //public Tarjeta? Tarjeta { get; set; } se llega a travez de cliente
/*
    public bool seSuperpone(DateTime inicio, DateTime fin) //???
    {
        if (inicio >= FechaInicio && fin <= FechaFin)
            return true;
        else if (inicio <= FechaInicio && fin >= FechaFin)
        {
            return true;
        }
        return false;
    }
} */

