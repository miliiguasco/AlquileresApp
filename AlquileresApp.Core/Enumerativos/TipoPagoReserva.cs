   namespace AlquileresApp.Core.Enumerativos;
   
   public TipoPagoReserva TipoPago { get; set; }
    
    public enum TipoPagoReserva
    {
        Total,
        Parcial,
        SinAnticipo
    }