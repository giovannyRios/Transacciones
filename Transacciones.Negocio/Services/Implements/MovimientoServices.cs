using AutoMapper;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class MovimientoServices : IMovimientoServices
    {
        private readonly IMovimientosRepository _MovimientoRepository;
        private readonly IMapper _mapper;
        public MovimientoServices(IMovimientosRepository movimientosRepository, IMapper mapper)
        {
            _MovimientoRepository = movimientosRepository;
            _mapper = mapper;
        }
        public async Task<bool> ActualizarMovimiento(MovimientoDTO movimiento)
        {
            bool resultado = false;
            Movimiento FindMovimiento = await _MovimientoRepository.ObtenerMovimientosPorId(movimiento.Id);
            if (FindMovimiento != null)
            {
                resultado = await _MovimientoRepository.ActualizarMovimiento(_mapper.Map<Movimiento>(movimiento));
            }
            return resultado;
        }

        public async Task<bool> AdicionarMovimiento(MovimientoDTO movimiento)
        {
            bool resultado = false;

            Movimiento FindMovimiento = await _MovimientoRepository.ObtenerMovimientosPorId(movimiento.Id);
            if (FindMovimiento == null)
            {
                resultado = await _MovimientoRepository.AdicionarMovimiento(_mapper.Map<Movimiento>(movimiento));
            }

            return resultado;
        }

        public async Task<bool> EliminarMovimiento(MovimientoDTO movimiento)
        {
            bool resultado = false;

            Movimiento FindMovimiento = await _MovimientoRepository.ObtenerMovimientosPorId(movimiento.Id);
            if (FindMovimiento == null)
            {
                resultado = await _MovimientoRepository.AdicionarMovimiento(FindMovimiento);
            }

            return resultado;
        }

        public async Task<bool> EliminarMovimientosPorCuentaId(int CuentaId)
        {
            bool resultado = false;
            int conteoResultado = 0;
            List<Movimiento> movimientos = await _MovimientoRepository.ObtenerMovimientosPorCuentaId(CuentaId);
            if (movimientos != null && movimientos.Count > 0)
            {
                foreach (var movimiento in movimientos)
                {
                    resultado = await _MovimientoRepository.EliminarMovimiento(movimiento);
                    if (resultado)
                        conteoResultado++;
                }

            }

            return conteoResultado > 0;
        }

        public async Task<List<MovimientoDTO>> ObtenerMovimientos()
        {
            List<MovimientoDTO> MovimientoDTOs = new List<MovimientoDTO>();
            List<Movimiento> movimientos = await _MovimientoRepository.ObtenerMovimientos();
            if (movimientos != null && movimientos.Count > 0)
                MovimientoDTOs.AddRange(_mapper.Map<List<MovimientoDTO>>(movimientos));
            return MovimientoDTOs;
        }

        public async Task<List<MovimientoDTO>> ObtenerMovimientosPorCuentaId(int CuentaId)
        {
            List<MovimientoDTO> MovimientoDTOs = new List<MovimientoDTO>();
            List<Movimiento> movimientos = await _MovimientoRepository.ObtenerMovimientosPorCuentaId(CuentaId);
            if (movimientos != null && movimientos.Count > 0)
                MovimientoDTOs.AddRange(_mapper.Map<List<MovimientoDTO>>(movimientos));
            return MovimientoDTOs;
        }

        public async Task<MovimientoDTO> ObtenerMovimientosPorId(int Id)
        {
            MovimientoDTO movimientoDTO = null;
            Movimiento movimiento = await _MovimientoRepository.ObtenerMovimientosPorId(Id);
            if (movimiento != null && movimiento.CuentaId != null)
                movimientoDTO = _mapper.Map<MovimientoDTO>(movimiento);
            return movimientoDTO;
        }
    }
}
