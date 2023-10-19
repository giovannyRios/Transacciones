using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> ActualizarCliente(Cliente Cliente)
        {
            Cliente FindCliente = await _context.Clientes.Where(p => p.Identificacion == Cliente.Identificacion).FirstOrDefaultAsync();
            bool resultado = false;
            if (FindCliente != null)
            {
                FindCliente.Direccion = Cliente.Direccion;
                FindCliente.Edad = Cliente.Edad;
                FindCliente.Identificacion = Cliente.Identificacion;
                FindCliente.Telefono = Cliente.Telefono;
                FindCliente.GeneroId = Cliente.GeneroId;
                FindCliente.Nombre = Cliente.Nombre;
                FindCliente.FechaActualizacion = Cliente.FechaActualizacion;
                FindCliente.Estado = FindCliente.Estado;
                resultado = await _context.SaveChangesAsync() > 0;
            }

            return resultado;
        }

        public async Task<bool> AdicionarCliente(Cliente Cliente)
        {
            _context.Clientes.Add(Cliente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarCliente(Cliente Cliente)
        {
            _context.Clientes.Remove(Cliente);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Cliente> ObtenerClientePorId(int Id)
        {
            Cliente FindCliente = await _context.Clientes.Where(Cliente => Cliente.Id == Id).FirstOrDefaultAsync();
            return FindCliente != null ? FindCliente : null;
        }

        public async Task<Cliente> ObtenerClientePorIdentificacion(string numeroIdentificacion)
        {
            Cliente FindCliente = await _context.Clientes.Where(Cliente => Cliente.Identificacion == numeroIdentificacion).FirstOrDefaultAsync();
            return FindCliente != null ? FindCliente : null;
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _context.Clientes.OrderByDescending(p => p.Id).ToListAsync();
        }
    }
}
