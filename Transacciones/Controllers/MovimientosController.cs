using Microsoft.AspNetCore.Mvc;
using Transacciones.Dominio.Context;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Implements;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoServices _movimientoServices;
        private readonly ICuentaService _cuentaService;
        public MovimientosController(IMovimientoServices movimientoServices, ICuentaService cuentaService)
        {
            _movimientoServices = movimientoServices;
            _cuentaService = cuentaService;
        }

        [HttpGet]
        [Route("ObtenerMovimientos")]
        public async Task<IActionResult> ObtenerMovimientos()
        {
            try
            {
                List<MovimientoDTO> MovimientoDTOs = await _movimientoServices.ObtenerMovimientos();
                List<MovimientoDTO> resultado = new List<MovimientoDTO>();
                if (MovimientoDTOs != null && MovimientoDTOs.Count > 0)
                {
                    List<CuentaDTO> Cuentas = await _cuentaService.ObtenerCuentas();
                    foreach (var movimiento in MovimientoDTOs)
                    {
                        var cuentaDto = Cuentas.Where(p => p.Id == movimiento.CuentaId).FirstOrDefault();
                        movimiento.NumeroDeCuenta = cuentaDto.NumeroCuenta;
                        resultado.Add(movimiento);
                    }

                    return Ok(resultado);

                }
                return NotFound("No hay movimientos en el sistema");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar los movimientos, detalle del error:{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("ObtenerMovimientosPorCuentaId")]
        public async Task<IActionResult> ObtenerMovimientosPorCuentaId(int CuentaId)
        {
            try
            {
                List<MovimientoDTO> busqueda = await _movimientoServices.ObtenerMovimientosPorCuentaId(CuentaId);
                List<MovimientoDTO> resultado = new List<MovimientoDTO>();
                if (busqueda != null && busqueda.Count > 0)
                {
                    List<CuentaDTO> Cuentas = await _cuentaService.ObtenerCuentas();
                    foreach (var movimiento in busqueda)
                    {
                        var cuentaDto = Cuentas.Where(p => p.Id == movimiento.CuentaId).FirstOrDefault();
                        movimiento.NumeroDeCuenta = cuentaDto.NumeroCuenta;
                        resultado.Add(movimiento);
                    }

                    return Ok(resultado);

                }

                return NotFound($"No hay movimientos asociados en el sistema con la CuentaId={CuentaId}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar los movimientos por el CuentaId={CuentaId}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("ObtenerMovimientosPorId")]
        public async Task<IActionResult> ObtenerMovimientosPorId(int Id)
        {
            try
            {
                MovimientoDTO moviminetoDTO = await _movimientoServices.ObtenerMovimientosPorId(Id);
                if (moviminetoDTO != null)
                {
                    List<CuentaDTO> Cuentas = await _cuentaService.ObtenerCuentas();
                    var cuentaDto = Cuentas.Where(p => p.Id == moviminetoDTO.CuentaId).FirstOrDefault();
                    moviminetoDTO.NumeroDeCuenta = cuentaDto.NumeroCuenta;
                    return Ok(moviminetoDTO);

                }

                return NotFound($"No hay movimiento asociado en el sistema con el Id={Id}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al consultar el movimiento por el Id={Id}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpPost]
        [Route("AdicionarMovimiento")]
        public async Task<IActionResult> AdicionarMovimiento(MovimientoDTO movimiento)
        {
            try
            {
                //Validacion del modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool resultado = false;

                //Validacion de la cuenta
                List<CuentaDTO> Cuentas = await _cuentaService.ObtenerCuentas();
                var cuentaDto = Cuentas.Where(p => p.Id == movimiento.CuentaId).FirstOrDefault();
                if (cuentaDto == null || string.IsNullOrEmpty(cuentaDto.NumeroCuenta))
                    return BadRequest($"El movimiento con cuentaId {movimiento.CuentaId} no se puede adicionar ya que ");

                //Validacion del Saldo
                decimal? saldoCuenta = cuentaDto.Saldo;
                decimal? SaldoMovimiento = movimiento.Saldo;
                if(SaldoMovimiento < 0)
                {
                    if (0 > (saldoCuenta + SaldoMovimiento))
                        return BadRequest("Saldo no disponible");
                    cuentaDto.Saldo = (saldoCuenta + SaldoMovimiento);
                }
                else
                {
                    if (0 > (saldoCuenta - SaldoMovimiento))
                        return BadRequest("Saldo no disponible");
                    cuentaDto.Saldo = (saldoCuenta - SaldoMovimiento);
                }

                //Actualizacion saldo de la cuenta
                bool resultadoActualizacionCuenta = await _cuentaService.ActualizarCuenta(cuentaDto);

                if (resultadoActualizacionCuenta)
                {
                    //generacion Id y adicion cuenta
                    List<MovimientoDTO> MovimientoDTOs = await _movimientoServices.ObtenerMovimientos();
                    int Id = 0;
                    if (MovimientoDTOs != null && MovimientoDTOs.Count > 0)
                    {
                        Id = MovimientoDTOs.FirstOrDefault().Id + 1;
                    }
                    else
                    {
                        Id = 1;
                    }

                    MovimientoDTO MovimientoClonado = movimiento.Clone() as MovimientoDTO;
                    MovimientoClonado.Id = Id;

                    resultado = await _movimientoServices.AdicionarMovimiento(MovimientoClonado);
                    if (resultado)
                        return Ok("Movimiento adicionado con exito");
                    return Problem("Ocurrio un error al adicionar un Movimiento");

                }
                else
                {
                    return Problem("No fue posible actualizar el saldo de la cuenta al intentar validar el movimiento");
                }
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un problema al adicionar el movimiento {movimiento.DescripcionMovimiento}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpDelete]
        [Route("EliminarMovimientosPorCuentaId")]
        public async Task<IActionResult> EliminarCuentaPorClienteId(int CuentaId)
        {
            try
            {
                bool resultado = await _movimientoServices.EliminarMovimientosPorCuentaId(CuentaId);
                if (resultado)
                    return Ok($"movimientos asociados a la CuentaId={CuentaId} eliminados con exito");
                return Problem($"Ha ocurrido un error al eliminar los movimientos asociados a la cuentaId={CuentaId}");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al eliminar los movimientos asociados a la cuentaId={CuentaId}, detalle del error:{ex.ToString()}");
            }
        }

        [HttpDelete]
        [Route("EliminarMovimiento")]
        public async Task<IActionResult> EliminarMovimiento(MovimientoDTO movimiento)
        {
            try
            {
                //Validacion del modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool resultado = await _movimientoServices.EliminarMovimiento(movimiento);
                if (resultado)
                    return Ok($"Movimiento eliminado con exito");
                return Problem($"Ha ocurrido un error al eliminar el movimiento ingresado");
            }
            catch (Exception ex)
            {
                return Problem($"Ha ocurrido un error al eliminar el movimiento asociado con Id={movimiento.Id}, detalle del error:{ex.ToString()}");
            }
        }
    }
}
