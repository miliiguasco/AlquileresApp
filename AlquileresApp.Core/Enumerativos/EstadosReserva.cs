   namespace AlquileresApp.Core.Enumerativos;
   
   public EstadoReserva Estado { get; set; }
    
    public enum EstadoReserva
    {
        Pendiente,
        Confirmada,
        Cancelada,
        EnCurso,
        Finalizada
    }