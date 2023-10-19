using AutoMapper;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Decorators.ClienteDecorator;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class ClienteService : IClienteService
    {
        private readonly IclienteRepository _clienteRepository;
        private readonly IclienteDecorator _clienteDecorator;
        private readonly IMapper _mapper;

        public ClienteService(IclienteRepository ClienteRepository, IMapper mapper, IclienteDecorator clienteDecorator)
        {
            _clienteRepository = ClienteRepository;
            _mapper = mapper;
            _clienteDecorator = clienteDecorator;
        }
        public async Task<bool> ActualizarCliente(ClienteDTO clienteDTO)
        {
            bool resultado = false;

            Cliente Cliente = await _clienteRepository.ObtenerClientePorIdentificacion(clienteDTO.Identificacion);
            if (Cliente != null)
            {
                resultado = await _clienteRepository.ActualizarCliente(_mapper.Map<Cliente>(clienteDTO));
            }

            return resultado;

        }

        public async Task<bool> AdicionarCliente(ClienteDTO clienteDTO)
        {
            bool resultado = false;
            Cliente Cliente = await _clienteRepository.ObtenerClientePorIdentificacion(clienteDTO.Identificacion);
            if (Cliente == null)
            {
                Cliente ClienteAdicional = _mapper.Map<Cliente>(clienteDTO);
                resultado = await _clienteRepository.AdicionarCliente(ClienteAdicional);
            }
            return resultado;

        }

        public async Task<bool> eliminarCliente(ClienteDTO clienteDTO)
        {
            bool resultado = false;

            Cliente Cliente = await _clienteRepository.ObtenerClientePorIdentificacion(clienteDTO.Identificacion);
            if (Cliente != null)
            {
                resultado = await _clienteRepository.EliminarCliente(Cliente);
            }

            return resultado;

        }

        public async Task<ClienteDTO> ObtenerClientePorId(int Id)
        {
            Cliente Cliente = await _clienteDecorator.ObtenerClientePorId(Id);
            ClienteDTO clienteDTO = new ClienteDTO();
            if (Cliente != null)
            {
                clienteDTO = _mapper.Map<ClienteDTO>(Cliente);
            }

            return clienteDTO;
        }

        public async Task<ClienteDTO> ObtenerClientePorNumeroIdentificacion(string numeroIdentificacion)
        {
            Cliente cliente = await _clienteDecorator.ObtenerClientePorIdentificacion(numeroIdentificacion);
            ClienteDTO clienteDTO = new ClienteDTO();
            if (cliente != null)
            {
                clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            }

            return clienteDTO;
        }

        public async Task<List<ClienteDTO>> obtenerClientes()
        {

            List<Cliente> Clientes = await _clienteRepository.ObtenerClientes();
            List<ClienteDTO> clienteDTO = new List<ClienteDTO>();
            if (Clientes != null)
            {
                clienteDTO.AddRange(_mapper.Map<List<ClienteDTO>>(Clientes));
            }

            return clienteDTO;

        }
    }
}
