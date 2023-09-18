using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface IclienteRepository
    {
        public Task<bool> AdicionarCliente(Cliente cliente);

        public Task<bool> EliminarCliente(Cliente cliente);

        public Task<Cliente> ObtenerClientePorClienteId(string ClienteId);

        public Task<List<Cliente>> ObtenerClientes();

        public Task<List<Cliente>> ObtenerClientesPorPersonaId(int personaId);

        public Task<bool> EliminarClientesPorPersonaId(int personaId);

        public Task<bool> ActualizarCliente(Cliente cliente);


    }
}
