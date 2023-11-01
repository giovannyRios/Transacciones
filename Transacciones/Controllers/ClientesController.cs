using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transacciones.Filters;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ValidateJWTFilter))]
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IGeneroService _generoService;
        private readonly ILogger<ClientesController> _logger;
        public ClientesController(IClienteService clienteService, IGeneroService genero,ILogger<ClientesController> logger)
        {
            _clienteService = clienteService;
            _generoService = genero;
            _logger = logger;
        }

        [HttpGet]
        [Route("ObtenerClientes")]
        public async Task<IActionResult> obtenerClientes()
        {
            try
            {
                List<ClienteDTO> consultaClientes = await _clienteService.obtenerClientes();
                List<ClienteDTO> resultadoClientes = new List<ClienteDTO>();
                foreach (ClienteDTO Cliente in consultaClientes)
                {
                    int Id = Cliente.GeneroId ?? 0;
                    var genero = await _generoService.ObtenerGeneroPorId(Cliente.GeneroId ?? 0);
                    Cliente.ValorGenero = genero != null ? genero.Valor : "Sin genero";
                    resultadoClientes.Add(Cliente);
                }
                return Ok(resultadoClientes);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al momento de consultar las Clientes en el metodo 'ObtenerClientes'");
                return Problem($"Ha ocurrido un error al momento de consultar las Clientes, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpPost]
        [Route("AdicionarCliente")]
        public async Task<IActionResult> AdicionarCliente(ClienteDTO ClienteDTO)
        {
            try
            {
                bool resultado = false;

                //valida el modelo y retorna el mensaje de error de los dataAnnotations de ClienteDTO
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"Modelo de datos invalido al adicionar el usuario {ClienteDTO.Identificacion}");
                    return BadRequest(ModelState.Values.SelectMany(error => error.Errors).Select(mensaje => mensaje.ErrorMessage).ToList());
                }

                ClienteDTO ClienteDTO2 = ClienteDTO.Clone() as ClienteDTO;
                resultado = await _clienteService.AdicionarCliente(ClienteDTO2);
                if (resultado)
                    return Ok("Cliente adicionada con éxito");
                return Problem("Ocurrio un problema al adicionar el usuario");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el metodo 'AdicionarCliente'");
                return Problem($"Ha ocurrido un error al momento de adicionar el usuario, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpPut]
        [Route("ActualizarCliente")]
        public async Task<IActionResult> ActualizarCliente(ClienteDTO ClienteDTO)
        {
            try
            {
                bool resultado = false;

                //valida el modelo y retorna el mensaje de error de los dataAnnotations de ClienteDTO
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"Modelo de datos invalido al actualizar el usuario {ClienteDTO.Identificacion}");
                    return BadRequest(ModelState.Values.SelectMany(error => error.Errors).Select(mensaje => mensaje.ErrorMessage).ToList());
                }

                ClienteDTO Cliente = await _clienteService.ObtenerClientePorNumeroIdentificacion(ClienteDTO.Identificacion);

                if (Cliente == null) 
                {
                    _logger.LogWarning($"La Cliente enviada con los siguientes datos: {ClienteDTO.ToString()} no existe en el sistema");
                    return BadRequest($"La Cliente enviada con los siguientes datos: {ClienteDTO.ToString()} no existe en el sistema");
                }

                resultado = await _clienteService.ActualizarCliente(ClienteDTO);

                return resultado ? Ok("Cliente actualizada con exito") : BadRequest("Error al actualizar el usuario,recuerde que si no actualiza valores no se realiza ninguna acción");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ha ocurrido un error al momento de adicionar el usuario, verifique");
                return Problem($"Ha ocurrido un error al momento de adicionar el usuario, verifique: {ex.InnerException.Message}");
            }
        }


        [HttpDelete]
        [Route("EliminarCliente")]
        public async Task<IActionResult> EliminarCliente(string numeroIdentificacion)
        {
            try
            {
                bool resultado = false;

                //valida el modelo y retorna el mensaje de error de los dataAnnotations de ClienteDTO
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"Modelo de datos invalido al eliminar el usuario {numeroIdentificacion}");
                    return BadRequest(ModelState.Values.SelectMany(error => error.Errors).Select(mensaje => mensaje.ErrorMessage).ToList()); return BadRequest(ModelState);
                }

                ClienteDTO Cliente = await _clienteService.ObtenerClientePorNumeroIdentificacion(numeroIdentificacion);

                if (Cliente == null) 
                {
                    _logger.LogWarning($"El numero de identificacion={numeroIdentificacion} enviado no tiene Cliente asociada en el sistema");
                    return BadRequest($"El numero de identificacion={numeroIdentificacion} enviado no tiene Cliente asociada en el sistema");
                }
                    

                resultado = await _clienteService.eliminarCliente(Cliente);

                return resultado ? Ok("Cliente eliminada con exito") : BadRequest("Error al eliminar el usuario, recuerde que el campo numero de identificacion es indispensable para la eliminación");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ha ocurrido un error al momento de eliminar el usuario, recuerde que si la Cliente tiene cuentas asociadas no puede eliminarlo, verifique");
                return Problem($"Ha ocurrido un error al momento de eliminar el usuario, recuerde que si la Cliente tiene cuentas asociadas no puede eliminarlo, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerClientePorIdentificacion")]
        public async Task<IActionResult> ObtenerClientePorIdentificacion(string Identificacion)
        {
            try
            {
                ClienteDTO consultaCliente = await _clienteService.ObtenerClientePorNumeroIdentificacion(Identificacion);
                if (consultaCliente != null && !string.IsNullOrEmpty(consultaCliente.Identificacion))
                {
                    var genero = await _generoService.ObtenerGeneroPorId((int)consultaCliente.GeneroId);
                    consultaCliente.ValorGenero = genero.Valor;
                    return Ok(consultaCliente);
                }
                else
                {
                    _logger.LogWarning($"Cliente con numero de identificacion {Identificacion} no encontrado");
                    return BadRequest("Cliente no encontrada");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ha ocurrido un error al momento de consultar la Cliente por Identificacion, verifique");
                return Problem($"Ha ocurrido un error al momento de consultar la Cliente por Identificacion, verifique: {ex.InnerException.Message}");
            }
        }
    }
}
