using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface IClienteService
    {
        public Task<bool> AdicionarCliente(ClienteDTO cliente);

        public Task<bool> EliminarCliente(ClienteDTO cliente);

        public Task<ClienteDTO> ObtenerClientePorClienteId(string ClienteId);

        public Task<List<ClienteDTO>> ObtenerClientes();

        public Task<List<ClienteDTO>> ObtenerClientesPorPersonaId(int personaId);

        public Task<bool> EliminarClientesPorPersonaId(int personaId);

        public Task<bool> ActualizarCliente(ClienteDTO cliente);

    }
}
