using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Decorators.ClienteDecorator
{
    public interface IclienteDecorator
    {
        public Task<Cliente> ObtenerClientePorIdentificacion(string numeroIdentificacion);

        public Task<Cliente> ObtenerClientePorId(int Id);
    }
}
