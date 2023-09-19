using Microsoft.AspNetCore.Mvc;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IGeneroService _generoService;
        public ClientesController(IClienteService clienteService, IGeneroService genero) 
        { 
            _clienteService = clienteService;
            _generoService = genero;
        }

        [HttpGet]
        [Route("ObtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            try 
            { 
                List<ClienteDTO> consulta = await _clienteService.obtenerClientes();
                List<ClienteDTO> resultado = new List<ClienteDTO>();
                foreach (ClienteDTO Cliente in consulta)
                {
                    int Id = Cliente.GeneroId ?? 0;
                    var genero = await _generoService.ObtenerGeneroPorId(Cliente.GeneroId ?? 0);
                    Cliente.ValorGenero = genero != null ? genero.Valor : "Sin genero";
                    resultado.Add(Cliente);
                }   
                return Ok(resultado);
            
            }catch (Exception ex) 
            {
                return Problem($"Ha ocurrido un error al momento de consultar las Clientes, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpPost]
        [Route("AdicionarCliente")]
        public async Task<IActionResult> AdicionarCliente(ClienteDTO ClienteDTO)
        {
            try
            {
                int Id = 0;
                bool resultado = false;

                //valida el modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
    
                //Se adiciona la Cliente
                ClienteDTO ClienteDTO2 = ClienteDTO.Clone() as ClienteDTO;
                resultado = await _clienteService.AdicionarCliente(ClienteDTO2);
                if (resultado)
                    return Ok("Cliente adicionada con éxito");
                return Problem("Ocurrio un problema al adicionar el usuario");

            }
            catch (Exception ex)
            {
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

                //valida el modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ClienteDTO Cliente = await _clienteService.ObtenerClientePorNumeroIdentificacion(ClienteDTO.Identificacion);
                
                if (Cliente == null)
                    return BadRequest($"La Cliente enviada con los siguientes datos: {ClienteDTO.ToString()} no existe en el sistema");

                resultado = await _clienteService.ActualizarCliente(ClienteDTO);

                return resultado ? Ok("Cliente actualizada con exito") : BadRequest("Error al actualizar el usuario,recuerde que si no actualiza valores no se realiza ninguna acción");
            }
            catch (Exception ex)
            {
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

                //valida el modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ClienteDTO Cliente = await _clienteService.ObtenerClientePorNumeroIdentificacion(numeroIdentificacion);

                if (Cliente == null)
                    return BadRequest($"El numero de identificacion={numeroIdentificacion} enviado no tiene Cliente asociada en el sistema");

                resultado = await _clienteService.eliminarCliente(Cliente);

                return resultado ? Ok("Cliente eliminada con exito") : BadRequest("Error al eliminar el usuario, recuerde que el campo numero de identificacion es indispensable para la eliminación");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de eliminar el usuario, recuerde que si la Cliente tiene cuentas asociadas no puede eliminarlo, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerClientePorIdentificacion")]
        public async Task<IActionResult> ObtenerClientePorIdentificacion(string Identificacion)
        {
            try
            {
                ClienteDTO consulta = await _clienteService.ObtenerClientePorNumeroIdentificacion(Identificacion);
                if (consulta != null && !string.IsNullOrEmpty(consulta.Identificacion))
                {
                    var genero = await _generoService.ObtenerGeneroPorId((int)consulta.GeneroId);
                    consulta.ValorGenero = genero.Valor;
                    return Ok(consulta);
                }
                else
                {
                    return BadRequest("Cliente no encontrada");
                }

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de consultar la Cliente por Identificacion, verifique: {ex.InnerException.Message}");
            }
        }
    }
}
