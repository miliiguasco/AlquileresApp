namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface ICasoDeUsoVerReserva
{
     Task<Reserva?> Ejecutar(int reservaId, int usuarioId);
} 