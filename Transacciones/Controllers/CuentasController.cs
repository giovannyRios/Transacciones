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
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        private readonly IClienteService _clienteService;
        private readonly ITipoCuentaService _tipoCuentaService;
        public CuentasController(ICuentaService cuentaService, IClienteService clienteService, ITipoCuentaService tipoCuentaService)
        {
            _cuentaService = cuentaService;
            _clienteService = clienteService;
            _tipoCuentaService = tipoCuentaService;
        }
        [HttpGet]
        [Route("ObtenerCuentas")]
        public async Task<IActionResult> ObtenerCuentas()
        {
            try
            {
                List<CuentaDTO> cuentaDTOs = await _cuentaService.ObtenerCuentas();
                List<CuentaDTO> resultado = new List<CuentaDTO>();
                if (cuentaDTOs != null && cuentaDTOs.Count > 0)
                {
                    foreach (var cuenta in cuentaDTOs)
                    {
                        TipoCuentaDTO tipoCuenta = await _tipoCuentaService.ObtenerTipoCuentaPorId((int)cuenta.TipoCuentaId);
                        cuenta.ValorTipoCuenta = tipoCuenta.Valor;
                        resultado.Add(cuenta);
                    }

                    return Ok(resultado);

                }
                return NotFound("No hay cuentas en el sistema");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar las cuentas, detalle del error:{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("ObtenerCuentasPorClienteId")]
        public async Task<IActionResult> ObtenerCuentasPorClienteId(int ClienteId)
        {
            try
            {
                List<CuentaDTO> busqueda = await _cuentaService.ObtenerCuentasPorClienteId(ClienteId);
                List<CuentaDTO> resultado = new List<CuentaDTO>();
                if (busqueda != null && busqueda.Count > 0)
                {
                    foreach (var cuenta in busqueda)
                    {
                        TipoCuentaDTO tipoCuenta = await _tipoCuentaService.ObtenerTipoCuentaPorId((int)cuenta.TipoCuentaId);
                        cuenta.ValorTipoCuenta = tipoCuenta.Valor;
                        resultado.Add(cuenta);
                    }

                    return Ok(resultado);

                }

                return NotFound($"No hay cuentas en el sistema asociadas al ClienteId={ClienteId}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar las cuentas por el ClienteId={ClienteId}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("ObtenerCuentasPorNumeroCuenta")]
        public async Task<IActionResult> ObtenerCuentasPorNumeroCuenta(string NumeroCuenta)
        {
            try
            {
                CuentaDTO cuentaDTO = await _cuentaService.ObtenerCuentaPorNumeroCuenta(NumeroCuenta);
                if (cuentaDTO != null)
                {
                    TipoCuentaDTO tipoCuenta = await _tipoCuentaService.ObtenerTipoCuentaPorId((int)cuentaDTO.TipoCuentaId);
                    cuentaDTO.ValorTipoCuenta = tipoCuenta.Valor;
                    return Ok(cuentaDTO);
                }

                return NotFound($"No hay cuentas en el sistema asociadas al NumeroCuenta={NumeroCuenta}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar las cuentas por el NumeroCuenta={NumeroCuenta}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpPost]
        [Route("AdicionarCuenta")]
        public async Task<IActionResult> AdicionarCuenta(CuentaDTO cuenta)
        {
            try
            {
                //Validacion del modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool resultado = false;

                //Validacion numero de cuenta
                CuentaDTO cuentaDTO = await _cuentaService.ObtenerCuentaPorNumeroCuenta(cuenta.NumeroCuenta);
                if (cuentaDTO != null)
                    return BadRequest($"La cuenta {cuenta.NumeroCuenta} ya existe en el sistema");

                //Validacion Cliente
                List<ClienteDTO> clienteDTO = await _clienteService.obtenerClientes();
                ClienteDTO FindCliente = clienteDTO.Where(x => x.Id == cuenta.ClienteId).FirstOrDefault();
                if (FindCliente == null)
                    return BadRequest($"La cuenta {cuenta.NumeroCuenta} debe estar asociada a un cliente que exista, recuerde el campo ClienteId");

                CuentaDTO cuentaClonado = cuenta.Clone() as CuentaDTO;

                resultado = await _cuentaService.AdicionarCuenta(cuentaClonado);

                if (resultado)
                    return Ok("Cuenta adicionada con exito");
                return Problem("Ocurrio un error al adicionar una cuenta");

            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar las cuentas por el NumeroCuenta={cuenta.NumeroCuenta}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpDelete]
        [Route("EliminarCuentaPorClienteId")]
        public async Task<IActionResult> EliminarCuentaPorClienteId(int ClienteId)
        {
            try
            {
                bool resultado = await _cuentaService.EliminarCuentasPorClienteId(ClienteId);
                if (resultado)
                    return Ok($"Cuentas asociadas al ClienteId={ClienteId} eliminadas con exito");
                return Problem($"Ha ocurrido un error al eliminar las cuentas asociadas al ClienteId={ClienteId}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al eliminar las cuentas asociadas al ClienteId={ClienteId}, recuerde que no puede eliminar cuentas que tengan movimientos asociados, detalle del error:{ex.ToString()}");
            }
        }

        [HttpDelete]
        [Route("EliminarCuenta")]
        public async Task<IActionResult> EliminarCuenta(CuentaDTO cuenta)
        {
            try
            {
                //Validacion del modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool resultado = await _cuentaService.EliminarCuenta(cuenta);
                if (resultado)
                    return Ok($"Cuenta eliminada con exito");
                return Problem($"Ha ocurrido un error al eliminar las cuenta numero ={cuenta.NumeroCuenta}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al eliminar la cuenta numero={cuenta.NumeroCuenta}, recuerde que no puede eliminar cuentas que tengan movimientos asociados, detalle del error:{ex.ToString()}");
            }
        }
    }
}
