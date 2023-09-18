using Microsoft.AspNetCore.Mvc;
using Transacciones.Dominio.Context;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IPersonaService _personaService;
        public ClientesController(IClienteService clienteService,IPersonaService personaService) 
        { 
            _clienteService = clienteService;
            _personaService = personaService;
        }

        [HttpGet]
        [Route("ObtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                List<ClienteDTO> resultado = await _clienteService.ObtenerClientes();
                if(resultado != null && resultado.Count > 0)
                    return Ok(resultado);
                return NotFound("No existen clientes en el sistema");
            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de consultar los Clientes, verifique: {ex.InnerException.Message}");
               
            }
        }

        [HttpGet]
        [Route("ObtenerClientePorClienteId")]
        public async Task<IActionResult> ObtenerClientePorClienteId(string ClienteId)
        {
            try
            {
                ClienteDTO resultado = await _clienteService.ObtenerClientePorClienteId(ClienteId);
                if (resultado != null)
                    return Ok(resultado);
                return NotFound($"No existe cliente en el sistema asociado al ClienteId {ClienteId}");
            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de consultar los Cliente por ClienteId={ClienteId}, verifique: {ex.InnerException.Message}");

            }
        }

        [HttpGet]
        [Route("ObtenerClientesPorPersonaId")]
        public async Task<IActionResult> ObtenerClientesPorPersonaId(int PersonaId)
        {
            try
            {
                List<ClienteDTO> resultado = await _clienteService.ObtenerClientesPorPersonaId(PersonaId);
                if (resultado != null && resultado.Count > 0)
                    return Ok(resultado);
                return NotFound($"No existen clientes en el sistema asociados a PersonaId={PersonaId}");
            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de consultar los Clientes por PersonaId={PersonaId}, verifique: {ex.InnerException.Message}");

            }
        }

        [HttpPost]
        [Route("AdicionarCliente")]
        public async Task<IActionResult> AdicionarCliente(ClienteDTO clienteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var persona = await _personaService.ObtenerPersonaPorId((int)clienteDTO.PersonaId);
                bool resultado = false;

                //Valida que la persona a la cual se quiere asociar la persona exista
                if (persona == null)
                    return BadRequest("El cliente debe estar asociado a una persona, valide que el campo PersonaId tenga un valor asociado a una persona");

                //Para Adicionar el Id
                int Id = 0;
                List<ClienteDTO> clientes = await _clienteService.ObtenerClientes();
                if (clientes != null && clientes.Count > 0)
                {
                    Id = clientes.First().Id + 1;
                }
                else
                {
                    Id = 1;
                }

                ClienteDTO clienteDTO2 = clienteDTO.Clone() as ClienteDTO;
                clienteDTO2.Id = Id;

                resultado = await _clienteService.AdicionarCliente(clienteDTO2);

                if (resultado)
                    return Ok("Cliente adicionado correctamente");
                return Problem("Ocurrio un error al adicionar el cliente=" + clienteDTO.ToString());


            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de adicionar el cliente={clienteDTO.ToString()}, verifique: {ex.InnerException.Message}");

            }
        }


        [HttpPut]
        [Route("ActualizarCliente")]
        public async Task<IActionResult> ActualizarCliente(ClienteDTO clienteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var persona = await _personaService.ObtenerPersonaPorId((int)clienteDTO.PersonaId);
                bool resultado = false;

                //Valida que la persona a la cual se quiere actualizar la persona exista
                if (persona == null)
                    return BadRequest("El cliente debe estar asociado a una persona, valide que el campo PersonaId tenga un valor asociado a una persona");


                resultado = await _clienteService.ActualizarCliente(clienteDTO);

                if (resultado)
                    return Ok("Cliente actualizado correctamente");
                return Problem("Ocurrio un error al actualizado el cliente=" + clienteDTO.ToString());


            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de Actualizar el cliente, verifique: {ex.Message}");

            }
        }

        [HttpDelete]
        [Route("BorrarCliente")]
        public async Task<IActionResult> BorrarCliente(string ClienteId)
        {
            try
            {
                //Valida que el cliente exista para informarlo
                ClienteDTO clienteDTO = await _clienteService.ObtenerClientePorClienteId(ClienteId);
                if (clienteDTO == null)
                    return BadRequest($"No existe un cliente asociado al ClienteId={ClienteId}");


                bool resultado = await _clienteService.EliminarCliente(clienteDTO);
                if (resultado)
                    return Ok("Cliente eliminado correctamente");
                return Problem("Ocurrio un error al eliminar el cliente=" + clienteDTO.ToString());


            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de eliminar el cliente con ClienteId={ClienteId}, recuerde que para eliminar un cliente este no debe tener cuentas asociadas, verifique: {ex.InnerException.Message}");

            }
        }

        [HttpDelete]
        [Route("BorrarClientesPorPersonaId")]
        public async Task<IActionResult> BorrarClientesPorPersonaId(int personaId)
        {
            try
            {
                var persona = await _personaService.ObtenerPersonaPorId(personaId);
                bool resultado = false;

                //Valida que la persona a la cual se quiere actualizar la persona exista
                if (persona == null)
                    return BadRequest($"La persona de cual intenta eliminar las cuentas no existe, valide que el valor enviado {personaId} tenga una persona asociada");


                resultado = await _clienteService.EliminarClientesPorPersonaId(personaId);

                if (resultado)
                    return Ok("Clientes eliminados de manera correcta");
                return Problem($"Ocurrio un error al momento de eliminar los clientes asociados al PersonaId={personaId}");


            }
            catch (Exception ex)
            {
                return Problem($"Ocurrio un error al momento de eliminar los clientes asociados a la persona con PersonaId={personaId}, recuerde que para eliminar un cliente este no debe tener cuentas asociadas, verifique: {ex.InnerException.Message}");

            }
        }
    }
}
