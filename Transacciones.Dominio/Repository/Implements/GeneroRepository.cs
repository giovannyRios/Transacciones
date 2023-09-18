using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;

namespace Transacciones.Dominio.Repository.Implements
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly TransaccionesContext _context;

        public GeneroRepository(TransaccionesContext transaccionesContext)
        {
            _context = transaccionesContext;
        }
        public bool ActualizarGenero(Genero genero)
        {

            var FindGenero = _context.Generos.Find(genero.Id);
            if (FindGenero != null)
            {
                FindGenero.Valor = genero.Valor;
            }

            return _context.SaveChanges() > 0;

        }

        public bool AdicionarGenero(Genero genero)
        {
            _context.Generos.Add(genero);
            return _context.SaveChanges() > 0;
        }

        public async Task<Genero> ObtenerGeneroPorId(int Id)
        {
            return await _context.Generos.Where(genero => genero.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Genero> ObtenerGeneroPorValor(string valor)
        {
            return await _context.Generos.Where(genero => genero.Valor == valor).FirstOrDefaultAsync();
        }

        public async Task<List<Genero>> ObtenerGeneros()
        {
            return await _context.Generos.ToListAsync();
        }

        public bool RemoverGenero(Genero genero)
        { 
            _context.Generos.Remove(genero);
            return _context.SaveChanges() > 0;

        }
    }
}
