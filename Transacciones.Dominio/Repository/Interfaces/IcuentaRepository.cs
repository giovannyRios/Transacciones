using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface IcuentaRepository
    {
        public Task<bool> AdicionarCuenta(Cuenta cuenta);

        public Task<bool> EliminarCuenta(Cuenta cuenta);

        public Task<Cuenta?> ObtenerCuentaPorNumeroCuenta(string NumeroCuenta);

        public Task<List<Cuenta>> ObtenerCuentas();

        public Task<List<Cuenta>> ObtenerCuentasPorClienteId(int ClienteId);

        public Task<bool> EliminarCuentasPorClienteId(int ClienteId);

        public Task<bool> ActualizarCuenta(Cuenta cuenta);

    }
}
