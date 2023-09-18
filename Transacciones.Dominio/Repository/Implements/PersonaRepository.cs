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
    public class PersonaRepository : IPersonaRepository
    {
        private readonly TransaccionesContext _context;

        public PersonaRepository(TransaccionesContext transaccionesContext)
        {
            _context = transaccionesContext;
        }
        public async Task<bool> ActualizarPersona(Persona persona)
        {
            Persona FindPersona = _context.Personas.Find(persona.Id);
            bool resultado = false;
            if (FindPersona != null)
            {
                FindPersona.Direccion = persona.Direccion;
                FindPersona.Edad = persona.Edad;
                FindPersona.Identificacion = persona.Identificacion;
                FindPersona.Telefono = persona.Telefono;
                FindPersona.GeneroId = persona.GeneroId;
                FindPersona.Nombre = persona.Nombre;
                resultado = await _context.SaveChangesAsync() > 0;
            }

            return resultado;
        }

        public async Task<bool> AdicionarPersona(Persona persona)
        {
            _context.Personas.Add(persona);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarPersona(Persona persona)
        {
            _context.Personas.Remove(persona);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Persona> ObtenerPersonaPorId(int Id)
        {
            Persona FindPersona = await _context.Personas.Where(persona => persona.Id == Id).FirstOrDefaultAsync();
            return FindPersona != null ? FindPersona : null;
        }

        public async Task<Persona> ObtenerPersonaPorIdentificacion(string numeroIdentificacion)
        {
            Persona FindPersona = await _context.Personas.Where(persona => persona.Identificacion == numeroIdentificacion).FirstOrDefaultAsync();
            return FindPersona != null ? FindPersona : null;
        }

        public async Task<List<Persona>> ObtenerPersonas()
        {
            return await _context.Personas.OrderByDescending(p => p.Id).ToListAsync();
        }
    }
}
