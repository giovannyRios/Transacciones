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
        public Task<bool> AdicionarCliente(ClienteDTO ClienteDTO);

        Task<bool> ActualizarCliente(ClienteDTO ClienteDTO);

        Task<ClienteDTO> ObtenerClientePorNumeroIdentificacion(string numeroIdentificacion);

        Task<ClienteDTO> ObtenerClientePorId(int Id);

        Task<List<ClienteDTO>>  obtenerClientes();

        Task<bool> eliminarCliente(ClienteDTO ClienteDTO);
    }
}
