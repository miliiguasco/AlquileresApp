namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface ICasoDeUsoVerReserva
{
    Reserva? Ejecutar(int reservaId, int usuarioId);
} 