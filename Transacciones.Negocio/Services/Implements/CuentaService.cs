using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Implements;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class CuentaService : ICuentaService
    {
        private readonly IcuentaRepository _cuentaRepository;
        private readonly IMapper _mapper;
        public CuentaService(IcuentaRepository icuentaRepository,IMapper mapper) 
        {
            _cuentaRepository = icuentaRepository;
            _mapper = mapper;
        }
        public async Task<bool> ActualizarCuenta(CuentaDTO cuenta)
        {
            bool resultado = false;

            Cuenta clienteDto = await _cuentaRepository.ObtenerCuentaPorNumeroCuenta(cuenta.NumeroCuenta);
            if (clienteDto != null)
            {
                resultado = await _cuentaRepository.ActualizarCuenta(_mapper.Map<Cuenta>(cuenta));
            }

            return resultado;
        }

        public async Task<bool> AdicionarCuenta(CuentaDTO cuenta)
        {
            bool resultado = false;

            Cuenta clienteDto = await _cuentaRepository.ObtenerCuentaPorNumeroCuenta(cuenta.NumeroCuenta);
            if (clienteDto == null)
            {
                resultado = await _cuentaRepository.AdicionarCuenta(_mapper.Map<Cuenta>(cuenta));
            }

            return resultado;
        }

        public async Task<bool> EliminarCuenta(CuentaDTO cuenta)
        {
            bool resultado = false;

            Cuenta clienteDto = await _cuentaRepository.ObtenerCuentaPorNumeroCuenta(cuenta.NumeroCuenta);
            if (clienteDto != null)
            {
                resultado = await _cuentaRepository.EliminarCuenta(clienteDto);
            }

            return resultado;
        }

        public async Task<bool> EliminarCuentasPorClienteId(int ClienteId)
        {
            bool resultado = false;
            int conteoResultado = 0;
            List<Cuenta> clienteDto = await _cuentaRepository.ObtenerCuentasPorClienteId(ClienteId);
            if (clienteDto != null && clienteDto.Count > 0)
            {
                foreach (var cliente in clienteDto)
                {
                    resultado = await _cuentaRepository.EliminarCuenta(cliente);
                    if(resultado)
                        conteoResultado++;
                }
                
            }

            return conteoResultado > 0;
        }

        public async Task<CuentaDTO> ObtenerCuentaPorNumeroCuenta(string NumeroCuenta)
        {
            Cuenta clienteDto = await _cuentaRepository.ObtenerCuentaPorNumeroCuenta(NumeroCuenta);
            if (clienteDto != null)
            {
                return _mapper.Map<CuentaDTO>(clienteDto);
            }

            return null;
        }

        public async Task<List<CuentaDTO>> ObtenerCuentas()
        {
            List<CuentaDTO> cuentaDTOs = new List<CuentaDTO>();
            List<Cuenta> cuentas = await _cuentaRepository.ObtenerCuentas();
            if (cuentas != null && cuentas.Count > 0)
                cuentaDTOs.AddRange(_mapper.Map<List<CuentaDTO>>(cuentas));
            return cuentaDTOs;
        }

        public async Task<List<CuentaDTO>> ObtenerCuentasPorClienteId(int ClienteId)
        {
            List<CuentaDTO> cuentaDTOs = new List<CuentaDTO>();
            List<Cuenta> cuentas = await _cuentaRepository.ObtenerCuentasPorClienteId(ClienteId);
            if (cuentas != null && cuentas.Count > 0)
                cuentaDTOs.AddRange(_mapper.Map<List<CuentaDTO>>(cuentas));
            return cuentaDTOs;
        }
    }
}
