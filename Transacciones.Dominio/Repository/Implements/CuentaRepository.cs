using Microsoft.EntityFrameworkCore;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;

namespace Transacciones.Dominio.Repository.Implements
{
    public class CuentaRepository : IcuentaRepository
    {
        private readonly TransaccionesContext _context;

        public CuentaRepository(TransaccionesContext transaccionesContext)
        {
            _context = transaccionesContext;
        }
        public async Task<bool> ActualizarCuenta(Cuenta cuenta)
        {

            var FindCuenta = _context.Cuentas.Find(cuenta.Id);
            if (FindCuenta != null)
            {
                FindCuenta.Estado = cuenta.Estado;
                FindCuenta.Saldo = cuenta.Saldo;
            }

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> AdicionarCuenta(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarCuenta(Cuenta cuenta)
        {

            _context.Cuentas.Remove(cuenta);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> EliminarCuentasPorClienteId(int ClienteId)
        {
            int resultado = 0;
            List<Cuenta> listaEliminar = new List<Cuenta>();
            listaEliminar = await _context.Cuentas.Where(cuenta => cuenta.ClienteId == ClienteId).ToListAsync();
            if (listaEliminar.Count > 0)
            {
                foreach (var item in listaEliminar)
                {
                    _context.Cuentas.Remove(item);
                    resultado += _context.SaveChanges();
                }
            }
            return resultado > 0;

        }

        public async Task<Cuenta> ObtenerCuentaPorNumeroCuenta(string NumeroCuenta)
        {
            return await _context.Cuentas.Where(cuenta => cuenta.NumeroCuenta == NumeroCuenta).FirstOrDefaultAsync();
        }

        public async Task<List<Cuenta>> ObtenerCuentas()
        {
            return await _context.Cuentas.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<Cuenta>> ObtenerCuentasPorClienteId(int ClienteId)
        {

            List<Cuenta> cuentas = new List<Cuenta>();
            cuentas = await _context.Cuentas.Where(cuenta => cuenta.ClienteId == ClienteId).ToListAsync();

            if (cuentas != null && cuentas.Count > 0)
            {
                return cuentas;
            }
            else
            {
                return null;
            }

        }
    }
}
