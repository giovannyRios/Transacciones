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
        public Task<bool> AdicionarCliente(Cliente Cliente);

        public Task<bool> EliminarCliente(Cliente Cliente);

        public Task<Cliente> ObtenerClientePorIdentificacion(string numeroIdentificacion);

        public Task<Cliente> ObtenerClientePorId(int Id);

        public Task<List<Cliente>> ObtenerClientes();

        public Task<bool> ActualizarCliente(Cliente Cliente);


    }
}
