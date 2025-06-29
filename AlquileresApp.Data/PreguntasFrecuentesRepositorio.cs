using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using AlquileresApp.Data;
using System.IO;

public class PreguntaFrecuenteRepositorio(AppDbContext dbContext) : IPreguntasFrecuentesRepositorio
{
    public PreguntaFrecuente CrearPreguntaFrecuente(string pregunta, string respuesta)
    {
        var preguntaFrecuente = new PreguntaFrecuente { Pregunta = pregunta, Respuesta = respuesta };
        dbContext.PreguntasFrecuentes.Add(preguntaFrecuente);
        dbContext.SaveChanges();    
        return preguntaFrecuente;
    }

    public List<PreguntaFrecuente> MostrarPreguntasFrecuentes()
    {
        return dbContext.PreguntasFrecuentes.ToList();
    }

    public async Task EliminarPreguntaFrecuente(int preguntaFrecuenteId)
    {
        var preguntaFrecuente = await dbContext.PreguntasFrecuentes.FindAsync(preguntaFrecuenteId);
        if (preguntaFrecuente != null)
        {
            dbContext.PreguntasFrecuentes.Remove(preguntaFrecuente);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task ModificarPreguntaFrecuente(PreguntaFrecuente preguntaFrecuente)
    {
        dbContext.PreguntasFrecuentes.Update(preguntaFrecuente);
        await dbContext.SaveChangesAsync();
    }
}