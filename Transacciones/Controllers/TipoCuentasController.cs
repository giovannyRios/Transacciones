using Microsoft.AspNetCore.Mvc;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TipoCuentasController : ControllerBase
    {
        private readonly ITipoCuentaService _TipoCuentaService;
        public TipoCuentasController(ITipoCuentaService tipoCuentaService) 
        { 
            _TipoCuentaService = tipoCuentaService;
        }

        [HttpGet]
        [Route("ObtenerTipoCuentaPorId")]
        public async Task<IActionResult> ObtenerTipoCuentaPorId(int Id)
        {
            try
            {
                TipoCuentaDTO resultado = await _TipoCuentaService.ObtenerTipoCuentaPorId(Id);
                if(resultado != null)
                    return Ok(resultado);
                return BadRequest("Tipo cuenta no encontrada");

            }
            catch (Exception ex) 
            {
                return Problem($"Ha ocurrido un error al momento de obtener el tipo de cuenta por Id, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerTipoCuentaPorValor")]
        public async Task<IActionResult> ObtenerTipoCuentaPorValor(string Valor)
        {
            try
            {
                TipoCuentaDTO resultado = await _TipoCuentaService.ObtenerTipoCuentaPorValor(Valor);
                if (resultado != null)
                    return Ok(resultado);
                return BadRequest("Tipo cuenta no encontrada");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de obtener el tipo de cuenta por Valor, verifique: {ex.InnerException.Message}");
            }
        }

        [HttpGet]
        [Route("ObtenerTipoCuentas")]
        public async Task<IActionResult> ObtenerTipoCuentas()
        {
            try
            {
                List<TipoCuentaDTO> resultado = await _TipoCuentaService.ObtenerTiposCuenta();
                if (resultado != null)
                    return Ok(resultado);
                return BadRequest("No hay tipos de cuenta en el sistema");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al momento de obtener los tipo de cuenta del sistema, verifique: {ex.InnerException.Message}");
            }
        }
    }
}
