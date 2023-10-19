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
    public class ReportesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ICuentaService _cuentaService;
        private readonly ITipoCuentaService _tipoCuentaService;
        private readonly IMovimientoServices _movimientoServices;
        public ReportesController(IMovimientoServices movimientoServices, IClienteService clienteService, ITipoCuentaService tipoCuentaService, ICuentaService cuentaService)
        {
            _movimientoServices = movimientoServices;
            _clienteService = clienteService;
            _cuentaService = cuentaService;
            _tipoCuentaService = tipoCuentaService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get([FromQuery] RangoFechas rangoFechas)
        {
            try
            {
                List<reporteDTO> reportes = new List<reporteDTO>();

                if (rangoFechas.FechaInicio == null || rangoFechas.FechaFIn == null)
                {
                    return BadRequest("Debe ingresar el rango de fechas para generar la información");
                }
                else if (rangoFechas.FechaInicio > rangoFechas.FechaFIn)
                {
                    return BadRequest("La fecha inicial no puede ser superior a la fecha final");
                }
                else
                {
                    var listaMovimientos = await _movimientoServices.ObtenerMovimientos();
                    var listaCuentas = await _cuentaService.ObtenerCuentas();
                    var ListaClientes = await _clienteService.obtenerClientes();


                    var ListaMovimientosFiltrada = listaMovimientos.Where(movimiento => movimiento.FechaMovimiento >= rangoFechas.FechaInicio && movimiento.FechaMovimiento <= rangoFechas.FechaFIn).ToList();
                    if (ListaMovimientosFiltrada != null)
                    {
                        foreach (var movimiento in ListaMovimientosFiltrada)
                        {
                            reporteDTO reporteDTO = new reporteDTO();
                            reporteDTO.Fecha = movimiento.FechaMovimiento?.ToString("dd/MM/yyyy");
                            reporteDTO.Movimiento = (decimal)movimiento.Saldo;

                            var cuenta = listaCuentas.Where(cuenta => cuenta.Id == movimiento.CuentaId).FirstOrDefault();
                            reporteDTO.NumeroCuenta = cuenta.NumeroCuenta;

                            var TipoCuenta = await _tipoCuentaService.ObtenerTipoCuentaPorId((int)cuenta.TipoCuentaId);
                            reporteDTO.Tipo = TipoCuenta.Valor;
                            reporteDTO.SaldoInicial = (decimal)cuenta.Saldo + (decimal)movimiento.Saldo;
                            reporteDTO.SaldoDisponible = (decimal)cuenta.Saldo;
                            reporteDTO.Estado = (bool)cuenta.Estado;

                            var cliente = ListaClientes.Where(cliente => cliente.Id == cuenta.ClienteId).FirstOrDefault();


                            reporteDTO.Cliente = cliente.Nombre;

                            reportes.Add(reporteDTO);


                        }

                        return Ok(reportes);

                    }
                    else
                    {
                        return NotFound("No existe información completa para el reporte en el rango de fechas enviado");
                    }
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
