namespace AlquileresApp.Core.Interfaces;
using AlquileresApp.Core.Entidades;

public interface IPreguntasFrecuentesRepositorio{
    PreguntaFrecuente CrearPreguntaFrecuente(string pregunta, string respuesta);
    List<PreguntaFrecuente> MostrarPreguntasFrecuentes();
    Task EliminarPreguntaFrecuente(int preguntaFrecuenteId);
    Task ModificarPreguntaFrecuente(PreguntaFrecuente preguntaFrecuente);
} 
 