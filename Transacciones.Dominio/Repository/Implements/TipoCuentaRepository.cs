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
    public class TipoCuentaRepository : ITipoCuentaRepository
    {
        private readonly TransaccionesContext _context;

        public TipoCuentaRepository(TransaccionesContext context)
        {
            _context = context;
        }
        public bool ActualizarTipoCuenta(TipoCuentum tipoCuenta)
        {
            var FindTipoCuenta = _context.TipoCuenta.Find(tipoCuenta.Id);
            if (FindTipoCuenta != null)
            {
                FindTipoCuenta.Valor = tipoCuenta.Valor;
                FindTipoCuenta.Estado = tipoCuenta.Estado;
            }

            return _context.SaveChanges() > 0;

        }

        public bool AdicionarTipoCuenta(TipoCuentum tipoCuenta)
        {
            _context.TipoCuenta.Add(tipoCuenta);
            return _context.SaveChanges() > 0;
        }

        public async Task<TipoCuentum> ObtenerTipoCuentaPorId(int Id)
        {
            return await _context.TipoCuenta.Where(tipoCuenta => tipoCuenta.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<TipoCuentum>> ObtenerTipoCuentas()
        {
            return await _context.TipoCuenta.ToListAsync();
        }

        public async Task<TipoCuentum> ObteneTipoCuentaPorValor(string valor)
        {
            return await _context.TipoCuenta.Where(tipoCuenta => tipoCuenta.Valor == valor).FirstOrDefaultAsync();
        }

        public bool RemoverTipoCuenta(TipoCuentum tipoCuenta)
        {
            _context.TipoCuenta.Remove(tipoCuenta);
            return _context.SaveChanges() > 0;

        }
    }
}
