namespace AlquileresApp.Core.CasosDeUso.PreguntasFrecuentes;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using System.Threading.Tasks;

public class CasoDeUsoModificarPreguntaFrecuente(IPreguntasFrecuentesRepositorio preguntasFrecuentesRepositorio)
{
    public async Task Ejecutar(PreguntaFrecuente preguntaFrecuente)
    {
        await preguntasFrecuentesRepositorio.ModificarPreguntaFrecuente(preguntaFrecuente);
    }
}
