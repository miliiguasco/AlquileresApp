namespace AlquileresApp.Core.CasosDeUso.Reserva;
using AlquileresApp.Core.Entidades;
using AlquileresApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


    public class CasoDeUsoListarMisReservas
    {
        private readonly IReservaRepositorio reservaRepository;

        public CasoDeUsoListarMisReservas(IReservaRepositorio reservaRepository)
        {
            this.reservaRepository = reservaRepository;
        }

        public  List<Reserva> Ejecutar(int usuarioId)
        {
            return  reservaRepository.ListarMisReservas(usuarioId);
        }
    }

