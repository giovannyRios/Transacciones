using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface ITipoCuentaService
    {
        public Task<TipoCuentaDTO> ObtenerTipoCuentaPorId(int Id);
        public Task<TipoCuentaDTO> ObtenerTipoCuentaPorValor(string Valor);
        public Task<List<TipoCuentaDTO>> ObtenerTiposCuenta();
    }
}
