using Microsoft.EntityFrameworkCore;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;

namespace Transacciones.Dominio.Repository.Implements
{
    public class MovimientoRepository : IMovimientosRepository
    {
        private readonly TransaccionesContext _context;
        public MovimientoRepository(TransaccionesContext transaccionesContext)
        {
            _context = transaccionesContext;
        }
        public async Task<bool> ActualizarMovimiento(Context.Movimiento movimiento)
        {
            var FindMovimiento = _context.Movimientos.Find(movimiento.Id);
            if (FindMovimiento != null)
            {
                FindMovimiento.DescripcionMovimiento = movimiento.DescripcionMovimiento;
                FindMovimiento.FechaMovimiento = movimiento.FechaMovimiento;
                FindMovimiento.Saldo = movimiento.Saldo;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AdicionarMovimiento(Context.Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarMovimiento(Context.Movimiento movimiento)
        {
            _context.Movimientos.Remove(movimiento);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarMovimientosPorCuentaId(int CuentaId)
        {
            int resultado = 0;
            List<Movimiento> listaEliminar = new List<Movimiento>();
            listaEliminar = await _context.Movimientos.Where(movimiento => movimiento.CuentaId == CuentaId).ToListAsync();
            if (listaEliminar.Count > 0)
            {
                foreach (var item in listaEliminar)
                {
                    _context.Movimientos.Remove(item);
                    resultado += await _context.SaveChangesAsync();
                }
            }
            return resultado > 0;

        }

        public async Task<List<Movimiento>> ObtenerMovimientos()
        {
            return await _context.Movimientos.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<Movimiento>> ObtenerMovimientosPorCuentaId(int CuentaId)
        {
            List<Movimiento> movimientos = new List<Movimiento>();
            movimientos = await _context.Movimientos.Where(movimiento => movimiento.CuentaId == CuentaId).ToListAsync();

            if (movimientos != null && movimientos.Count > 0)
            {
                return movimientos;
            }
            else
            {
                return null;
            }
        }

        public async Task<Movimiento> ObtenerMovimientosPorId(int Id)
        {
            return await _context.Movimientos.Where(movimiento => movimiento.Id == Id).FirstOrDefaultAsync();
        }
    }
}
