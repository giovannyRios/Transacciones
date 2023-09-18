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
    public class ClienteRepository : IclienteRepository
    {
        private readonly TransaccionesContext _context;
        public ClienteRepository(TransaccionesContext transaccionesContext)
        {
            _context = transaccionesContext;
        }
        public async Task<bool> ActualizarCliente(Cliente cliente)
        {

            Cliente FindCliente = await _context.Clientes.Where(client => client.ClienteId == cliente.ClienteId).FirstOrDefaultAsync();
            bool resultado = false;
            if (FindCliente != null)
            {
                FindCliente.Contrasena = cliente.Contrasena;
                FindCliente.Estado = cliente.Estado;
                FindCliente.PersonaId = cliente.PersonaId;
                resultado = await _context.SaveChangesAsync() > 0;
            }

            return resultado;

        }

        public async Task<bool> AdicionarCliente(Context.Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarCliente(Context.Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarClientesPorPersonaId(int personaId)
        {
            int resultado = 0;
            List<Cliente> listaEliminar = new List<Cliente>();
            listaEliminar = await _context.Clientes.Where(cliente => cliente.PersonaId == personaId).ToListAsync();
            if (listaEliminar.Count > 0)
            {
                foreach (var item in listaEliminar)
                {
                    _context.Clientes.Remove(item);
                    resultado += await _context.SaveChangesAsync();
                }
            }
            return resultado > 0;


        }

        public async Task<Cliente> ObtenerClientePorClienteId(string ClienteId)
        {
            return await _context.Clientes.Where(cliente => cliente.ClienteId == ClienteId).FirstOrDefaultAsync();
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _context.Clientes.OrderByDescending(p => p.Id).ToListAsync();
        }

        public async Task<List<Cliente>> ObtenerClientesPorPersonaId(int personaId)
        {

            List<Cliente> clientes = new List<Cliente>();
            clientes = await _context.Clientes.Where(cliente => cliente.PersonaId == personaId).ToListAsync();

            if (clientes != null && clientes.Count > 0)
            {
                return clientes;
            }
            else
            {
                return null;
            }
        }
    }
}
