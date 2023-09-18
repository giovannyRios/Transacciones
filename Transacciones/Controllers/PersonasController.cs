using Microsoft.AspNetCore.Mvc;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private readonly IGeneroService _generoService;
        public PersonasController(IPersonaService personaService, IGeneroService genero) 
        { 
            _personaService = personaService;
            _generoService = genero;
        }

        [HttpGet]
        [Route("ObtenerPersonas")]
        public async Task<IActionResult> ObtenerPersonas()
        {
            try 
            { 
                List<PersonaDTO> consulta = await _personaService.obtenerPersonas();
                List<PersonaDTO> resultado = new List<PersonaDTO>();
                foreach (PersonaDTO persona in consulta)
                {
                    int Id = persona.GeneroId ?? 0;
                    var genero = await _generoService.ObtenerGeneroPorId(persona.GeneroId ?? 0);
                    persona.ValorGenero = genero != null ? genero.Valor : "Sin genero";
                    resultado.Add(persona);
                }   
                return Ok(resultado);
            
            }catch (Exception ex) 
            {
                return Problem($"Ha ocurrido un error al momento de consultar las personas, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpPost]
        [Route("AdicionarPersona")]
        public async Task<IActionResult> AdicionarPersona(PersonaDTO personaDTO)
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

                //Genera el Id para la persona
                List<PersonaDTO> personas = await _personaService.obtenerPersonas();
                if(personas != null && personas.Count > 0)
                {
                    Id = personas.First().Id + 1;
                }
                else
                {
                    Id = 1;
                }
                    
                //Se adiciona la persona
                PersonaDTO personaDTO2 = personaDTO.Clone() as PersonaDTO;
                personaDTO2.Id = Id;
                resultado = await _personaService.AdicionarPersona(personaDTO2);
                if (resultado)
                    return Ok("Persona adicionada con éxito");
                return Problem("Ocurrio un problema al adicionar el usuario");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de adicionar el usuario, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpPut]
        [Route("ActualizarPersona")]
        public async Task<IActionResult> ActualizarPersona(PersonaDTO personaDTO)
        {
            try
            {
                bool resultado = false;

                //valida el modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                PersonaDTO persona = await _personaService.ObtenerPersonaPorId(personaDTO.Id);
                
                if (persona == null)
                    return BadRequest($"La persona enviada con los siguientes datos: {personaDTO.ToString()} no existe en el sistema");

                resultado = await _personaService.ActualizarPersona(personaDTO);

                return resultado ? Ok("Persona actualizada con exito") : BadRequest("Error al actualizar el usuario,recuerde que si no actualiza valores no se realiza ninguna acción");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de adicionar el usuario, verifique: {ex.InnerException.Message}");
            }
        }


        [HttpDelete]
        [Route("EliminarPersona")]
        public async Task<IActionResult> EliminarPersona(int Id)
        {
            try
            {
                bool resultado = false;

                //valida el modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                PersonaDTO persona = await _personaService.ObtenerPersonaPorId(Id);

                if (persona == null)
                    return BadRequest($"El Id={Id} enviado no tiene Persona asociada en el sistema");

                resultado = await _personaService.eliminarPersona(persona);

                return resultado ? Ok("Persona eliminada con exito") : BadRequest("Error al eliminar el usuario, recuerde que el campo Id es indispensable para la eliminación");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de eliminar el usuario, recuerde que si la persona tiene cuentas asociadas no puede eliminarlo, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerPersonasPorId")]
        public async Task<IActionResult> ObtenerPersonaPorId(int Id)
        {
            try
            {
                PersonaDTO consulta = await _personaService.ObtenerPersonaPorId(Id);
                if (consulta != null && !string.IsNullOrEmpty(consulta.Identificacion))
                {
                    var genero = await _generoService.ObtenerGeneroPorId((int)consulta.GeneroId);
                    consulta.ValorGenero = genero.Valor;
                    return Ok(consulta);
                }
                else
                {
                    return BadRequest("Persona no encontrada");
                }
                
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de consultar la persona por Id, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerPersonaPorIdentificacion")]
        public async Task<IActionResult> ObtenerPersonaPorIdentificacion(string Identificacion)
        {
            try
            {
                PersonaDTO consulta = await _personaService.ObtenerPersonaPorNumeroIdentificacion(Identificacion);
                if (consulta != null && !string.IsNullOrEmpty(consulta.Identificacion))
                {
                    var genero = await _generoService.ObtenerGeneroPorId((int)consulta.GeneroId);
                    consulta.ValorGenero = genero.Valor;
                    return Ok(consulta);
                }
                else
                {
                    return BadRequest("Persona no encontrada");
                }

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de consultar la persona por Identificacion, verifique: {ex.InnerException.Message}");
            }
        }
    }
}
