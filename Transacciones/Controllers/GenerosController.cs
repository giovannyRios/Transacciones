using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService _generoServices;

        public GenerosController(IGeneroService generoService)
        {
            _generoServices = generoService;
        }

        [HttpGet]
        [Route("ObtenerGeneroPorId")]
        public async Task<IActionResult> ObtenerGeneroPorId(int Id)
        {
            try
            {
                GeneroDTO resultado = await _generoServices.ObtenerGeneroPorId(Id);
                if(resultado != null)
                    return Ok(resultado);
                return BadRequest("Genero no encontrado");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de consultar el genero por Id, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerGeneroPorValor")]
        public async Task<IActionResult> ObtenerGeneroPorId(string valor)
        {
            try
            {
                GeneroDTO resultado = await _generoServices.ObtenerGeneroPorValor(valor);
                if (resultado != null)
                    return Ok(resultado);
                return BadRequest("Genero no encontrado");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de consultar el genero por valor, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerGeneros")]
        public async Task<IActionResult> ObtenerGeneros()
        {
            try
            {
                List<GeneroDTO> resultado = await _generoServices.ObtenerGeneros();
                if (resultado != null)
                    return Ok(resultado);
                return BadRequest("Sin generos en el sistema");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de consultar los generos, verifique: {ex.InnerException.Message}");
            }
        }

    }
}
