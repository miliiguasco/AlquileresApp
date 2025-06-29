namespace AlquileresApp.Core.CasosDeUso.PreguntasFrecuentes;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using System.Threading.Tasks;

public class CasoDeUsoEliminarPreguntaFrecuente(IPreguntasFrecuentesRepositorio preguntasFrecuentesRepositorio)
{
    public async Task Ejecutar(int preguntaFrecuenteId)
    {
        await preguntasFrecuentesRepositorio.EliminarPreguntaFrecuente(preguntaFrecuenteId);
    }
}
