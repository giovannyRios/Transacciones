using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Implements;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class ClienteService : IClienteService
    {
        private readonly IclienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteService(IclienteRepository iclienteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _clienteRepository = iclienteRepository;
        }
        public async Task<bool> ActualizarCliente(ClienteDTO cliente)
        {
            bool resultado = false;

            Cliente clienteDto = await _clienteRepository.ObtenerClientePorClienteId(cliente.ClienteId);
            if (clienteDto != null)
            {
                resultado = await _clienteRepository.ActualizarCliente(_mapper.Map<Cliente>(cliente));
            }

            return resultado;
        }

        public async Task<bool> AdicionarCliente(ClienteDTO cliente)
        {
            bool resultado = false;
            Cliente clienteDto = await _clienteRepository.ObtenerClientePorClienteId(cliente.ClienteId);
            if (clienteDto == null)
            {
                Cliente ClienteAdicional = _mapper.Map<Cliente>(cliente);
                ClienteAdicional.ClienteId = Guid.NewGuid().ToString();
                resultado = await _clienteRepository.AdicionarCliente(ClienteAdicional);
            }
            return resultado;
        }

        public async Task<bool> EliminarCliente(ClienteDTO cliente)
        {
            bool resultado = false;

            Cliente clienteDto = await _clienteRepository.ObtenerClientePorClienteId(cliente.ClienteId);
            if (clienteDto != null)
            {
                resultado = await _clienteRepository.EliminarCliente(clienteDto);
            }

            return resultado;
        }

        public async Task<bool> EliminarClientesPorPersonaId(int personaId)
        {
            int resultado = 0;
            bool validacion = false;

            List<Cliente> clientesDto = await _clienteRepository.ObtenerClientesPorPersonaId(personaId);
            if (clientesDto != null)
            {
                foreach (var cliente in clientesDto)
                {
                    validacion = await _clienteRepository.EliminarCliente(cliente);
                    if (validacion)
                    {
                        resultado++;
                    }
                }

            }

            return resultado > 0;
        }

        public async Task<ClienteDTO> ObtenerClientePorClienteId(string ClienteId)
        {
            Cliente clienteDto = await _clienteRepository.ObtenerClientePorClienteId(ClienteId);
            if (clienteDto != null)
                return _mapper.Map<ClienteDTO>(clienteDto);
            return null;


        }

        public async Task<List<ClienteDTO>> ObtenerClientes()
        {
            List<ClienteDTO> clienteDTOs = new List<ClienteDTO>();
            List<Cliente> clientes = await _clienteRepository.ObtenerClientes();
            if (clientes != null && clientes.Count > 0)
            {
                clienteDTOs.AddRange(_mapper.Map<List<ClienteDTO>>(clientes));
            }

            return clienteDTOs;
        }

        public async Task<List<ClienteDTO>> ObtenerClientesPorPersonaId(int personaId)
        {
            List<ClienteDTO> clienteDTOs = new List<ClienteDTO>();
            List<Cliente> clientes = await _clienteRepository.ObtenerClientesPorPersonaId(personaId);
            if (clientes != null && clientes.Count > 0)
            {
                clienteDTOs.AddRange(_mapper.Map<List<ClienteDTO>>(clientes));
            }

            return clienteDTOs;
        }
    }
}
