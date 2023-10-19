using AutoMapper;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class TipoCuentaService : ITipoCuentaService
    {
        private readonly ITipoCuentaRepository _tipoCuentaRepository;
        private readonly IMapper _mapper;
        public TipoCuentaService(ITipoCuentaRepository tipoCuentaRepository, IMapper mapper)
        {
            _tipoCuentaRepository = tipoCuentaRepository;
            _mapper = mapper;
        }
        public async Task<TipoCuentaDTO> ObtenerTipoCuentaPorId(int Id)
        {
            TipoCuentum tipoCuentum = await _tipoCuentaRepository.ObtenerTipoCuentaPorId(Id);
            if (tipoCuentum != null)
                return _mapper.Map<TipoCuentaDTO>(tipoCuentum);
            return null;

        }

        public async Task<TipoCuentaDTO> ObtenerTipoCuentaPorValor(string Valor)
        {
            TipoCuentum tipoCuentum = await _tipoCuentaRepository.ObteneTipoCuentaPorValor(Valor);
            if (tipoCuentum != null)
                return _mapper.Map<TipoCuentaDTO>(tipoCuentum);
            return null;
        }

        public async Task<List<TipoCuentaDTO>> ObtenerTiposCuenta()
        {
            List<TipoCuentum> tipoCuentas = await _tipoCuentaRepository.ObtenerTipoCuentas();
            if (tipoCuentas != null && tipoCuentas.Count > 0)
                return _mapper.Map<List<TipoCuentaDTO>>(tipoCuentas);
            return null;
        }
    }
}
