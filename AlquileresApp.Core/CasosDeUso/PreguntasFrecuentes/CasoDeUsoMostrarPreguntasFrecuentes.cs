namespace AlquileresApp.Core.CasosDeUso.PreguntasFrecuentes;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;

public class CasoDeUsoMostrarPreguntasFrecuentes(IPreguntasFrecuentesRepositorio preguntasFrecuentesRepositorio)
{
    public List<PreguntaFrecuente> Ejecutar()
    {
        return preguntasFrecuentesRepositorio.MostrarPreguntasFrecuentes();
    }
}
