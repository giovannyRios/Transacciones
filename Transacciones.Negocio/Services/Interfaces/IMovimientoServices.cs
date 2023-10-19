using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface IMovimientoServices
    {
        public Task<bool> AdicionarMovimiento(MovimientoDTO movimiento);

        public Task<bool> EliminarMovimiento(MovimientoDTO movimiento);

        public Task<List<MovimientoDTO>> ObtenerMovimientosPorCuentaId(int CuentaId);

        public Task<MovimientoDTO> ObtenerMovimientosPorId(int Id);

        public Task<bool> EliminarMovimientosPorCuentaId(int CuentaId);
        public Task<List<MovimientoDTO>> ObtenerMovimientos();

        public Task<bool> ActualizarMovimiento(MovimientoDTO movimiento);
    }
}
