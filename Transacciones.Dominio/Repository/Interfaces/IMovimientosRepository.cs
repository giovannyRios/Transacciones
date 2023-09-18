using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface IMovimientosRepository
    {
        public Task<bool> AdicionarMovimiento(Movimiento movimiento);

        public Task<bool> EliminarMovimiento(Movimiento movimiento);

        public Task<List<Movimiento>> ObtenerMovimientosPorCuentaId(int CuentaId);

        public Task<Movimiento> ObtenerMovimientosPorId(int Id);

        public Task<bool> EliminarMovimientosPorCuentaId(int CuentaId);
        public Task<List<Movimiento>> ObtenerMovimientos();

        public Task<bool> ActualizarMovimiento(Movimiento movimiento);
    }
}
