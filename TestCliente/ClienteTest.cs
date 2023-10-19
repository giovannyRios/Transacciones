using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Transacciones.Controllers;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Decorators.ClienteDecorator;
using Transacciones.Dominio.Repository.Implements;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.MappingEntities;
using Transacciones.Negocio.Services.Implements;
using Transacciones.Negocio.Services.Interfaces;

namespace TestCliente
{
    public class ClienteTest
    {
        private readonly ClientesController _clientesController;
        private readonly IClienteService _clienteService;
        private readonly IGeneroService _generoService;
        private readonly IclienteRepository _clienteRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly TransaccionesContext _context;
        private readonly MapperConfig _mapperConfig;
        private readonly IclienteDecorator _clienteDecorator;
        public ClienteTest()
        {
            _context = new TransaccionesContext();
            _mapperConfig = new MapperConfig();
            _clienteDecorator = new ClienteDecorator(_clienteRepository, _memoryCache);
            _generoRepository = new GeneroRepository(_context);
            _clienteService = new ClienteService(_clienteRepository, _mapperConfig.getMappper(), _clienteDecorator);
            _generoService = new GenerosServices(_generoRepository, _mapperConfig.getMappper());
            _clientesController = new ClientesController(_clienteService, _generoService);
        }

        public async Task insertarUsuario()
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Edad = 30;
            clienteDTO.Direccion = "Calle falsa 123";
            clienteDTO.FechaCreacion = DateTime.Now;
            clienteDTO.Identificacion = "1234567";
            clienteDTO.Contrasena = "Prueba123***";
            clienteDTO.Estado = true;
            clienteDTO.FechaActualizacion = DateTime.Now;
            clienteDTO.GeneroId = 1; //default en el sistema son 1(masculino),2(Femenino),3(otro)
            clienteDTO.Nombre = "Test 1";
            clienteDTO.Telefono = "3228749734";

            var result = await _clientesController.AdicionarCliente(clienteDTO);
        }

        public async Task BorrarUsuario()
        {
            var result = await _clientesController.EliminarCliente("1234567");
        }

        [Fact]
        public async Task TestGetAllOkObjectResult()
        {
            var result = await _clientesController.ObtenerClientes();
            Assert.IsType<OkObjectResult>(result);
        }


        //hecho para evaluar la data, se requiere que existan Clientes creados para que esta hecho pase completamente ok
        [Fact]
        public async Task TestGetAllOkData()
        {
            var result = await _clientesController.ObtenerClientes();
            if (result is OkObjectResult resultado)
            {
                var clientes = resultado.Value;
                var lista = Assert.IsType<List<ClienteDTO>>(clientes);
                Assert.True(lista.Count > 0);
            }
            else
            {
                Assert.True(false, "No llego un OkObjectResult, esto quiere decir que hay un posible error en el controlador de Clientes");
            }

        }

        [Fact]
        public async Task TestInsertClientOk()
        {
            await BorrarUsuario();
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Edad = 30;
            clienteDTO.Direccion = "Calle falsa 123";
            clienteDTO.FechaCreacion = DateTime.Now;
            clienteDTO.Identificacion = "1234567";
            clienteDTO.Contrasena = "Prueba123***";
            clienteDTO.Estado = true;
            clienteDTO.FechaActualizacion = DateTime.Now;
            clienteDTO.GeneroId = 1; //default en el sistema son 1(masculino),2(Femenino),3(otro)
            clienteDTO.Nombre = "Test 1";
            clienteDTO.Telefono = "3228749734";

            var result = await _clientesController.AdicionarCliente(clienteDTO);
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task TestUpdateClientOk()
        {
            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Edad = 30;
            clienteDTO.Direccion = "Calle falsa 12345";
            clienteDTO.FechaCreacion = DateTime.Now;
            clienteDTO.Identificacion = "1234567";
            clienteDTO.Contrasena = "Prueba123*";
            clienteDTO.Estado = true;
            clienteDTO.FechaActualizacion = DateTime.Now;
            clienteDTO.GeneroId = 1; //default en el sistema son 1(masculino),2(Femenino),3(otro)
            clienteDTO.Nombre = "Test 34";
            clienteDTO.Telefono = "3228749734";

            var clienteDTOUpdate = new ClienteDTO();
            var resultCliente = await _clientesController.ObtenerClientePorIdentificacion("1234567");
            if (resultCliente is OkObjectResult resultado)
            {
                clienteDTOUpdate = (ClienteDTO)resultado.Value;
            }
            if (string.IsNullOrEmpty(clienteDTOUpdate.Identificacion))
            {
                await insertarUsuario();
                var result = await _clientesController.ActualizarCliente(clienteDTO);
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                clienteDTOUpdate.Direccion = "Otra calle falsa 123";
                var result = await _clientesController.ActualizarCliente(clienteDTOUpdate);
                Assert.IsType<OkObjectResult>(result);
            }
        }



        [Fact]
        public async Task TestDeleteClientOkByIdentificationNumber()
        {
            var clienteDTOUpdate = new ClienteDTO();
            var resultCliente = await _clientesController.ObtenerClientePorIdentificacion("1234567");
            if (resultCliente is OkObjectResult resultado)
            {
                clienteDTOUpdate = (ClienteDTO)resultado.Value;
            }
            if (string.IsNullOrEmpty(clienteDTOUpdate.Identificacion))
            {
                await insertarUsuario();
                var result = await _clientesController.EliminarCliente("1234567");
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                var result = await _clientesController.EliminarCliente("1234567");
                Assert.IsType<OkObjectResult>(result);
            }

        }

        [Fact]
        public async Task TestDeleteNoExistClientByIdentification()
        {
            await BorrarUsuario();
            var result = await _clientesController.EliminarCliente("1234567");
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestUpdateClientNotOk()
        {
            await BorrarUsuario();

            ClienteDTO clienteDTO = new ClienteDTO();
            clienteDTO.Edad = 30;
            clienteDTO.Direccion = "Calle falsa 12345";
            clienteDTO.FechaCreacion = DateTime.Now;
            clienteDTO.Identificacion = "1234567";
            clienteDTO.Contrasena = "Prueba123*";
            clienteDTO.Estado = true;
            clienteDTO.FechaActualizacion = DateTime.Now;
            clienteDTO.GeneroId = 1; //default en el sistema son 1(masculino),2(Femenino),3(otro)
            clienteDTO.Nombre = "Test 34";
            clienteDTO.Telefono = "3228749734";

            var result = await _clientesController.ActualizarCliente(clienteDTO);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}