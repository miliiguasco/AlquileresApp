namespace AlquileresApp.Core.CasosDeUso.PreguntasFrecuentes;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoCrearPreguntaFrecuente(IPreguntasFrecuentesRepositorio preguntasFrecuentesRepositorio)
{
    public PreguntaFrecuente Ejecutar(string pregunta, string respuesta)
    {
        return preguntasFrecuentesRepositorio.CrearPreguntaFrecuente(pregunta, respuesta);
    }
}
