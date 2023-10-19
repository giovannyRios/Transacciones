using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface ICuentaService
    {
        public Task<bool> AdicionarCuenta(CuentaDTO cuenta);

        public Task<bool> EliminarCuenta(CuentaDTO cuenta);

        public Task<CuentaDTO> ObtenerCuentaPorNumeroCuenta(string NumeroCuenta);

        public Task<List<CuentaDTO>> ObtenerCuentas();

        public Task<List<CuentaDTO>> ObtenerCuentasPorClienteId(int ClienteId);

        public Task<bool> EliminarCuentasPorClienteId(int ClienteId);

        public Task<bool> ActualizarCuenta(CuentaDTO cuenta);
    }
}
