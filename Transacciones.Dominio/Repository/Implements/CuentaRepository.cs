using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<bool> ActualizarCuenta(Cuentum cuenta)
        {

            var FindCuenta = _context.Cuenta.Find(cuenta.Id);
            if (FindCuenta != null)
            {
                FindCuenta.Estado = cuenta.Estado;
                FindCuenta.Saldo = cuenta.Saldo;
            }

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> AdicionarCuenta(Cuentum cuenta)
        {
            _context.Cuenta.Add(cuenta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarCuenta(Cuentum cuenta)
        {

            _context.Cuenta.Remove(cuenta);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> EliminarCuentasPorClienteId(int ClienteId)
        {
            int resultado = 0;
            List<Cuentum> listaEliminar = new List<Cuentum>();
            listaEliminar = await _context.Cuenta.Where(cuenta => cuenta.ClienteId == ClienteId).ToListAsync();
            if (listaEliminar.Count > 0)
            {
                foreach (var item in listaEliminar)
                {
                    _context.Cuenta.Remove(item);
                    resultado += _context.SaveChanges();
                }
            }
            return resultado > 0;

        }

        public async Task<Cuentum> ObtenerCuentaPorNumeroCuenta(string NumeroCuenta)
        {
            return await _context.Cuenta.Where(cuenta => cuenta.NumeroCuenta == NumeroCuenta).FirstOrDefaultAsync();
        }

        public async Task<List<Cuentum>> ObtenerCuentas()
        {
            return await _context.Cuenta.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<Cuentum>> ObtenerCuentasPorClienteId(int ClienteId)
        {
    
                List<Cuentum> cuentas = new List<Cuentum>();
                cuentas = await _context.Cuenta.Where(cuenta => cuenta.ClienteId == ClienteId).ToListAsync();

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
