namespace AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


    public class CasoDeUsoListarReservasAdm
    {
        private readonly IReservaRepositorio reservaRepository;

        public CasoDeUsoListarReservasAdm(IReservaRepositorio reservaRepository)
        {
            this.reservaRepository = reservaRepository;
        }

        public  List<Reserva> Ejecutar()
        {
            return  reservaRepository.ListarReservas();
        }
    }

