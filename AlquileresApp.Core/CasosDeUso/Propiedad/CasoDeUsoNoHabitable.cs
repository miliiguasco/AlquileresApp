namespace AlquileresApp.Core.CasosDeUso.Propiedad;

using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
public class CasoDeUsoMarcarPropiedadComoNoHabitable(IPropiedadRepositorio propiedadesRepositorio, ITarjetaRepositorio tarjetaRepositorio, IReservaRepositorio reservasRepositorio)
{
    public void Ejecutar(Propiedad propiedadOriginal)
    {
        // Obtener todas las reservas activas de la propiedad original
        List<Reserva> reservas = reservasRepositorio.ListarReservas()
            .Where(r => r.PropiedadId == propiedadOriginal.Id)
            .ToList();

        foreach (var reserva in reservas)
        {
            bool propiedadAlternativaEncontrada = false;
            List<Propiedad> propiedadesEnLocalidad = propiedadesRepositorio.ListarPropiedades()
                .Where(p => p.Localidad == propiedadOriginal.Localidad && p.Id != propiedadOriginal.Id && !p.NoHabitable)
                .ToList();

            foreach (Propiedad propiedadAlternativa in propiedadesEnLocalidad)
            {
                try
                {
                    propiedadesRepositorio.ComprobarDisponibilidad(propiedadAlternativa, reserva.FechaInicio, reserva.FechaFin);
                    // Si no lanza excepción, la propiedad alternativa está disponible
                    Console.WriteLine($"Propiedad disponible encontrada: {propiedadAlternativa.Titulo} (ID: {propiedadAlternativa.Id}) para la reserva {reserva.Id}");
                    reserva.Propiedad = propiedadAlternativa;
                    reserva.PropiedadId = propiedadAlternativa.Id;
                    reservasRepositorio.ModificarReserva2(reserva);
                    propiedadAlternativaEncontrada = true;
                    break; // Salir del bucle al encontrar una propiedad disponible
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Propiedad {propiedadAlternativa.Titulo} no disponible: {ex.Message}");
                    // Continuar buscando otras propiedades
                }
            }

            // Si no se encontró ninguna propiedad alternativa disponible, reembolsar al cliente
            if (!propiedadAlternativaEncontrada)
            {
                Console.WriteLine($"No se encontraron propiedades alternativas disponibles en la localidad: {propiedadOriginal.Localidad} para la reserva {reserva.Id}. Procediendo al reembolso.");
                Tarjeta? tarjeta = tarjetaRepositorio.ObtenerTarjetaPorId(reserva.Cliente.Id);
                if (tarjeta != null)
                {
                    tarjeta.Saldo += propiedadOriginal.MontoPagoAnticipado;
                    tarjetaRepositorio.ModificarTarjeta(tarjeta);
                    Console.WriteLine($"Reembolso de {propiedadOriginal.MontoPagoAnticipado} realizado a la tarjeta del cliente {reserva.Cliente.Id}.");
                }
                else
                {
                    Console.WriteLine($"No se encontró la tarjeta para el cliente {reserva.Cliente.Id}. No se pudo realizar el reembolso.");
                }
            }
        }

        // Marcar la propiedad original como no habitable (esto debería hacerse siempre después de intentar la recolocación o el reembolso)
        propiedadesRepositorio.MarcarPropiedadComoNoHabitable(propiedadOriginal);
        Console.WriteLine($"Propiedad original (ID: {propiedadOriginal.Id}, Título: {propiedadOriginal.Titulo}) marcada como no habitable.");
    }
}