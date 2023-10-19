using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface ITipoCuentaRepository
    {
        public bool AdicionarTipoCuenta(TipoCuentum tipoCuenta);

        public bool RemoverTipoCuenta(TipoCuentum tipoCuenta);

        public Task<TipoCuentum> ObtenerTipoCuentaPorId(int Id);

        public Task<TipoCuentum> ObteneTipoCuentaPorValor(string valor);

        public Task<List<TipoCuentum>> ObtenerTipoCuentas();

        public bool ActualizarTipoCuenta(TipoCuentum tipoCuenta);
    }
}
