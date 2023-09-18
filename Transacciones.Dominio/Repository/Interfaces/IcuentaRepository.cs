using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface IcuentaRepository
    {
        public Task<bool> AdicionarCuenta(Cuentum cuenta);

        public Task<bool> EliminarCuenta(Cuentum cuenta);

        public Task<Cuentum> ObtenerCuentaPorNumeroCuenta(string NumeroCuenta);

        public Task<List<Cuentum>> ObtenerCuentas();

        public Task<List<Cuentum>> ObtenerCuentasPorClienteId(int ClienteId);

        public Task<bool> EliminarCuentasPorClienteId(int ClienteId);

        public Task<bool> ActualizarCuenta(Cuentum cuenta);

    }
}
